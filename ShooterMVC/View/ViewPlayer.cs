using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ViewPlayer
    {
        public static void Draw(ModelPlayer player, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(player.Texture, player.CurrentPosition, 
                null, Color.White, player.RotationAngle, player.CenterRotate, 1, SpriteEffects.None, 1);
        }
    }
}
