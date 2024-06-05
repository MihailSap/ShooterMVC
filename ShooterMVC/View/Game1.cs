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

        private ModelMap _map;
        private ModelPlayer _player;

        private Texture2D _texturePlayer; // В отдельный класс GameView или в представлении
        private Texture2D _textureBullet;
        private Texture2D _textureExp;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        protected override void Initialize()
        {
            GetTextures();
            _graphics.ApplyChanges();
            Bounds = new(ModelMap.Tiles.GetLength(1) * ModelMap.TileSize, ModelMap.Tiles.GetLength(0) * ModelMap.TileSize);

            var center = new Vector2(Bounds.X / 2, Bounds.Y / 2);
            _player = new ModelPlayer(_texturePlayer, center);
            _map = new ModelMap(_graphics);

            ModelEnemy.Init(Content);
            ModelBullet.Init(_textureBullet);
            ModelCoin.Init(_textureExp, Content);

            ViewBulletCounter.Init(_textureBullet);

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

            _player.Update(ModelEnemy.EnemyList);
            ControllerPlayer.Update();
            ModelEnemy.Update(_player);
            ModelBullet.Update(ModelEnemy.EnemyList);
            ModelCoin.Update(_player, Bounds);

            if (_player.IsDead)
                Restart();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);
            _spriteBatch.Begin();

            ViewBullet.Draw(_spriteBatch, ModelBullet.Bullets);
            //ViewBulletCounter.Draw(_player, _spriteBatch, Content);
            ViewMap.Draw(_spriteBatch, Content, _map.Target, _map.GetTiles(), _map.GetTileSize());
            ViewEnemy.Draw(_spriteBatch, ModelEnemy.EnemyList);
            ViewPlayer.Draw(_player, _spriteBatch);
            ViewCoin.Draw(_spriteBatch, ModelCoin.spriteFont,
                ModelCoin.coinsCount, ModelCoin.textPosition, ModelCoin.coins);

            ViewBulletCounter.Draw(_player, _spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }


        public void Restart()
        {
            ModelBullet.Reset();
            ModelEnemy.Reset();
            _player.Reset(Bounds);
            ModelCoin.Reset();
        }

        public void GetTextures()
        {
            _texturePlayer = Content.Load<Texture2D>("big-player-rotated");
            _textureBullet = Content.Load<Texture2D>("big-bullet");
            _textureExp = Content.Load<Texture2D>("big-coin");
        }

        private static void UpdateGameTime(GameTime gameTime) => Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}