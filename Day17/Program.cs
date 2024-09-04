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
            Cave cave = new Cave();

            Rock rock = new RockShape1();
            rock.GenerateRock(cave);

            foreach (var c in rock.RockShape)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public static void Part2()
        {

        }
    }
}