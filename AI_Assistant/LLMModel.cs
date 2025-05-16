using LLMKit;
using LLMKit.Providers;

namespace AI_Assistant
{
    public class LLMModel
    {
        public LLMClient Client;
        public string ApiKey = string.Empty;
        public string Model = string.Empty;
        public int MaxTokens = Constants.LLM.MaxTokens;
        public double Temperature = Constants.LLM.Temperature;

        public int MaxMessages = Constants.LLM.MaxMaxMessages;

        public string ProviderWithModel = string.Empty;
        public string SystemMessage = string.Empty;
    }

    public class LLMModelFactory
    {
        private LLMModel _lLMModel;

        public LLMModelFactory()
        {
            _lLMModel = new LLMModel();
        }

        public LLMModel InitModel()
        {
            try
            {
                AppSettings appSettings = AppSettings.LoadSettings();
                // path for the appsettings.json file
                var appSettingsPath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFile);
                var appSettingsExamplePath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFileExample);
                if (!File.Exists(appSettingsPath))
                {
                    File.Copy(appSettingsExamplePath, appSettingsPath);
                }

                string providerName = appSettings.DefaultProvider;
                var (apiKey, model) = appSettings.GetProviderDetails(providerName);
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(model) || apiKey.StartsWith("YOUR-"))
                {
                    throw new Exception($"API Key or Model is missing for provider '{providerName}' in '{Constants.LLM.AppSettingsJsonFile}'");
                }

                _lLMModel.ApiKey = apiKey;
                _lLMModel.Model = model;
                _lLMModel.MaxTokens = int.TryParse(appSettings.DefaultMaxTokens, out var maxTokens) ? maxTokens : Constants.LLM.MaxTokens;
                _lLMModel.Temperature = double.TryParse(appSettings.DefaultTemparature, out var temperature) ? temperature : Constants.LLM.Temperature;
                _lLMModel.MaxMessages = int.TryParse(appSettings.MaxMessages, out var maxMessages) ? maxMessages : Constants.LLM.MaxMessages;

                if (providerName == Providers.Gemini.ToString())
                    _lLMModel.Client = new LLMClient(new GeminiProvider(_lLMModel.ApiKey, _lLMModel.Model), _lLMModel.MaxTokens, _lLMModel.Temperature, _lLMModel.MaxMessages);
                else if (providerName == Providers.OpenAI.ToString())
                    _lLMModel.Client = new LLMClient(new OpenAIProvider(_lLMModel.ApiKey, _lLMModel.Model), _lLMModel.MaxTokens, _lLMModel.Temperature, _lLMModel.MaxMessages);
                else if (providerName == Providers.DeepSeek.ToString())
                    _lLMModel.Client = new LLMClient(new DeepSeekProvider(_lLMModel.ApiKey, _lLMModel.Model), _lLMModel.MaxTokens, _lLMModel.Temperature, _lLMModel.MaxMessages);

                _lLMModel.Client.SetSystemMessage(Constants.SystemMessages.Default);

                _lLMModel.ProviderWithModel = $"{providerName} - {_lLMModel.Model} - Max Token: '{_lLMModel.MaxTokens}' - Temp: '{_lLMModel.Temperature}' - Max Messages: '{_lLMModel.MaxMessages}'";

                return _lLMModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing model: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Open the appsettings file for the user to edit
                string appSettingsPath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFile);
                if (File.Exists(appSettingsPath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "notepad.exe",
                        Arguments = appSettingsPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show($"Configuration file '{Constants.LLM.AppSettingsJsonFile}' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return new LLMModel();
            }
        }
    }
}