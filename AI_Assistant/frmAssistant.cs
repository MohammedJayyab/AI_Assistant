using Microsoft.Web.WebView2.WinForms;

namespace AI_Assistant
{
    public partial class frmAssistant : Form
    {
        private WebView2 webView;
        private string accumulatedHtml = string.Empty;

        //private static string _apiKey = string.Empty, _model = string.Empty;
        private static string _systemMessage = Constants.SystemMessages.Default;

        private string _defaultLanguage = Constants.Language.Arabic.ToString();

        //private LLMClient _client;
        private LLMModel _llmModel;

        public frmAssistant()
        {
            InitializeComponent();
            InitModel();

            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            webView = new WebView2();
            pnlBody.Controls.Add(webView);
            webView.BackColor = Color.Green;
            webView.BringToFront();
            webView.Dock = DockStyle.Fill;
            await webView.EnsureCoreWebView2Async();
        }

        public async void PreviewHtml(string htmlContent)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.NavigateToString(htmlContent);
                await webView.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.documentElement.scrollHeight);");
            }
        }

        private void InitModel()
        {
            LLMModelFactory lLMModelFactory = new LLMModelFactory();
            _llmModel = lLMModelFactory.InitModel();

            lblLLM.Text = $"LLM: {_llmModel.ProviderWithModel}";
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
                    response = await _llmModel.Client.GenerateTextWithImageAsync(prompt, txtImageUrl.Text);
                }
                else
                {
                    response = await _llmModel.Client.GenerateTextAsync(prompt);
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
            _systemMessage = Constants.SystemMessages.Default;
            accumulatedHtml = string.Empty; // Reset the accumulated HTML content
            _llmModel.Client.ClearConversation();
            webView.CoreWebView2.NavigateToString("<html><body></body></html>");

            txtPrompt.Clear();
            txtPrompt.Focus();
            txtPrompt.Enabled = true;
            txtPrompt.SelectAll();
        }

        private void AddToResponse(string text)
        {
            string cleanedText = text.Replace("```html", "").Replace("```", "").Replace("`", "");
            string formattedText = cleanedText;

            if (cleanedText.StartsWith("You:"))
            {
                string userText = cleanedText.Trim();
                if (userText.Length > 550)
                {
                    userText = userText.Substring(0, 500) + "\r\n........";
                }
                formattedText = $"<div style=\"text-align: right; background-color: #f0f0f0; padding: 8px; border-radius: 4px; margin-bottom: 6px;\">{userText}</div>";
            }
            else
            {
                formattedText = cleanedText.Replace("Assistant >>", "<strong style=\"color: #4285f4;\">Answer:</strong>");
                formattedText = $"<div style=\"margin-bottom: 6px;\">{formattedText}</div>"; // Add a little spacing
            }

            accumulatedHtml += formattedText;
            PreviewHtml(accumulatedHtml);
        }

        private void btnUpdateSize_Click(object sender, EventArgs e)
        {
            int defaultSize = _llmModel.Client.GetMaxMessages();
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

                _llmModel.Client.UpdateMaxMessages(newSize);
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
            _systemMessage = Constants.SystemMessages.Default;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Here is my prompt: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnSummarize_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.Summarize;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Summarize the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.Translate;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Translate the following text to " + _defaultLanguage + ": \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnExplain_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.Explain;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Explain the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnOnlyKeyPoints_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.KeyPoints;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Extract the key points from the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.Search;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Search the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnEmailResponse_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.EmailResponse;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Write a professional response for the following email: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private void btnPrompt_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.Prompt;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Write a prompt for the following text: \n\n" + txtPrompt.Text;
            btnSend_Click(sender, e);
        }

        private async void btnImage_Click(object sender, EventArgs e)
        {
            _systemMessage = Constants.SystemMessages.Image;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Please explain the attached image.";
            await GetResponseAsync(txtPrompt.Text.Trim(), true);
        }

        private async void btnCaptureAndAskImage_Click(object sender, EventArgs e)
        {
            string tempImagesPath = ImageHelper.GetTempImagesFolderPath();
            string imagePath = ImageHelper.SaveClipboardImageAsJpg(tempImagesPath);
            txtImageUrl.Text = imagePath;

            _systemMessage = Constants.SystemMessages.Image;
            _llmModel.Client.SetSystemMessage(_systemMessage);
            txtPrompt.Text = "Please explain the attached image.";

            await GetResponseAsync(txtPrompt.Text.Trim(), true);
        }

        private void radioAr_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = Constants.Language.Arabic.ToString();
        }

        private void radioEn_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = Constants.Language.English.ToString();
        }

        private void radioDe_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = Constants.Language.German.ToString();
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmModelSettings frmModelSettings = new frmModelSettings();
            frmModelSettings.ShowDialog();

            InitModel();
        }
    }
}