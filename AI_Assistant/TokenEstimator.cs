namespace AI_Assistant
{
    /// <summary>
    /// Rough token count, used while a request runs and when the provider reports no usage:
    /// about 4 characters per token for latin text, about 2 for other scripts such as Arabic.
    /// </summary>
    public static class TokenEstimator
    {
        public static int Estimate(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            int latin = text.Count(c => c < 128);
            int other = text.Length - latin;
            return (int)Math.Ceiling(latin / 4.0 + other / 2.0);
        }
    }
}
