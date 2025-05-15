using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assistant
{
    public static class SystemMessagesConstants
    {
        public const string prefix = "You are a helpful assistant. response to user request as best as you can as simple plain text no format, only new lines for long paragraphs.";
        public const string DefaultSystemMessage = $"{prefix}";

        // write all system messages as prompts for: 1) Search, 2) summarize, 3) translate, 4) KeyPoints/Important, 5) explain, 6) fix, 7) write response for email, 8) brainstorm , 9) refine text
        public const string SearchSystemMessage = $"{prefix}. Search in the internet-online for the user reques in the provided text.";

        public const string SummarizeSystemMessage = $"{prefix}. Summarize professionally the provided text.";
        public const string TranslateSystemMessage = $"{prefix}. Translate the provided text to the specified language.";
        public const string KeyPointsSystemMessage = $"{prefix}. Extract the key points from the provided text.";
        public const string ExplainSystemMessage = $"{prefix}. Explain the provided text in simple terms.";
        public const string FixSystemMessage = $"{prefix}. Fix the provided text.";
        public const string BrainstormSystemMessage = $"{prefix}. Brainstorm ideas based on the provided text.";
        public const string RefineTextSystemMessage = $"{prefix}. Refine the provided text professionally.";
        public const string EmailResponseSystemMessage = $"{prefix}. Write a professional simple but accurate response for the provided email.";
        public const string PromptSystemMessage = $"{prefix}. Generate a professional well structured prompt, that will be used by LLM AI for the provided text.";
        public const string CodeSystemMessage = $"{prefix}. Write a code for the provided text.";
        public const string ImageSystemMessage = $"{prefix}. Explain the provided image in simple terms.";
    }
}