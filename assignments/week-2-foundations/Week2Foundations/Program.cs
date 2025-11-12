using System;
using System.Diagnostics;

namespace Week2Foundations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // RunArrayDemo();
            // RunListDemo();
            // RunStackDemo();
            // RunQueueDemo();
            // RunDictionaryDemo();
            // RunHashSetDemo();
            RunBenchmark(1_000_000);

        }

        public static void RunArrayDemo()
        {
            int[] numbers = new int[10];

            // assign 3 values
            numbers[2] = 20;
            numbers[5] = 50;
            numbers[9] = 90;

            // print index 2
            Console.WriteLine($"Index 2: {numbers[2]}");

            // linearly search for a value
            int target = 90;
            bool found = false;

            foreach (int num in numbers)
            {
                if (num == target)
                {
                    found = true;
                    break;
                }
            }

            Console.WriteLine(found
                ? $"The search for {target} is found!"
                : $"The search for {target} was not found.");
        }

        public static void RunListDemo()
        {
            List<int> numbers = new List<int>();

            // Add items
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
            numbers.Add(4);
            numbers.Add(5);

            // insert 99 at index 2 and remove
            numbers.Insert(2, 99);
            numbers.Remove(99);

            // print final count
            Console.WriteLine($"Amount in list: {numbers.Count}");

        }

        public static void RunStackDemo()
        {
            Stack<string> urls = new Stack<string>();

            // push three page URLs
            urls.Push("https://www.google.com/");
            urls.Push("https://www.contoso.com/");
            urls.Push("https://learn.microsoft.com/");

            // peek the current page
            Console.WriteLine($"Current Webpage: {urls.Peek()}");

            // simulate “Back” navigation; print order visited
            while (urls.Count > 0)
            {
                string previousPage = urls.Pop();
                Console.WriteLine($"Previous Page: {previousPage}");
            }

            Console.WriteLine("No more pages to backtrack.");

        }

        public static void RunQueueDemo()
        {
            Queue<string> printJobs = new Queue<string>();

            // Enqueue three "print jobs"
            printJobs.Enqueue("Job1");
            printJobs.Enqueue("Job2");
            printJobs.Enqueue("Job3");

            // Peek the next
            Console.WriteLine($"{printJobs.Peek()} is next in line.");

            // Dequeue all; print processing order
            Console.WriteLine("Processing order:");
            while (printJobs.Count > 0)
            {
                string currentJob = printJobs.Dequeue();
                Console.WriteLine($"Processing {currentJob}");
            }

            Console.WriteLine("All jobs processed.");
        }

        public static void RunDictionaryDemo()
        {
            Dictionary<string, int> itemQuantity = new Dictionary<string, int>();

            // Map three SKUs to quantities
            itemQuantity.Add("SKU-117", 30);
            itemQuantity.Add("SKU-137", 10);
            itemQuantity.Add("SKU-181", 50);

            // Update one quantity
            itemQuantity["SKU-117"] = 25;

            // Show TryGetValue("missing") result
            bool found = itemQuantity.TryGetValue("SKU-999", out int quantity);

            Console.WriteLine($"Was SKU-999 found? {found}");
            Console.WriteLine($"Quantity: {quantity}");
        }

        public static void RunHashSetDemo()
        {
            HashSet<int> numbers = new HashSet<int>();

            // Add a few integers with duplicates
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);

            // show whether adding a duplicate returns false
            Console.WriteLine(string.Join(",", numbers));
            bool addedThreeFirst = numbers.Add(4);      // true
            bool addedThreeDuplicate = numbers.Add(4);  // false

            Console.WriteLine($"Adding first 4 succeeded? {addedThreeFirst}");
            Console.WriteLine($"Adding duplicate 4 succeeded? {addedThreeDuplicate}");

            // Perform a UnionWith on {3,4,5} and print final Count
            HashSet<int> numbers2 = new HashSet<int> { 3, 4, 5 };

            numbers.UnionWith(numbers2);
            Console.WriteLine(string.Join(",", numbers));
            Console.WriteLine($"Final Count: {numbers.Count}");
        }

        public static void RunBenchmark(int N)
        {
            Console.WriteLine($"N={N}");

            // Create data
            List<int> list = new List<int>();
            HashSet<int> hashSet = new HashSet<int>();
            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            for (int i = 0; i < N; i++)
            {
                list.Add(i);
                hashSet.Add(i);
                dict[i] = true;
            }
            int targetExists = N - 1;
            int targetMissing = -1;

            Stopwatch sw = new Stopwatch();

            // List contains
            sw.Restart();
            list.Contains(targetExists);
            sw.Stop();
            Console.WriteLine($"N={N}, List.Contains({targetExists}): {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            hashSet.Contains(targetExists);
            sw.Stop();
            Console.WriteLine($"N={N}, HashSet.Contains({targetExists}): {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            dict.ContainsKey(targetExists);
            sw.Stop();
            Console.WriteLine($"N={N}, Dict.ContainsKey({targetExists}): {sw.Elapsed.TotalMilliseconds} ms");

            // Check missing element
            sw.Restart();
            list.Contains(targetMissing);
            sw.Stop();
            Console.WriteLine($"N={N}, List.Contains({targetMissing}): {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            hashSet.Contains(targetMissing);
            sw.Stop();
            Console.WriteLine($"N={N}, HashSet.Contains({targetMissing}): {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            dict.ContainsKey(targetMissing);
            sw.Stop();
            Console.WriteLine($"N={N}, Dict.ContainsKey({targetMissing}): {sw.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine(new string('-', 40));
        }

    }
}
