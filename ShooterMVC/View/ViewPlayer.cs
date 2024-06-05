using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ViewPlayer
    {
        public static void Draw(ModelPlayer player, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(player._texture, player.currentPosition, 
                null, Color.White, player.RotationAngle, player.centerRotate, 1, SpriteEffects.None, 1);
        }
    }
}
