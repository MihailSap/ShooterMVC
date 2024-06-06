using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    public class ModelSprite
    {
        public Vector2 CurrentPosition { get; set; }
        public int Speed { get; set; }
        public float RotationAngle { get; set; }
        public float Scale { get; set; } = 1f;
        public readonly Vector2 CenterRotate;
        public Texture2D Texture;
        public ModelSprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            CurrentPosition = position;
            CenterRotate = new Vector2(texture.Width / 2, texture.Height / 2);
            Speed = 300;
        }

        public Rectangle GetRectangleBounds(Vector2 position)
        {
            return new Rectangle(
                (int)(position.X - 20),
                (int)(position.Y - 20),
                Texture.Width - 50,
                Texture.Height - 50
                );
        }
    }
}