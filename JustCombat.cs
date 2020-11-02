using JustCombat.Collision;
using JustCombat.Entities;
using JustCombat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;

namespace JustCombat
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JustCombat : Game
    {
        private Player _player;
        
        private SpriteBatch _spriteBatch;
        private SpriteFont _frizQuadFont;
        private TiledMap _gameMap;

        public static Matrix TransformMatrix;
        
        public static TiledMapRenderer GameMapRenderer;
        public static CollisionBox ObstacleTest;
        public static OrthographicCamera WorldCamera;
        public static Wraith WraithOne;
        public static Wraith WraithTwo;
        public static UserInterface UserInterface;
        public static TargetingSystem TargetingSystem;
        public static List<Entity> EntityContainer = new List<Entity>();
        public static GraphicsDeviceManager GraphicsManager;
        public static GameContent GameContent;

        public JustCombat()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
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
            // Add your initialization logic here

            //this.IsMouseVisible = true;
            Window.Title = "Just Combat";

            base.Initialize();

            _gameMap = GameContent.GameMap;
            GameMapRenderer = new TiledMapRenderer(GraphicsDevice, _gameMap);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Use this.Content to load your game content here
            GameContent = new GameContent(Content);

            GraphicsManager.PreferredBackBufferWidth = Constants.SCREEN_WIDTH;
            GraphicsManager.PreferredBackBufferHeight = Constants.SCREEN_HEIGHT;
            GraphicsManager.ApplyChanges();

            _frizQuadFont = GameContent.FontFrizQuad;
            _player = Player.Instance();
            
            WraithOne = new Wraith(2, "Bane Fang the Mad", 200, 200, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT, Constants.SPRITE_SCALE, new Direction(180.0f), 30);
            WraithTwo = new Wraith(3, "Snot Maggot", 420, 300, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT, Constants.SPRITE_SCALE, new Direction(180.0f), 100);

            WraithOne.SetAlignment(Actor.Alignment.NEUTRAL);
            WraithTwo.SetAlignment(Actor.Alignment.HOSTILE);

            UserInterface = UserInterface.Instance();
            TargetingSystem = TargetingSystem.Instance();

            //EntityContainer.Add(Player.Instance());
            EntityContainer.Add(WraithOne);
            EntityContainer.Add(WraithTwo);

            //ObstacleTest = new CollisionBox(590, 390, 50, 50);
            //ObstacleTest.GetPrimRectangle().SetColor(Color.Red);

            WorldCamera = new OrthographicCamera(GraphicsDevice);

            TransformMatrix = WorldCamera.GetViewMatrix();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
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
            
            InputHandler.HandleInput();
            InputHandler.OnMouseHover();

            GameMapRenderer.Update(gameTime);
            _player.Update(gameTime);

            WraithOne.Update(gameTime);
            WraithTwo.Update(gameTime);

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

            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, TransformMatrix);

                DrawOrderManager.Draw(_spriteBatch);

                _spriteBatch.End();
            }

            {
                // Draw UserInterface with default (null) transformation matrix.
                _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

                JustCombat.UserInterface.Draw(_spriteBatch);

                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
