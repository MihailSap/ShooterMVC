using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class CoinMethods
    {
        private static List<ModelCoin> coins = new();
        private static Texture2D texture;
        private static SpriteFont spriteFont;
        private static Vector2 textPosition;
        private static string coinsCount;

        public static void Init(Texture2D texture, ContentManager Content)
        {
            CoinMethods.texture = texture;
            spriteFont = Content.Load<SpriteFont>("font");
        }

        public static void Update(Player player, Point Bounds)
        {
            foreach (var experience in coins)
            {
                experience.Update();
                if ((experience.currentPosition - player.currentPosition).Length() < 50)
                {
                    experience.GetCollected();
                    player.GetExperience();
                }
            }

            coins.RemoveAll((experience) => experience.Lifespan <= 0);
            coinsCount = player.Experience.ToString();
            var textWidth = spriteFont.MeasureString(coinsCount).X / 2;
            textPosition = new Vector2(Bounds.X - textWidth - 32, 14);
        }

        public static void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.DrawString(spriteFont, coinsCount, textPosition, Color.Red);
            coins.ForEach(coin => coin.Draw(_spritebatch));
        }

        public static void Reset() => coins.Clear();

        public static void GetExperience(Vector2 position) 
            => coins.Add(new ModelCoin(texture, position));
    }
}
