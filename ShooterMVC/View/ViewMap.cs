using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterMVC
{
    internal class ViewMap
    {
        public static void Draw(SpriteBatch spriteBatch, 
            Texture2D tileTexture,
            RenderTarget2D _target, int[,] tiles, int TileSize)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] == 0)
                        continue;
                    var positionX = y * TileSize;
                    var positionY = x * TileSize;
                    if (tiles[x, y] == 1)
                    {
                        spriteBatch.Draw(tileTexture, new Vector2(positionX, positionY), Color.White);
                    }
                    
                }
            }
            spriteBatch.Draw(_target, Vector2.Zero, Color.White);
        }
    }
}
