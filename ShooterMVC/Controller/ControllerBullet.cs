using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace ShooterMVC.Controller
{
    internal class ControllerBullet
    {
        public static void UpdatePosition(ModelBullet bullet)
        {
            var newPosition = bullet.currentPosition + (bullet.Direction * bullet.Speed * Game1.Time);
            var bulletRectangle = bullet.GetRectangleBounds(newPosition);
            var horizontalCheckRect = bullet.GetRectangleBounds(new(newPosition.X, bullet.currentPosition.Y));
            var verticalCheckRect = bullet.GetRectangleBounds(new(bullet.currentPosition.X, newPosition.Y));
            foreach (var collider in ModelMap.GetNearestColliders(bulletRectangle))
                if (horizontalCheckRect.Intersects(collider) || verticalCheckRect.Intersects(collider))
                    Destroy(bullet);
            bullet.currentPosition = newPosition;
        }

        public static void Destroy(ModelBullet bullet) => bullet.Lifespan = 0;
    }
}
