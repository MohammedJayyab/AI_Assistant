using Python.Runtime;

namespace AI_Assistant
{
    public class YoutubeTranscriptFetcher : IDisposable
    {
        private dynamic _serviceInstance;
        private bool _isInitialized = false;

        public YoutubeTranscriptFetcher()
        {
            InitializePython();
            using (Py.GIL())
            {
                dynamic transcriptServiceModule = Py.Import("transcript_service");
                _serviceInstance = transcriptServiceModule.TranscriptService();
            }
        }

        private void InitializePython()
        {
            if (!_isInitialized)
            {
                // Set Python DLL path before initializing the engine
                if (Runtime.PythonDLL == null || Runtime.PythonDLL != Constants.PythonConstants.PythonDll)
                    Runtime.PythonDLL = Constants.PythonConstants.PythonDll;

                // Set PYTHONHOME environment variable before initialization
                Environment.SetEnvironmentVariable("PYTHONHOME", Constants.PythonConstants.PythonHome);

                // Initialize Python runtime
                PythonEngine.Initialize();

                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    sys.path.append(Constants.PythonConstants.PythonScriptsPath);
                    sys.path.append(Constants.PythonConstants.PythonSitePackagesPath);
                }

                _isInitialized = true;
            }
        }

        public (string Title, string Transcript) FetchTranscript(string youtubeUrl)
        {
            if (string.IsNullOrWhiteSpace(youtubeUrl))
                throw new ArgumentException("YouTube URL must not be empty", nameof(youtubeUrl));

            using (Py.GIL())
            {
                dynamic result = _serviceInstance.fetch_transcript_text(youtubeUrl);

                // Extract tuple values
                string title = result[1].ToString();
                string transcript = result[0].ToString();

                return (title, transcript);
            }
        }

        public void Dispose()
        {
            if (_isInitialized)
            {
                using (Py.GIL())
                {
                    (_serviceInstance as PyObject)?.Dispose(); // release the Python object before the runtime goes away
                }
                PythonEngine.Shutdown();
                _isInitialized = false;
            }
        }
    }
}