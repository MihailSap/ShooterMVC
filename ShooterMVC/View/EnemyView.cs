using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShooterMVC
{
    internal class EnemyView
    {
        public static List<Enemy> EnemyList { get; } = new(); //
        public static Texture2D texture;
        private static float spawnCooldown;
        private static float spawnTime;
        private static Random random;
        private static int _padding;

        public static void Init(Texture2D tex)
        {
            texture = tex;
            spawnCooldown = 2f; // Настройка кол-ва
            spawnTime = spawnCooldown;
            random = new Random();
            _padding = texture.Width / 2;
        }

        public static void Reset()
        {
            EnemyList.Clear();
            spawnTime = spawnCooldown;
        }

        private static Vector2 GetRandomPosition(Point Bounds)
        {
            var mapHeight = Map.tiles.GetLength(0);
            var mapWidth = Map.tiles.GetLength(1);

            var zeroCells = new List<Vector2>();
            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                    if (Map.tiles[y, x] == 0)
                        zeroCells.Add(new Vector2(x * Map.TileSize, y * Map.TileSize));

            var randomIndex = random.Next(zeroCells.Count);
            return zeroCells[randomIndex];
        }


        public static void Update(Player player, Point Bounds)
        {
            spawnTime -= Game1.Time;
            if (spawnTime <= 0)
            {
                spawnTime += spawnCooldown;
                EnemyList.Add(new Enemy(texture, GetRandomPosition(Bounds)));
            }

            EnemyList.ForEach(enemy => enemy.Update(player));
            EnemyList.RemoveAll((enemy) => !enemy.IsAlive);
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            EnemyList.ForEach(enemy => enemy.Draw(_spriteBatch));
        }
    }
}