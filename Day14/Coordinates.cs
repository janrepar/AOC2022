using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates Down => new Coordinates(X, Y + 1);
        public Coordinates DownLeft => new Coordinates(X - 1, Y + 1);
        public Coordinates DownRigth => new Coordinates(X + 1, Y + 1);

        public Coordinates(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        // Helper method to parse coordinates from input
        public static Coordinates Parse(string coords)
        {
            string [] splitCoords = coords.Split(',');

            return new Coordinates(int.Parse(splitCoords[0]), int.Parse(splitCoords[1]));
        }
    }
}
