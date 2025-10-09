using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataStructuresAndBigOAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            // RunArrayDemo();
            // RunListDemo();
            // RunStackDemo();
            // RunQueueDemo();
            // RunDictionaryDemo();
            // RunHashSetDemo();
            // RunBenchmarks();
        }

        public static void RunArrayDemo()
        {
            int[] array = new int[10];

            array[0] = 1;
            array[1] = 2;
            array[9] = 3;

            Console.WriteLine(array[2]);

            int search = 6;
            bool found = false;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == search)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Console.WriteLine("Value found!");
            }
            else
            {
                Console.WriteLine("Value not found.");
            }
        }

        public static void RunListDemo()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            numbers.Insert(2, 99);
            numbers.Remove(99);
            Console.WriteLine(numbers.Count);
        }

        public static void RunStackDemo()
        {
            var websites = new Stack<string>();
            websites.Push("google.com");
            websites.Push("youtube.com");
            websites.Push("neocities.com");
            Console.WriteLine($"Current page: {websites.Peek()}");

            Console.Write("Back Navigation: ");
            while (websites.Count > 0)
            {
                Console.WriteLine(websites.Pop());
            }
        }

        public static void RunQueueDemo()
        {
            var printJobs = new Queue<string>();
            printJobs.Enqueue("cat picture");
            printJobs.Enqueue("philosophy essay");
            printJobs.Enqueue("birthday cake recipe");
            Console.WriteLine($"current print job: {printJobs.Peek()}");

            Console.Write("Dequeue Order:");
            while (printJobs.Count > 0)
            {
                Console.WriteLine(printJobs.Dequeue());
            }
        }

        public static void RunDictionaryDemo()
        {
            var SKUs = new Dictionary<string, int>
            {
                {"123", 15 },
                {"5578", 4},
                {"2354345", 2}
            };

            SKUs["123"] = 25;

            Console.WriteLine(SKUs.TryGetValue("missing", out int qty));
        }

        public static void RunHashSetDemo()
        {
            var hashNums = new HashSet<int>();

            hashNums.Add(15);
            hashNums.Add(4000000);
            hashNums.Add(8787);
            hashNums.Add(45);

            bool duplicate = hashNums.Add(15);
            Console.WriteLine($"add a duplicate? {duplicate}");

            var hashNums2 = new HashSet<int> { 3, 4, 5 };
            hashNums.UnionWith(hashNums2);
            Console.WriteLine($"Final count of HashSet: {hashNums.Count}");
        }

        public static void RunBenchmarks()
        {
            int[] Ns = { 1000, 10000, 100000, 250000 };

            foreach (var N in Ns)
            {
                Console.WriteLine($"N = {N}");
                var list = new List<int>();
                var hashSet = new HashSet<int>();
                var dict = new Dictionary<int, bool>();

                for (int i = 0; i < N; i++)
                {
                    list.Add(i);
                    hashSet.Add(i);
                    dict[i] = true;
                }

                // Targets
                int present = N - 1;
                int missing = -1;

                // Stopwatch
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Restart();
                list.Contains(present);
                stopwatch.Stop();
                Console.WriteLine($"List.Contains({present}):    {stopwatch.Elapsed.TotalMilliseconds} ms");

                stopwatch.Restart();
                hashSet.Contains(present);
                stopwatch.Stop();
                Console.WriteLine($"HashSet.Contains:      {stopwatch.Elapsed.TotalMilliseconds} ms");

                stopwatch.Restart();
                dict.ContainsKey(present);
                stopwatch.Stop();
                Console.WriteLine($"Dict.ContainsKey:      {stopwatch.Elapsed.TotalMilliseconds} ms");

                stopwatch.Restart();
                list.Contains(missing);
                stopwatch.Stop();
                Console.WriteLine($"List.Contains({missing}):     {stopwatch.Elapsed.TotalMilliseconds} ms");

                stopwatch.Restart();
                hashSet.Contains(missing);
                stopwatch.Stop();
                Console.WriteLine($"HashSet.Contains({missing}):  {stopwatch.Elapsed.TotalMilliseconds} ms");

                stopwatch.Restart();
                dict.ContainsKey(missing);
                stopwatch.Stop();
                Console.WriteLine($"Dict.ContainsKey({missing}):  {stopwatch.Elapsed.TotalMilliseconds} ms");
            }
        }
    }
}