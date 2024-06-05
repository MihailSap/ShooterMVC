using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ViewPlayer
    {
        public static void Draw(ModelPlayer player, SpriteBatch _spriteBatch, ContentManager Content)
        {
            var playerTexture = Content.Load<Texture2D>("big-player-rotated");
            _spriteBatch.Draw(playerTexture, player.currentPosition, 
                null, Color.White, player.RotationAngle, player.centerRotate, 1, SpriteEffects.None, 1);
        }
    }
}
