using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public abstract class Rock
    {
        public List<Coordinates> RockShape { get; set; } = new List<Coordinates>();

        public abstract void GenerateRock(Cave cave);
    }

    public class RockShape1 : Rock
    {
        public override void GenerateRock(Cave cave)
        {
            for (int i = 2; i < 6; i++)
            {
                Coordinates newCoords = new Coordinates(i, cave.Floor.Max(f => f.Y) + 4);
                this.RockShape.Add(newCoords);
            }
        }
    }

    public class RockShape2
    {

    }

    public class RockShape3
    {

    }

    public class RockShape4
    {

    }

    public class RockShape5
    {

    }
}


/* ROCK SHAPES

####

.#.
###
.#.

..#
..#
###

#
#
#
#

##
##

*/