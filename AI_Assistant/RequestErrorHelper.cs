using System.Net;
using System.Text.RegularExpressions;

namespace AI_Assistant
{
    /// <summary>
    /// Turns a failed LLM request into a short reason and what the user can do about it.
    /// LLMKit wraps the original error, so the status code and the provider's response are read from the inner exceptions.
    /// </summary>
    public static class RequestErrorHelper
    {
        public static (string Reason, string Advice) Explain(Exception ex, LLMModel model)
        {
            var chain = new List<Exception>();
            for (Exception? e = ex; e != null; e = e.InnerException)
            {
                chain.Add(e);
            }
            string details = string.Join(" ", chain.Select(e => e.Message)).ToLowerInvariant();

            if (chain.Any(e => e is TimeoutException) || chain.Any(e => e is TaskCanceledException))
            {
                return ("No response in time",
                    $"{model.Provider} did not answer within the time limit. Long thinking and long answers take the most time: try a lower Thinking level, a lower Max Tokens or a shorter text.");
            }

            HttpStatusCode? status = GetStatusCode(chain);
            switch ((int?)status)
            {
                case 401:
                case 403:
                    return ("API key rejected",
                        $"The API key of {model.Provider} is wrong, expired, or has no access to the model '{model.Model}'. Check the key in the settings.");
                case 404:
                    return ("Model not found",
                        $"{model.Provider} does not know the model '{model.Model}', or your key cannot use it. Check the model name in the settings.");
                case 402:
                case 429:
                    return details.Contains("quota") || details.Contains("billing") || details.Contains("credit") || details.Contains("balance") || (int?)status == 402
                        ? ("Quota or credit used up", $"Your {model.Provider} account has no quota or credit left. Check your plan and billing with {model.Provider}, or choose another provider in the settings.")
                        : ("Too many requests", $"{model.Provider} limits how many requests you can send. Wait a minute and try again.");
                case >= 500:
                    return ("Provider server error",
                        $"{model.Provider} has a problem on its side right now (LLMKit already retried). Try again in a few minutes, or choose another provider in the settings.");
                case 400:
                case 413:
                case 422:
                    return ExplainRejectedRequest(details, model);
            }

            if (chain.Any(e => e is HttpRequestException))
            {
                return ("Connection failed",
                    $"{model.Provider} could not be reached. Check your internet connection, VPN or proxy, then try again.");
            }

            if (chain.Any(e => e is FileNotFoundException || e is DirectoryNotFoundException))
            {
                return ("Image not found",
                    "The attached image was moved or deleted before it was sent. Attach it again, then send.");
            }

            return ("Unexpected error", "See the details below. If it happens again, start a new chat or restart the app.");
        }

        // A 400 names the rejected value in the provider's response: point to the setting that causes it.
        private static (string Reason, string Advice) ExplainRejectedRequest(string details, LLMModel model)
        {
            if (Regex.IsMatch(details, "context.?length|context window|too many tokens|prompt is too long|too long|input token"))
            {
                return ("Conversation too long",
                    $"The conversation and the new text are more than the model '{model.Model}' can read. Compact the conversation, start a new chat, lower Max Messages or Max Tokens, or send a shorter text.");
            }
            if (Regex.IsMatch(details, "max_tokens|max_completion_tokens|maxoutputtokens|max_output_tokens|budget_tokens"))
            {
                return ("Max Tokens not accepted",
                    $"The model '{model.Model}' does not accept Max Tokens {model.MaxTokens:N0}. Lower Max Tokens in the settings (most models allow 4,096 to 32,000; reasoning models often more).");
            }
            if (Regex.IsMatch(details, "reasoning|thinking|effort"))
            {
                return ("Thinking level not accepted",
                    $"The model '{model.Model}' does not accept the thinking level '{model.ReasoningEffort?.ToString() ?? Constants.LLM.ReasoningDefault}'. Choose another level, or Default, in the settings.");
            }
            if (Regex.IsMatch(details, "temperature|top_p"))
            {
                return ("Temperature not accepted",
                    $"The model '{model.Model}' does not accept the temperature {model.Temperature:0.0#}. Set another temperature, or a thinking level of None, in the settings.");
            }
            if (Regex.IsMatch(details, "image|vision|multimodal"))
            {
                return ("Image not accepted",
                    $"The model '{model.Model}' does not accept this image. Use a JPG or PNG image, or choose a model that reads images.");
            }
            if (Regex.IsMatch(details, "model"))
            {
                return ("Model not accepted",
                    $"{model.Provider} does not accept the model '{model.Model}' for this request. Check the model name in the settings.");
            }
            if (Regex.IsMatch(details, "api.?key"))
            {
                return ("API key rejected",
                    $"The API key of {model.Provider} is not valid. Check the key in the settings.");
            }
            return ("Request rejected",
                $"{model.Provider} rejected the request. The details below name the cause; change that setting and try again.");
        }

        private static HttpStatusCode? GetStatusCode(List<Exception> chain)
        {
            HttpStatusCode? status = chain.OfType<HttpRequestException>().Select(e => e.StatusCode).FirstOrDefault(code => code != null);
            if (status != null)
            {
                return status;
            }

            // Gemini puts the status name in the message: "Error response from Gemini API: BadRequest - {...}"
            foreach (Exception e in chain)
            {
                Match match = Regex.Match(e.Message, @"API: (\w+) - ");
                if (match.Success && Enum.TryParse(match.Groups[1].Value, out HttpStatusCode code))
                {
                    return code;
                }
            }
            return null;
        }
    }
}
