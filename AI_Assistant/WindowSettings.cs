using System.Text.Json;

namespace AI_Assistant
{
    /// <summary>
    /// The size of the main window when it was closed, used again on the next start.
    /// Stored as JSON next to the exe, like the prompts.
    /// </summary>
    public class WindowSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Maximized { get; set; }

        private static string FilePath => Path.Combine(AppContext.BaseDirectory, Constants.FileSystem.WindowFile);

        // Null when nothing is stored yet or the file cannot be read: the window opens with its designed size then.
        public static WindowSettings? Load()
        {
            try
            {
                return File.Exists(FilePath) ? JsonSerializer.Deserialize<WindowSettings>(File.ReadAllText(FilePath)) : null;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is JsonException)
            {
                return null;
            }
        }

        // Called while the app closes: a size that cannot be stored is not worth an error message.
        public void Save()
        {
            try
            {
                File.WriteAllText(FilePath, JsonSerializer.Serialize(this));
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
            }
        }
    }
}
