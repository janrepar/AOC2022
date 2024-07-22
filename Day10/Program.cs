using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;

namespace Day10
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

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_10.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            Dictionary<int, int> cycleValueDict = new Dictionary<int, int>();
            int value = 1;
            int cycle = 0;

            foreach (string line in input)
            {
                string[] instructions = line.Split(' ');

                if (instructions[0] == "addx")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        cycle++;
                        if (i == 1) value += int.Parse(instructions[1]);

                        cycleValueDict.Add(cycle, value);
                    }
                }
                else
                {
                    cycle++;

                    cycleValueDict.Add(cycle, value);
                }
            }

            int signalStrengthSum = (cycleValueDict[19] * 20) + (cycleValueDict[59] * 60) +
                                    (cycleValueDict[99] * 100) + (cycleValueDict[139] * 140) +
                                    (cycleValueDict[179] * 180) + (cycleValueDict[219] * 220);

            Console.WriteLine(signalStrengthSum);
        }

        public static void Part2()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_10.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            int[] spritePos = {0, 1, 2};
            int cycle = 0;
           
            foreach (string line in input)
            {
                string[] instructions = line.Split(' ');

                if (instructions[0] == "addx")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (cycle == 40)
                        {
                            cycle = 0;
                            Console.WriteLine();
                        }

                        if (spritePos[0] == cycle || spritePos[1] == cycle || spritePos[2] == cycle)
                        {
                            Console.Write('#');
                            cycle++;
                        }
                        else
                        {
                            Console.Write('.');
                            cycle++;
                        }

                        if (i == 1)
                        {
                            spritePos[0] += int.Parse(instructions[1]);
                            spritePos[1] += int.Parse(instructions[1]);
                            spritePos[2] += int.Parse(instructions[1]);
                        }
                    }
                }
                else
                {
                    if (cycle == 40)
                    {
                        cycle = 0;
                        Console.WriteLine();
                    }

                    if (spritePos[0] == cycle || spritePos[1] == cycle || spritePos[2] == cycle)
                    {
                        Console.Write('#');
                        cycle++;
                    }
                    else
                    {
                        Console.Write('.');
                        cycle++;
                    }
                }                                  
            }              
        }
    }
}
