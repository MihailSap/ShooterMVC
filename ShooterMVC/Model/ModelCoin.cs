using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ModelCoin(Texture2D tex, Vector2 pos) : ModelSprite(tex, pos)
    {
        public float Lifespan = 5f;
        public static List<ModelCoin> CoinsList = new();
        public static SpriteFont spriteFont;
        public static Vector2 textPosition;
        public static string coinsCount;
        public static Texture2D texture;

        public static void SetTextureToModel(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("big-coin"); 
            spriteFont = Content.Load<SpriteFont>("font");
        }

        public static void Reset() => CoinsList.Clear();

        public void GetCollected() => Lifespan = 0;

        public static void GetExperience(Vector2 position)
            => CoinsList.Add(new ModelCoin(texture, position));

        public static void Update(ModelPlayer player, Point Bounds)
        {
            foreach (var experience in CoinsList)
            {
                experience.Lifespan -= Game1.Time;
                experience.Scale = 0.33f + (experience.Lifespan / 5f * 0.66f);

                if ((experience.CurrentPosition - player.CurrentPosition).Length() < 50)
                {
                    experience.GetCollected();
                    player.GetExperience();
                }
            }

            CoinsList.RemoveAll((experience) => experience.Lifespan <= 0);
            coinsCount = player.Experience.ToString();
            var textWidth = spriteFont.MeasureString(coinsCount).X / 2;
            textPosition = new Vector2(Bounds.X - textWidth - 32, 14);
        }
    }
}
