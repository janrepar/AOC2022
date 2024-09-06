using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public class Cave
    { 
        public List<Coordinates> Floor { get; set; } = new List<Coordinates>();
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

            while (rockCount < maxRocks)
            {
                int rockType = (rockCount % 5) + 1;

                IRock rock = factory.CreateRock(rockType, cave);
                
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == '>' && rock.RockShape.Max(c => c.X) < 6)
                    {
                        // Move rock to the right
                        rock.RockFall('>');
                    }
                    else if (input[i] == '<' && rock.RockShape.Min(c => c.X) > 0)
                    {
                        // Move rock to the left
                        rock.RockFall('<');
                    }

                    if (rock.RockShape.Any(c => cave.Floor.Contains(c)))
                    {
                        foreach (var c in rock.RockShape)
                        {
                            cave.Floor.Add(c); 
                        }
                        break; // Exit the loop and start new rockshape
                    }

                    rock.RockFall('c');
                }

                rockCount++;
            }
        }
    }
}
