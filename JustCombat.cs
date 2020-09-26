using JustCombat.Entities;
using JustCombat.Panels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace JustCombat
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JustCombat : Game
    {
        private Player _player;

        private TargetingSystem _targetingSystem;

        public static Texture2D Cursor;
        public static Texture2D[] Cursor2 = new Texture2D[2];
        private SpriteSheet _cursorSheet;
        private Vector2 _cursorPosition;

        private SpriteFont _font;
        private TiledMap _gameMap;
        private TiledMapRenderer _gameMapRenderer;

        public static CharacterPanel CharPanel;
        public static InventoryPanel InvPanel;

        public static Wraith Wraith;

        public static GraphicsDeviceManager graphics;
        public static GameContent gameContent;

        public SpriteBatch spriteBatch;

        public JustCombat()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //this.IsMouseVisible = true;

            base.Initialize();

            _gameMap = gameContent.GameMap;
            _gameMapRenderer = new TiledMapRenderer(GraphicsDevice, _gameMap);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameContent = new GameContent(Content);

            graphics.PreferredBackBufferWidth = Constants.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Constants.SCREEN_HEIGHT;
            graphics.ApplyChanges();

            CharPanel = new CharacterPanel("Character", 20, 20, 220, 440, Color.Wheat);
            InvPanel = new InventoryPanel("Inventory", 960, 440, 220, 220, Color.CornflowerBlue);

            _font = gameContent.GameFont;
            _player = Player.Instance();

            _targetingSystem = TargetingSystem.Instance();

            Wraith = new Wraith("Wraith", 200, 200, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT, Constants.SPRITE_SCALE, new Direction(180.0f));

            _cursorSheet = new SpriteSheet(gameContent.Cursor, 48, 48);

            Cursor = _cursorSheet.GetTexture(0, 0);

            Cursor2[0] = _cursorSheet.GetTexture(0, 0);
            Cursor2[1] = _cursorSheet.GetTexture(1, 0);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            _cursorPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            InputHandler.HandleInput();
            InputHandler.OnMouseHover();

            _gameMapRenderer.Update(gameTime);
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            
            spriteBatch.DrawString(_font, "Level " + _player.GetLevel().ToString(), new Vector2(50.0f, 2.0f), Color.White);
            spriteBatch.DrawString(_font, "Health " + _player.GetHitPoints().ToString() + " / " + _player.GetMaxHitPoints().ToString(), new Vector2(50.0f, 20.0f), Color.White);
            
            spriteBatch.DrawString(_font, "       Player State: " + _player.GetState().ToString(), new Vector2(10.0f, 90.0f), Color.White);
            spriteBatch.DrawString(_font, "HealthBar State: " + _player.GetHealthBar().GetState().ToString(), new Vector2(10.0f, 110.0f), Color.White);
            spriteBatch.DrawString(_font, "            Heading: " + _player.GetDirection().GetHeading().ToString(), new Vector2(10.0f, 130.0f), Color.White);

            _gameMapRenderer.Draw();

            _player.Draw(spriteBatch);

            if (JustCombat.InvPanel.IsDisplayed())
            {
                InvPanel.Draw(spriteBatch);
            }

            if (JustCombat.CharPanel.IsDisplayed())
            {
                CharPanel.Draw(spriteBatch);
            }

            Wraith.Draw(spriteBatch);

            //spriteBatch.Draw(Cursor, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);

            spriteBatch.DrawString(_font, "X: " + _player.GetX() + ", Y: " + _player.GetY(), new Vector2(600.0f, 600.0f), Color.White);

            spriteBatch.DrawString(_font, DateTime.Now.ToString(), new Vector2(600.0f, 620.0f), Color.White);

            spriteBatch.Draw(Cursor, _cursorPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
