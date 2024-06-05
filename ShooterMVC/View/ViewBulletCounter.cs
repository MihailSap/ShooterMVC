using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ViewBulletCounter
    {
        private static Texture2D _bulletTexture;

        public static void Init(Texture2D tex) => _bulletTexture = tex;

        public static void Draw(ModelPlayer player, SpriteBatch spriteBatch)
        {
            var bulletsColor = player.IsReloading ? Color.Red : Color.White;
            for (int i = 0; i < player.AmmoCount; i++)
            {
                var position = new Vector2(0, i * _bulletTexture.Height * 2);
                spriteBatch.Draw(_bulletTexture, position, null, bulletsColor * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            }
        }
    }
}
