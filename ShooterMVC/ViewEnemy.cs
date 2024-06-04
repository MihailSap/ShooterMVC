using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ShooterMVC
{
    internal class ViewEnemy
    {

        public static void Draw(SpriteBatch spriteBatch, List<ModelEnemy> enemyList)
        {
            foreach (var enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
