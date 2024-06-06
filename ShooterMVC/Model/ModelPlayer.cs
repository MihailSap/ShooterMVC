using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class ModelPlayer : ModelSprite
    {
        public int maxAmmo = 5;
        public readonly float cooldown; 
        public float cooldownLeft; 

        public readonly float ReloadTime;
        public bool IsReloading { get; set; } 
        public int AmmoCount { get; set; } 
        public bool IsDead { get; set; }
        public int Experience { get; private set; }

        public ModelPlayer(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            cooldown = 0.25f;
            cooldownLeft = 0f;
            AmmoCount = maxAmmo;
            ReloadTime = 2f;
            IsReloading = false;
        }

        public void GetExperience() => Experience += 1;

        public void Reset(Point Bounds)
        {
            IsDead = false;
            currentPosition = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            Experience = 0;
        }
    }
}