using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace ShooterMVC
{
    public class Game1 : Game
    {
        public static float Time { get; private set; }
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private static Point bounds;
        private ModelMap map;
        private ModelPlayer player;

        private Texture2D texturePlayer;
        private Texture2D textureEnemy;
        private Texture2D textureBullet;
        private Texture2D textureCoin;
        private Texture2D textureTile;
        private SpriteFont spriteFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        protected override void Initialize()
        {
            graphics.ApplyChanges();

            SetContent();
            bounds = new(ModelMap.Tiles.GetLength(1) * ModelMap.TileSize, ModelMap.Tiles.GetLength(0) * ModelMap.TileSize);
            var center = new Vector2(bounds.X / 2, bounds.Y / 2);
            player = new ModelPlayer(texturePlayer, center);
            map = new ModelMap(graphics);

            ModelEnemy.SetTextureToModel(textureEnemy);
            ModelBullet.SetTextureToModel(textureBullet);
            ModelCoin.SetTextureToModel(textureCoin, spriteFont);

            base.Initialize();
        }

        protected override void LoadContent() => spriteBatch = new SpriteBatch(GraphicsDevice);

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UpdateGameTime(gameTime);

            ControllerBullet.Update(ModelEnemy.EnemyList, ModelBullet.Bullets);
            ControllerPlayer.Update(ModelEnemy.EnemyList, player);
            ControllerEnemy.Update(ModelEnemy.EnemyList, player);
            ModelCoin.Update(player, bounds);

            if (player.IsDead) Restart();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);
            spriteBatch.Begin();

            ViewMap.Draw(spriteBatch, textureTile, map.Target, map.GetTiles(), map.GetTileSize());
            ViewBullet.Draw(spriteBatch, ModelBullet.Bullets);
            ViewEnemy.Draw(spriteBatch, ModelEnemy.EnemyList);
            ViewBulletCounter.Draw(player, spriteBatch, textureBullet);
            ViewPlayer.Draw(player, spriteBatch);
            ViewCoin.Draw(spriteBatch,
                ModelCoin.coinsCount, ModelCoin.textPosition, ModelCoin.CoinsList, spriteFont);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SetContent()
        {
            texturePlayer = Content.Load<Texture2D>("player");
            textureBullet = Content.Load<Texture2D>("bullet");
            textureEnemy = Content.Load<Texture2D>("enemy");
            textureCoin = Content.Load<Texture2D>("coin");
            textureTile = Content.Load<Texture2D>("tile");
            spriteFont = Content.Load<SpriteFont>("font");
        }

        public void Restart()
        {
            player.Reset(bounds);
            ModelBullet.Reset();
            ModelEnemy.Reset();
            ModelCoin.Reset();
        }

        private static void UpdateGameTime(GameTime gameTime) => Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}