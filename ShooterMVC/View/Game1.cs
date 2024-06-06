using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using ShooterMVC.Controller;

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
            SetTextures();
            bounds = new(ModelMap.Tiles.GetLength(1) * ModelMap.TileSize, ModelMap.Tiles.GetLength(0) * ModelMap.TileSize);
            var center = new Vector2(bounds.X / 2, bounds.Y / 2);
            player = new ModelPlayer(texturePlayer, center);
            map = new ModelMap(graphics);

            ModelEnemy.SetTextureToModel(textureEnemy);
            ModelBullet.SetTextureToModel(textureBullet);
            ModelCoin.SetTextureToModel(Content);

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

            ViewMap.Draw(spriteBatch, Content, map.Target, map.GetTiles(), map.GetTileSize());
            ViewBullet.Draw(spriteBatch, ModelBullet.Bullets, Content);
            ViewEnemy.Draw(spriteBatch, ModelEnemy.EnemyList);
            ViewBulletCounter.Draw(player, spriteBatch, textureBullet);
            ViewPlayer.Draw(player, spriteBatch);
            ViewCoin.Draw(spriteBatch,
                ModelCoin.coinsCount, ModelCoin.textPosition, ModelCoin.CoinsList, Content);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SetTextures()
        {
            texturePlayer = Content.Load<Texture2D>("big-player-rotated");
            textureEnemy = Content.Load<Texture2D>("big-enemy");
            textureBullet = Content.Load<Texture2D>("big-bullet");
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