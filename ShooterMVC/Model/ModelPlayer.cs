using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class ModelPlayer : ModelSprite
    {
        public int maxAmmo = 5; // 
        public readonly float cooldown; // 
        public float cooldownLeft; // 

        public readonly float ReloadTime;
        public bool IsReloading { get; set; } // 
        public int AmmoCount { get; set; } // 
        public bool IsDead { get; private set; }
        public int Experience { get; private set; }

        public ModelPlayer(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            cooldown = 0.25f;
            cooldownLeft = 0f;
            AmmoCount = maxAmmo;
            ReloadTime = 2f;
            IsReloading = false;
        }

        public void GetExperience() => Experience += 1;

        public void Reset(Point Bounds)
        {
            IsDead = false;
            currentPosition = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            Experience = 0;
        }

        private void CheckCollisionWithEnemies(List<ModelEnemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive && (currentPosition - enemy.currentPosition).Length() < 60)
                {
                    IsDead = true;
                    break;
                }
            }
        }

        private void MoveWithCollisions()
        {
            var movement = ControllerPlayer.GetPlayerDirection();
            var newPosition = currentPosition + (movement * Game1.Time);
            var playerRectangle = GetRectangleBounds(newPosition);
            var horizontalCheckRect = GetRectangleBounds(new(newPosition.X, currentPosition.Y));
            var verticalCheckRect = GetRectangleBounds(new(currentPosition.X, newPosition.Y));

            foreach (var collider in ModelMap.GetNearestColliders(playerRectangle))
            {
                if (horizontalCheckRect.Intersects(collider))
                {
                    if (movement.X > 0)
                        newPosition.X = collider.Left - 25;
                    else
                        newPosition.X = collider.Right + 20;
                }

                if (verticalCheckRect.Intersects(collider))
                {
                    if (movement.Y > 0)
                        newPosition.Y = collider.Top - 30;
                    else
                        newPosition.Y = collider.Bottom + 25;
                }
            }
            currentPosition = newPosition;
        }

        public void Update(List<ModelEnemy> enemy)
        {
            if (cooldownLeft > 0)
                cooldownLeft -= Game1.Time;
            else if (IsReloading)
                IsReloading = false;

            MoveWithCollisions();
            CheckCollisionWithEnemies(enemy);

            ControllerPlayer.RotateToMouse(this);
            ControllerPlayer.Fire(this);
            if (ControllerPlayer.RightMouseClicked)
                ControllerPlayer.Reload(this);
        }
    }
}