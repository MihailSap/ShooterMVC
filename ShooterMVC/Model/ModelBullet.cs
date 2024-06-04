using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ModelBullet : Sprite // B Model
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; set; }
        public static List<ModelBullet> Bullets { get; } = new();
        private static Texture2D _texture; // маленькая часть лабиринта

        public ModelBullet(Texture2D tex, Tuple<Vector2, float> positionAndRotation) : base(tex, positionAndRotation.Item1)
        {
            Speed = 1200;
            RotationAngle = positionAndRotation.Item2;
            Direction = new Vector2((float)Math.Cos(RotationAngle), (float)Math.Sin(RotationAngle));
            Lifespan = 2;
        }

        public void UpdatePosition()
        {
            var newPosition = currentPosition + (Direction * Speed * Game1.Time);
            var bulletRectangle = GetRectangleBounds(newPosition);
            var horizontalCheckRect = GetRectangleBounds(new(newPosition.X, currentPosition.Y));
            var verticalCheckRect = GetRectangleBounds(new(currentPosition.X, newPosition.Y));
            foreach (var collider in ModelMap.GetNearestColliders(bulletRectangle))
                if (horizontalCheckRect.Intersects(collider) || verticalCheckRect.Intersects(collider))
                    Destroy();
            currentPosition = newPosition;
        }

        public void Destroy() => Lifespan = 0;

        public void Update()
        {
            UpdatePosition();
            Lifespan -= Game1.Time;
        }

        public static void Init(Texture2D tex) => _texture = tex;

        public static void Reset() => Bullets.Clear();

        public static void CreateBullet(Tuple<Vector2, float> positionAndRotation)
            => Bullets.Add(new ModelBullet(_texture, positionAndRotation));

        public static void Update(List<Enemy> enemies)
        {
            foreach (var bullet in Bullets)
            {
                bullet.UpdatePosition();
                bullet.Lifespan -= Game1.Time;

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