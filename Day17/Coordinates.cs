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
        public Coordinates Rigth => new Coordinates(X + 1, Y);

        public Coordinates(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }
    }
}
