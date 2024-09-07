using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public class Grid
    {
        public HashSet<Coordinates> Cubes { get; set; } = new HashSet<Coordinates>();
        public List<string> Directions { get; set; } = new List<string>() { "Down", "Up", "Left", "Right", "Forward", "Backwards" };

        public void CalculateSurfaceArea()
        {
            int totalArea = 0;

            foreach (Coordinates coord in this.Cubes)
            {
                int cubeSides = 6; // Each cube has 6 sides

                // Calculate neighbours
                foreach (string direction in this.Directions) 
                {
                    Coordinates neighbour = Grid.CalculateNeighbour(direction, coord);

                    if (this.Cubes.Contains(neighbour))
                    {
                        cubeSides--;
                    }
                }

                totalArea += cubeSides;
            }

            Console.WriteLine(totalArea);
        }

        public void CalculateExteriorSurfaceArea()
        {
            // Get the bounds of cubes
            int minX = this.Cubes.Min(c => c.X) - 1;
            int maxX = this.Cubes.Max(c => c.X) + 1;
            int minY = this.Cubes.Min(c => c.Y) - 1;
            int maxY = this.Cubes.Max(c => c.Y) + 1;
            int minZ = this.Cubes.Min(c => c.Z) - 1;
            int maxZ = this.Cubes.Max(c => c.Z) + 1;
          
            HashSet<Coordinates> reachableAir = new HashSet<Coordinates>();
            Queue<Coordinates> queue = new Queue<Coordinates>();
            queue.Enqueue(new Coordinates(minX, minY, minZ));

            // Fill reachable air from outside point
            while (queue.Count > 0)
            {
                Coordinates currentPoint = queue.Dequeue();

                // If the current point is out of bounds or already visited, skip it
                if (reachableAir.Contains(currentPoint) ||
                    currentPoint.X < minX || currentPoint.X > maxX ||
                    currentPoint.Y < minY || currentPoint.Y > maxY ||
                    currentPoint.Z < minZ || currentPoint.Z > maxZ)
                {
                    continue;
                }

                // If the current point is a cube, skip it
                if (this.Cubes.Contains(currentPoint))
                {
                    continue;
                }

                reachableAir.Add(currentPoint);

                // Check all 6 directions to continue filling reachable air
                foreach (string direction in this.Directions)
                {
                    List<string> directions = new List<string>() { "Down", "Up", "Left", "Right", "Forward", "Backwards" };
                    
                    Coordinates neighbour = Grid.CalculateNeighbour(direction, currentPoint);

                    if (!reachableAir.Contains(neighbour))
                    {
                        queue.Enqueue(neighbour);
                    }
                }
            }

            // Calculate the exterior surface area
            int exteriorSurfaceArea = 0;

            foreach (Coordinates coord in this.Cubes)
            {
                foreach (string direction in this.Directions)
                {
                    Coordinates neighbour = Grid.CalculateNeighbour(direction, coord);

                    // If the neighbor is reachable air, it's an exposed side
                    if (reachableAir.Contains(neighbour))
                    {
                        exteriorSurfaceArea++;
                    }
                }
            }

            Console.WriteLine($"Exterior Surface Area: {exteriorSurfaceArea}");
        }

        public static Coordinates CalculateNeighbour(string direction, Coordinates coords)
        {
            coords = direction switch
            {
                "Down" => coords.Down,
                "Up" => coords.Up,
                "Left" => coords.Left,
                "Right" => coords.Right,
                "Forward" => coords.Forward,
                "Backwards" => coords.Backwards,
                _ => throw new ArgumentException("Invalid direction") // Default case in case of switch expression (discard)
            };

            return coords;
        }

        public void ParseInput(List<string> input)
        {
            foreach (string line in input)
            {
                Coordinates newCoords = Coordinates.Parse(line);
                this.Cubes.Add(newCoords);
            }
        }
    }
}
