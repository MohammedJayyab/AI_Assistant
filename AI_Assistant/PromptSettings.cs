using System.Text.Json;

namespace AI_Assistant
{
    /// <summary>
    /// A single named prompt: the task instruction appended to the base system prompt,
    /// and the template used to wrap the user's text (placeholders: {text}, {language}, {title}).
    /// </summary>
    public class PromptEntry
    {
        public string Description { get; set; } = string.Empty;
        public string Instruction { get; set; } = string.Empty;
        public string UserTemplate { get; set; } = string.Empty;
    }

    public class PromptSettings
    {
        public string BaseSystemPrompt { get; set; } = string.Empty;
        public Dictionary<string, PromptEntry> Prompts { get; set; } = new();

        // Deep copy, so the editor can be cancelled without touching the live prompts.
        public PromptSettings Clone()
        {
            return JsonSerializer.Deserialize<PromptSettings>(JsonSerializer.Serialize(this))!;
        }

        public static PromptSettings CreateDefault()
        {
            return new PromptSettings
            {
                BaseSystemPrompt = "You are a helpful assistant. Respond to the user's request as best you can, as simple HTML, and reduce empty spaces. " +
                                   "Highlight important keywords and terms. When the user asks for source code, format it nicely including the tags for the requested language. " +
                                   "Use a suitable font name. Consider the text alignment based on the text language (English or Arabic), but keep the terms and keywords in English where possible.",
                Prompts = new Dictionary<string, PromptEntry>
                {
                    [Constants.Prompts.Default] = new PromptEntry
                    {
                        Description = "Standard chat. Used for every plain message."
                    },
                    [Constants.Prompts.Image] = new PromptEntry
                    {
                        Description = "Used when an image is attached to the message.",
                        Instruction = "Please explain the attached image shortly. If it has text, also write its content. Highlight important/head words."
                    },
                    [Constants.Prompts.Summarize] = new PromptEntry
                    {
                        Description = "Summarize the text.",
                        Instruction = "Summarize professionally the provided text.",
                        UserTemplate = "Summarize the following text: \n\n{text}"
                    },
                    [Constants.Prompts.Translate] = new PromptEntry
                    {
                        Description = "Translate the text to the selected language ({language}).",
                        Instruction = "Translate the provided text to the specified language. For Arabic please maintain the text direction.",
                        UserTemplate = "Translate the following text to {language}: \n\n{text}"
                    },
                    [Constants.Prompts.Explain] = new PromptEntry
                    {
                        Description = "Explain the text in simple terms.",
                        Instruction = "Explain the provided text in simple terms.",
                        UserTemplate = "Explain the following text: \n\n{text}"
                    },
                    [Constants.Prompts.KeyPoints] = new PromptEntry
                    {
                        Description = "Extract the key points of the text.",
                        Instruction = "Extract the key points from the provided text.",
                        UserTemplate = "Extract the key points from the following text: \n\n{text}"
                    },
                    [Constants.Prompts.EmailResponse] = new PromptEntry
                    {
                        Description = "Write a reply to the email text.",
                        Instruction = "Write a professional, simple but accurate response for the provided email.",
                        UserTemplate = "Write a professional response for the following email: \n\n{text}"
                    },
                    [Constants.Prompts.Prompt] = new PromptEntry
                    {
                        Description = "Generate an LLM prompt from the text.",
                        Instruction = "Generate a professional, well structured prompt that will be used by an LLM AI for the provided text.",
                        UserTemplate = "Write a prompt for the following text: \n\n{text}"
                    },
                    [Constants.Prompts.MindMap] = new PromptEntry
                    {
                        Description = "Generate a text mind map of the content.",
                        Instruction = "You are an expert mind map creator. Read the provided content carefully and generate a text, tree-structured mind map. Focus only on key concepts, using very abstract terms. Bold the most important keywords. No explanations. Output only the structured mind map.",
                        UserTemplate = "The content is: \n\n{text}"
                    },
                    [Constants.Prompts.YoutubeSummary] = new PromptEntry
                    {
                        Description = "Summarize a YouTube transcript ({title} is the video title).",
                        Instruction = "Extract the key points from the provided text.",
                        UserTemplate = "Extract the key points, but show 'Title: {title}' from the following youtube transcript: \n\n {text}"
                    },
                    [Constants.Prompts.Compact] = new PromptEntry
                    {
                        Description = "Compact the conversation: the LLM summarizes everything so far, and the summary replaces the old messages sent to the LLM.",
                        Instruction = "You are an expert at condensing conversations. Summarize the whole conversation so far into a compact summary that keeps everything needed to continue it: the user's goals, key facts, decisions, constraints, preferences, names, numbers, important code or snippets, open questions and the latest state. Drop greetings, repetition and filler. If the conversation starts from an earlier summary, keep its content in the new summary. Write the summary in the main language of the conversation. Output only the summary.",
                        UserTemplate = "Compact our conversation so far into a summary that I can continue from."
                    },
                    [Constants.Prompts.CompactContext] = new PromptEntry
                    {
                        Description = "How the summary goes back to the LLM after compacting: the user template is added to the system message ({text} is the summary), so the new context starts with that one message. The System instruction is not used.",
                        Instruction = string.Empty,
                        UserTemplate = "This is a summary of our conversation so far. Use it as the context for the rest of our conversation:\n\n{text}"
                    }
                }
            };
        }
    }

