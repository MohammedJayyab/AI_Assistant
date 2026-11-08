using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AI_Assistant
{
    public class ChatEntry
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;
    }

    /// <summary>
    /// One chat session: everything exchanged between two "New Chat" actions.
    /// Stored as a single JSON file named after the session start time.
    /// </summary>
    public class ChatSession
    {
        private const int MaxTitleLength = 50;

        public DateTime StartedAt { get; set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public List<ChatEntry> Entries { get; set; } = new();

        [JsonIgnore]
        public string FilePath => Path.Combine(ChatHistory.FolderPath, $"{StartedAt:yyyy-MM-dd_HH-mm-ss}.json");

        public void Add(string text, string titleSource = "")
        {
            Entries.Add(new ChatEntry { Text = text });

            if (string.IsNullOrEmpty(Title) && !string.IsNullOrWhiteSpace(titleSource))
            {
                string title = Regex.Replace(titleSource, @"\s+", " ").Trim();
                Title = title.Length > MaxTitleLength ? title.Substring(0, MaxTitleLength) + "..." : title;
            }
        }
    }

    public static class ChatHistory
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

        public static string FolderPath
        {
            get
            {
                string path = Path.Combine(AppContext.BaseDirectory, Constants.FileSystem.HistoryFolder);
                Directory.CreateDirectory(path);
                return path;
            }
        }

        public static void Save(ChatSession session)
        {
            File.WriteAllText(session.FilePath, JsonSerializer.Serialize(session, JsonOptions));
        }

        /// <summary>
        /// Returns the most recent sessions, newest first. Unreadable files are skipped.
        /// </summary>
        public static List<ChatSession> GetRecent(int count)
        {
            var sessions = new List<ChatSession>();
            var files = Directory.GetFiles(FolderPath, "*.json").OrderByDescending(Path.GetFileName);

            foreach (string file in files)
            {
                try
                {
                    var session = JsonSerializer.Deserialize<ChatSession>(File.ReadAllText(file));
                    if (session != null)
                    {
                        sessions.Add(session);
                    }
                }
                catch (JsonException)
                {
                    continue;
                }

                if (sessions.Count == count)
                {
                    break;
                }
            }

            return sessions;
        }
    }
}
