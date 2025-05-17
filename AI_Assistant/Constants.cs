namespace AI_Assistant
{
    public static class Constants
    {
        public static class SystemMessages
        {
            private const string BasePrefix = @"You are a helpful assistant. response to user request as best as you can as simple html and reduce empty spaces.
                            highlight important keywords and Terms, formatted pretty source code including tags for requested language, when user asked for source code,
                                    and use suitable font name.";

            public const string Default = BasePrefix;
            public const string Search = $"{BasePrefix}. Search in the internet-online for the user reques in the provided text.";
            public const string Summarize = $"{BasePrefix}. Summarize professionally the provided text.";
            public const string Translate = $"{BasePrefix}. Translate the provided text to the specified language.";
            public const string KeyPoints = $"{BasePrefix}. Extract the key points from the provided text.";
            public const string Explain = $"{BasePrefix}. Explain the provided text in simple terms.";
            public const string Fix = $"{BasePrefix}. Fix the provided text.";
            public const string Brainstorm = $"{BasePrefix}. Brainstorm ideas based on the provided text.";
            public const string RefineText = $"{BasePrefix}. Refine the provided text professionally.";
            public const string EmailResponse = $"{BasePrefix}. Write a professional simple but accurate response for the provided email.";
            public const string Prompt = $"{BasePrefix}. Generate a professional well structured prompt, that will be used by LLM AI for the provided text.";
            public const string Code = $"{BasePrefix}. Write a code for the provided text.";
            public const string Image = $"{BasePrefix}. Please explain the attached image shortly, If it has text, also write its content. Highlight important/head words";
        }

        public static class LLM
        {
            public const string AppSettingsJsonFile = "appsettings.local.json";
            public const string AppSettingsJsonFileExample = "appsettings.example.json";
            public const int DefaultMaxTokens = 4096;
            public const double DefaultTemperature = 0.3;
            public const int DefaultMaxMessages = 25;
            public const int MinMessages = 2;
            public const int MaxMessages = 50;
            public const double MinTemperature = 0.0;
            public const double MaxTemperature = 1.0;
            public const int MinTokens = 1000;
            public const int MaxTokens = 100_000;
        }

        public static class FileSystem
        {
            public const string ImageFolder = "Temp_Images";
        }

        public enum Language
        {
            Arabic,
            English,
            German
        }

        public enum Providers
        {
            OpenAI,
            Gemini,
            DeepSeek
        }
    }
}