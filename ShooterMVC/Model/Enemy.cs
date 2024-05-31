using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class Enemy : Sprite // модель
    {
        private const float Offset = 20f;
        public bool IsAlive { get; private set; }
        public Enemy(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Speed = 100;
            IsAlive = true;
        }

        private void UpdatePosition(Player player)
        {
            var toPlayer = player.currentPosition - currentPosition;
            RotationAngle = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            if (toPlayer.Length() > 4)
            {
                var direction = Vector2.Normalize(toPlayer);
                var newPosition = currentPosition + direction * Speed * Game1.Time;
                var enemyRectangle = GetRectangleBounds(newPosition);
                var horizontalCheckRect = GetRectangleBounds(new(newPosition.X, currentPosition.Y));
                var verticalCheckRect = GetRectangleBounds(new(currentPosition.X, newPosition.Y));

                foreach (var collider in Map.GetNearestColliders(enemyRectangle))
                {
                    if (horizontalCheckRect.Intersects(collider))
                    {
                        if (direction.X > 0)
                            newPosition.X = collider.Left - (Offset / 20); // отскок влево Ф
                        else
                            newPosition.X = collider.Right + Offset; // отскок вправо Ф
                    }

                    if (verticalCheckRect.Intersects(collider))
                    {
                        if (direction.Y > 0)
                            newPosition.Y = collider.Top - (Offset + 3); // отскок вверх Ф
                        else
                            newPosition.Y = collider.Bottom + Offset; // отскок вниз Ф
                    }
                }
                currentPosition = newPosition;
            }
        }

        public void Update(Player player) => UpdatePosition(player);

        public void Destroy()
        {
            IsAlive = false;
            CoinMethods.GetExperience(currentPosition);
        }
    }
}