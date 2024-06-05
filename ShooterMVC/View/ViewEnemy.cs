using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ShooterMVC
{
    internal class ViewEnemy
    {
        public static void Draw(SpriteBatch _spriteBatch, List<ModelEnemy> EnemyList)
        {
            foreach (var enemy in EnemyList)
            {
                _spriteBatch.Draw(enemy._texture, enemy.currentPosition,
                    null, Color.White, enemy.RotationAngle, enemy.centerRotate, 1, SpriteEffects.None, 1);
            }
        }
    }
}
