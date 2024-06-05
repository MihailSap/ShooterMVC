using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewCoin
    {
        public static void Draw(SpriteBatch _spritebatch, 
            string coinsCount, Vector2 textPosition, List<ModelCoin> coins, ContentManager Content)
        {
            var textureCoin = Content.Load<Texture2D>("big-coin");
            var spriteFontCoin = Content.Load<SpriteFont>("font");
            _spritebatch.DrawString(spriteFontCoin, coinsCount, textPosition, Color.Red);
            foreach (var coin in coins)
            {
                _spritebatch.Draw(textureCoin, coin.currentPosition,
                    null, Color.White, coin.RotationAngle, coin.centerRotate, 1, SpriteEffects.None, 1);
            }
        }
    }
}
