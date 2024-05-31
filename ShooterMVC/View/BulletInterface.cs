using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class BulletInterface
    {
        private static Texture2D bulletTexture;
        public static void Init(Texture2D texture) => bulletTexture = texture;

        public static void Draw(Player player, SpriteBatch spriteBatch)
        {
            var color = player.IsReloading ? Color.Red : Color.White;
            for (int i = 0; i < player.AmmoCount; i++)
            {
                var position = new Vector2(0, i * bulletTexture.Height * 2);
                spriteBatch.Draw(bulletTexture, position, null, color * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            }
        }
    }
}
