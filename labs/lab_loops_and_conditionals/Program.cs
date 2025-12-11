using System.Configuration.Assemblies;
using System.Collections.Generic;

namespace LoopsAndConditionalsLab
{
    public class Program
    {
        static void Main(string[] args)
        {
            SumEvenNums();
            GetLetterGrade(60); //D
        }

        public static void SumEvenNums()
        {
            int sum = 0;

            //for loop
            for (int i = 1; i <= 100; i++)
            {
                if (i % 2 == 0)
                {
                    sum += i;
                }
            }
            Console.WriteLine($"For loop sum: {sum}");

            //resetting the sum
            sum = 0;

            //while loop
            int count = 0;
            while (count <= 100)
            {
                if (count % 2 == 0)
                {
                    sum += count;
                }
                count++;
            }
            Console.WriteLine($"While loop sum: {sum}");

            //resetting the sum
            sum = 0;

            //initializing a list to store numbers 1-100
            List<int> numbers = new List<int>() { };
            for (int i = 1; i <= 100; i++)
            {
                numbers.Add(i);
            }

            //foreach loop
            foreach (int num in numbers)
            {
                if (num % 2 == 0)
                {
                    sum += num;
                }
            }
            Console.WriteLine($"Foreach loop sum: {sum}");

            //mini challenge!
            //if/else
            if (sum >= 2000)
            {
                Console.WriteLine("That's a big number! (if/else)");
            }

            //ternary
            string greaterThan2000;
            greaterThan2000 = sum >= 2000 ? "That's a big number! (ternary)" : "";
            Console.WriteLine(greaterThan2000);
        }

        public static void GetLetterGrade(int score)
        {
            //if else
            if (score < 60)
            {
                Console.WriteLine("If/Else Grade: F");
            }
            else if (score < 70)
            {
                Console.WriteLine("If/Else Grade: D");
            }
            else if (score < 80)
            {
                Console.WriteLine("If/Else Grade: C");
            }
            else if (score < 90)
            {
                Console.WriteLine("If/Else Grade: B");
            }
            else
            {
                Console.WriteLine("If/Else Grade: A");
            }

            //switch expression
            string letterGrade = score switch
            {
                < 60 => "Switch Grade: F",
                < 70 => "Switch Grade: D",
                < 80 => "Switch Grade: C",
                < 90 => "Switch Grade: B",
                _ => "Switch Grade: A"
            };
            Console.WriteLine(letterGrade);
        }
    }
}
