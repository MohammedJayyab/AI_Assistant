using Microsoft.Web.WebView2.WinForms;

namespace AI_Assistant
{
    public partial class frmAssistant : Form
    {
        private WebView2 _webView;
        private string _accumulatedHtml = string.Empty;
        private string _defaultLanguage = Constants.Language.Arabic.ToString();
        private LLMModel _llmModel;

        public frmAssistant()
        {
            InitializeComponent();
            InitModel();

            InitializeWebView();
            EnableButtons();
        }

        private async void InitializeWebView()
        {
            _webView = new WebView2();
            pnlBody.Controls.Add(_webView);
            _webView.BringToFront();
            _webView.Dock = DockStyle.Fill;
            await _webView.EnsureCoreWebView2Async();
        }

        private void frmAssistant_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                txtPrompt.Focus();
                txtPrompt.SelectAll();
            }));
        }

        public async void PreviewHtml(string htmlContent)
        {
            if (_webView != null && _webView.CoreWebView2 != null)
            {
                _webView.CoreWebView2.NavigateToString(htmlContent);
                await _webView.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.documentElement.scrollHeight);");
            }
        }

        private void InitModel()
        {
            LLMModelFactory lLMModelFactory = new LLMModelFactory();
            _llmModel = lLMModelFactory.InitModel();

            lblLLM.Text = $"LLM: {_llmModel.ProviderWithModel}";
        }

        private async Task GetResponseAsync(string prompt, bool isForImage = false)
        {
            try

            {
                if (string.IsNullOrEmpty(txtPrompt.Text.Trim()))
                {
                    return;
                }
                txtPrompt.Enabled = false;

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
            EnableButtons();
            _accumulatedHtml = string.Empty; // Reset the accumulated HTML content
            _llmModel.Client.ClearConversation();
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Default);
            _webView.CoreWebView2.NavigateToString("<html><body></body></html>");

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

            _accumulatedHtml += formattedText;
            PreviewHtml(_accumulatedHtml);
        }

        private async void txtPrompt_KeyDown(object sender, KeyEventArgs e)
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
                await GetResponseAsync(txtPrompt.Text.Trim());
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Default);
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnSummarize_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Summarize);
            txtPrompt.Text = "Summarize the following text: \n\n" + txtPrompt.Text;
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Translate);
            txtPrompt.Text = "Translate the following text to " + _defaultLanguage + ": \n\n" + txtPrompt.Text;
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnExplain_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Explain);
            txtPrompt.Text = "Explain the following text: \n\n" + txtPrompt.Text;
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnOnlyKeyPoints_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.KeyPoints);
            txtPrompt.Text = "Extract the key points from the following text: \n\n" + txtPrompt.Text;
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnEmailResponse_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.EmailResponse);
            txtPrompt.Text = "Write a professional response for the following email: \n\n" + txtPrompt.Text;
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnPrompt_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Prompt);
            txtPrompt.Text = "Write a prompt for the following text: \n\n" + txtPrompt.Text;
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnImage_Click(object sender, EventArgs e)
        {
            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Image);
            txtPrompt.Text = "Please explain the attached image.";
            await GetResponseAsync(txtPrompt.Text.Trim(), true);
        }

        private async void btnCaptureAndAskImage_Click(object sender, EventArgs e)
        {
            string imagePath = ImageHelper.SaveClipboardImageAsJpg();
            if (string.IsNullOrEmpty(imagePath))
            {
                MessageBox.Show("No image found in the clipboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtImageUrl.Text = imagePath;

            _llmModel.Client.SetSystemMessage(Constants.SystemMessages.Image);
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

        private void EnableButtons()
        {
            bool enabled = !string.IsNullOrWhiteSpace(txtPrompt.Text);
            btnSend.Enabled = enabled;
            btnSummarize.Enabled = enabled;
            btnTranslate.Enabled = enabled;
            btnExplain.Enabled = enabled;
            btnOnlyKeyPoints.Enabled = enabled;
            btnEmailResponse.Enabled = enabled;
            btnPrompt.Enabled = enabled;
            //btnImage.Enabled = enabled;
            //btnCaptureAndAskImage.Enabled = enabled;
        }

        private void txtPrompt_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }
    }
}