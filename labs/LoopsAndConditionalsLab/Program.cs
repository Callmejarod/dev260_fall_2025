namespace LoopsAndConditionalsLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SumEvenNumbersForLoop());
            Console.WriteLine(SumEvenNumberWhileLoop());
            Console.WriteLine(SumEvenNumbersForEachLoop());

            Console.WriteLine($"Your grade is: {GetLetterGradeIfElse(61)}");
            Console.WriteLine($"Your grade is: {GetLetterGradeSwitch(90)}");
        }

        public static int SumEvenNumbersForLoop()
        {
            // for loop example
            int total = 0;

            for (int i = 2; i <= 100; i += 2)
            {
                total += i;
            }

            if (total > 2000)
            {
                Console.WriteLine("That's a big number!");
            }

            string message = (total > 2000) ? "That's a big number!" : "The sum is normal.";
            Console.WriteLine(message);

            return total;

        }

        public static int SumEvenNumberWhileLoop()
        {
            // while loop example
            int total = 0;
            int i = 2;

            while (i <= 100)
            {
                total += i;
                i += 2;
            }

            if (total > 2000)
            {
                Console.WriteLine("That's a big number!");
            }

            string message = (total > 2000) ? "That's a big number!" : "The sum is normal.";
            Console.WriteLine(message);

            return total;

        }

        public static int SumEvenNumbersForEachLoop()
        {
            // for each loop example
            int total = 0;
            List<int> numbers = new List<int>();

            for (int i = 1; i <= 100; i++)
            {
                numbers.Add(i);
            }

            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                {
                    total += number;
                }
            }

            if (total > 2000)
            {
                Console.WriteLine("That's a big number!");
            }

            string message = (total > 2000) ? "That's a big number!" : "The sum is normal.";
            Console.WriteLine(message);

            return total;
        }


        public static string GetLetterGradeIfElse(int score)
        {
            // Using if/else if/else

            if (score >= 90)
            {
                return "A";
            }
            else if (score >= 80)
            {
                return "B";
            }
            else if (score >= 70)
            {
                return "C";
            }
            else if (score >= 60)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }

        public static string GetLetterGradeSwitch(int score)
        {
            // Using a switch expression
            return score switch
            {
                >= 90 and <= 100 => "A",
                >= 80 and <= 89 => "B",
                >= 70 and <= 79 => "C",
                >= 60 and <= 69 => "D",
                < 60 => "F",
            };

        }
    }
}
