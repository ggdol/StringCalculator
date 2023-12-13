namespace StringCalculatorKata
{
    public class StringCalculator
    {
        private List<string> delimiters = new() {",", "\n"};
        public int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
            {

                return 0;
            }

            IEnumerable<int> parsedNumbers = ParseNumbers(numbers);
            ThrowErrorIfAnyNegativeNumbersAreFound(parsedNumbers);

            return parsedNumbers.Where(x => x<= 1000).Sum();
        }

        private IEnumerable<int> ParseNumbers(string numbers)
        {
            numbers = ExtractCustomDelimiter(numbers);

            var parsedNumbers = SplitNumbers(numbers).Select(int.Parse);
            return parsedNumbers;
        }

        private static void ThrowErrorIfAnyNegativeNumbersAreFound(IEnumerable<int> parsedNumbers)
        {
            var negatives = parsedNumbers.Where(i => i < 0);

            if (negatives.Any())
                throw new Exception("negatives not allowed: " + string.Join(", ", negatives));
        }

        private string ExtractCustomDelimiter(string numbers)
        {
            if (HasCustomDelimiters(numbers))
            {
                var endOfDelimiters = numbers.IndexOf("\n");
                Console.WriteLine("endOfDelimiters: "+ endOfDelimiters);
                var fullDelimiterDefinition = numbers.Substring(2, endOfDelimiters - 2);
                Console.WriteLine("fullDelimiterDefinition: " + fullDelimiterDefinition);
                if (fullDelimiterDefinition.Length > 1) {

                    delimiters.Add(fullDelimiterDefinition.Trim('[', ']'));
                    Console.WriteLine("delimiters.Add: " + fullDelimiterDefinition.Substring(1, fullDelimiterDefinition.Length - 2));
                }
                else
                {

                    delimiters.Add(numbers.Substring(2, 1));
                    Console.WriteLine("delimiters.Add(numbers.Substring(2, 1)): " + numbers.Substring(2, 1));
                }
                numbers = numbers.Substring(endOfDelimiters);
                Console.WriteLine("numbers.Substring(endOfDelimiters): " + numbers.Substring(endOfDelimiters));
            }

            return numbers;
        }

        private static bool HasCustomDelimiters(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private string[] SplitNumbers(string numbers)
        {
            return numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
