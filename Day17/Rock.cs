using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public interface IRock
    {
        List<Coordinates> RockShape { get; set; }
        void GenerateRock(Cave cave);
        void RockFall(char c);
    }

    public abstract class Rock : IRock
    {
        public List<Coordinates> RockShape { get; set; } = new List<Coordinates>();

        public abstract void GenerateRock(Cave cave);


        public void RockFall(char c)
        {
            for (int i = 0; i < this.RockShape.Count; i++)
            {
                if (c == '<')
                {
                    this.RockShape[i] = this.RockShape[i].Left;
                }
                else if (c == '>')
                {
                    this.RockShape[i] = this.RockShape[i].Right; 
                }
                else
                {
                    this.RockShape[i] = this.RockShape[i].Down;
                }
            }
        }
    }

    public class RockShape1 : Rock
    {
        public override void GenerateRock(Cave cave)
        {
            for (int i = 2; i < 6; i++)
            {
                Coordinates newCoords = new Coordinates(i, cave.BaseY);
                this.RockShape.Add(newCoords);
            }
        }
    }

    public class RockShape2 : Rock
    {
        public override void GenerateRock(Cave cave)
        {
            this.RockShape.Add(new Coordinates(3, cave.BaseY + 2));
            this.RockShape.Add(new Coordinates(2, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(3, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(4, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(3, cave.BaseY));
        }
    }

    public class RockShape3 : Rock
    {
        public override void GenerateRock(Cave cave)
        {
            this.RockShape.Add(new Coordinates(4, cave.BaseY + 2));
            this.RockShape.Add(new Coordinates(4, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(2, cave.BaseY));
            this.RockShape.Add(new Coordinates(3, cave.BaseY));
            this.RockShape.Add(new Coordinates(4, cave.BaseY));
        }

    }

    public class RockShape4 : Rock
    {
        public override void GenerateRock(Cave cave)
        {
            this.RockShape.Add(new Coordinates(2, cave.BaseY + 3));
            this.RockShape.Add(new Coordinates(2, cave.BaseY + 2));
            this.RockShape.Add(new Coordinates(2, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(2, cave.BaseY));
        }
    }

    public class RockShape5 : Rock
    {
        public override void GenerateRock(Cave cave)
        {
            this.RockShape.Add(new Coordinates(2, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(3, cave.BaseY + 1));
            this.RockShape.Add(new Coordinates(2, cave.BaseY));
            this.RockShape.Add(new Coordinates(3, cave.BaseY));
        }
    }
}


/* ROCK SHAPES
2345
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