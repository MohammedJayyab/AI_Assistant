using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assistant
{
    public static class Htmltemplate
    {
        public static string EmptyHtml = "<html><head></head><body></body></html>";

        public static string EmptyHtmlWithTitle = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>AI Power Assistant</title>
    <style>
        body {
            text-align: center; /* Center text */
            background-color: white; /* White background for the body */
            margin: 0; /* Remove default margin */
            padding: 50px; /* Add some padding to the body */
        }
        .message {
            background-color: #fff3cd; /* Light yellow background for the div */
            color: darkred; /* Red font color */
            font-style: italic; /* Italic text */
            padding: 20px; /* Add padding inside the div */
            border-radius: 10px; /* Rounded corners */
            display: inline-block; /* Make the div fit the content */
        }
    </style>
</head>
<body>
    <div class=""message"">
        Welcome to the AI Power Assistant! Select an action or type your query.
    </div>
</body>
</html>
";
    }
}