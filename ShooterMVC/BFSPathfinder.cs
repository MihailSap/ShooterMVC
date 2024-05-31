using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace ShooterMVC
{
    internal static class BFSPathfinder
    {
        public static List<Vector2> FindPath(Vector2 start, Vector2 goal, Func<Vector2, bool> isWalkable, Vector2 gridSize, float cellSize)
        {
            var startCell = new Cell(start);
            var goalCell = new Cell(goal);
            var queue = new Queue<Cell>();
            var visited = new HashSet<Vector2>();

            queue.Enqueue(startCell);
            visited.Add(start);

            var directions = new Vector2[]
            {
                new Vector2(0, -1), // вверх
                new Vector2(1, 0),  // вправо
                new Vector2(0, 1),  // вниз
                new Vector2(-1, 0)  // влево
            };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.Position == goal)
                {
                    var path = new List<Vector2>();
                    while (current != null)
                    {
                        path.Add(current.Position);
                        current = current.Parent;
                    }
                    path.Reverse();
                    return path;
                }

                foreach (var direction in directions)
                {
                    var neighborPosition = current.Position + direction * cellSize;
                    if (IsWithinBounds(neighborPosition, gridSize) && isWalkable(neighborPosition) && !visited.Contains(neighborPosition))
                    {
                        var neighbor = new Cell(neighborPosition) { Parent = current };
                        queue.Enqueue(neighbor);
                        visited.Add(neighborPosition);
                    }
                }
            }

            return null;
        }

        private static bool IsWithinBounds(Vector2 position, Vector2 gridSize)
        {
            return position.X >= 0 && position.Y >= 0 && position.X < gridSize.X && position.Y < gridSize.Y;
        }
    }
}
