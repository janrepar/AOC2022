using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Xml;

namespace Day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        public static void Part1()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_13.txt");
            while (srFile.EndOfStream == false)
            {
                input.Add(srFile.ReadLine());
            }
            srFile.Close();

            int pairCount = 1;
            int sumOfPairs = 0;

            // Skips empty lines in input list
            for (int i = 0; i < input.Count; i += 3) 
            {
                bool? c = Compare(input[i], input[i + 1]);
                Console.WriteLine($"Pair {pairCount}: {c}");

                if (c == true)
                {
                    sumOfPairs += pairCount;
                }

                pairCount++;
            }

            Console.WriteLine("Sum of pairs: " + sumOfPairs);
        }

        public static void Part2()
        {
            // Reading from file and saving input to List
            List<string> input = new List<string>();

            StreamReader srFile = new StreamReader("C:/Temp/aoc2022_13.txt");
            while (srFile.EndOfStream == false)
            {
                string line = srFile.ReadLine();
                
                // Ignore empty lines
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                input.Add(line);
            }
            srFile.Close();

            // Add divider packets
            input.Add("[[2]]");
            input.Add("[[6]]");

            // Sort the list using the custom Compare function (from part 1)
            input.Sort((a, b) =>
            {
                bool? result = Compare(a, b);

                // a is less than b and should come before b
                if (result == true)
                {
                    return -1;
                }

                // b is less than a and should come before a
                if (result == false)
                {
                    return 1;
                }

                // a and b are equal, both stay at pos they are at
                return 0; 
            });

            int divider1Index = input.IndexOf("[[2]]") + 1;
            int divider2Index = input.IndexOf("[[6]]") + 1;

            // Console.WriteLine(divider1Index + " " + divider2Index);

            int result = divider1Index * divider2Index;

            /*
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
            */

            Console.WriteLine("\nDecoder key: " + result);
        }

        /// <summary>
        /// Function to compare two packets according to AOC2022 Day 13 rules.
        /// </summary>
        /// <param name="packet1"></param>
        /// <param name="packet2"></param>
        /// <returns>True if inputs are in the right order. False if inputs are not in the right order. Null if packets are equal.</returns>
        public static bool? Compare(string packet1, string packet2)
        {
            // Console.WriteLine($"Compare: {packet1} | {packet2}");

            // Handling mixed types
            // Wraps the packet in [] if one of the packets doesn't start with [
            if (packet1[0] == '[' && packet2[0] != '[')
            {
                return Compare(packet1, "[" + packet2 + "]");
            }

            if (packet1[0] != '[' && packet2[0] == '[')
            {
                return Compare("[" + packet1 + "]", packet2);
            }

            // Comparing numbers
            if (packet1[0] != '[' && packet2[0] != '[')
            {
                // Convert to integer and compare
                int packet1Num = int.Parse(packet1);
                int packet2Num = int.Parse(packet2);

                if (packet1Num < packet2Num)
                {
                    return true;
                }
                else if (packet1Num > packet2Num)
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }

            // Recursive comparison for lists
            // Substring removes first [ and last ] -> ListToString needs to get just the contents of the list as a parameter
            string[] packet1String = ListToString(packet1.Substring(1, packet1.Length - 2));
            string[] packet2String = ListToString(packet2.Substring(1, packet2.Length - 2));

            int counter = 0;

            while (true)
            {
                if (packet1String.Length == counter)
                {
                    if (packet2String.Length == counter)
                    {
                        return null; // Both lists are equal in length and values.
                    }
                    else
                    {
                        return true; // Left list ran out of items first.
                    }
                }
                
                if (packet2String.Length == counter)
                {
                    return false; // Right list ran out of items first.
                }

                bool? comparison = Compare(packet1String[counter], packet2String[counter]);
                if (comparison == true)
                {
                    return true;
                }
                else if (comparison == false)
                {
                    return false;
                }

                counter++;
            }
        }

        public static string[] ListToString(string packetString)
        {
            // Console.WriteLine($"ListToString: {packetString}");

            List<string> newList = new List<string>();

            for (int startIndex = 0; startIndex < packetString.Length; startIndex++)
            {
                int count = 0;
                int endIndex = startIndex;

                while (true)
                {
                    if (packetString[endIndex] == ',' && count == 0 || endIndex == packetString.Length - 1)
                    {
                        newList.Add(packetString.Substring(startIndex, endIndex - startIndex + 1).TrimEnd(','));
                        startIndex = endIndex;
                        break;
                    }

                    if (packetString[endIndex] == '[')
                    {
                        count++;
                    }

                    if (packetString[endIndex] == ']')
                    {
                        count--;
                    }

                    endIndex++;
                }
            }

            return newList.ToArray();
        }
    }
}
