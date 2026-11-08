namespace AI_Assistant
{
    public static class Htmltemplate
    {
        // Style and script of the buttons shown on every message: Copy, Edit + Send on the last question, and Re-Generate on the last answer.
        // The script posts JSON messages to the host (see frmAssistant.WebView_WebMessageReceived):
        // { action: 'copy', text }, { action: 'resend', text } and { action: 'regenerate', text: '' }.
        public static string MessageSupport = @"<style>
    .msg-actions { text-align: right; margin-top: 4px; }
    .msg-actions button { font-size: 11px; padding: 1px 10px; margin-left: 4px; border: 1px solid #c8d3e6; border-radius: 10px; background: #fff; color: #3367d6; cursor: pointer; }
    .msg-actions button:hover:not(:disabled) { background: #e8f0fe; border-color: #4285f4; }
    .msg-actions button:disabled { color: #aaa; border-color: #ddd; cursor: default; }
    .edit-btn, .send-btn { display: none; }
    .msg.user.editable .edit-btn, .msg.user.editable .send-btn { display: inline-block; }
    .regen-btn { display: none; }
    .msg.assistant.regenerable .regen-btn { display: inline-block; }
    .msg.user.editing .msg-body { display: block; text-align: left; background: #fff; border: 1px solid #4285f4; border-radius: 4px; padding: 6px; white-space: pre-wrap; outline: none; }
</style>
<script>
    function post(action, text) {
        window.chrome.webview.postMessage(JSON.stringify({ action: action, text: text }));
    }

    function copyMsg(btn) {
        var msg = btn.closest('.msg');
        post('copy', msg.hasAttribute('data-copy') ? msg.getAttribute('data-copy') : msg.querySelector('.msg-body').innerText);
        btn.textContent = 'Copied';
        setTimeout(function () { btn.textContent = 'Copy'; }, 1500);
    }

    function normalize(text) {
        return text.replace(/\r\n/g, '\n').trim();
    }

    // Edit switches the question into an editor; the same button then cancels it.
    function editMsg(btn) {
        var msg = btn.closest('.msg');
        var body = msg.querySelector('.msg-body');
        var send = msg.querySelector('.send-btn');
        if (!msg.classList.contains('editing')) {
            msg.shownHtml = body.innerHTML;
            msg.classList.add('editing');
            body.innerText = msg.getAttribute('data-copy');
            body.contentEditable = 'plaintext-only';
            body.oninput = function () {
                var text = normalize(body.innerText);
                send.disabled = text === '' || text === normalize(msg.getAttribute('data-copy'));
            };
            body.focus();
            btn.textContent = 'Cancel';
        } else {
            body.contentEditable = 'false';
            body.innerHTML = msg.shownHtml;
            msg.classList.remove('editing');
            send.disabled = true;
            btn.textContent = 'Edit';
        }
    }

    function sendEdit(btn) {
        var msg = btn.closest('.msg');
        post('resend', normalize(msg.querySelector('.msg-body').innerText));
    }

    function regenerateMsg(btn) {
        post('regenerate', '');
    }

    // Only the last answer can be regenerated, while it is the last message of the chat.
    document.addEventListener('DOMContentLoaded', function () {
        var all = document.querySelectorAll('.msg');
        if (all.length > 0 && all[all.length - 1].classList.contains('assistant')) all[all.length - 1].classList.add('regenerable');
    });

    // Only the last question can be edited, and not once a summary has replaced it in the conversation.
    document.addEventListener('DOMContentLoaded', function () {
        var users = document.querySelectorAll('.msg.user');
        if (users.length === 0) return;
        var last = users[users.length - 1];
        var summaries = document.querySelectorAll('.msg.compact');
        if (summaries.length > 0 && (last.compareDocumentPosition(summaries[summaries.length - 1]) & Node.DOCUMENT_POSITION_FOLLOWING)) return;
        last.classList.add('editable');
    });
</script>
";

        public static string MessageActions(bool isUserMessage, bool isAnswer = false)
        {
            string buttons = @"<button class=""copy-btn"" onclick=""copyMsg(this)"">Copy</button>";
            if (isUserMessage)
            {
                buttons += @"<button class=""edit-btn"" onclick=""editMsg(this)"">Edit</button>" +
                           @"<button class=""send-btn"" onclick=""sendEdit(this)"" disabled>Send</button>";
            }
            if (isAnswer)
            {
                buttons += @"<button class=""regen-btn"" onclick=""regenerateMsg(this)"" title=""Ask the same question again: the new answer replaces this one"">Re-Generate</button>";
            }
            return $@"<div class=""msg-actions"">{buttons}</div>";
        }

        // The page of a new chat, with the model in use on its own line.
        public static string EmptyHtmlWithTitle(string model) => EmptyHtmlTemplate.Replace("{model}", System.Net.WebUtility.HtmlEncode(model));

        private const string EmptyHtmlTemplate = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>AI Power Assistant</title>
    <style>
        body {
            text-align: center; /* Center text */
            background-color: white; /* White background for the body */
            margin: 0; /* Remove default margin */
            padding: 50px; /* Add some padding to the body */
        }
        .message {
            background-color: #fff3cd; /* Light yellow background for the div */
            color: darkred; /* Red font color */
            font-style: italic; /* Italic text */
            padding: 20px; /* Add padding inside the div */
            border-radius: 10px; /* Rounded corners */
            display: inline-block; /* Make the div fit the content */
        }
        .model {
            color: darkblue;
            font-size: 1.5em; /* Larger than the welcome text */
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <div class=""message"">
        Welcome to the AI Power Assistant! Select an action or type your query.
        <div class=""model"">{model}</div>
    </div>
</body>
</html>
";
    }
}