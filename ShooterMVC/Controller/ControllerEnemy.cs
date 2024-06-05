using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace ShooterMVC.Controller
{
    internal class ControllerEnemy
    {
        public static void UpdatePath(ModelPlayer player, ModelEnemy enemy)
        {
            enemy.updatePathTimer -= Game1.Time;
            if (enemy.updatePathTimer <= 0)
            {
                enemy.path = ModelBFS.GetPath(enemy.currentPosition, player.currentPosition);
                enemy.updatePathTimer = 0.5f; // Обновление пути каждую секунду
            }
            FollowThePath(enemy);
        }

        private static void FollowThePath(ModelEnemy enemy)
        {
            if (enemy.path.Count > 0)
            {
                var nextPosition = enemy.path.Peek();
                if (Vector2.Distance(enemy.currentPosition, nextPosition) < 4)
                {
                    enemy.path.Dequeue();
                    enemy.currentPosition = nextPosition;
                }

                if (enemy.path.Count > 0)
                {
                    nextPosition = enemy.path.Peek();
                    var direction = Vector2.Normalize(nextPosition - enemy.currentPosition);
                    enemy.currentPosition += direction * enemy.Speed * Game1.Time;
                    enemy.RotationAngle = (float)Math.Atan2(direction.Y, direction.X);
                }
            }
        }

        public static void Destroy(ModelEnemy enemy)
        {
            enemy.IsAlive = false;
            ModelCoin.GetExperience(enemy.currentPosition);
        }
    }
}