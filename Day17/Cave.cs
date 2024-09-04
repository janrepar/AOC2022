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

        public Cave()
        {
            for (int i = 0; i < 7; i++)
            {
                Coordinates newCoords = new Coordinates(i, -1);
                this.Floor.Add(newCoords);
            }
        }
    }
}
