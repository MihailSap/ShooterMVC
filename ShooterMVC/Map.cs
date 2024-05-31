using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace ShooterMVC
{
    internal class Map
    {
        private readonly RenderTarget2D _target;
        public static readonly int TileSize = 128;

        public static readonly int[,] tiles = {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        };

        private static Rectangle[,] Colliders { get; } = new Rectangle[tiles.GetLength(0), tiles.GetLength(1)];

        public Map(GraphicsDeviceManager _graphicsDeviceManager)
        {
            _target = new(_graphicsDeviceManager.GraphicsDevice, tiles.GetLength(1) * TileSize, tiles.GetLength(0) * TileSize);
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] == 0)
                        continue;
                    var positionX = y * TileSize;
                    var positionY = x * TileSize;
                    Colliders[x, y] = new(positionX, positionY, TileSize, TileSize);
                }
            }
        }

        public static IEnumerable<Rectangle> GetNearestColliders(Rectangle sprite)
        {
            var leftTile = (int)Math.Floor((float)sprite.Left / TileSize);
            var rightTile = (int)Math.Ceiling((float)sprite.Right / TileSize) - 1;
            var topTile = (int)Math.Floor((float)sprite.Top / TileSize);
            var bottomTile = (int)Math.Ceiling((float)sprite.Bottom / TileSize) - 1;

            leftTile = MathHelper.Clamp(leftTile, 0, tiles.GetLength(1));
            rightTile = MathHelper.Clamp(rightTile, 0, tiles.GetLength(1));
            topTile = MathHelper.Clamp(topTile, 0, tiles.GetLength(0));
            bottomTile = MathHelper.Clamp(bottomTile, 0, tiles.GetLength(0));

            for (int x = topTile; x <= bottomTile; x++)
                for (int y = leftTile; y <= rightTile; y++)
                    if (tiles[x, y] != 0)
                        yield return Colliders[x, y];
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture1, Texture2D texture2)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] == 0)
                        continue;
                    var positionX = y * TileSize;
                    var positionY = x * TileSize;
                    var currentTexture = tiles[x, y] == 1 ? texture1 : texture2;
                    spriteBatch.Draw(currentTexture, new Vector2(positionX, positionY), Color.White);
                }
            }
            spriteBatch.Draw(_target, Vector2.Zero, Color.White);
        }
    }
}