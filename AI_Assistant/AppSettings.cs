using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace AI_Assistant
{
    public class AppSettings
    {
        public string DefaultProvider { get; set; } = "OpenAI";
        public string DefaultTemparature { get; set; } = "0";
        public string DefaultMaxTokens { get; set; } = "1024";
        public string MaxMessages { get; set; } = "30";
        public string ProviderApiKey { get; set; } = string.Empty;
        public string ProviderModel { get; set; } = "OpenAI";
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
            "DefaultTemparature",
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
                DefaultTemparature = configuration["LLM:DefaultTemparature"],
                DefaultMaxTokens = configuration["LLM:DefaultMaxTokens"],
                MaxMessages = configuration["Conversation:MaxMessages"],
                ProviderApiKey = configuration[$"LLM:{defaultProvider}:ApiKey"],
                ProviderModel = configuration[$"LLM:{defaultProvider}:Model"],
                AllProviderNames = providerNames
            };

            return settings;
        }

        public (string ApiKey, string Model) GetProviderDetails(string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                throw new ArgumentException("Provider name must not be empty.", nameof(provider));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"{Constants.LLM.AppSettingsJsonFile}")
                .Build();

            string apiKey = configuration[$"LLM:{provider}:ApiKey"];
            string model = configuration[$"LLM:{provider}:Model"];

            return (apiKey ?? string.Empty, model ?? string.Empty);
        }
    }
}