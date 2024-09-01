using System.Diagnostics;

namespace Day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        public static void Part1()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_16.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Cave cave = new Cave();
            cave.ParseInput(input);

            int maxPressure = cave.MaxPressure("AA", 30, new HashSet<string>());

            Console.WriteLine("Max pressure is: " + maxPressure); // Result: 1850

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
