using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ModelBullet : ModelSprite
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; set; }
        public static List<ModelBullet> Bullets { get; } = new();
        private static Texture2D texture;

        public ModelBullet(Texture2D tex, Tuple<Vector2, float> positionAndRotation) 
            : base(tex, positionAndRotation.Item1)
        {
            Speed = 1200;
            RotationAngle = positionAndRotation.Item2;
            Direction = new Vector2((float)Math.Cos(RotationAngle), (float)Math.Sin(RotationAngle));
            Lifespan = 2;
        }

        public static void SetTextureToModel(Texture2D tex) => texture = tex;

        public static void Reset() => Bullets.Clear();

        public static void CreateBullet(Tuple<Vector2, float> bulletData)
            => Bullets.Add(new ModelBullet(texture, bulletData));
    }
}