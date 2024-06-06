using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ControllerPlayer
    {
        private static MouseState _lastMouseState;
        private static Vector2 Direction;
        public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
        public static bool LeftMouseClicked { get; private set; }
        public static bool RightMouseClicked { get; private set; }
        private const float Speed = 500;

        public static void MoveWithCollisions(ModelPlayer player)
        {
            var movement = GetPlayerDirection();
            var newPosition = player.CurrentPosition + (movement * Game1.Time);
            var playerRectangle = player.GetRectangleBounds(newPosition);
            var horizontalCheckRect = player.GetRectangleBounds(new(newPosition.X, player.CurrentPosition.Y));
            var verticalCheckRect = player.GetRectangleBounds(new(player.CurrentPosition.X, newPosition.Y));

            foreach (var collider in ModelMap.GetNearestColliders(playerRectangle))
            {
                if (horizontalCheckRect.Intersects(collider))
                    newPosition.X = movement.X > 0 ? collider.Left - 25 : collider.Right + 20;
                if (verticalCheckRect.Intersects(collider))
                    newPosition.Y = movement.Y > 0 ? collider.Top - 30 : collider.Bottom + 25;
            }
            player.CurrentPosition = newPosition;
        }

        public static void CheckCollisionWithEnemies(List<ModelEnemy> enemies, ModelPlayer player)
        {
            foreach (var enemy in enemies)
                if (enemy.IsAlive && (player.CurrentPosition - enemy.CurrentPosition).Length() < 60)
                    player.IsDead = true;
        }

        public static void RotateToMouse(ModelPlayer player)
        {
            var toMouse = MousePosition - player.CurrentPosition;
            player.RotationAngle = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        public static void Fire(ModelPlayer player)
        { 
            if (LeftMouseClicked)
            {
                if (player.cooldownLeft > 0 || player.IsReloading)
                    return;
                player.AmmoCount--;
                if (player.AmmoCount > 0)
                    player.cooldownLeft = player.cooldown;
                else
                    Reload(player);

                ModelBullet.CreateBullet(Tuple.Create(player.CurrentPosition, player.RotationAngle));
            }
        }

        public static void Reload(ModelPlayer player)
        {
            if (player.IsReloading) return;
            player.cooldownLeft = player.ReloadTime;
            player.IsReloading = true;
            player.AmmoCount = player.maxAmmo;
        }

        public static Vector2 GetPlayerDirection()
        {
            var keyboardState = Keyboard.GetState();
            Direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
                Direction.Y -= Speed;
            if (keyboardState.IsKeyDown(Keys.S))
                Direction.Y += Speed;
            if (keyboardState.IsKeyDown(Keys.A))
                Direction.X -= Speed;
            if (keyboardState.IsKeyDown(Keys.D))
                Direction.X += Speed;
            return Direction;
        }

        public static void GetMouseState()
        {
            var mouseState = Mouse.GetState();
            LeftMouseClicked = mouseState.LeftButton == ButtonState.Pressed;
            RightMouseClicked = mouseState.RightButton == ButtonState.Pressed
                && (_lastMouseState.RightButton == ButtonState.Released);
            _lastMouseState = mouseState;
        }

        public static void Update(List<ModelEnemy> enemy, ModelPlayer player)
        {
            if (player.cooldownLeft > 0)
                player.cooldownLeft -= Game1.Time;
            else if (player.IsReloading)
                player.IsReloading = false;

            GetMouseState();
            MoveWithCollisions(player);
            CheckCollisionWithEnemies(enemy, player);
            RotateToMouse(player);
            Fire(player);
            if (RightMouseClicked)
                Reload(player);
        }
    }
}