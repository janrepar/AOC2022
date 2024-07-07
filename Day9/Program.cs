namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }
        public struct Coordinates
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coordinates(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        // Part 1 (day 9)
        public static void Part1 ()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_9_1.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Coordinates head = new Coordinates(0, 0);
            Coordinates tail = new Coordinates(0, 0);

            // HashSet for storing all unique positions of the Tail
            HashSet<Coordinates> positions = new HashSet<Coordinates>();

            foreach (string line in input)
            {
                string[] instructions = line.Split(' ');

                for (int i = int.Parse(instructions[1]); i > 0; i--)
                {
                    if (instructions[0] == "U")
                    {
                        head.Y++;
                    }
                    else if (instructions[0] == "D")
                    {
                        head.Y--;
                    }
                    else if (instructions[0] == "L")
                    {
                        head.X--;
                    }
                    else if (instructions[0] == "R")
                    {
                        head.X++;
                    }

                    /*
                    Console.WriteLine("Head: " + head.X + " " + head.Y);
                    Console.WriteLine("Tail: " + tail.X + " " + tail.Y);
                    */

                    // Check if head x or y is 2 spaces away from tail x or y
                    if (Math.Abs(head.X - tail.X) == 2 || Math.Abs(head.Y - tail.Y) == 2)
                    {
                        // Check if head x is different as tail x and increment or decrement tail x
                        if (head.X != tail.X)
                        {
                            if (head.X > tail.X)
                            {
                                tail.X++;
                            }
                            else
                            {
                                tail.X--;
                            }
                        }
                        // Check if head y is different as tail y and increment or decrement tail y
                        if (head.Y != tail.Y)
                        {
                            if (head.Y > tail.Y)
                            {
                                tail.Y++;
                            }
                            else
                            {
                                tail.Y--;
                            }
                        }
                    }
                    // Add positions to hashset
                    positions.Add(tail);
                }
            }
            Console.WriteLine(positions.Count());
        }

        // Part 2 (day 9)
        public static void Part2()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_9_1.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            // Now rope is of length 10 and each segment has its own coordinates (head is at index 0, tail at index 9)
            Coordinates[] ropeCoords = new Coordinates[10];
            
            for (int i = 0; i < 10; i++)
            {
                Coordinates coordinates = new Coordinates(0,0);
                ropeCoords.Append(coordinates);
            }

            // HashSet for storing all unique positions of the Tail (9)
            HashSet<Coordinates> positions = new HashSet<Coordinates>();

            foreach (string line in input)
            {
                string[] instructions = line.Split(' ');

                for (int i = int.Parse(instructions[1]); i > 0; i--)
                {
                    if (instructions[0] == "U")
                    {
                        ropeCoords[0].Y++;
                    }
                    else if (instructions[0] == "D")
                    {
                        ropeCoords[0].Y--;
                    }
                    else if (instructions[0] == "L")
                    {
                        ropeCoords[0].X--;
                    }
                    else if (instructions[0] == "R")
                    {
                        ropeCoords[0].X++;
                    }

                    for (int j = 1; j < ropeCoords.Length; j++)
                    {
                        // j is tail and j - 1 is head at each iteration of the loop to the end of the rope 
                        if (Math.Abs(ropeCoords[j - 1].X - ropeCoords[j].X) == 2 || Math.Abs(ropeCoords[j - 1].Y - ropeCoords[j].Y) == 2)
                        {
                            if (ropeCoords[j - 1].X != ropeCoords[j].X)
                            {
                                if (ropeCoords[j - 1].X > ropeCoords[j].X)
                                {
                                    ropeCoords[j].X++;
                                }
                                else
                                {
                                    ropeCoords[j].X--;
                                }
                            }
                            if (ropeCoords[j - 1].Y != ropeCoords[j].Y)
                            {
                                if (ropeCoords[j - 1].Y > ropeCoords[j].Y)
                                {
                                    ropeCoords[j].Y++;
                                }
                                else
                                {
                                    ropeCoords[j].Y--;
                                }
                            }

                            /*
                            Console.WriteLine(ropeCoords[j].X + " " + ropeCoords[j].Y);
                            Console.WriteLine(ropeCoords[j - 1].X + " " + ropeCoords[j - 1].Y);
                            */
                        }
                        // Add positions to hashset
                        positions.Add(ropeCoords[9]);
                    }
                }                  
            }
            Console.WriteLine(positions.Count());
        }
    }
}
