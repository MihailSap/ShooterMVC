using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterMVC
{
    public class Game1 : Game
    {
        public static float Time { get; private set; }

        private GraphicsDeviceManager _graphics; // В отдельный класс GameView
        private SpriteBatch _spriteBatch;

        private static Point Bounds;

        private Map _map;
        private Player _player;

        private Texture2D _texturePlayer; // В отдельный класс GameView или в представлении
        private Texture2D _textureBullet;
        private Texture2D _textureEnemy;
        private Texture2D _textureExp;
        private Texture2D _textureTile1;
        private Texture2D _textureTile2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //_graphics.IsFullScreen = true; // Вывод во весь экран
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        protected override void Initialize()
        {
            GetTextures();
            _graphics.ApplyChanges();
            Bounds = new(Map.tiles.GetLength(1) * Map.TileSize, Map.tiles.GetLength(0) * Map.TileSize);

            var center = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            _player = new Player(_texturePlayer, center);
            _map = new Map(_graphics);

            EnemyView.Init(_textureEnemy);
            BulletView.Init(_textureBullet);
            BulletInterface.Init(_textureBullet);
            CoinMethods.Init(_textureExp, Bounds, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UpdateGameTime(gameTime);
            _player.Update(EnemyView.EnemyList, Bounds);
            Input.Update();
            EnemyView.Update(_player, Bounds);
            BulletView.Update(EnemyView.EnemyList);
            CoinMethods.Update(_player, Bounds);
            if (_player.IsDead)
                Restart();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);
            _spriteBatch.Begin();
            _map.Draw(_spriteBatch, _textureTile1, _textureTile2);
            _player.Draw(_spriteBatch);
            EnemyView.Draw(_spriteBatch);
            BulletView.Draw(_spriteBatch);
            BulletInterface.Draw(_player, _spriteBatch);
            CoinMethods.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        public void Restart()
        {
            BulletView.Reset();
            EnemyView.Reset();
            _player.Reset(Bounds);
            CoinMethods.Reset();
        }

        public void GetTextures()
        {
            _texturePlayer = Content.Load<Texture2D>("big-player-rotated");
            _textureBullet = Content.Load<Texture2D>("big-bullet");
            _textureEnemy = Content.Load<Texture2D>("big-enemy");
            _textureExp = Content.Load<Texture2D>("big-coin");
            _textureTile1 = Content.Load<Texture2D>("tile11");
            _textureTile2 = Content.Load<Texture2D>("tile22");
        }

        private static void UpdateGameTime(GameTime gameTime) => Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}