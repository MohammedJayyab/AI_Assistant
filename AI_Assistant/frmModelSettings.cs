using System.Globalization;
using System.Text.Json;

namespace AI_Assistant
{
    public partial class frmModelSettings : Form
    {
        // Temperature text and thinking level per provider; the boxes show the ones of the provider selected in the combo box.
        private readonly Dictionary<string, string> _temperatures = new();
        private readonly Dictionary<string, string> _reasoningEfforts = new();
        private string _shownProvider;

        public frmModelSettings()
        {
            InitializeComponent();
            InitJsonSetting();

        }

        private static bool TryParseTemperature(string text, out double temperature) =>
            double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out temperature) &&
            temperature >= Constants.LLM.MinTemperature &&
            temperature <= Constants.LLM.MaxTemperature;

        private void cmbDefaultLLM_SelectedIndexChanged(object sender, EventArgs e)
        {
            // keep what was typed for the provider being left, then show the newly selected one
            KeepShownProvider();

            _shownProvider = cmbDefaultLLM.SelectedItem?.ToString();
            if (_shownProvider != null && _temperatures.TryGetValue(_shownProvider, out var text))
            {
                txtDefaultTemperature.Text = text;
                cmbReasoningEffort.SelectedItem = _reasoningEfforts[_shownProvider];
            }
        }

        private void KeepShownProvider()
        {
            if (_shownProvider != null)
            {
                _temperatures[_shownProvider] = txtDefaultTemperature.Text;
                _reasoningEfforts[_shownProvider] = cmbReasoningEffort.SelectedItem?.ToString() ?? Constants.LLM.ReasoningDefault;
            }
        }

        private void InitJsonSetting()
        {
            AppSettings appSettings = AppSettings.LoadSettings();
            // Clear existing items to prevent duplicates
            cmbDefaultLLM.Items.Clear();
            _temperatures.Clear();
            _reasoningEfforts.Clear();
            _shownProvider = null;
            // get the list of all provider names
            var providerNames = appSettings.AllProviderNames;
            // fill the combo box with the provider names
            foreach (var providerName in providerNames)
            {
                cmbDefaultLLM.Items.Add(providerName);
                var details = appSettings.GetProviderDetails(providerName);
                _temperatures[providerName] = details.Temperature.ToString(CultureInfo.InvariantCulture);
                _reasoningEfforts[providerName] = details.ReasoningEffort?.ToString() ?? Constants.LLM.ReasoningDefault;
            }
            // set the default provider (this shows its temperature and thinking level)
            cmbDefaultLLM.SelectedItem = appSettings.DefaultProvider;
            // set the default max tokens
            txtDefaultMaxTokens.Text = appSettings.DefaultMaxTokens;
            // set the max messages
            txtMaxMessages.Text = appSettings.MaxMessages;
        }

        private void SaveJsonSettings()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFile);

            // Read existing JSON
            string json = File.ReadAllText(filePath);
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement.Clone();

            // Validate values
            KeepShownProvider();

            var invalidProvider = _temperatures.Where(t => !TryParseTemperature(t.Value, out _)).Select(t => t.Key).FirstOrDefault();
            if (invalidProvider != null)
            {
                MessageBox.Show($"Temperature for {invalidProvider} must be a number between {Constants.LLM.MinTemperature} and {Constants.LLM.MaxTemperature}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbDefaultLLM.SelectedItem = invalidProvider;
                txtDefaultTemperature.SelectAll();
                txtDefaultTemperature.Focus();
                return;
            }

            if (!int.TryParse(txtDefaultMaxTokens.Text, out int maxTokens) ||
                maxTokens < Constants.LLM.MinTokens ||
                maxTokens > Constants.LLM.MaxTokens)
            {
                MessageBox.Show($"Max Tokens must be between {Constants.LLM.MinTokens} and {Constants.LLM.MaxTokens}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDefaultMaxTokens.SelectAll();
                txtDefaultMaxTokens.Focus();
                return;
            }

            if (!int.TryParse(txtMaxMessages.Text, out int maxMessages) ||
                maxMessages < Constants.LLM.MinMessages ||
                maxMessages > Constants.LLM.MaxMessages)
            {
                MessageBox.Show($"Max Messages must be between {Constants.LLM.MinMessages} and {Constants.LLM.MaxMessages}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaxMessages.SelectAll();
                txtMaxMessages.Focus();
                return;
            }

            string selectedProvider = cmbDefaultLLM.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedProvider))
            {
                MessageBox.Show("Please select a default provider.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbDefaultLLM.Focus();
                cmbDefaultLLM.DroppedDown = true;

                return;
            }

            // Convert root to mutable Dictionary
            var rootDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var llmJson = JsonSerializer.Deserialize<Dictionary<string, object>>(root.GetProperty("LLM").ToString());
            var convoJson = JsonSerializer.Deserialize<Dictionary<string, object>>(root.GetProperty("Conversation").ToString());

            llmJson["DefaultProvider"] = selectedProvider;
            llmJson["DefaultMaxTokens"] = maxTokens.ToString();
            llmJson.Remove("DefaultTemparature"); // old global setting, replaced by each provider's own Temparature
            foreach (var (provider, text) in _temperatures)
            {
                TryParseTemperature(text, out var temperature);
                var providerJson = JsonSerializer.Deserialize<Dictionary<string, object>>(llmJson[provider].ToString());
                providerJson["Temparature"] = temperature.ToString(CultureInfo.InvariantCulture);
                if (_reasoningEfforts[provider] == Constants.LLM.ReasoningDefault)
                    providerJson.Remove("ReasoningEffort"); // the provider decides
                else
                    providerJson["ReasoningEffort"] = _reasoningEfforts[provider];
                llmJson[provider] = providerJson;
            }
            convoJson["DefaultMaxMessages"] = maxMessages.ToString();

            // Reassign updated dictionaries
            rootDict["LLM"] = llmJson;
            rootDict["Conversation"] = convoJson;

            // Serialize and write back
            var newJson = JsonSerializer.Serialize(rootDict, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, newJson);

            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveJsonSettings();
        }

        private void btnOpenJson_Click(object sender, EventArgs e)
        {
            // use notepad.exe
            // to open the appsettings.json file

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
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            InitJsonSetting();
        }

        private void btnEditPrompts_Click(object sender, EventArgs e)
        {
            using var frmPromptSettings = new frmPromptSettings { Icon = Icon };
            frmPromptSettings.ShowDialog(this);
        }
               

      
    }
}