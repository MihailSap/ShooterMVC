﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterMVC;
using System;

internal class Enemy : Sprite // В Model
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
    public static Vector2 GetRandomPosition()
    {
        var random = new Random();
        var mapHeight = ModelMap.Tiles.GetLength(0);
        var mapWidth = ModelMap.Tiles.GetLength(1);

        var zeroCells = new List<Vector2>();
        for (int y = 0; y < mapHeight; y++)
            for (int x = 0; x < mapWidth; x++)
                if (ModelMap.Tiles[y, x] == 0)
                    zeroCells.Add(new Vector2(x * ModelMap.TileSize + ModelMap.TileSize / 2, y * ModelMap.TileSize + ModelMap.TileSize / 2));

        var randomIndex = random.Next(zeroCells.Count);
        return zeroCells[randomIndex];
    }

    public void Update(Player player)
    {
        updatePathTimer -= Game1.Time;
        if (updatePathTimer <= 0)
        {
            path = ModelBFS.GetPath(currentPosition, player.currentPosition);
            updatePathTimer = 0.5f; // Обновление пути каждую секунду
        }
        FollowThePath();
    }

    public void Destroy()
    {
        IsAlive = false;
        CoinMethods.GetExperience(currentPosition);
    }
}
