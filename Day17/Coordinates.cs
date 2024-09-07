using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates Down => new Coordinates(X, Y - 1);
        public Coordinates Left => new Coordinates(X - 1, Y);
        public Coordinates Right => new Coordinates(X + 1, Y);

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }

        // Override Equals method
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Coordinates other = (Coordinates)obj;
            return X == other.X && Y == other.Y;
        }

        // Override GetHashCode method
        public override int GetHashCode()
        {
            // A simple hash code calculation based on the X and Y values
            return X * 397 ^ Y;
        }
    }
}
