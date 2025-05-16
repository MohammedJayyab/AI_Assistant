using Microsoft.Extensions.FileSystemGlobbing;
using System.Text.Json;

namespace AI_Assistant
{
    public partial class frmModelSettings : Form
    {
       

        public frmModelSettings()
        {
            InitializeComponent();
            InitJsonSetting();
           
        }

        private void InitJsonSetting()
        {
            AppSettings appSettings = AppSettings.LoadSettings();
            // get the list of all provider names
            var providerNames = appSettings.AllProviderNames;
            // fill the combo box with the provider names
            foreach (var providerName in providerNames)
            {
                cmbDefaultLLM.Items.Add(providerName);
            }
            // set the default provider
            cmbDefaultLLM.SelectedItem = appSettings.DefaultProvider;
            // set the default temperature
            txtDefaultTemperature.Text = appSettings.DefaultTemparature;
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
            if (!double.TryParse(txtDefaultTemperature.Text, out double temperature) ||
                temperature < Constants.LLM.MinTemperature ||
                temperature > Constants.LLM.MaxTemperature)
            {
                MessageBox.Show($"DefaultTemperature must be a number between {Constants.LLM.MinTemperature} and {Constants.LLM.MaxTemperature}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            llmJson["DefaultTemparature"] = temperature.ToString();
            llmJson["DefaultMaxTokens"] = maxTokens.ToString();
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
               

      
    }
}