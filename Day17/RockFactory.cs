using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public class RockFactory
    {
        // Factory method to create rocks based on type
        public IRock CreateRock(double rockType, Cave cave)
        {
            IRock rock = rockType switch
            {
                1 => new RockShape1(),
                2 => new RockShape2(),
                3 => new RockShape3(),
                4 => new RockShape4(),
                5 => new RockShape5(),
                _ => throw new ArgumentException("Invalid rock type") // Default case in case of switch expression (discard)
            };

            rock.GenerateRock(cave); // Generate the rock shape based on cave structure
            return rock;
        }
    }
}
