using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Coordinates(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Helper method to parse coordinates from input
        public static Coordinates Parse(string coords)
        {
            string[] splitCoords = coords.Split(',');

            return new Coordinates(int.Parse(splitCoords[0]), int.Parse(splitCoords[1]), int.Parse(splitCoords[2]));
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }
    }
}
