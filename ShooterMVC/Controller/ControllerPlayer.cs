using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

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

        public static void Update()
        {
            LeftMouseClicked = Mouse.GetState().LeftButton == ButtonState.Pressed;
            RightMouseClicked = Mouse.GetState().RightButton == ButtonState.Pressed 
                && (_lastMouseState.RightButton == ButtonState.Released);

            _lastMouseState = Mouse.GetState();
        }

        public static void RotateToMouse(ModelPlayer player)
        {
            var toMouse = MousePosition - player.currentPosition;
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
                ModelBullet.CreateBullet(Tuple.Create(player.currentPosition, player.RotationAngle));
            }
        }

        public static void Reload(ModelPlayer player)
        {
            if (player.IsReloading)
                return;
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
    }
}