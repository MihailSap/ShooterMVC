using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterMVC;
using System;
using Microsoft.Xna.Framework.Content;
using ShooterMVC.Controller;

internal class ModelEnemy : ModelSprite
{
    public static List<ModelEnemy> EnemyList { get; } = new();
    public bool IsAlive { get;  set; }
    public Queue<Vector2> path;
    public float updatePathTimer;
    public static Texture2D texture;
    private static float spawnCooldown;
    private static float spawnTime;

    public ModelEnemy(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 500; 
        IsAlive = true;
        path = new Queue<Vector2>();
        updatePathTimer = 0f;
    }

    public static void Init(ContentManager Content)
    {
        texture = Content.Load<Texture2D>("big-enemy");
        spawnTime = spawnCooldown = 1f; // Настройка кол-ва
    }

    public static void Reset()
    {
        EnemyList.Clear();
        spawnTime = spawnCooldown;
    }

    public static Vector2 GetRandomPosition(Vector2 playerPosition)
    {
        var random = new Random();
        var mapHeight = ModelMap.Tiles.GetLength(0);
        var mapWidth = ModelMap.Tiles.GetLength(1);
        var tileSize = ModelMap.TileSize;
        var minDistance = 3 * tileSize; // минимальное допустимое расстояние в пикселях
        var zeroCells = new List<Vector2>();

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (ModelMap.Tiles[y, x] == 0)
                {
                    var cellCenter = new Vector2(x * tileSize + tileSize / 2, y * tileSize + tileSize / 2);
                    if (Vector2.Distance(cellCenter, playerPosition) >= minDistance)
                        zeroCells.Add(cellCenter);
                }
            }
        }

        var randomIndex = random.Next(zeroCells.Count);
        return zeroCells[randomIndex];
    }

    public static void Update(ModelPlayer player)
    {
        spawnTime -= Game1.Time;
        if (spawnTime <= 0)
        {
            spawnTime += spawnCooldown;
            EnemyList.Add(new ModelEnemy(texture, GetRandomPosition(player.currentPosition)));
        }

        EnemyList.ForEach(enemy => ControllerEnemy.UpdatePath(player, enemy));
        EnemyList.RemoveAll((enemy) => !enemy.IsAlive);
    }
}