using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class Cell
    {
        public Vector2 Position { get; set; }
        public Cell Parent { get; set; }

        public Cell(Vector2 position)
        {
            Position = position;
            Parent = null;
        }
    }
}
