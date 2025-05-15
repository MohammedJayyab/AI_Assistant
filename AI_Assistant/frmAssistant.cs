using LLMKit;
using LLMKit.Models;
using LLMKit.Providers;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AI_Assistant
{
    public partial class frmAssistant : Form
    {
        public static string GetTempImagesFolderPath()
        {
            // Get the base directory of the application (usually the bin folder or a subfolder within it)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the "Temp_Images" folder name
            string tempImagesPath = Path.Combine(baseDirectory, "Temp_Images");

            return tempImagesPath;
        }

        private static string _apiKey, _model;
        private static string _systemMessage = SystemMessagesConstants.DefaultSystemMessage;
        private string _defaultLanguage = "ar";

        private LLMClient _client;

        public frmAssistant()
        {
            InitializeComponent();
            InitModel();
        }

        private void InitModel()
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.Local.json")
                    .Build();

            _apiKey = configuration["Gemini:ApiKey"];
            _model = configuration["Gemini:Model"];

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("API key is missing in appsettings.json.");
            }
            if (string.IsNullOrEmpty(_model))
            {
                throw new Exception("Model is missing in appsettings.json");
            }

            _client = new LLMClient(new GeminiProvider(_apiKey, _model), 1024, 0.3, 25);
            _client.SetSystemMessage(_systemMessage);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async Task GetResponseAsync(string prompt, bool isForImage = false)
        {
            try

            {
                txtPrompt.Enabled = false;
                if (string.IsNullOrEmpty(txtPrompt.Text.Trim()))
                {
                    return;
                }

                AddToResponse($"You: {txtPrompt.Text.Trim()}");
                string response = string.Empty;
                if (isForImage)
                {
                    if (string.IsNullOrEmpty(txtImageUrl.Text.Trim()))
                    {
                        MessageBox.Show("Please enter a valid image URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    response = await _client.GenerateTextWithImageAsync(prompt, txtImageUrl.Text);
                }
                else
                {
                    response = await _client.GenerateTextAsync(prompt);
                }

                AddToResponse($"Assistant >>  {response.Trim()}");
                txtPrompt.Clear();
                txtPrompt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtPrompt.Enabled = true;
                txtPrompt.Clear();
                txtPrompt.Focus();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.DefaultSystemMessage;
            _client.ClearConversation();
            txtResponse.Clear();
            txtPrompt.Clear();
            txtPrompt.Focus();
            txtPrompt.Enabled = true;
            txtPrompt.SelectAll();
        }

        private void AddToResponse(string text)
        {
            if (txtResponse.InvokeRequired)
            {
                txtResponse.Invoke(new Action(() => txtResponse.AppendText(text)));
            }
            else
            {
                txtResponse.AppendText(text);
                txtResponse.AppendText(Environment.NewLine);

                txtResponse.ScrollToCaret();
            }
        }

        private void btnUpdateSize_Click(object sender, EventArgs e)
        {
            int defaultSize = _client.GetMaxMessages();
            var input = Microsoft.VisualBasic.Interaction.InputBox("Enter the new size (2-100):", "Update Size", defaultSize.ToString());

            // User pressed Cancel or left it empty
            if (string.IsNullOrWhiteSpace(input))
                return;

            if (int.TryParse(input, out int newSize))
            {
                if (newSize < 2 || newSize > 100)
                {
                    MessageBox.Show("Size must be between 2 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _client.UpdateMaxMessages(newSize);
            }
            else
            {
                MessageBox.Show("Invalid size. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift)
            {
                // Allow multi-line input
                txtPrompt.AppendText(Environment.NewLine);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Prevent the default behavior of the Enter key
                e.SuppressKeyPress = true;
                btnSend_Click(sender, e);
            }
        }

        private void frmAssistant_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                txtPrompt.Focus();
                txtPrompt.SelectAll();
            }));
        }

        private void btnStandard_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.DefaultSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Here is my prompt: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnSummarize_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.SummarizeSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Summarize the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {                // todo : add a language selector
            _systemMessage = SystemMessagesConstants.TranslateSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Translate the following text to " + _defaultLanguage + ": \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnExplain_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.ExplainSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Explain the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnOnlyKeyPoints_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.KeyPointsSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Extract the key points from the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.SearchSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Search the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnEmailResponse_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.EmailResponseSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Write a professional response for the following email: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnPrompt_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.PromptSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Write a prompt for the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private async void btnImage_Click(object sender, EventArgs e)
        {
            _systemMessage = SystemMessagesConstants.ImageSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Explain the image attached, if it has question or code challanges then also provide a solution.";
            await GetResponseAsync(txtPrompt.Text.Trim(), true);
        }

        private async void btnCaptureAndAskImage_Click(object sender, EventArgs e)
        {
            string tempImagesPath = GetTempImagesFolderPath();
            string imagePath = ImageHelper.SaveClipboardImageAsJpg(tempImagesPath);
            txtImageUrl.Text = imagePath;

            _systemMessage = SystemMessagesConstants.ImageSystemMessage;
            _client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Explain the image attached, if it has question or code challanges then also provide a solution.";

            await GetResponseAsync(txtPrompt.Text.Trim(), true);
        }

        private void radioAr_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = "Arabic";
        }

        private void radioEn_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = "English";
        }

        private void radioDe_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = "German";
        }

        private void lnkBrowsImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Updated filter to include common image types and handle both lowercase and uppercase extensions.
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.gif; *.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.JPG;*.JPEG;*.PNG;*.BMP;*.GIF;*.TIFF|All files (*.*)|*.*";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImageUrl.Text = openFileDialog.FileName;
                }
            }
        }
    }
}