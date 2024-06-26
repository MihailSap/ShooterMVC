﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ShooterMVC
{
    internal class ModelBFS
    {
        public static Queue<Vector2> GetPath(Vector2 currentPosition, Vector2 goalPosition)
        {
            var startTile = GetTileCenter(currentPosition);
            var goalTile = GetTileCenter(goalPosition);
            var path = new Queue<Vector2>(FindPathBFS(startTile, goalTile));
            return path;
        }

        private static Vector2 GetTileCenter(Vector2 position)
        {
            var tileSize = ModelMap.TileSize;
            var halfTileSize = tileSize / 2.0f;

            return new Vector2(
                ((int)(position.X / tileSize) * tileSize) + halfTileSize,
                ((int)(position.Y / tileSize) * tileSize) + halfTileSize
            );
        }


        private static List<Vector2> FindPathBFS(Vector2 start, Vector2 goal)
        {
            var reversedPath = new Queue<Vector2>();
            var cameFrom = new Dictionary<Vector2, Vector2?>();
            reversedPath.Enqueue(start);
            cameFrom[start] = null;

            while (reversedPath.Count > 0)
            {
                var current = reversedPath.Dequeue();
                if (current == goal) break;

                foreach (var next in GetNeighbors(current))
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        reversedPath.Enqueue(next);
                        cameFrom[next] = current;
                    }
                }
            }

            if (!cameFrom.ContainsKey(goal))
                return new List<Vector2>();

            return GetResultPath(start, goal, cameFrom);
        }

        public static List<Vector2> GetResultPath(Vector2 start, Vector2 goal, 
            Dictionary<Vector2, Vector2?> cameFrom)
        {
            var resultPath = new List<Vector2>();
            var temp = goal;
            while (!temp.Equals(start))
            {
                resultPath.Add(temp);
                temp = cameFrom[temp].Value;
            }
            resultPath.Add(start);
            resultPath.Reverse();
            return resultPath;
        }

        private static IEnumerable<Vector2> GetNeighbors(Vector2 current)
        {
            var neighbors = new List<Vector2>();
            foreach (var direction in GetDirections())
            {
                var neighborPosition = current + direction * ModelMap.TileSize;
                var neighborTileCenter = GetTileCenter(neighborPosition);
                if (IsPossible(neighborTileCenter))
                    neighbors.Add(neighborTileCenter);
            }

            return neighbors;
        }

        public static IEnumerable<Vector2> GetDirections()
        {
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    if (x == 0 || y == 0)
                        yield return new(x, y);
        }

        private static bool IsPossible(Vector2 position)
        {
            var x = (int)(position.Y / ModelMap.TileSize);
            var y = (int)(position.X / ModelMap.TileSize);

            if (x < 0 || x >= ModelMap.Tiles.GetLength(0) 
                || y < 0 || y >= ModelMap.Tiles.GetLength(1))
                return false;

            return ModelMap.Tiles[x, y] == 0;
        }
    }
}
