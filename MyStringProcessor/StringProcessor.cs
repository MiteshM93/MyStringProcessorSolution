namespace MyStringProcessor
{
    public static class StringProcessor
    {
        public static IEnumerable<string> ProcessStrings(IEnumerable<string> inputStrings)
        {
            if (inputStrings == null) return new List<string>();

            var processedStrings = new List<string>();

            foreach (var str in inputStrings)
            {
                if (string.IsNullOrWhiteSpace(str)) continue;

                var truncatedStr = new string(
                    str.Take(15)
                       .Where(c => c != '_' && c != '4')
                       .ToArray()
                );

                var transformedStr = new StringBuilder();
                char? lastChar = null;

                foreach (var ch in truncatedStr)
                {
                    var currentChar = ch == '$' ? '£' : ch;

                    if (lastChar != currentChar)
                    {
                        transformedStr.Append(currentChar);
                        lastChar = currentChar;
                    }
                }

                if (transformedStr.Length > 0)
                {
                    processedStrings.Add(transformedStr.ToString());
                }
            }

            return processedStrings;
        }
    }
}
