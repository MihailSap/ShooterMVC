using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ShooterMVC
{
    internal class ControllerEnemy
    {
        public static void UpdatePath(ModelPlayer player, ModelEnemy enemy)
        {
            enemy.updatePathTimer -= Game1.Time;
            if (enemy.updatePathTimer <= 0)
            {
                enemy.path = ModelBFS.GetPath(enemy.CurrentPosition, player.CurrentPosition);
                enemy.updatePathTimer = 0.5f;
            }
            FollowThePath(enemy);
        }

        private static void FollowThePath(ModelEnemy enemy)
        {
            if (enemy.path.Count > 0)
            {
                var nextPosition = enemy.path.Peek();
                if (Vector2.Distance(enemy.CurrentPosition, nextPosition) < 4)
                {
                    enemy.path.Dequeue();
                    enemy.CurrentPosition = nextPosition;
                }

                if (enemy.path.Count > 0)
                {
                    nextPosition = enemy.path.Peek();
                    var direction = Vector2.Normalize(nextPosition - enemy.CurrentPosition);
                    enemy.CurrentPosition += direction * enemy.Speed * Game1.Time;
                    enemy.RotationAngle = (float)Math.Atan2(direction.Y, direction.X);
                }
            }
        }

        public static void Kill(ModelEnemy enemy)
        {
            enemy.IsAlive = false;
            ModelCoin.SetExperienceToPlayer(enemy.CurrentPosition);
        }

        public static void Update(List<ModelEnemy> enemies, ModelPlayer player)
        {
            ModelEnemy.SpawnEnemy(player);
            enemies.ForEach(enemy => UpdatePath(player, enemy));
            enemies.RemoveAll((enemy) => !enemy.IsAlive);
        }
    }
}