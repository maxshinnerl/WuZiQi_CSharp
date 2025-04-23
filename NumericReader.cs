// NumericReader.cs

namespace Wuziqi
{
    public static class InputHelper
    {
        public static int GetNumericInput(string message)
        {
            Console.WriteLine(message);
            string? userInput = Console.ReadLine();
            int result;

            while (!int.TryParse(userInput, out result))
            {
                Console.WriteLine("Not a numeric input, please try again");
                userInput = Console.ReadLine();
            }

            return result;
        }

        public static int GetNumericInputBounded(string message, int lower, int upper)
        {
            
            if ((lower > upper) || (upper >= int.MaxValue) || lower <= int.MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(lower), nameof(upper), "Invalid bounds!");
            }

            int result = GetNumericInput(message);

            // while result is not between upper and lower, 
            while ((result < lower) || (result > upper))
            {
                Console.WriteLine($"Coordinate must be between {lower} and {upper}, please try again");
                result = GetNumericInput(message);
            }

            return result;
        }
    }
}