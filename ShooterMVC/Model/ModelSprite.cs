using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace ShooterMVC
{
    public class ModelSprite
    {
        public readonly Texture2D _texture;
        public readonly Vector2 centerRotate;
        public Vector2 currentPosition { get; set; }
        public int Speed { get; set; }
        public float RotationAngle { get; set; }
        public float Scale { get; set; } = 1f;
        public ModelSprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            currentPosition = position;
            centerRotate = new Vector2(texture.Width / 2, texture.Height / 2);
            Speed = 300;
        }

        public Rectangle GetRectangleBounds(Vector2 position)
        {
            return new Rectangle(
                (int)(position.X - 20),
                (int)(position.Y - 20),
                _texture.Width - 50,
                _texture.Height - 50
                );
        }
    }
}