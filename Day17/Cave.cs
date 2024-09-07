using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public class Cave
    { 
        public HashSet<Coordinates> Floor { get; set; } = new HashSet<Coordinates>();
        public int BaseY => this.Floor.Max(f => f.Y) + 4;

        public Cave()
        {
            for (int i = 0; i < 7; i++)
            {
                Coordinates newCoords = new Coordinates(i, -1);
                this.Floor.Add(newCoords);
            }
        }

        public static void CaveSimulation(char[] input)
        {
            int rockCount = 0;
            int maxRocks = 2022;
            RockFactory factory = new RockFactory(); 
            Cave cave = new Cave();

            int index = 0;

            while (rockCount < maxRocks)
            {
                int rockType = (rockCount % 5) + 1;

                IRock rock = factory.CreateRock(rockType, cave);
                
                // Console.WriteLine($"Rock {rockCount + 1} created at: {string.Join(", ", rock.RockShape)}");

                while (true)
                {
                    if (index >= input.Length)
                    {
                        index = 0;
                    }

                    if (input[index] == '>' && rock.RockShape.Max(c => c.X) < 6)
                    {
                        if (rock.RockShape.All(c => c.X < 6 && !cave.Floor.Contains(c.Right))) // Check for collision
                        {
                            rock.RockFall('>');  // Move rock to the right
                        }
                    }
                    else if (input[index] == '<' && rock.RockShape.Min(c => c.X) > 0)
                    {
                        if (rock.RockShape.All(c => c.X > 0 && !cave.Floor.Contains(c.Left))) // Check for collision
                        {
                            rock.RockFall('<');  // Move rock to the left
                        }
                    }
                
                    // Console.WriteLine($"Instruction: {input[index]}, Rock Position After Left or Right Move: {string.Join(", ", rock.RockShape)}");

                    index++;

                    if (rock.RockShape.Any(c => cave.Floor.Contains(c.Down)))
                    {
                        foreach (var c in rock.RockShape)
                        {
                            cave.Floor.Add(c); 
                        }
                        rockCount++;
                        break; // Exit the loop and start new rockshape
                    }

                    rock.RockFall('c');

                    
                    // Console.WriteLine($"Rock Position After Fall: {string.Join(", ", rock.RockShape)}");
                }
            }

            Console.WriteLine($"Tower is {cave.Floor.Max(c => c.Y) + 1} rocks tall!");
        }

        public static long CaveSimulationWithStates(char[] input)
        {
            long rockCount = 0;
            long maxRocks = 1_000_000_000_000;
            RockFactory factory = new RockFactory();
            Cave cave = new Cave();

            int index = 0;

            Dictionary<string, (long rockCount, long caveHeight)> seenStates = new Dictionary<string, (long, long)>();

            long cycleLength = 0;
            long cycleStartRock = 0;
            long cycleHeightDifference = 0;
            long finalHeight = 0;
            int patternDetected = 3; // Ensures that we check multiple patterns (first pattern doesn't give the right answer)

            while (rockCount < maxRocks)
            {
                int rockType = (int)(rockCount % 5) + 1;

                IRock rock = factory.CreateRock(rockType, cave);

                // Console.WriteLine($"Rock {rockCount + 1} created at: {string.Join(", ", rock.RockShape)}");

                while (true)
                {
                    if (index >= input.Length)
                    {
                        index = 0;
                    }

                    if (input[index] == '>' && rock.RockShape.Max(c => c.X) < 6)
                    {
                        if (rock.RockShape.All(c => c.X < 6 && !cave.Floor.Contains(c.Right))) // Check for collision
                        {
                            rock.RockFall('>');  // Move rock to the right
                        }
                    }
                    else if (input[index] == '<' && rock.RockShape.Min(c => c.X) > 0)
                    {
                        if (rock.RockShape.All(c => c.X > 0 && !cave.Floor.Contains(c.Left))) // Check for collision
                        {
                            rock.RockFall('<');  // Move rock to the left
                        }
                    }

                    // Console.WriteLine($"Instruction: {input[index]}, Rock Position After Left or Right Move: {string.Join(", ", rock.RockShape)}");

                    index++;

                    if (rock.RockShape.Any(c => cave.Floor.Contains(c.Down)))
                    {
                        foreach (var c in rock.RockShape)
                        {
                            cave.Floor.Add(c);
                        }
                        rockCount++;

                        // Part 2
                        string stateKey = GetRockStateKey(rock, index);

                        Console.WriteLine($"State at rock {rockCount}: {stateKey}");

                        if (seenStates.TryGetValue(stateKey, out var previousState))
                        {
                            cycleStartRock = previousState.rockCount;
                            cycleHeightDifference = cave.Floor.Max(c => c.Y) - previousState.caveHeight;
                            cycleLength = rockCount - cycleStartRock;
                            patternDetected--;
                            break;
                        }
                        else
                        {
                            seenStates[stateKey] = (rockCount, cave.Floor.Max(c => c.Y));
                        }

                        break; // Exit the loop and start new rockshape
                    }

                    rock.RockFall('c');
                }

                if (patternDetected == 0)
                {
                    long remainingRocks = maxRocks - rockCount;
                    long fullCycles = remainingRocks / cycleLength;
                    long leftoverRocks = remainingRocks % cycleLength;

                    long currentHeight = cave.Floor.Max(c => c.Y);

                    finalHeight = currentHeight + (cycleHeightDifference * fullCycles);

                    for (int i = 0; i < (int)leftoverRocks; i++)
                    {
                        rockType = (int)((rockCount % 5) + 1);
                        rock = factory.CreateRock(rockType, cave);

                        // Simulate remaining rocks
                        while (true)
                        {
                            if (index >= input.Length)
                            {
                                index = 0;
                            }

                            if (input[index] == '>' && rock.RockShape.Max(c => c.X) < 6)
                            {
                                if (rock.RockShape.All(c => c.X < 6 && !cave.Floor.Contains(c.Right))) // Check for collision
                                {
                                    rock.RockFall('>');  // Move rock to the right
                                }
                            }
                            else if (input[index] == '<' && rock.RockShape.Min(c => c.X) > 0)
                            {
                                if (rock.RockShape.All(c => c.X > 0 && !cave.Floor.Contains(c.Left))) // Check for collision
                                {
                                    rock.RockFall('<');  // Move rock to the left
                                }
                            }

                            index++;

                            if (rock.RockShape.Any(c => cave.Floor.Contains(c.Down)))
                            {
                                foreach (var c in rock.RockShape)
                                {
                                    cave.Floor.Add(c);
                                }
                                rockCount++;

                                break; // Exit the loop and start new rockshape
                            }

                            rock.RockFall('c');
                        }
                    }

                    finalHeight += (cave.Floor.Max(c => c.Y) - currentHeight);

                    return finalHeight;
                }
            }

            return finalHeight;
        }

        public static string GetRockStateKey(IRock rock, int index)
        {
            // Generate a unique key based on the current X coordinates of the rock and input index
            return $"{string.Join(",", rock.RockShape.OrderBy(c => c.X).Select(c => c.X))}|{index}";
        }
    }
}