    /// <summary>
    /// Loads and saves the prompts file. The file is re-read automatically
    /// whenever it changes on disk, so edits never need an application restart.
    /// </summary>
    public static class PromptStore
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
        private static PromptSettings? _settings;
        private static DateTime _loadedWriteTime;

        public static string FilePath => Path.Combine(AppContext.BaseDirectory, Constants.FileSystem.PromptsFile);

        public static PromptSettings Settings
        {
            get
            {
                Reload();
                return _settings!;
            }
        }

        public static string GetSystemMessage(string key)
        {
            var settings = Settings;
            string instruction = settings.Prompts[key].Instruction.Trim();
            string basePrompt = settings.BaseSystemPrompt.Trim();
            return instruction.Length == 0 ? basePrompt : $"{basePrompt} {instruction}";
        }

        public static string BuildUserPrompt(string key, string text, string language = "", string title = "")
        {
            string template = Settings.Prompts[key].UserTemplate;
            if (!template.Contains("{text}"))
            {
                // Never lose the user's text because a template has no placeholder.
                template = $"{template}\n\n{{text}}";
            }
            return template.Replace("{language}", language).Replace("{title}", title).Replace("{text}", text);
        }

        public static void Save(PromptSettings settings)
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(settings, JsonOptions));
            _settings = settings;
            _loadedWriteTime = File.GetLastWriteTimeUtc(FilePath);
        }

        private static void Reload()
        {
            if (!File.Exists(FilePath))
            {
                Save(PromptSettings.CreateDefault());
                return;
            }

            DateTime writeTime = File.GetLastWriteTimeUtc(FilePath);
            if (_settings != null && writeTime == _loadedWriteTime)
            {
                return;
            }
            _loadedWriteTime = writeTime;

            try
            {
                var loaded = JsonSerializer.Deserialize<PromptSettings>(File.ReadAllText(FilePath)) ?? throw new JsonException("The file is empty.");
                // Fill in anything the user removed so every prompt always resolves.
                var defaults = PromptSettings.CreateDefault();
                if (string.IsNullOrWhiteSpace(loaded.BaseSystemPrompt))
                {
                    loaded.BaseSystemPrompt = defaults.BaseSystemPrompt;
                }
                foreach (var (key, entry) in defaults.Prompts)
                {
                    loaded.Prompts.TryAdd(key, entry);
                }
                _settings = loaded;
            }
            catch (Exception ex)
            {
                _settings ??= PromptSettings.CreateDefault();
                MessageBox.Show($"'{Constants.FileSystem.PromptsFile}' could not be read, the previous prompts are used instead.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
