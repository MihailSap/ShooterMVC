using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class ModelCoin(Texture2D tex, Vector2 pos) : Sprite(tex, pos) // Сразу в Model
    {
        public float Lifespan { get; private set; } = 5f;

        public void Update()
        {
            Lifespan -= Game1.Time;
            Scale = 0.33f + (Lifespan / 5f * 0.66f);
        }

        public void GetCollected() => Lifespan = 0;
    }
}
