using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace ShooterMVC
{
    internal class ModelMap 
    {
        public readonly RenderTarget2D Target;
        public const int TileSize = 128;

        public static readonly int[,] Tiles = {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        };

        public int[,] GetTiles() => Tiles;

        public int GetTileSize() => TileSize;

        private static Rectangle[,] Colliders { get; } = new Rectangle[Tiles.GetLength(0), Tiles.GetLength(1)];

        public ModelMap(GraphicsDeviceManager _graphicsDeviceManager)
        {
            Target = new(_graphicsDeviceManager.GraphicsDevice, Tiles.GetLength(1) * TileSize, Tiles.GetLength(0) * TileSize);
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (Tiles[x, y] == 0)
                        continue;
                    var positionX = y * TileSize;
                    var positionY = x * TileSize;
                    Colliders[x, y] = new(positionX, positionY, TileSize, TileSize);
                }
            }
        }

        public static IEnumerable<Rectangle> GetNearestColliders(Rectangle sprite) // В Sprite
        {
            var leftTile = (int)Math.Floor((float)sprite.Left / TileSize);
            var rightTile = (int)Math.Ceiling((float)sprite.Right / TileSize) - 1;
            var topTile = (int)Math.Floor((float)sprite.Top / TileSize);
            var bottomTile = (int)Math.Ceiling((float)sprite.Bottom / TileSize) - 1;

            leftTile = MathHelper.Clamp(leftTile, 0, Tiles.GetLength(1));
            rightTile = MathHelper.Clamp(rightTile, 0, Tiles.GetLength(1));
            topTile = MathHelper.Clamp(topTile, 0, Tiles.GetLength(0));
            bottomTile = MathHelper.Clamp(bottomTile, 0, Tiles.GetLength(0));

            for (int x = topTile; x <= bottomTile; x++)
                for (int y = leftTile; y <= rightTile; y++)
                    if (Tiles[x, y] != 0)
                        yield return Colliders[x, y];
        }
    }
}