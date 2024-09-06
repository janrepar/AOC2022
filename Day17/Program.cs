namespace Day17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            // Part2();
        }

        public static void Part1()
        {
            char[] input = Array.Empty<char>();

            // Reading from file and saving input to List
            StreamReader srFile = new StreamReader("C:/Temp/test.txt");
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

        }
    }
}