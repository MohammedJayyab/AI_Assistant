# AI Assistant

A powerful Windows Forms application that provides a user-friendly interface for interacting with various AI models. The application supports multiple AI providers, image analysis, and specialized AI tasks.

## Features

- 🤖 **Multiple AI Providers**
  - OpenAI
  - DeepSeek
  - Gemini
  - Configurable through settings

- 🎯 **Specialized AI Tasks**
  - Standard chat
  - Text summarization
  - Translation (Arabic, English, German)
  - Text explanation
  - Key points extraction
  - Web search
  - Email response generation
  - Image analysis
  - Prompt generation
  - Code generation

- 🖼️ **Image Analysis**
  - Clipboard image capture
  - Image file upload
  - AI-powered image explanation
  - Code challenge solutions from images

- 💻 **Modern UI**
  - Clean, intuitive interface
  - HTML-formatted responses
  - Real-time interaction
  - Adjustable context window

## Prerequisites

- Windows OS
- .NET 8.0 SDK
- Visual Studio 2022 or later
- WebView2 Runtime

## Installation

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Open the solution in Visual Studio:
```bash
cd AI_Assistant
```

3. Configure your AI provider settings:
   - Copy `appsettings.example.json` to `appsettings.Local.json`
   - Update the API keys and model settings in `appsettings.Local.json`

4. Build and run the application:
```bash
dotnet build
dotnet run
```

## Configuration

The application uses JSON configuration files for settings:

- `appsettings.example.json`: Template configuration file
- `appsettings.Local.json`: Your local configuration (not tracked in source control)

Example configuration:
```json
{
  "OpenAI": {
    "ApiKey": "YOUR-OpenAI-KEY",
    "Model": "gpt-3.5-turbo"
  },
  "DeepSeek": {
    "ApiKey": "YOUR-DeepSeek-KEY",
    "Model": "deepseek-chat"
  },
  "Gemini": {
    "ApiKey": "YOUR-Gemini-KEY",
    "Model": "gemini-2.0-flash"
  }
}
```

## Usage

1. **Standard Chat**
   - Type your message in the input box
   - Press Enter or click "Send"

2. **Specialized Tasks**
   - Select the appropriate task button
   - Enter your text
   - Get AI-powered response

3. **Image Analysis**
   - Use "Browse" to select an image file
   - Or use "P>>" to capture from clipboard
   - Click "Explain Img" for AI analysis

4. **Language Selection**
   - Choose between Arabic (Ar), English (En), or German (De)
   - Default is Arabic

5. **Context Management**
   - Use "New Chat" to start fresh
   - Adjust context window size with "Update Size"

## Project Structure

```
AI_Assistant/
├── frmAssistant.cs              # Main form implementation
├── frmAssistant.Designer.cs     # Form design
├── ImageHelper.cs               # Image handling utilities
├── Program.cs                   # Application entry point
├── SystemMessagesConstants.cs   # AI system prompts
├── appsettings.example.json     # Configuration template
└── AI_Assistant.csproj         # Project file
```

## Dependencies

- Microsoft.Web.WebView2 (v1.0.3240.44)
- Microsoft.Extensions.Configuration.Json (v9.0.5)
- Custom LLMKit library

## Development

### Adding New Features

1. **New AI Task**
   - Add system message in `SystemMessagesConstants.cs`
   - Create button in form designer
   - Implement handler in `frmAssistant.cs`

2. **New AI Provider**
   - Add configuration in `appsettings.json`
   - Implement provider in LLMKit library
   - Update provider selection logic

### Best Practices

- Follow SOLID principles
- Maintain clean code structure
- Use proper error handling
- Keep UI responsive
- Document new features

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

[Your License Here]

## Support

For support, please [contact information or issue tracker link] 