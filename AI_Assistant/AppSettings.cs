using System.Globalization;
using LLMKit.Models;
using Microsoft.Extensions.Configuration;

namespace AI_Assistant
{
    public class AppSettings
    {
        public string DefaultProvider { get; set; } = "OpenAI";
        public string DefaultMaxTokens { get; set; } = "1024";
        public string MaxMessages { get; set; } = "30";
        public List<string> AllProviderNames { get; set; } = new List<string>();

        public static AppSettings LoadSettings()
        {
            var appSettingsPath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFile);
            var appSettingsExamplePath = Path.Combine(AppContext.BaseDirectory, Constants.LLM.AppSettingsJsonFileExample);
            if (!File.Exists(appSettingsPath))
            {
                File.Copy(appSettingsExamplePath, appSettingsPath);
            }
            // Load the configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"{Constants.LLM.AppSettingsJsonFile}")
                .Build();

            var llmSection = configuration.GetSection("LLM");

            var excludedKeys = new HashSet<string>
        {
            "DefaultProvider",
            "DefaultTemparature", // old global setting; each provider now has its own Temparature
            "DefaultMaxTokens"
        };

            var defaultProvider = configuration["LLM:DefaultProvider"];

            // Build the list of actual LLM provider names
            var providerNames = new List<string>();
            foreach (var section in llmSection.GetChildren())
            {
                if (!excludedKeys.Contains(section.Key))
                {
                    providerNames.Add(section.Key);
                }
            }

            var settings = new AppSettings
            {
                DefaultProvider = defaultProvider,
                DefaultMaxTokens = configuration["LLM:DefaultMaxTokens"],
                MaxMessages = configuration["Conversation:DefaultMaxMessages"],
                AllProviderNames = providerNames
            };

            return settings;
        }

        public (string ApiKey, string Model, double Temperature, ReasoningEffort? ReasoningEffort) GetProviderDetails(string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                throw new ArgumentException("Provider name must not be empty.", nameof(provider));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"{Constants.LLM.AppSettingsJsonFile}")
                .Build();

            string apiKey = configuration[$"LLM:{provider}:ApiKey"];
            string model = configuration[$"LLM:{provider}:Model"];

            // The provider's own value first, then the old global one, then the built-in default.
            string temperatureText = configuration[$"LLM:{provider}:Temparature"] ?? configuration["LLM:DefaultTemparature"];
            double temperature = double.TryParse(temperatureText, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed)
                ? parsed
                : Constants.LLM.DefaultTemperature;

            // The thinking level of the provider; missing, "Default" or anything unknown means the provider decides (null).
            ReasoningEffort? reasoningEffort = Enum.TryParse(configuration[$"LLM:{provider}:ReasoningEffort"], true, out ReasoningEffort effort) && Enum.IsDefined(effort)
                ? effort
                : null;

            return (apiKey ?? string.Empty, model ?? string.Empty, temperature, reasoningEffort);
        }
    }
}