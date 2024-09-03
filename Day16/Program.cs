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

            // Get all valve names with non-zero flow rates
            List<string> valveNames = cave.Valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name).ToList();
            int totalValveCount = valveNames.Count;
            int maxPressure = 0;

            // Generate all subsets of valves using bit manipulation
            int totalSubsets = 1 << totalValveCount; // 2^totalValveCount

            for (int i = 0; i < totalSubsets; i++)
            {
                HashSet<string> playerOpenedValves = new HashSet<string>();
                HashSet<string> elephantOpenedValves = new HashSet<string>();

                // Determine which valves are opened by the player based on the bitmask `i`
                for (int j = 0; j < totalValveCount; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        playerOpenedValves.Add(valveNames[j]);
                    }
                    else
                    {
                        elephantOpenedValves.Add(valveNames[j]);
                    }
                }

                // Only consider cases where the number of valves is almost evenly split
                int playerCount = playerOpenedValves.Count;
                int elephantCount = elephantOpenedValves.Count;

                if (Math.Abs(playerCount - elephantCount) <= 1) // Condition to ensure roughly even split
                {
                    // Calculate the maximum pressure for the player and the elephant for their respective opened valves
                    int playerPressure = cave.MaxPressure("AA", 26, playerOpenedValves);
                    int elephantPressure = cave.MaxPressure("AA", 26, elephantOpenedValves);

                    // Update the maximum pressure
                    maxPressure = Math.Max(maxPressure, playerPressure + elephantPressure); // 2306 - 1738s 
                }
            }

            // Stop measuring time
            stopwatch.Stop();

            Console.WriteLine($"Max pressure is: {maxPressure}");
            Console.WriteLine($"Time Elapsed: {stopwatch.Elapsed.TotalSeconds} s");
        }
    }
}
