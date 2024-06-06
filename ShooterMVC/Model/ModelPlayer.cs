using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ModelPlayer : ModelSprite
    {
        public int maxAmmo = 5;
        public readonly float cooldownBetweenFire; // ???
        public float cooldownBeforeFire; // ???

        public readonly float ReloadTime;
        public bool IsReloading { get; set; } 
        public int AmmoCount { get; set; } 
        public bool IsDead { get; set; }
        public int CoinsCount { get; private set; }

        public ModelPlayer(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            cooldownBetweenFire = 0.25f;
            cooldownBeforeFire = 0f;
            AmmoCount = maxAmmo;
            ReloadTime = 2f;
            IsReloading = false;
        }

        public void GetCoin() => CoinsCount += 1;

        public void Reset(Point Bounds)
        {
            IsDead = false;
            CurrentPosition = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            CoinsCount = 0;
        }
    }
}