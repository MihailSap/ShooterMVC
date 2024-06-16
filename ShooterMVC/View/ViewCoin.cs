using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewCoin
    {
        public static void Draw(SpriteBatch _spritebatch, 
            string coinsCount, Vector2 textPosition, List<ModelCoin> coins, SpriteFont spriteFont)
        {
            _spritebatch.DrawString(spriteFont, coinsCount, textPosition, Color.Black);
            foreach (var coin in coins)
                _spritebatch.Draw(coin.Texture, coin.CurrentPosition,
                    null, Color.White, coin.RotationAngle, coin.CenterRotate, 1, SpriteEffects.None, 1);
        }
    }
}
