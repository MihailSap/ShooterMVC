using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewCoin
    {
        public static void Draw(SpriteBatch _spritebatch, SpriteFont spriteFont, 
            string coinsCount, Vector2 textPosition, List<ModelCoin> coins)
        {
            _spritebatch.DrawString(spriteFont, coinsCount, textPosition, Color.Red);
            foreach (var coin in coins)
            {
                _spritebatch.Draw(coin._texture, coin.currentPosition,
                    null, Color.White, coin.RotationAngle, coin.centerRotate, 1, SpriteEffects.None, 1);
            }
        }
    }
}
