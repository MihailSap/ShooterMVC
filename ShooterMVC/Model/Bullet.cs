using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;

namespace ShooterMVC
{
    internal class Bullet : Sprite
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; set; }

        public Bullet(Texture2D tex, BulletData data) : base(tex, data.Position)
        {
            Speed = 600;
            RotationAngle = data.Rotation;
            Direction = new Vector2((float)Math.Cos(RotationAngle), (float)Math.Sin(RotationAngle));
            Lifespan = 2;
        }

        private void UpdatePosition()
        {
            var newPosition = currentPosition + (Direction * Speed * Game1.Time);
            var bulletRectangle = GetRectangleBounds(newPosition);
            var horizontalCheckRect = GetRectangleBounds(new(newPosition.X, currentPosition.Y));
            var verticalCheckRect = GetRectangleBounds(new(currentPosition.X, newPosition.Y));
            foreach (var collider in Map.GetNearestColliders(bulletRectangle))
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
    }
}