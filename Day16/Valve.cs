using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class Valve
    {
        public string Name { get; set; }
        public int FlowRate { get; set; }
        public HashSet<string> TunnelsTo { get; set; } = new HashSet<string>();

        public Valve(string name, int flowRate)
        {
            Name = name;
            FlowRate = flowRate;
        }
    }
}
