using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ModelCoin(Texture2D tex, Vector2 pos) : ModelSprite(tex, pos)
    {
        public float Lifespan { get; private set; } = 5f;

        public static List<ModelCoin> coins = new();
        public static Texture2D texture;
        public static SpriteFont spriteFont;
        public static Vector2 textPosition;
        public static string coinsCount;

        public static void Init(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("big-coin"); 
            spriteFont = Content.Load<SpriteFont>("font");
        }

        public static void Reset() => coins.Clear();

        public void GetCollected() => Lifespan = 0;

        public static void GetExperience(Vector2 position)
            => coins.Add(new ModelCoin(texture, position));

        public static void Update(ModelPlayer player, Point Bounds)
        {
            foreach (var experience in coins)
            {
                experience.Lifespan -= Game1.Time;
                experience.Scale = 0.33f + (experience.Lifespan / 5f * 0.66f);

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
    }
}
