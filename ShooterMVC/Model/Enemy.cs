using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterMVC;
using System;

internal class Enemy : Sprite
{
    public bool IsAlive { get; private set; }
    private Queue<Vector2> path;
    private float updatePathTimer;

    public Enemy(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 500; // Уменьшим скорость для более плавного движения
        IsAlive = true;
        path = new Queue<Vector2>();
        updatePathTimer = 0f;
    }

    private List<Vector2> FindPathBFS(Vector2 start, Vector2 goal)
    {
        var frontier = new Queue<Vector2>();
        frontier.Enqueue(start);

        var cameFrom = new Dictionary<Vector2, Vector2?>();
        cameFrom[start] = null;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == goal)
                break;

            foreach (var next in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
            return new List<Vector2>();

        var path = new List<Vector2>();
        var temp = goal;

        while (!temp.Equals(start))
        {
            path.Add(temp);
            temp = cameFrom[temp].Value;
        }
        path.Add(start);
        path.Reverse();

        return path;
    }

    private IEnumerable<Vector2> GetNeighbors(Vector2 current)
    {
        var neighbors = new List<Vector2>();
        foreach (var direction in GetDirections())
        {
            var neighborTileCenter = new Vector2(
                (int)((current.X + direction.X * Map.TileSize) / Map.TileSize) * Map.TileSize + Map.TileSize / 2,
                (int)((current.Y + direction.Y * Map.TileSize) / Map.TileSize) * Map.TileSize + Map.TileSize / 2
            );
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

    private bool IsPossible(Vector2 position)
    {
        var x = (int)(position.Y / Map.TileSize);
        var y = (int)(position.X / Map.TileSize);

        if (x < 0 || x >= Map.tiles.GetLength(0) || y < 0 || y >= Map.tiles.GetLength(1))
            return false;

        return Map.tiles[x, y] == 0;
    }

    private void UpdatePath(Vector2 playerPosition)
    {
        var startTile = new Vector2(
            (int)(currentPosition.X / Map.TileSize) * Map.TileSize + Map.TileSize / 2,
            (int)(currentPosition.Y / Map.TileSize) * Map.TileSize + Map.TileSize / 2
        );

        var goalTile = new Vector2(
            (int)(playerPosition.X / Map.TileSize) * Map.TileSize + Map.TileSize / 2,
            (int)(playerPosition.Y / Map.TileSize) * Map.TileSize + Map.TileSize / 2
        );

        path = new Queue<Vector2>(FindPathBFS(startTile, goalTile));
    }

    private void FollowThePath()
    {
        if (path.Count > 0)
        {
            var nextPosition = path.Peek();
            if (Vector2.Distance(currentPosition, nextPosition) < 4)
            {
                path.Dequeue();
                currentPosition = nextPosition;
            }

            if (path.Count > 0)
            {
                nextPosition = path.Peek();
                var direction = Vector2.Normalize(nextPosition - currentPosition);
                currentPosition += direction * Speed * Game1.Time;
                RotationAngle = (float)Math.Atan2(direction.Y, direction.X);
            }
        }
    }

    private void UpdatePosition(Player player)
    {
        updatePathTimer -= Game1.Time;
        if (updatePathTimer <= 0)
        {
            UpdatePath(player.currentPosition);
            updatePathTimer = 0.5f; // Обновление пути каждую секунду
        }
        FollowThePath();
    }

    public void Update(Player player) => UpdatePosition(player);

    public void Destroy()
    {
        IsAlive = false;
        CoinMethods.GetExperience(currentPosition);
    }
}
