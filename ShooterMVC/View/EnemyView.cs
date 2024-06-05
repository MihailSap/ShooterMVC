using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class EnemyView
    {
        public static List<Enemy> EnemyList { get; } = new();
        public static Texture2D texture;
        private static float spawnCooldown;
        private static float spawnTime;

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

        public static void Update(Player player)
        {
            spawnTime -= Game1.Time;
            if (spawnTime <= 0)
            {
                spawnTime += spawnCooldown;
                EnemyList.Add(new Enemy(texture, Enemy.GetRandomPosition()));
            }

            EnemyList.ForEach(enemy => enemy.UpdatePath(player));
            EnemyList.RemoveAll((enemy) => !enemy.IsAlive);
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            EnemyList.ForEach(enemy => enemy.Draw(_spriteBatch));
        }
    }
}
