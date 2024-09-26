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

        public Coordinates Down => new Coordinates(X, Y - 1, Z);
        public Coordinates Up => new Coordinates(X, Y + 1, Z);
        public Coordinates Left => new Coordinates(X - 1, Y, Z);
        public Coordinates Right => new Coordinates(X + 1, Y, Z);
        public Coordinates Forward => new Coordinates(X, Y, Z + 1);
        public Coordinates Backwards => new Coordinates(X, Y, Z - 1);

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
            return $"({this.X},{this.Y},{this.Z})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Coordinates other = (Coordinates)obj;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
