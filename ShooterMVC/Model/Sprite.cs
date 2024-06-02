using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace ShooterMVC
{
    public class Sprite // перенести view
    {
        protected readonly Texture2D _texture;
        protected readonly Vector2 centerRotate;
        public Vector2 currentPosition { get; set; } //
        public int Speed { get; set; } //
        public float RotationAngle { get; set; }
        public float Scale { get; set; } = 1f;

        /*public Rectangle GetRectangleBounds(Vector2 position)
        {
            return new Rectangle(
                (int)(position.X - 20) /* правая граница */ //,
                //(int)(position.Y - 20) /* нижняя граница */,
                //_texture.Width - (20 / 2) /* левая граница */,
                //_texture.Height - (20 / 2) /* верхняя граница */
                //);
        //}*/
        public Rectangle GetRectangleBounds(Vector2 position)
        {
            return new Rectangle(
                (int)(position.X - 20) /* правая граница Ф */,
                (int)(position.Y - 20) /* нижняя граница Ф */,
                _texture.Width - 50 /* левая граница Ф */,
                _texture.Height - 50 /* верхняя граница */
                );
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            currentPosition = position;
            centerRotate = new Vector2(texture.Width / 2, texture.Height / 2);
            Speed = 300;
        }

        public virtual void Draw(SpriteBatch _spriteBatch) //
        {
            _spriteBatch.Draw(_texture, currentPosition, null, Color.White, RotationAngle, centerRotate, 1, SpriteEffects.None, 1);
        }
    }
}