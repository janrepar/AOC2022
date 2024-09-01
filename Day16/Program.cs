using System.Diagnostics;

namespace Day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }

        public static void Part1()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            string test = "C:/Temp/test.txt";
            string prod = "C:/Temp/aoc2022_16.txt";

            StreamReader srFile = new StreamReader(prod);
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Cave cave = new Cave();
            cave.ParseInput(input);

            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int maxPressure = cave.MaxPressure("AA", 30, new HashSet<string>());

            // Stop measuring time
            stopwatch.Stop();

            Console.WriteLine("Max pressure is: " + maxPressure); // Result: 1850

            Console.WriteLine($"Time Elapsed: {stopwatch.Elapsed.TotalSeconds} s"); 

            /*
            Console.WriteLine("Parsed input:");
            foreach (var valve in cave.Valves)
            {
                Console.WriteLine($"Valve: {valve.Key}, Flow Rate: {valve.Value.FlowRate}, Tunnels To: {string.Join(", ", valve.Value.TunnelsTo)}");
            }
            */
        }

        public static void Part2()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            string test = "C:/Temp/test.txt";
            string prod = "C:/Temp/aoc2022_16.txt";

            StreamReader srFile = new StreamReader(test);
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Cave cave = new Cave();
            cave.ParseInput(input);

            // Start measuring time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int maxPressure = cave.MaxPressureWithElephant("AA", "AA", 26, new HashSet<string>());

            // Stop measuring time
            stopwatch.Stop();

            Console.WriteLine($"Time Elapsed: {stopwatch.Elapsed.TotalSeconds} s"); 

            Console.WriteLine("Max pressure is: " + maxPressure); 

            /*
            Console.WriteLine("Parsed input:");
            foreach (var valve in cave.Valves)
            {
                Console.WriteLine($"Valve: {valve.Key}, Flow Rate: {valve.Value.FlowRate}, Tunnels To: {string.Join(", ", valve.Value.TunnelsTo)}");
            }
            */
        }
    }
}
