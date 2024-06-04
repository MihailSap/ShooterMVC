using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class ViewBulletCounter
    {
        public static void Draw(Player player, SpriteBatch spriteBatch, ContentManager Content)
        {
            var bulletTexture = Content.Load<Texture2D>("big-bullet");
            var color = player.IsReloading ? Color.Red : Color.White;
            for (int i = 0; i < player.AmmoCount; i++)
            {
                var position = new Vector2(0, i * bulletTexture.Height * 2);
                spriteBatch.Draw(bulletTexture, position, null, color * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            }
        }
    }
}
