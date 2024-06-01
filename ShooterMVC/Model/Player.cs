using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class Player : Sprite
    {
        private const float Offset = 20f;
        private const int maxAmmo = 5;
        private readonly float cooldown;
        private float cooldownLeft;

        public readonly float ReloadTime;
        public bool IsReloading { get; private set; }
        public int AmmoCount { get; private set; }
        public bool IsDead { get; private set; }
        public int Experience { get; private set; }

        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            cooldown = 0.25f;
            cooldownLeft = 0f;
            AmmoCount = maxAmmo;
            ReloadTime = 2f;
            IsReloading = false;
        }

        public void GetExperience() => Experience += 1;

        private void Reload()
        {
            if (IsReloading)
                return;
            cooldownLeft = ReloadTime;
            IsReloading = true;
            AmmoCount = maxAmmo;
        }

        public void Reset(Point Bounds)
        {
            IsDead = false;
            currentPosition = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            Experience = 0;
        }

        private void CheckCollisionWithEnemies(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive && (currentPosition - enemy.currentPosition).Length() < 25)
                {
                    IsDead = true;
                    break;
                }
            }
        }

        public void Update(List<Enemy> enemy, Point bounds)
        {
            if (cooldownLeft > 0)
                cooldownLeft -= Game1.Time;
            else if (IsReloading)
                IsReloading = false;

            UpdatePosition();
            RotateTowardsMouse();
            Fire();
            CheckCollisionWithEnemies(enemy);

            if (Input.RightMouseClicked)
                Reload();
        }

        private void UpdatePosition()
        {
            var movement = Input.GetPlayerDirection();
            var newPosition = currentPosition + (movement * Game1.Time);
            var playerRectangle = GetRectangleBounds(newPosition);
            var horizontalCheckRect = GetRectangleBounds(new(newPosition.X, currentPosition.Y));
            var verticalCheckRect = GetRectangleBounds(new(currentPosition.X, newPosition.Y));

            foreach (var collider in Map.GetNearestColliders(playerRectangle))
            {
                if (horizontalCheckRect.Intersects(collider))
                {
                    if (movement.X > 0)
                        newPosition.X = collider.Left - Offset;
                    else
                        newPosition.X = collider.Right + Offset;
                }

                if (verticalCheckRect.Intersects(collider))
                {
                    if (movement.Y > 0)
                        newPosition.Y = collider.Top - Offset;
                    else
                        newPosition.Y = collider.Bottom + Offset;
                }
            }
            currentPosition = newPosition;
        }

        private void RotateTowardsMouse()
        {
            var toMouse = Input.MousePosition - currentPosition;
            RotationAngle = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        private void Fire()
        { // Обработка нажатий клавиш отдельно
            if (Input.LeftMouseClicked) // MouseDown - для выстрела при каждом нажатии
            {
                if (cooldownLeft > 0 || IsReloading)
                    return;
                AmmoCount--;
                if (AmmoCount > 0)
                    cooldownLeft = cooldown;
                else
                    Reload();

                var projectileData = new BulletData
                {
                    Position = currentPosition,
                    Rotation = RotationAngle,
                };
                BulletView.AddProjectile(projectileData);
            }
        }
    }
}