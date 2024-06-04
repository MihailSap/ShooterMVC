using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class Player : Sprite // В Model
    {
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

        private void Reload() // В Controller?
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
                if (enemy.IsAlive && (currentPosition - enemy.currentPosition).Length() < 60)
                {
                    IsDead = true;
                    break;
                }
            }
        }

        public void Update(List<Enemy> enemy)
        {
            if (cooldownLeft > 0)
                cooldownLeft -= Game1.Time;
            else if (IsReloading)
                IsReloading = false;

            MoveWithCollisions();
            RotateTowardsMouse();
            Fire();
            CheckCollisionWithEnemies(enemy);

            if (Input.RightMouseClicked)
                Reload();
        }

        private void MoveWithCollisions()
        {
            var movement = Input.GetPlayerDirection();
            var newPosition = currentPosition + (movement * Game1.Time);
            var playerRectangle = GetRectangleBounds(newPosition);
            var horizontalCheckRect = GetRectangleBounds(new(newPosition.X, currentPosition.Y));
            var verticalCheckRect = GetRectangleBounds(new(currentPosition.X, newPosition.Y));

            foreach (var collider in ModelMap.GetNearestColliders(playerRectangle))
            {
                if (horizontalCheckRect.Intersects(collider))
                {
                    if (movement.X > 0)
                        newPosition.X = collider.Left - 25; /* отскок влево Ф */
                    else
                        newPosition.X = collider.Right + 20; /* отскок вправо Ф */
                }

                if (verticalCheckRect.Intersects(collider))
                {
                    if (movement.Y > 0)
                        newPosition.Y = collider.Top - 30; /* отскок вверх Ф */
                    else
                        newPosition.Y = collider.Bottom + 25; /* отскок вниз Ф */
                }
            }
            currentPosition = newPosition;
        }

        private void RotateTowardsMouse() // В Controller
        {
            var toMouse = Input.MousePosition - currentPosition;
            RotationAngle = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        private void Fire() // В Controller
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
                ModelBullet.CreateBullet(Tuple.Create(currentPosition, RotationAngle));
            }
        }
    }
}