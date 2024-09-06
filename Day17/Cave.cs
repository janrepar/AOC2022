﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
