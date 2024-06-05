using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewBullet // только отрисовка, контроллер передает
    {
        public static void Draw(SpriteBatch _spriteBatch, List<ModelBullet> Bullets)
        {
            foreach (var  bullet in Bullets)
            {
                _spriteBatch.Draw(bullet._texture, bullet.currentPosition, 
                    null, Color.White, bullet.RotationAngle, bullet.centerRotate, 1, SpriteEffects.None, 1);
            }
        }
    }
}