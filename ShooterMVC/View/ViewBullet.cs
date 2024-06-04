using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewBullet // только отрисовка, контроллер передает
    {
        public static void Draw(SpriteBatch _spriteBatch, List<ModelBullet> Bullets) // View
            => Bullets.ForEach((projectile) => projectile.Draw(_spriteBatch));
    }
}