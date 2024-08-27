using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public class Cave
    {
        public int BottomY { get; set; }

        public HashSet<Coordinates> Rocks = new HashSet<Coordinates>();

        public HashSet<Coordinates> SandInPlace = new HashSet<Coordinates>();

        public Cave(HashSet<Coordinates> rocks, int bottomY)
        {
            this.Rocks = rocks;
            this.BottomY = bottomY;
        }

        public static Cave ParseInput(string[] input)
        {
            HashSet<Coordinates> rocks = new HashSet<Coordinates>();
            int bottomY = 0;
            
            foreach (string line in input)
            {
                string[] splitLines = line.Split(" -> ");

                for (int i = 0; i < splitLines.Length; i++)
                {
                    Coordinates rockStart = Coordinates.Parse(splitLines[i]);
                    Coordinates rockEnd = Coordinates.Parse(splitLines[i + 1]);

                    bottomY = Math.Max(bottomY, rockStart.Y);
                    bottomY = Math.Max(bottomY, rockEnd.Y);

                    rocks.Add(rockStart);

                    while (rockStart != rockEnd)
                    {
                        // Better solution than if clauses
                        int differenceX = Math.Sign(rockEnd.X - rockStart.X);
                        int differenceY = Math.Sign(rockEnd.Y - rockStart.Y);

                        rockStart = new Coordinates(differenceX + rockStart.X, differenceY + rockStart.Y);
                        rocks.Add((rockStart));
                    }
                }
            }

            return new Cave(rocks, bottomY);
        }
    }
}
