using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinaoleLau_ConvertedData
{
    class Validation
    {
        public static int GetInt(string message = "Enter an integer: ")
        {
            int validatedInt;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (!Int32.TryParse(input, out validatedInt));

            return validatedInt;
        }

        public static int GetInt(int min, int max, string message = "Enter an integer: ")
        {
            int validatedInt;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (!(Int32.TryParse(input, out validatedInt) && (validatedInt >= min && validatedInt <= max)));

            return validatedInt;
        }

        public static bool GetBool(string message = "Enter yes or no: ")
        {
            bool answer = false;
            string input = null;

            bool needAValidResponse = true;

            while (needAValidResponse)
            {
                Console.Write(message);
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "yes":
                    case "y":
                    case "true":
                    case "t":
                        {
                            answer = true;
                            needAValidResponse = false;
                        }
                        break;
                    case "no":
                    case "n":
                    case "false":
                    case "f":
                        {
                            needAValidResponse = false;
                        }
                        break;
                }
            }

            return answer;
        }

        public static double GetDouble(string message = "Enter a number: ")
        {
            double validatedDouble;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (!Double.TryParse(input, out validatedDouble));

            return validatedDouble;
        }

        public static double GetDouble(int min, int max, string message = "Enter a number: ")
        {
            double validatedDouble;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (!(Double.TryParse(input, out validatedDouble) && (validatedDouble >= min && validatedDouble <= max)));

            return validatedDouble;
        }

        public static decimal GetDecimal(string message = "Enter a decimal: ")
        {
            decimal validatedDecimal;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (!(Decimal.TryParse(input, out validatedDecimal)) || validatedDecimal < 0);

            return validatedDecimal;
        }

        public static decimal GetDecimal(int min, int max, string message = "Enter a decimal: ")
        {
            decimal validatedDecimal;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (!(Decimal.TryParse(input, out validatedDecimal) && (validatedDecimal >= min && validatedDecimal <= max)));

            return validatedDecimal;
        }

        public static string GetString(string message = "Enter a string: ")
        {
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(input));

            return input;
        }
    }
}

