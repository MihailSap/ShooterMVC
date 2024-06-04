using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterMVC;
using System;
using Microsoft.Xna.Framework.Content;

internal class ModelEnemy : Sprite
{
    public bool IsAlive { get; private set; } = true;

    private Queue<Vector2> path = new();
    private float updatePathTimer;

    private static List<ModelEnemy> enemyList = new();
    private static float spawnCooldown = 1f;
    private static float spawnTime = spawnCooldown;
    private static Random random = new();
    private static Texture2D texture;

    public static List<ModelEnemy> EnemyList => enemyList;

    public ModelEnemy(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 500; // Уменьшенная скорость для более плавного движения
    }

    public List<ModelEnemy> GetEnemies()
    {
        return enemyList;
    }

    public void Reset()
    {
        enemyList.Clear();
        spawnTime = spawnCooldown;
    }

    public static Vector2 GetRandomPosition()
    {
        var zeroCells = new List<Vector2>();
        for (int y = 0; y < ModelMap.Tiles.GetLength(0); y++)
            for (int x = 0; x < ModelMap.Tiles.GetLength(1); x++)
                if (ModelMap.Tiles[y, x] == 0)
                    zeroCells.Add(new Vector2(
                        x * ModelMap.TileSize + ModelMap.TileSize / 2, 
                        y * ModelMap.TileSize + ModelMap.TileSize / 2)
                        );

        return zeroCells[random.Next(zeroCells.Count)];
    }

    public void Update(Player player)
    {
        spawnTime -= Game1.Time;
        if (spawnTime <= 0)
        {
            spawnTime += spawnCooldown;
            enemyList.Add(new ModelEnemy(texture, GetRandomPosition()));
        }

        enemyList.ForEach(enemy => enemy.UpdateCurrent(player));
        enemyList.RemoveAll(enemy => !enemy.IsAlive);
    }

    public void Destroy()
    {
        IsAlive = false;
        CoinMethods.GetExperience(currentPosition);
    }

    private void FollowThePath()
    {
        if (path.Count == 0) return;

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

    private void UpdateCurrent(Player player)
    {
        if ((updatePathTimer -= Game1.Time) <= 0)
        {
            path = ModelBFS.GetPath(currentPosition, player.currentPosition);
            updatePathTimer = 0.5f; // Обновление пути каждые 0.5 секунды
        }
        FollowThePath();
    }
}
