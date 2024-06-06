using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewBullet 
    {
        public static void Draw(SpriteBatch _spriteBatch, List<ModelBullet> Bullets)
        {
            foreach (var  bullet in Bullets)
                _spriteBatch.Draw(Bullets[0].Texture, bullet.CurrentPosition, 
                    null, Color.White, bullet.RotationAngle, bullet.CenterRotate, 1, SpriteEffects.None, 1);
        }
    }
}