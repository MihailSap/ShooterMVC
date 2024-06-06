using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ViewBulletCounter
    {
        public static void Draw(ModelPlayer player, SpriteBatch spriteBatch, Texture2D texture)
        {
            var bulletsColor = player.IsReloading ? Color.Red : Color.White;
            for (int i = 0; i < player.AmmoCount; i++)
            {
                var position = new Vector2(0, i * texture.Height * 2);
                spriteBatch.Draw(texture, position, null, bulletsColor * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            }
        }
    }
}
