using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShooterMVC.Controller;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ModelBullet : ModelSprite // B Model
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; set; }
        public static List<ModelBullet> Bullets { get; } = new();
        private static Texture2D _texture; // маленькая часть лабиринта

        public ModelBullet(Texture2D tex, Tuple<Vector2, float> positionAndRotation) 
            : base(tex, positionAndRotation.Item1)
        {
            Speed = 1200;
            RotationAngle = positionAndRotation.Item2;
            Direction = new Vector2((float)Math.Cos(RotationAngle), (float)Math.Sin(RotationAngle));
            Lifespan = 2;
        }

        public static void Init(ContentManager Content) => _texture = Content.Load<Texture2D>("big-bullet");

        public static void Reset() => Bullets.Clear();

        public static void CreateBullet(Tuple<Vector2, float> positionAndRotation)
            => Bullets.Add(new ModelBullet(_texture, positionAndRotation));

        public static void Update(List<ModelEnemy> enemies)
        {
            foreach (var bullet in Bullets)
            {
                ControllerBullet.UpdatePosition(bullet);
                bullet.Lifespan -= Game1.Time;

                foreach (var enemy in enemies)
                {
                    if (enemy.IsAlive && (bullet.currentPosition - enemy.currentPosition).Length() < 32)
                    {
                        ControllerEnemy.Destroy(enemy);
                        ControllerBullet.Destroy(bullet);
                        break;
                    }
                }
            }
            Bullets.RemoveAll((bullet) => bullet.Lifespan <= 0);
        }
    }
}