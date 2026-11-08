namespace AI_Assistant
{
    public static class Constants
    {
        public static class Prompts
        {
            public const string Default = "Default";
            public const string Image = "Image";
            public const string Summarize = "Summarize";
            public const string Translate = "Translate";
            public const string Explain = "Explain";
            public const string KeyPoints = "KeyPoints";
            public const string EmailResponse = "EmailResponse";
            public const string Prompt = "Prompt";
            public const string MindMap = "MindMap";
            public const string YoutubeSummary = "YoutubeSummary";
            public const string Compact = "Compact";
            public const string CompactContext = "CompactContext";
        }

        public static class LLM
        {
            public const string AppSettingsJsonFile = "appsettings.local.json";
            public const string AppSettingsJsonFileExample = "appsettings.example.json";
            public const int DefaultMaxTokens = 4096;
            public const double DefaultTemperature = 0.3;
            public const int DefaultMaxMessages = 25;
            public const int MinMessages = 2;
            public const int MaxMessages = 100;
            public const double MessagesWarningRatio = 0.75; // the used messages turn orange
            public const double MessagesCriticalRatio = 0.9; // and red
            public const string ReasoningDefault = "Default"; // no thinking level in the settings: the provider decides
            public const double MinTemperature = 0.0;
            public const double MaxTemperature = 1.0;
            public const int MinTokens = 1000;
            public const int MaxTokens = 100_000;
        }

        public static class FileSystem
        {
            public const string ImageFolder = "Temp_Images";
            public const string HistoryFolder = "History";
            public const string PromptsFile = "prompts.json";
            public const string WindowFile = "window.json";
        }

        public static class PythonConstants
        {
            // Path to your Python virtual environment folder
            public const string PythonHome = @"C:\Users\mjayy\source\repos\youtub-to-transcript\venv";

            // Path to the python DLL
            public const string PythonDll = @"C:\Python310\python310.dll";

            // Path to the folder containing your Python scripts
            public const string PythonScriptsPath = @"C:\Users\mjayy\source\repos\youtub-to-transcript";

            // Path to the virtual environment site-packages folder
            public const string PythonSitePackagesPath = @"C:\Users\mjayy\source\repos\youtub-to-transcript\venv\Lib\site-packages";
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
            DeepSeek,
            Anthropic
        }
    }
}