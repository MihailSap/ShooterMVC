using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ShooterMVC
{
    public abstract class State
    {
        #region Fields
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;
        #endregion

        #region Methods
        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate(GameTime gameTime);
        public State(ContentManager content, GraphicsDevice graphicsDevice, Game1 game)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
            _game = game;
        }

        #endregion
    }
}
