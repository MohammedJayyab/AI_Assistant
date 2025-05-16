using Microsoft.Extensions.FileSystemGlobbing;
using System.Text.Json;

namespace AI_Assistant
{
    public partial class frmModelSettings : Form
    {
        private FileSystemWatcher _watcher;

        public frmModelSettings()
        {
            InitializeComponent();
            InitJsonSetting();
            InitFileWatcher();
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
                MessageBox.Show($"Temperature must be a number between {Constants.LLM.MinTemperature} and {Constants.LLM.MaxTemperature}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDefaultTemperature.SelectAll();
                txtDefaultTemperature.Focus();
                return;
            }

            if (!int.TryParse(txtDefaultMaxTokens.Text, out int maxTokens) ||
                maxTokens < Constants.LLM.MinMaxTokens ||
                maxTokens > Constants.LLM.MaxMaxTokens)
            {
                MessageBox.Show($"Max Tokens must be between {Constants.LLM.MinMaxTokens} and {Constants.LLM.MaxMaxTokens}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDefaultMaxTokens.SelectAll();
                txtDefaultMaxTokens.Focus();
                return;
            }

            if (!int.TryParse(txtMaxMessages.Text, out int maxMessages) ||
                maxMessages < Constants.LLM.MinMaxMessages ||
                maxMessages > Constants.LLM.MaxMaxMessages)
            {
                MessageBox.Show($"Max Messages must be between {Constants.LLM.MinMaxMessages} and {Constants.LLM.MaxMaxMessages}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            convoJson["MaxMessages"] = maxMessages.ToString();

            // Reassign updated dictionaries
            rootDict["LLM"] = llmJson;
            rootDict["Conversation"] = convoJson;

            // Serialize and write back
            var newJson = JsonSerializer.Serialize(rootDict, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, newJson);

            MessageBox.Show("Settings updated successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload settings
            AppSettings.LoadSettings();
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

        private void InitFileWatcher()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFile);

            _watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(filePath),
                Filter = Path.GetFileName(filePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                EnableRaisingEvents = true
            };

            _watcher.Changed += (s, e) =>
            {
                // Avoid multiple triggers by debouncing
                Thread.Sleep(200); // Delay to ensure file write completion

                this.Invoke(new Action(() =>
                {
                    try
                    {
                        InitJsonSetting();
                        //MessageBox.Show("Settings file was updated externally. Changes reloaded.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error reloading settings. File may be invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            };
        }

        private void frmModelSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                _watcher = null;
            }
        }
    }
}