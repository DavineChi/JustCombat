using JustCombat.Entities;
using JustCombat.Panels;
using JustCombat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;

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

        private SpriteFont _frizQuadFont;
        private TiledMap _gameMap;
        private TiledMapRenderer _gameMapRenderer;

        public static CharacterPanel CharPanel;
        public static InventoryPanel InvPanel;

        public static Wraith WraithOne;
        public static Wraith WraithTwo;

        public static UserInterface UserInterface;
        public static TargetingSystem TargetingSystem;

        private InfoPanel _testInfoPanel;

        public static List<Entity> EntityContainer = new List<Entity>();

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

            _frizQuadFont = gameContent.FontFrizQuad;
            _player = Player.Instance();
            
            WraithOne = new Wraith(2, "Wraith One", 200, 200, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT, Constants.SPRITE_SCALE, new Direction(180.0f), 100);
            WraithTwo = new Wraith(3, "Wraith Two", 420, 300, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT, Constants.SPRITE_SCALE, new Direction(180.0f), 100);

            WraithOne.SetAlignment(Actor.Alignment.NEUTRAL);
            WraithTwo.SetAlignment(Actor.Alignment.HOSTILE);

            UserInterface = UserInterface.Instance();

            _cursorSheet = new SpriteSheet(gameContent.Cursor, 48, 48);

            Cursor2[0] = _cursorSheet.GetTexture("glove", 0, 0);
            Cursor2[1] = _cursorSheet.GetTexture("sword", 1, 0);

            Cursor = Cursor2[0];

            _testInfoPanel = new InfoPanel("Wraith", 998, (Constants.SCREEN_HEIGHT - 22), 200, 20, new Color(0.0f, 0.004f, 0.125f, 0.5f));

            TargetingSystem = TargetingSystem.Instance();

            //EntityContainer.Add(Player.Instance());
            EntityContainer.Add(WraithOne);
            EntityContainer.Add(WraithTwo);
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //{
            //    Exit();
            //}
            
            _cursorPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            InputHandler.HandleInput();
            InputHandler.OnMouseHover();

            _gameMapRenderer.Update(gameTime);
            _player.Update(gameTime);

            TargetingSystem.Update(gameTime);

            UserInterface.Update(gameTime);

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

            WraithOne.Draw(spriteBatch);
            WraithTwo.Draw(spriteBatch);

            //spriteBatch.Draw(Cursor, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);

            UserInterface.Draw(spriteBatch);
            
            _testInfoPanel.Draw(spriteBatch);

            spriteBatch.Draw(Cursor, _cursorPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
