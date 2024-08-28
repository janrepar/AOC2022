namespace Day14
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

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_14.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Cave cave = Cave.ParseInput(input);

            var sandSettled = cave.SandFall();

            Console.WriteLine($"\nSand count is {sandSettled.Count}");         
        }

        public static void Part2()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_14.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Cave caveWithFloor = Cave.ParseInput(input);

            caveWithFloor.AddFloor();

            var sandSettled = caveWithFloor.SandFall();

            Console.WriteLine($"\nSand count is {sandSettled.Count}");
        }
    }
}
