/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    internal class GameState : State
    {
        public static float Time { get; private set; }
        private SpriteBatch _spriteBatch;
        private static Point Bounds;
        private Player _player;
        private Texture2D _texturePlayer;
        private Texture2D _textureBullet;
        private Texture2D _textureEnemy;
        private Texture2D _textureExp;

        private Texture2D _textureTile1;
        private Texture2D _textureTile2;
        private Map _map;


        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) : base(content, graphicsDevice, game)
        {
            GetTextures();
            Bounds = new Point(_graphicsDevice.Viewport.Width - 20, _graphicsDevice.Viewport.Height - 20);
            var center = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            _player = new Player(_texturePlayer, center);

            EnemyView.Init(_textureEnemy);
            BulletView.Init(_textureBullet);
            Interface.Init(_textureBullet);
            ExperienceManager.Init(_textureExp, Bounds, _content);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            EnemyView.Draw(_spriteBatch);
            BulletView.Draw(_spriteBatch);
            Interface.Draw(_player, _spriteBatch);
            ExperienceManager.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UpdateGameTime(gameTime);
            _player.Update(EnemyView.EnemyList, Bounds);
            Input.Update();
            EnemyView.Update(_player, Bounds);
            BulletView.Update(EnemyView.EnemyList);
            ExperienceManager.Update(_player, Bounds);
            if (_player.Is_dead)
                Restart();
        }

        public void Restart()
        {
            BulletView.Reset();
            EnemyView.Reset();
            _player.Reset(Bounds);
            ExperienceManager.Reset();
        }

        public void GetTextures()
        {
            _texturePlayer = _content.Load<Texture2D>("player");
            _textureBullet = _content.Load<Texture2D>("bullet");
            _textureEnemy = _content.Load<Texture2D>("enemy");
            _textureExp = _content.Load<Texture2D>("exp");
            _textureTile1 = _content.Load<Texture2D>("tile1");
            _textureTile2 = _content.Load<Texture2D>("tile2");
        }

        private static void UpdateGameTime(GameTime gameTime) => Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}*/
