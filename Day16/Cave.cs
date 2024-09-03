using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day16
{
    public class Cave
    {
        public Dictionary<string, Valve> Valves { get; set; } = new Dictionary<string, Valve>();
        public Dictionary<(string, int, string), int> MemoDic { get; set; } = new Dictionary<(string, int, string), int>();
        public Dictionary<(string, string, int, string), int> MemoDicElephant { get; set; } = new Dictionary<(string, string, int, string), int>();

        public void ParseInput(List<string> input)
        {
            foreach (string line in input)
            {
                string str = RemoveSemicolonsAndCommas(line);
                string[] splitStr = str.Split(" ");
                string[] flowRateStr = splitStr[4].Split("=");

                string valveName = splitStr[1];
                int flowRate = int.Parse(flowRateStr[1]);

                Valve valve = new Valve(valveName, flowRate);

                int tunnelStartIndex = Array.IndexOf(splitStr, "to") + 1;
                
                for (int i = tunnelStartIndex + 1; i < splitStr.Length; i++)
                {
                    valve.TunnelsTo.Add(splitStr[i]);
                }

                this.Valves.Add(valveName, valve);
            }
        }

        public int MaxPressure(string currentPosition, int timeLeft, HashSet<string> openedValves)
        {
            // Base case
            if (timeLeft <= 0)
            {
                return 0;
            }

            // Memoization key
            string openedKey = string.Join(",", openedValves.OrderBy(x => x)); // If ordered it has to check less states than of not ordered
            var key = (currentPosition, timeLeft, openedKey);

            // Check if the result is already in the memoization dictionary
            if (this.MemoDic.TryGetValue(key, out int res))
            {
                return res;
            }

            int maxPressure = 0;

            // Open current valve (if not already opened and if it has a flow rate)
            if (!openedValves.Contains(currentPosition) && this.Valves[currentPosition].FlowRate > 0)
            {
                HashSet<string> newOpenedValves = new HashSet<string>(openedValves) { currentPosition };
                int absPressure = this.Valves[currentPosition].FlowRate * (timeLeft - 1);
                maxPressure = Math.Max(maxPressure, absPressure + MaxPressure(currentPosition, timeLeft - 1, newOpenedValves));

                //Console.WriteLine($"Opening valve {currentPosition} with flow rate {this.Valves[currentPosition].FlowRate}. Pressure: {absPressure}, New Time Left: {timeLeft - 2}");
            }

            // Move to connected valves (if any time is left)
            foreach (string tunnelTo in this.Valves[currentPosition].TunnelsTo)
            {
                maxPressure = Math.Max(maxPressure, MaxPressure(tunnelTo, timeLeft - 1, new HashSet<string>(openedValves)));

                // Console.WriteLine($"Moving from {currentPosition} to {tunnelTo}. Time Left: {timeLeft - 1}");
            }

            // Store result in memoization dictionary
            this.MemoDic[key] = maxPressure;
            
            Console.WriteLine($"Storing result in dictionary: ({currentPosition}, {timeLeft}, {openedKey}) => {maxPressure}");

            return maxPressure;
        }

        /*
        public int MaxPressureWithElephant(string currentPosition, string elephantPosition, int timeLeft, HashSet<string> openedValves)
        {
            if (timeLeft <= 0)
            {
                return 0;
            }

            string openedKey = string.Join(",", openedValves.OrderBy(x => x));
            var key = (currentPosition, elephantPosition, timeLeft, openedKey);

            if (this.MemoDicElephant.TryGetValue(key, out int res))
            {
                return res;
            }

            int maxPressure = 0;

            if (Valves.Values.All(v => v.FlowRate == 0 || openedValves.Contains(v.Name)))
            {
                return 0;
            }

            if (currentPosition == elephantPosition && timeLeft < 26)
            {
                return 0;
            }

            // Both open their valves if possible
            if (!openedValves.Contains(currentPosition) && !openedValves.Contains(elephantPosition) &&
                this.Valves[currentPosition].FlowRate > 0 && this.Valves[elephantPosition].FlowRate > 0 &&
                currentPosition != elephantPosition)
            {
                var newOpenedValves = new HashSet<string>(openedValves) { currentPosition, elephantPosition };
                int absPressure = (this.Valves[currentPosition].FlowRate + this.Valves[elephantPosition].FlowRate) * (timeLeft - 1);
                maxPressure = Math.Max(maxPressure, absPressure + MaxPressureWithElephant(currentPosition, elephantPosition, timeLeft - 1, newOpenedValves));
            }

            // Only self opens the valve
            if (!openedValves.Contains(currentPosition) && this.Valves[currentPosition].FlowRate > 0)
            {
                var newOpenedValves = new HashSet<string>(openedValves) { currentPosition };
                int absPressure = this.Valves[currentPosition].FlowRate * (timeLeft - 1);

                foreach (string tunnelToElephant in this.Valves[elephantPosition].TunnelsTo)
                {
                    maxPressure = Math.Max(maxPressure, absPressure + MaxPressureWithElephant(currentPosition, tunnelToElephant, timeLeft - 1, newOpenedValves));
                }
            }

            // Only elephant opens the valve
            if (!openedValves.Contains(elephantPosition) && this.Valves[elephantPosition].FlowRate > 0)
            {
                var newOpenedValves = new HashSet<string>(openedValves) { elephantPosition };
                int absPressure = this.Valves[elephantPosition].FlowRate * (timeLeft - 1);

                foreach (string tunnelToPlayer in this.Valves[currentPosition].TunnelsTo)
                {
                    maxPressure = Math.Max(maxPressure, absPressure + MaxPressureWithElephant(tunnelToPlayer, elephantPosition, timeLeft - 1, newOpenedValves));
                }
            }

            // Both move to connected valves
            foreach (string tunnelToPlayer in this.Valves[currentPosition].TunnelsTo)
            {
                foreach (string tunnelToElephant in this.Valves[elephantPosition].TunnelsTo)
                {
                    maxPressure = Math.Max(maxPressure, MaxPressureWithElephant(tunnelToPlayer, tunnelToElephant, timeLeft - 1, openedValves));
                }
            }

            this.MemoDicElephant[key] = maxPressure;

            // Console.WriteLine($"Storing result in dictionary: ({currentPosition}, {timeLeft}, {openedKey}) => {maxPressure}");

            return maxPressure;
        }
        */

        // Replace semicolons and commas with empty strings
        static string RemoveSemicolonsAndCommas(string str)
        {
            return str.Replace(";", "").Replace(",", "");
        }
    }
}
