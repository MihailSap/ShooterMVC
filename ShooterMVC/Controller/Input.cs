using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ShooterMVC
{
    internal class Input
    {
        private static MouseState _lastMouseState;
        private static Vector2 Direction;
        public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
        public static bool LeftMouseClicked { get; private set; }
        public static bool RightMouseClicked { get; private set; }

        public static void Update()
        {
            LeftMouseClicked = Mouse.GetState().LeftButton == ButtonState.Pressed;
            RightMouseClicked = Mouse.GetState().RightButton == ButtonState.Pressed 
                && (_lastMouseState.RightButton == ButtonState.Released);

            _lastMouseState = Mouse.GetState();
        }

        public static Vector2 GetPlayerDirection()
        {
            var keyboardState = Keyboard.GetState();
            Direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
                Direction.Y -= 300;
            if (keyboardState.IsKeyDown(Keys.S))
                Direction.Y += 300;
            if (keyboardState.IsKeyDown(Keys.A))
                Direction.X -= 300;
            if (keyboardState.IsKeyDown(Keys.D))
                Direction.X += 300;
            return Direction;
        }
    }
}