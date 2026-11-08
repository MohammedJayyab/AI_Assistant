using LLMKit.Models;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AI_Assistant
{
    public partial class frmAssistant : Form
    {
        private const string UserPrefix = "You:";
        private const string AssistantPrefix = "Assistant >>";
        private const string CompactPrefix = "Compact >>";
        private const int HistoryRecentCount = 5;
        private const int HistoryMoreCount = 10;
        private const int EmptyContextMessages = 1; // an empty LLM context holds just the system message
        private const string CompactCommand = "/compact";
        private const string SettingsTip = "Click to open the settings";
        private const string NoTokensText = "Tokens: -";
        private const string AppTitle = "AI Assistant";
        private static readonly Color ThinkingHighlight = Color.Yellow;

        private WebView2 _webView;
        private YoutubeTranscriptFetcher _youtubeFetcher; // created on first use, released when the form closes
        private readonly CancellationTokenSource _lifetime = new CancellationTokenSource(); // cancels a running request when the form closes
        private readonly Font _boldStatusFont; // the model and the messages stand out in the status bar
        private string _accumulatedHtml = string.Empty;
        private string _defaultLanguage = Constants.Language.Arabic.ToString();
        private LLMModel _llmModel;
        private ChatSession _session = new ChatSession();
        private readonly Stopwatch _requestTimer = new Stopwatch();
        private int _tokensIn;
        private string _summary; // after a compaction: the summary that replaced the old messages sent to the LLM
        private string _promptKey = Constants.Prompts.Default; // the system prompt of the next request
        private (string PromptKey, string ImagePath) _lastAnswered = (Constants.Prompts.Default, string.Empty); // how the last answer was asked, so Re-Generate asks the same way

        public frmAssistant()
        {
            InitializeComponent();
            RestoreWindowSize();
            _boldStatusFont = new Font(lblMessages.Font, FontStyle.Bold);
            lblModel.Font = _boldStatusFont;
            lblMessages.Font = _boldStatusFont;
            statusStrip1.Renderer = new SleekRenderer();
            InitModel();

            EnableButtons();
        }

        private async Task InitializeWebView()
        {
            _webView = new WebView2();
            pnlBody.Controls.Add(_webView);
            _webView.BringToFront();
            _webView.Dock = DockStyle.Fill;
            await _webView.EnsureCoreWebView2Async();
            _webView.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;
        }

        // The buttons of a message post JSON: { action: "copy" | "resend", text }.
        private async void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string action;
            string text;
            try
            {
                using JsonDocument message = JsonDocument.Parse(e.TryGetWebMessageAsString());
                action = message.RootElement.GetProperty("action").GetString();
                text = message.RootElement.GetProperty("text").GetString();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is JsonException || ex is KeyNotFoundException || ex is InvalidOperationException)
            {
                return; // not one of our messages
            }

            if (action == "copy")
            {
                CopyToClipboard(text);
            }
            else if (action == "resend")
            {
                await ResendEditedQuestionAsync(text);
            }
            else if (action == "regenerate")
            {
                await RegenerateLastAnswerAsync();
            }
        }

        // Asks the last question again, with the same prompt and image: the new answer replaces the last one in place,
        // in the LLM context, the chat and the history. When no new answer comes (an error or an empty answer), the previous one is kept.
        private async Task RegenerateLastAnswerAsync()
        {
            if (_requestTimer.IsRunning || !txtPrompt.Enabled)
            {
                lblStatus.Text = "Please wait for the current request before regenerating";
                return;
            }

            int answerIndex = _session.Entries.Count - 1;
            if (answerIndex < 1 || !_session.Entries[answerIndex].Text.StartsWith(AssistantPrefix) || !_session.Entries[answerIndex - 1].Text.StartsWith(UserPrefix))
            {
                return; // only the last answer, right after its question
            }

            var (promptKey, imagePath) = _lastAnswered;
            bool withImage = imagePath.Length > 0;
            if (withImage && !File.Exists(imagePath))
            {
                MessageBox.Show("The image of this question was deleted, so the answer cannot be regenerated with it. Attach the image again and send the question.", "Re-Generate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (withImage && !_llmModel.Client.Capabilities.SupportsImages)
            {
                MessageBox.Show($"This question has an image, and {_llmModel.Provider} does not accept images. Choose another provider in the settings to regenerate it.", "Re-Generate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatEntry question = _session.Entries[answerIndex - 1];
            ChatEntry previousAnswer = _session.Entries[answerIndex];
            _session.Entries.RemoveRange(answerIndex - 1, 2);
            _accumulatedHtml = string.Concat(_session.Entries.Select(entry => FormatMessage(entry.Text)));

            string draft = txtPrompt.Text; // what was typed (and attached) for the next message meanwhile
            string draftImage = txtImageUrl.Text;
            string questionText = question.Text.Substring(UserPrefix.Length).Trim();
            txtPrompt.Text = questionText;
            txtImageUrl.Text = imagePath;
            SetSystemMessage(promptKey);
            await GetResponseAsync(questionText, withImage);
            txtPrompt.Text = draft;
            txtImageUrl.Text = draftImage;

            if (_session.Entries.LastOrDefault()?.Text.StartsWith(AssistantPrefix) != true && !_lifetime.IsCancellationRequested)
            {
                // no new answer (the reason is already shown): the previous answer is back in its place
                if (_session.Entries.LastOrDefault()?.Text.StartsWith(UserPrefix) != true)
                {
                    AddToResponse(question.Text);
                }
                AddToResponse(previousAnswer.Text);
                lblStatus.Text += ". The previous answer is kept.";
            }
        }

        // The edited question replaces the last question and its answer, in the LLM context, the chat and the history.
        private async Task ResendEditedQuestionAsync(string newQuestion)
        {
            newQuestion = newQuestion.Trim();
            int index = _session.Entries.FindLastIndex(entry => entry.Text.StartsWith(UserPrefix));
            if (newQuestion.Length == 0 || index < 0)
            {
                return;
            }
            if (_requestTimer.IsRunning || !txtPrompt.Enabled)
            {
                lblStatus.Text = "Please wait for the current request before editing";
                return;
            }
            if (_session.Entries.Skip(index).Any(entry => entry.Text.StartsWith(CompactPrefix)))
            {
                return; // the question is already part of a summary
            }

            bool hasAnswer = index + 1 < _session.Entries.Count && _session.Entries[index + 1].Text.StartsWith(AssistantPrefix);
            _session.Entries.RemoveRange(index, hasAnswer ? 2 : 1);
            _accumulatedHtml = string.Concat(_session.Entries.Select(entry => FormatMessage(entry.Text)));

            string draft = txtPrompt.Text; // what was typed in the box meanwhile
            txtPrompt.Text = newQuestion;
            SetSystemMessage(Constants.Prompts.Default);
            await GetResponseAsync(newQuestion);
            txtPrompt.Text = draft;
        }

        private void CopyToClipboard(string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    Clipboard.SetText(text.Replace("\r\n", "\n").Replace("\n", "\r\n")); // Windows line breaks
                    lblStatus.Text = "Copied to clipboard";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Copy failed: {ex.Message}";
            }
        }

        private async void btnCopyLast_Click(object sender, EventArgs e)
        {
            // the plain text of the last answer, as shown in the chat
            string json = await _webView.CoreWebView2.ExecuteScriptAsync("(function () { var b = document.querySelectorAll('.msg.assistant .msg-body'); return b.length ? b[b.length - 1].innerText : ''; })()");
            CopyToClipboard(JsonSerializer.Deserialize<string>(json));
        }

        private async void frmAssistant_Load(object sender, EventArgs e)
        {
            if (_llmModel.Client == null)
            {
                Close(); // the settings are not usable (the factory reported it and opened the file): nothing to chat with
                return;
            }

            this.BeginInvoke(new Action(() =>
            {
                txtPrompt.Focus();
                txtPrompt.SelectAll();
            }));
            await InitializeWebView();
            btnNew_Click(sender, e);
        }

        private async void PreviewHtml(string htmlContent)
        {
            if (_webView != null && _webView.CoreWebView2 != null)
            {
                _webView.CoreWebView2.NavigateToString(Htmltemplate.MessageSupport + htmlContent);
                await _webView.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.documentElement.scrollHeight);");
            }
        }

        private void InitModel()
        {
            LLMModelFactory lLMModelFactory = new LLMModelFactory();
            _llmModel = lLMModelFactory.InitModel();
            ShowModelSettings();
        }

        private void ShowModelSettings()
        {
            Text = string.IsNullOrEmpty(_llmModel.Model) ? AppTitle : $"{AppTitle} - {_llmModel.Model}";
            lblProvider.Text = $"Provider: {_llmModel.Provider}";
            lblModel.Text = $"Model: {_llmModel.Model}";
            lblMaxTokens.Text = $"Max Tokens: {_llmModel.MaxTokens:N0}";

            // what LLMKit sends for this model with these settings: a setting the model does not use is marked
            ProviderCapabilities capabilities = _llmModel.Client?.Capabilities;
            bool temperatureUnused = capabilities?.SupportsTemperature == false;
            bool thinkingUnused = _llmModel.ReasoningEffort != null && capabilities?.SupportsReasoningEffort == false;
            bool thinkingLowest = _llmModel.ReasoningEffort == ReasoningEffort.None && capabilities?.SupportsReasoningEffort == true && !capabilities.CanTurnOffReasoning;

            lblTemperature.Text = $"Temp.: {_llmModel.Temperature:0.0#}{(temperatureUnused ? " (not used)" : string.Empty)}";
            lblTemperature.ToolTipText = temperatureUnused
                ? $"The model does not take a temperature with these settings (for example while it thinks), so it is not sent.\n{SettingsTip}"
                : SettingsTip;

            lblReasoning.Text = $"Thinking: {ThinkingText(_llmModel.ReasoningEffort)}{(thinkingUnused ? " (not used)" : thinkingLowest ? " (lowest)" : string.Empty)}";
            // the model takes a thinking level: highlighted, so it is easy to see that the setting matters
            lblReasoning.BackColor = capabilities?.SupportsReasoningEffort == true ? ThinkingHighlight : Color.Empty;
            lblReasoning.ToolTipText = thinkingUnused ? $"The model does not think, or thinks at a fixed level, so the thinking level is not sent.\n{SettingsTip}"
                                     : thinkingLowest ? $"The model cannot turn thinking off: None uses its lowest level.\n{SettingsTip}"
                                     : SettingsTip;
            UpdateConversationStatus();
        }

        private static string ThinkingText(ReasoningEffort? effort) => effort?.ToString() ?? Constants.LLM.ReasoningDefault;

        // The system message of a request. After a compaction it also carries the summary, so the context is just this one message.
        private void SetSystemMessage(string promptKey)
        {
            _promptKey = promptKey;
            string systemMessage = PromptStore.GetSystemMessage(promptKey);
            if (_summary != null)
            {
                systemMessage += "\n\n" + PromptStore.BuildUserPrompt(Constants.Prompts.CompactContext, _summary);
            }
            _llmModel.Client.SetSystemMessage(systemMessage);
        }

        // Messages used by the LLM context, and the buttons that depend on the conversation.
        private void UpdateConversationStatus()
        {
            int used = _llmModel.Client?.CurrentConversation.MessageCount ?? 0;
            lblMessages.Text = $"Messages: {used} / {_llmModel.MaxMessages}";
            double usage = _llmModel.MaxMessages > 0 ? (double)used / _llmModel.MaxMessages : 0;
            lblMessages.ForeColor = usage >= Constants.LLM.MessagesCriticalRatio ? Color.Red
                                  : usage >= Constants.LLM.MessagesWarningRatio ? Color.DarkOrange
                                  : SystemColors.ControlText;
            lblCompacted.Visible = _summary != null;
            btnCopyLast.Enabled = _session.Entries.Any(entry => entry.Text.StartsWith(AssistantPrefix));
            btnCompact.Enabled = !_requestTimer.IsRunning && used > EmptyContextMessages;
        }

        private void ShowTokens(int tokensIn, int? tokensOut)
        {
            string tokensOutText = tokensOut.HasValue ? $"~{tokensOut.Value:N0}" : "...";
            lblTokens.Text = $"Tokens: in: ~{tokensIn:N0} - out: {tokensOutText}";
        }

        // After a response: the token counts the provider reported (out includes the thinking), or the estimate when it reported none.
        private void ShowUsage(string response)
        {
            TokenUsage usage = _llmModel.Client.LastUsage;
            if (usage == null)
            {
                ShowTokens(_tokensIn, TokenEstimator.Estimate(response));
                return;
            }

            // models that do not think report 0 thinking tokens: nothing to show then
            string thinking = usage.ReasoningTokens is int reasoningTokens && reasoningTokens > 0 ? $" (thinking {reasoningTokens:N0})" : string.Empty;
            lblTokens.Text = $"Tokens: in: {usage.InputTokens:N0} - out: {usage.OutputTokens:N0}{thinking}";
        }

        // A failed request: the reason in the status bar, and what to do about it with the provider's details in a message.
        private void ShowRequestError(string title, Exception ex)
        {
            StopRequest();
            lblTokens.Text = NoTokensText; // nothing was answered, so there is no usage
            var (reason, advice) = RequestErrorHelper.Explain(ex, _llmModel);
            lblStatus.Text = $"{title}: {reason}";
            MessageBox.Show($"{reason}.\n\n{advice}\n\nDetails:\n{ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Why the last answer is empty or incomplete, from the finish reason the provider reported; null for a complete answer.
        private string AnswerProblem(string response)
        {
            bool empty = string.IsNullOrWhiteSpace(response);
            // the thinking is to blame when the model reported thinking tokens, or thinks with these settings (Anthropic reports none separately)
            bool thinks = _llmModel.Client.LastUsage?.ReasoningTokens > 0
                       || (_llmModel.Client.Capabilities.SupportsReasoningEffort && _llmModel.ReasoningEffort != ReasoningEffort.None);
            return _llmModel.Client.LastFinishReason switch
            {
                FinishReason.MaxTokens when empty && thinks => $"The thinking used all {_llmModel.MaxTokens:N0} max tokens, so no answer was left. Increase Max Tokens (for example to 16,000) or set a lower Thinking level in the settings, then ask again.",
                FinishReason.MaxTokens when empty => $"The model used all {_llmModel.MaxTokens:N0} max tokens before writing an answer. Increase Max Tokens in the settings, then ask again.",
                FinishReason.MaxTokens => $"The answer was cut off at {_llmModel.MaxTokens:N0} max tokens. Increase Max Tokens in the settings for the full answer, or ask the model to continue.",
                FinishReason.ContentFilter when empty => "The provider's safety filter blocked the answer. Rephrase the request and try again.",
                FinishReason.ContentFilter => "The provider's safety filter stopped the answer before the end. Rephrase the request for a complete answer.",
                _ when empty => "The model returned an empty answer. Try again, or rephrase the request.",
                _ => null
            };
        }

        // Starts the progress bar and the elapsed-time display; the input tokens are the whole context sent with the prompt.
        private void StartRequest(string prompt)
        {
            _tokensIn = TokenEstimator.Estimate(_llmModel.Client.GetFormattedConversation()) + TokenEstimator.Estimate(prompt);
            ShowTokens(_tokensIn, null);
            progressRequest.Visible = true;
            _requestTimer.Restart();
            tmrRequest.Start();
            UpdateRequestStatus();
            UpdateConversationStatus();
        }

        private void StopRequest()
        {
            tmrRequest.Stop();
            _requestTimer.Stop();
            progressRequest.Visible = false;
        }

        private void UpdateRequestStatus()
        {
            lblStatus.Text = $"Waiting for response... {_requestTimer.Elapsed.TotalSeconds:0.0} s";
        }

        private void tmrRequest_Tick(object sender, EventArgs e)
        {
            UpdateRequestStatus();
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

                // what the LLM remembers is derived from the chat right before the request (the new question is not in it yet)
                RebuildContext(_session.Entries, _promptKey, forNextQuestion: true);
                AddToResponse($"{UserPrefix} {txtPrompt.Text.Trim()}");
                StartRequest(prompt);
                string response = isForImage
                    ? await _llmModel.Client.GenerateTextWithImageAsync(prompt, txtImageUrl.Text, _lifetime.Token)
                    : await _llmModel.Client.GenerateTextAsync(prompt, _lifetime.Token);

                StopRequest();
                ShowUsage(response);

                string problem = AnswerProblem(response);
                if (string.IsNullOrWhiteSpace(response))
                {
                    // nothing to show or keep: the question stays without an answer, like a failed request
                    lblStatus.Text = "No answer";
                    MessageBox.Show(problem, "No Answer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = $"Done in {_requestTimer.Elapsed.TotalSeconds:0.0} s{(problem != null ? $". {problem}" : string.Empty)}";
                AddToResponse($"{AssistantPrefix}  {response.Trim()}");
                _lastAnswered = (_promptKey, isForImage ? txtImageUrl.Text : string.Empty);
            }
            catch (Exception) when (_lifetime.IsCancellationRequested)
            {
                // the form is closing: the request was cancelled on purpose (LLMKit wraps the cancellation in its own exception)
            }
            catch (Exception ex)
            {
                ShowRequestError("Request failed", ex);
            }
            finally
            {
                txtPrompt.Enabled = true;
                txtPrompt.Clear();
                txtPrompt.Focus();
                txtImageUrl.Text = string.Empty; // Clear the image URL after processing
                UpdateConversationStatus();
            }
        }

        // Replaces the old messages sent to the LLM with a summary. The chat view and the history keep everything.
        private async void btnCompact_Click(object sender, EventArgs e)
        {
            await CompactAsync();
        }

        // Also runs when the user sends the /compact command.
        private async Task CompactAsync()
        {
            int messagesBefore = _llmModel.Client.CurrentConversation.MessageCount;
            if (_requestTimer.IsRunning || messagesBefore <= EmptyContextMessages)
            {
                lblStatus.Text = "Nothing to compact yet";
                return;
            }

            try
            {
                txtPrompt.Enabled = false;
                string prompt = PromptStore.BuildUserPrompt(Constants.Prompts.Compact, string.Empty).Trim();
                RebuildContext(_session.Entries, Constants.Prompts.Compact, forNextQuestion: true); // an earlier summary is part of it

                StartRequest(prompt);
                string summary = (await _llmModel.Client.GenerateTextAsync(prompt, _lifetime.Token)).Trim();
                StopRequest();
                ShowUsage(summary);

                // an empty or cut-off summary would lose the conversation: keep the current context instead
                string problem = AnswerProblem(summary);
                if (problem != null)
                {
                    RebuildContext(_session.Entries, Constants.Prompts.Default);
                    lblStatus.Text = "Compact failed";
                    MessageBox.Show($"The conversation was not compacted. {problem}", "Compact", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ResetContextWithSummary(summary);
                AddToResponse($"{CompactPrefix}  {summary}");
                lblStatus.Text = $"Conversation compacted: {messagesBefore} messages reset to {_llmModel.Client.CurrentConversation.MessageCount}";
            }
            catch (Exception) when (_lifetime.IsCancellationRequested)
            {
                // the form is closing: the request was cancelled on purpose
            }
            catch (Exception ex)
            {
                ShowRequestError("Compact failed", ex);
            }
            finally
            {
                txtPrompt.Enabled = true;
                txtPrompt.Focus();
                UpdateConversationStatus();
            }
        }

        // A new LLM context: just the system message, which carries the summary. The counter is back to 1.
        private void ResetContextWithSummary(string summary)
        {
            _summary = summary;
            _llmModel.Client.ClearConversation();
            SetSystemMessage(Constants.Prompts.Default);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableButtons();
            _accumulatedHtml = string.Empty; // Reset the accumulated HTML content
            _session = new ChatSession(); // A new chat is a new history session
            lblStatus.Text = "Ready";
            lblTokens.Text = NoTokensText;
            _summary = null;
            _lastAnswered = (Constants.Prompts.Default, string.Empty); // a chat opened from the history regenerates with the default prompt
            _llmModel.Client.ClearConversation();
            SetSystemMessage(Constants.Prompts.Default);
            UpdateConversationStatus();
            _webView.CoreWebView2.NavigateToString(Htmltemplate.EmptyHtmlWithTitle(_llmModel.Model));

            txtPrompt.Clear();
            txtPrompt.Focus();
            txtPrompt.Enabled = true;
            txtPrompt.SelectAll();

            txtImageUrl.Text = string.Empty;
        }

        private void AddToResponse(string text)
        {
            _accumulatedHtml += FormatMessage(text);
            PreviewHtml(_accumulatedHtml);
            SaveToHistory(text);
        }

        private void SaveToHistory(string text)
        {
            try
            {
                _session.Add(text, text.StartsWith(UserPrefix) ? text.Substring(UserPrefix.Length) : string.Empty);
                ChatHistory.Save(_session);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save the chat history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatMessage(string text)
        {
            string cleanedText = text.Replace("```html", "").Replace("```", "").Replace("`", "");
            string formattedText = cleanedText;

            if (cleanedText.StartsWith(UserPrefix))
            {
                string userText = cleanedText.Trim();
                if (userText.Length > 550)
                {
                    userText = userText.Substring(0, 500) + "\r\n........";
                }
                // the full query is copied, not the shortened text shown here
                string queryToCopy = WebUtility.HtmlEncode(text.Substring(UserPrefix.Length).Trim());
                formattedText = $"<div class=\"msg user\" data-copy=\"{queryToCopy}\" style=\"text-align: right; background-color: #f0f0f0; padding: 8px; border-radius: 4px; margin-bottom: 6px;\"><span class=\"msg-body\">{userText}</span>{Htmltemplate.MessageActions(true)}</div>";
            }
            else if (cleanedText.StartsWith(CompactPrefix))
            {
                formattedText = $"<div class=\"msg compact\" style=\"border-left: 4px solid #f0ad4e; background-color: #fff8e5; padding: 8px; margin-bottom: 6px;\"><strong style=\"color: #b26a00;\">Conversation compacted</strong> <small>(a new conversation starts from this summary; the messages above stay here and in the history, but are no longer sent to the LLM)</small><br><span class=\"msg-body\">{cleanedText.Substring(CompactPrefix.Length)}</span>{Htmltemplate.MessageActions(false)}</div>";
                formattedText += "<br>";
            }
            else
            {
                // the "Answer:" label stays outside of .msg-body, so it is not copied
                string label = string.Empty;
                string body = cleanedText;
                string cssClass = "msg";
                if (cleanedText.StartsWith(AssistantPrefix))
                {
                    label = "<strong style=\"color: #4285f4;\">Answer:</strong>";
                    body = cleanedText.Substring(AssistantPrefix.Length);
                    cssClass = "msg assistant";
                }
                formattedText = $"<div class=\"{cssClass}\" style=\"margin-bottom: 6px;\">{label}<span class=\"msg-body\">{body}</span>{Htmltemplate.MessageActions(false, isAnswer: cleanedText.StartsWith(AssistantPrefix))}</div>";
                // add new line
                formattedText += "<br>";
                formattedText += "<br>";
            }

            return formattedText;
        }

        private void txtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift)
            {
                // Allow multi-line input
                txtPrompt.AppendText(Environment.NewLine);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsImage())
                {
                    // Handle image paste if needed
                    string imagePath = ImageHelper.SaveClipboardImageAsJpg();
                    txtImageUrl.Text = imagePath;
                    txtPrompt.AppendText("Image Attached...\r\n");
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Prevent the default behavior of the Enter key
                e.SuppressKeyPress = true;
                btnSend_Click(sender, e); // Call the send button click event handler
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.Equals(txtPrompt.Text.Trim(), CompactCommand, StringComparison.OrdinalIgnoreCase))
            {
                txtPrompt.Clear(); // a command, not a message of the conversation
                await CompactAsync();
                return;
            }

            SetSystemMessage(Constants.Prompts.Default);
            bool isImageAttached = !string.IsNullOrWhiteSpace(txtImageUrl.Text.Trim());
            if (!string.IsNullOrWhiteSpace(txtImageUrl.Text))
            {
                if (!_llmModel.Client.Capabilities.SupportsImages)
                {
                    // LLMKit would drop the image and send only the text
                    MessageBox.Show($"{_llmModel.Provider} does not accept images. Remove the image or choose another provider in the settings.", "Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SetSystemMessage(Constants.Prompts.Image);
            }

            await GetResponseAsync(txtPrompt.Text.Trim(), isImageAttached);
        }

        // Applies the stored system message and user template of the prompt, then sends it.
        private async Task RunPromptAsync(string promptKey)
        {
            SetSystemMessage(promptKey);
            txtPrompt.Text = PromptStore.BuildUserPrompt(promptKey, txtPrompt.Text, _defaultLanguage);
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private async void btnSummarize_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.Summarize);
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.Translate);
        }

        private async void btnExplain_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.Explain);
        }

        private async void btnOnlyKeyPoints_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.KeyPoints);
        }

        private async void btnEmailResponse_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.EmailResponse);
        }

        private async void btnPrompt_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.Prompt);
        }

        private async void btnMindmap_Click(object sender, EventArgs e)
        {
            await RunPromptAsync(Constants.Prompts.MindMap);
        }

        // Fetches the transcript of the URL in the prompt box and shows it in the chat. False when it fails (the error is shown).
        private bool TryFetchTranscript(out string title, out string script)
        {
            title = script = string.Empty;
            try
            {
                _youtubeFetcher ??= new YoutubeTranscriptFetcher(); // one Python runtime for the whole session
                (title, script) = _youtubeFetcher.FetchTranscript(txtPrompt.Text.Trim());
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Transcript failed";
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            AddToResponse($"Youtube Transcript<br> Title: '{title}' <br>  {script.Trim()}");
            txtPrompt.Clear();
            txtPrompt.Focus();
            return true;
        }

        private void btnYoutubeScript_Click(object sender, EventArgs e)
        {
            TryFetchTranscript(out _, out _);
        }

        private async void btnYoutubeSummary_Click(object sender, EventArgs e)
        {
            if (!TryFetchTranscript(out string title, out string script))
            {
                return;
            }

            SetSystemMessage(Constants.Prompts.YoutubeSummary);
            txtPrompt.Text = PromptStore.BuildUserPrompt(Constants.Prompts.YoutubeSummary, script, title: title);
            await GetResponseAsync(txtPrompt.Text.Trim());
        }

        private void radioAr_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = Constants.Language.Arabic.ToString();
            UpdateLanguageMenu();
        }

        private void radioEn_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = Constants.Language.English.ToString();
            UpdateLanguageMenu();
        }

        private void radioDe_CheckedChanged(object sender, EventArgs e)
        {
            _defaultLanguage = Constants.Language.German.ToString();
            UpdateLanguageMenu();
        }

        // Keep the Language menu checkmarks in sync with the radio buttons.
        private void UpdateLanguageMenu()
        {
            mnuLangArabic.Checked = radioAr.Checked;
            mnuLangEnglish.Checked = radioEn.Checked;
            mnuLangGerman.Checked = radioDe.Checked;
        }

        private void mnuLangArabic_Click(object sender, EventArgs e) => radioAr.Checked = true;

        private void mnuLangEnglish_Click(object sender, EventArgs e) => radioEn.Checked = true;

        private void mnuLangGerman_Click(object sender, EventArgs e) => radioDe.Checked = true;

        private void mnuExit_Click(object sender, EventArgs e) => Close();

        // Rebuilt every time it opens, so new sessions show up immediately.
        private void mnuHistory_DropDownOpening(object sender, EventArgs e)
        {
            mnuHistory.DropDownItems.Clear();

            List<ChatSession> sessions = ChatHistory.GetRecent(HistoryRecentCount + HistoryMoreCount);
            if (sessions.Count == 0)
            {
                mnuHistory.DropDownItems.Add(new ToolStripMenuItem("(No history yet)") { Enabled = false });
                return;
            }

            foreach (ChatSession session in sessions.Take(HistoryRecentCount))
            {
                mnuHistory.DropDownItems.Add(CreateHistoryItem(session));
            }

            if (sessions.Count > HistoryRecentCount)
            {
                var mnuMore = new ToolStripMenuItem("More...");
                foreach (ChatSession session in sessions.Skip(HistoryRecentCount))
                {
                    mnuMore.DropDownItems.Add(CreateHistoryItem(session));
                }
                mnuHistory.DropDownItems.Add(new ToolStripSeparator());
                mnuHistory.DropDownItems.Add(mnuMore);
            }
        }

        private ToolStripMenuItem CreateHistoryItem(ChatSession session)
        {
            // '&' would otherwise be read as a menu mnemonic.
            string title = string.IsNullOrEmpty(session.Title) ? "(untitled)" : session.Title.Replace("&", "&&");
            var item = new ToolStripMenuItem($"{session.StartedAt:yyyy-MM-dd HH:mm}  -  {title}") { Tag = session };
            item.Click += mnuHistoryItem_Click;
            return item;
        }

        private void mnuHistoryItem_Click(object sender, EventArgs e)
        {
            OpenSession((ChatSession)((ToolStripMenuItem)sender).Tag);
        }

        // Shows a stored session and restores its messages as the LLM context, so the chat can continue.
        private void OpenSession(ChatSession session)
        {
            btnNew_Click(this, EventArgs.Empty);
            _session = session;
            _accumulatedHtml = string.Concat(session.Entries.Select(entry => FormatMessage(entry.Text)));
            int notLoaded = RebuildContext(session.Entries, Constants.Prompts.Default);

            UpdateConversationStatus();
            PreviewHtml(_accumulatedHtml);
            lblStatus.Text = notLoaded > 0
                ? $"Chat opened. The LLM context holds the latest messages that fit the maximum ({_llmModel.MaxMessages}); the oldest {notLoaded} are not sent to the LLM"
                : "Chat opened";
        }

        // The LLM context is derived from the chat, never kept as a separate state that can drift from it: the last
        // summary (if the chat was compacted), then the latest messages that fit the maximum, in their original order.
        // That is what a live chat holds, so loading a chat from the history gives the same context.
        // Right before a request (forNextQuestion) one slot is kept for the new question, and the context starts with
        // a question, because providers do not accept an answer first. Returns how many messages are left out.
        private int RebuildContext(IEnumerable<ChatEntry> entries, string promptKey, bool forNextQuestion = false)
        {
            _summary = null;
            _llmModel.Client.ClearConversation();

            // Only complete question/answer pairs: a question without an answer (a failed request) is left out.
            var messages = new List<(bool IsQuestion, string Text)>();
            string question = null;
            foreach (ChatEntry entry in entries)
            {
                if (entry.Text.StartsWith(UserPrefix))
                {
                    question = entry.Text.Substring(UserPrefix.Length).Trim();
                }
                else if (entry.Text.StartsWith(AssistantPrefix) && question != null)
                {
                    messages.Add((true, question));
                    messages.Add((false, entry.Text.Substring(AssistantPrefix.Length).Trim()));
                    question = null;
                }
                else if (entry.Text.StartsWith(CompactPrefix))
                {
                    _summary = entry.Text.Substring(CompactPrefix.Length).Trim(); // replaces everything before it
                    messages.Clear();
                    question = null;
                }
            }

            SetSystemMessage(promptKey);
            int capacity = Math.Max(0, _llmModel.MaxMessages - EmptyContextMessages - (forNextQuestion ? 1 : 0));
            int notLoaded = Math.Max(0, messages.Count - capacity);
            if (forNextQuestion && notLoaded < messages.Count && !messages[notLoaded].IsQuestion)
            {
                notLoaded++; // an answer whose question was cut off
            }

            foreach (var (isQuestion, text) in messages.Skip(notLoaded))
            {
                _llmModel.Client.CurrentConversation.AddMessage(isQuestion ? ChatMessage.Roles.User : ChatMessage.Roles.Assistant, text);
            }
            return notLoaded;
        }

        private void mnuOpenHistoryFolder_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = ChatHistory.FolderPath, UseShellExecute = true });
        }

        private void mnuOpenImagesFolder_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = ImageHelper.GetTempImagesFolderPath(), UseShellExecute = true });
        }

        // Deletes the pasted images after a confirmation. Images chosen with Add Image are the user's own files and are not touched.
        private void mnuClearImages_Click(object sender, EventArgs e)
        {
            if (_requestTimer.IsRunning)
            {
                lblStatus.Text = "Please wait for the current request before clearing the images";
                return;
            }

            int count = ImageHelper.CountTempImages();
            if (count == 0)
            {
                MessageBox.Show("There are no pasted images to delete.", "Clear Images", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string imagesFolder = ImageHelper.GetTempImagesFolderPath();
            bool attachedIsPasted = !string.IsNullOrWhiteSpace(txtImageUrl.Text)
                                 && txtImageUrl.Text.StartsWith(imagesFolder, StringComparison.OrdinalIgnoreCase);
            string attachedNote = attachedIsPasted ? "\n\nThe image attached to your message is pasted, so it is removed from the message." : string.Empty;
            if (MessageBox.Show($"Delete {(count == 1 ? "the pasted image" : $"all {count} pasted images")}?\n\n{imagesFolder}{attachedNote}\n\nThis cannot be undone. The chat and the history are not affected.",
                    "Clear Images", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            if (attachedIsPasted)
            {
                btnAddImage_Click(sender, e); // removes the attachment, which no longer exists
            }

            int deleted = ImageHelper.DeleteTempImages();
            int left = count - deleted;
            lblStatus.Text = left > 0
                ? $"Deleted {deleted} of {count} images; {left} are in use (for example open in an image viewer). Close them and clear again."
                : $"Deleted {deleted} image{(deleted == 1 ? string.Empty : "s")}";
        }

        private void mnuOpenAppLocation_Click(object sender, EventArgs e)
        {
            // Opens Explorer with the running exe selected.
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{Environment.ProcessPath}\"",
                UseShellExecute = true
            });
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

        // The LLM settings in the status bar look clickable: they open the settings.
        private void lblSettings_MouseEnter(object sender, EventArgs e) => statusStrip1.Cursor = Cursors.Hand;

        private void lblSettings_MouseLeave(object sender, EventArgs e) => statusStrip1.Cursor = Cursors.Default;

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (_requestTimer.IsRunning)
            {
                lblStatus.Text = "Please wait for the current request before changing the settings";
                return;
            }

            string settingsPath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFile);
            string settingsBefore = File.ReadAllText(settingsPath);

            using (var frmModelSettings = new frmModelSettings())
            {
                frmModelSettings.ShowDialog(this);
            }

            if (File.ReadAllText(settingsPath) == settingsBefore)
            {
                return; // nothing changed: same LLM client, the conversation goes on
            }

            LLMModel updated = new LLMModelFactory().InitModel();
            if (updated.Client == null)
            {
                return; // the new settings are not usable (the factory reported it): keep working with the current ones
            }

            if (!ConfirmNewSettings(_llmModel, updated))
            {
                File.WriteAllText(settingsPath, settingsBefore); // undo
                updated.Client.Dispose();
                return;
            }

            // A new settings means a new LLM client, so the context is rebuilt from the chat.
            _llmModel.Client.Dispose();
            _llmModel = updated;
            RebuildContext(_session.Entries, Constants.Prompts.Default);
            ShowModelSettings();
            if (_session.Entries.Count == 0)
            {
                _webView.CoreWebView2.NavigateToString(Htmltemplate.EmptyHtmlWithTitle(_llmModel.Model)); // the welcome page shows the new model
            }
        }

        // Asks before changed settings are applied while a conversation is going on. No undoes the change.
        private bool ConfirmNewSettings(LLMModel current, LLMModel updated)
        {
            int used = current.Client?.CurrentConversation.MessageCount ?? 0;
            if (used <= EmptyContextMessages)
            {
                return true; // no conversation yet, nothing is affected
            }

            var changes = new List<string>();
            if (current.Provider != updated.Provider) changes.Add($"Provider: {current.Provider} -> {updated.Provider}");
            if (current.Model != updated.Model) changes.Add($"Model: {current.Model} -> {updated.Model}");
            if (current.MaxTokens != updated.MaxTokens) changes.Add($"Max Tokens: {current.MaxTokens:N0} -> {updated.MaxTokens:N0}");
            if (current.Temperature != updated.Temperature) changes.Add($"Temp.: {current.Temperature:0.0#} -> {updated.Temperature:0.0#}");
            if (current.ReasoningEffort != updated.ReasoningEffort) changes.Add($"Thinking: {ThinkingText(current.ReasoningEffort)} -> {ThinkingText(updated.ReasoningEffort)}");
            if (current.MaxMessages != updated.MaxMessages) changes.Add($"Max Messages: {current.MaxMessages} -> {updated.MaxMessages}");
            if (changes.Count == 0) changes.Add("Other settings (for example the API key)");

            // the context is rebuilt from the chat: the latest messages that fit the new maximum (see RebuildContext)
            int messages = used - EmptyContextMessages;
            int dropped = Math.Max(0, messages - (updated.MaxMessages - EmptyContextMessages));
            string context = dropped > 0
                ? $"The LLM context holds {messages} messages. With the new maximum of {updated.MaxMessages}, the oldest {dropped} are dropped from it."
                : "The LLM context is kept: it is rebuilt from this chat.";

            return MessageBox.Show(
                $"The settings changed:\n\n  {string.Join("\n  ", changes)}\n\n{context}\nThe chat and the history are not affected.\n\nApply the new settings?\n(No undoes the change.)",
                "Apply Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
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
            btnMindmap.Enabled = enabled;
            btnYoutubeScript.Enabled = enabled;
            btnYoutubeSummary.Enabled = enabled;
            mnuTasks.Enabled = enabled;
            mnuYoutube.Enabled = enabled;
        }

        private void txtPrompt_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtImageUrl.Text))
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    // Updated filter to include common image types and handle both lowercase and uppercase extensions.
                    openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.gif; *.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.JPG;*.JPEG;*.PNG;*.BMP;*.GIF;*.TIFF|All files (*.*)|*.*";
                    openFileDialog.Title = "Select an Image File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtImageUrl.Text = openFileDialog.FileName;
                        txtPrompt.Text = $"Image Attached...\r\n";
                        txtPrompt.Focus();
                        txtPrompt.Select(txtPrompt.Text.Length, 0); // Move cursor to the end of the text
                    }
                }
            }
            else
            {
                // remove the image
                txtImageUrl.Text = string.Empty;
                txtPrompt.Text = txtPrompt.Text.Replace("Image Attached...\r\n", "");
                txtPrompt.Focus();
                txtPrompt.Select(txtPrompt.Text.Length, 0);
            }
        }

        private void txtImageUrl_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtImageUrl.Text))
            {
                btnAddImage.ImageIndex = 1; // delete image icon shown now
                btnAddImage.Text = "Remove Image"; // Update button text
            }
            else
            {
                btnAddImage.ImageIndex = 0; // add image icon shown now
                btnAddImage.Text = "Add Image"; // Reset button text
            }
        }

        // The window opens with the size it had when it was last closed (centered, as designed), kept within the screen.
        private void RestoreWindowSize()
        {
            WindowSettings? saved = WindowSettings.Load();
            if (saved == null || saved.Width <= 0 || saved.Height <= 0)
            {
                return; // first start: the designed size
            }

            Rectangle screen = Screen.FromPoint(Cursor.Position).WorkingArea; // the screen the centered window opens on
            Size = new Size(Math.Min(saved.Width, screen.Width), Math.Min(saved.Height, screen.Height));
            if (saved.Maximized)
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        // A maximized or minimized window stores its normal size, so it is restored to that size next time.
        private void SaveWindowSize()
        {
            Size size = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;
            new WindowSettings { Width = size.Width, Height = size.Height, Maximized = WindowState == FormWindowState.Maximized }.Save();
        }

        // Releases everything the form owns: the running request, the browser, the LLM client, the Python runtime and the temp images.
        private void frmAssistant_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveWindowSize();
            _lifetime.Cancel();
            tmrRequest.Stop();

            if (_webView != null)
            {
                if (_webView.CoreWebView2 != null)
                {
                    _webView.CoreWebView2.WebMessageReceived -= WebView_WebMessageReceived;
                }
                _webView.Dispose();
                _webView = null;
            }

            _llmModel.Client?.Dispose();
            _youtubeFetcher?.Dispose();
            _lifetime.Dispose();
            _boldStatusFont.Dispose();
            ImageHelper.DeleteTempImages();
        }
    }
}