namespace Day17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Part1(); // Answer is 3181
            Part2();
        }

        public static void Part1()
        {
            char[] input = Array.Empty<char>();

            // Reading from file and saving input to List
            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_17.txt");
            while (srFile.EndOfStream == false)
            {
                string line = srFile.ReadLine();
                input = line.ToCharArray();
            }
            srFile.Close();

            Cave.CaveSimulation(input);
        }

        public static void Part2()
        {
            char[] input = Array.Empty<char>();

            // Reading from file and saving input to List
            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_17.txt");
            while (srFile.EndOfStream == false)
            {
                string line = srFile.ReadLine();
                input = line.ToCharArray();
            }
            srFile.Close();

            long height = Cave.CaveSimulationWithStates(input);

            Console.WriteLine($"Tower is {height + 1} rocks tall!");
        }
    }
}