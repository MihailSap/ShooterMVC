using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class BulletView // только отрисовка, контроллер передает
    {
        private static Texture2D _texture; // маленькая часть лабиринта
        public static List<Bullet> Bullets { get; } = new();

        public static void Init(Texture2D tex) => _texture = tex;

        public static void Reset() => Bullets.Clear();

        public static void AddProjectile(BulletData data)
            => Bullets.Add(new Bullet(_texture, data));

        public static void Draw(SpriteBatch _spriteBatch)
            => Bullets.ForEach((projectile) => projectile.Draw(_spriteBatch));

        public static void Update(List<Enemy> enemies) // в модели
        {
            foreach (var bullet in Bullets)
            {
                bullet.Update();
                foreach (var enemy in enemies)
                {
                    if (enemy.IsAlive && (bullet.currentPosition - enemy.currentPosition).Length() < 32)
                    {
                        enemy.Destroy();
                        bullet.Destroy();
                        break;
                    }
                }
            }
            Bullets.RemoveAll((p) => p.Lifespan <= 0);
        }
    }
}