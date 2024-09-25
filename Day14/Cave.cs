using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public class Cave
    {
        public int BottomY { get; set; }

        public HashSet<Coordinates> OccupiedCoords = new HashSet<Coordinates>();

        public HashSet<Coordinates> SettledSand = new HashSet<Coordinates>();

        public Cave(HashSet<Coordinates> occupiedCoords, int bottomY)
        {
            this.OccupiedCoords = occupiedCoords;
            this.BottomY = bottomY;
        }

        public HashSet<Coordinates> SandFall()
        {
            bool abyssReached = false;
            bool endReached = false;
            
            while (abyssReached == false)
            {
                Sand sand = new Sand();

                sand.SandCoordinates = sand.StartPosition;

                while (endReached == false)
                {
                    if (sand.SandCoordinates.Y >= this.BottomY)
                    {
                        abyssReached = true; // Sand falls into abyss
                        break;
                    }
                                    
                    if (!this.OccupiedCoords.Contains(sand.SandCoordinates.Down)) 
                    {
                        sand.SandCoordinates = sand.SandCoordinates.Down; // Move sand down if not occupied
                    }
                    else if (!this.OccupiedCoords.Contains(sand.SandCoordinates.DownLeft))
                    {
                        sand.SandCoordinates = sand.SandCoordinates.DownLeft; // Move Sand down left if not occupied
                    }
                    else if (!this.OccupiedCoords.Contains(sand.SandCoordinates.DownRigth))
                    {
                        sand.SandCoordinates = sand.SandCoordinates.DownRigth; // Move sand down right if not occupied
                    }
                    else
                    {
                        endReached = true; // Sand reached settled position
                        this.SettledSand.Add(sand.SandCoordinates);
                        this.OccupiedCoords.Add(sand.SandCoordinates);
                    }
                }

                endReached = false;
                Console.WriteLine($"SettledCoords: {sand.SandCoordinates.X},{sand.SandCoordinates.Y}");

                // Needed for part 2
                if (sand.SandCoordinates.Y == sand.StartPosition.Y)
                {
                    abyssReached = true; // Reused abyssReachedVariable
                }
            }

            return this.SettledSand;
        }

        public static Cave ParseInput(List<string> input)
        {
            HashSet<Coordinates> rocks = new HashSet<Coordinates>();
            int bottomY = 0;
            
            foreach (string line in input)
            {
                string[] splitLines = line.Split(" -> ");

                for (int i = 1; i < splitLines.Length; i++)
                {
                    Coordinates rockStart = Coordinates.Parse(splitLines[i - 1]);
                    Coordinates rockEnd = Coordinates.Parse(splitLines[i]);

                    bottomY = Math.Max(bottomY, rockStart.Y);
                    bottomY = Math.Max(bottomY, rockEnd.Y);

                    rocks.Add(rockStart);

                    // Goes on until differenceX and Y is 0
                    while (true)
                    {
                        // Better solution than if clauses
                        int differenceX = Math.Sign(rockEnd.X - rockStart.X);
                        int differenceY = Math.Sign(rockEnd.Y - rockStart.Y);

                        if (differenceX == 0 && differenceY == 0)
                        {
                            break;
                        }

                        rockStart = new Coordinates(differenceX + rockStart.X, differenceY + rockStart.Y);
                        rocks.Add(rockStart);
                    }
                }
            }

            return new Cave(rocks, bottomY);
        }

        // Needed for part 2
        public void AddFloor()
        {
            for (int i = - 10000; i <= 10000; i++)
            {
                this.OccupiedCoords.Add(new Coordinates(i, this.BottomY + 2));
            }

            this.BottomY += 2;
        }
    }
}
