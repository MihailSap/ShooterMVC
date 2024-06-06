using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ControllerBullet
    {
        public static void UpdatePosition(ModelBullet bullet)
        {
            var newPosition = bullet.CurrentPosition + (bullet.Direction * bullet.Speed * Game1.Time);
            var bulletRectangle = bullet.GetRectangleBounds(newPosition);
            var horizontalCheckRect = bullet.GetRectangleBounds(new(newPosition.X, bullet.CurrentPosition.Y));
            var verticalCheckRect = bullet.GetRectangleBounds(new(bullet.CurrentPosition.X, newPosition.Y));
            foreach (var collider in ModelMap.GetNearestColliders(bulletRectangle))
                if (horizontalCheckRect.Intersects(collider) || verticalCheckRect.Intersects(collider))
                    Destroy(bullet);
            bullet.CurrentPosition = newPosition;
        }

        public static void Destroy(ModelBullet bullet) => bullet.Lifespan = 0;

        public void CreateBullet(Tuple<Vector2, float> positionAndRotation, 
            List<ModelBullet> bullets)
        {
            if (ControllerPlayer.LeftMouseClicked)
                bullets.Add(new ModelBullet(bullets[0].Texture, positionAndRotation));
        }

        public static void Update(List<ModelEnemy> enemies, List<ModelBullet> Bullets)
        {
            foreach (var bullet in Bullets)
            {
                ControllerBullet.UpdatePosition(bullet);
                bullet.Lifespan -= Game1.Time;

                foreach (var enemy in enemies)
                {
                    if (enemy.IsAlive && (bullet.CurrentPosition - enemy.CurrentPosition).Length() < 32)
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
