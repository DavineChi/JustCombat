using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace JustCombat
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JustCombat : Game
    {
        private Player _player;

        public static GraphicsDeviceManager graphics;
        public static GameContent gameContent;

        SpriteBatch spriteBatch;

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

            base.Initialize();
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

            _player = Player.Instance();
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

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _player.WalkNorth();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _player.WalkEast();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _player.WalkSouth();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _player.WalkWest();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                Teleport();
            }

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

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            spriteBatch.DrawString(gameContent.LabelFont, _player.GetDirection().GetHeading().ToString(), new Vector2(10.0f, 10.0f), Color.White);
            _player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Teleport()
        {
            _player.SetX(300.0f);
            _player.SetY(300.0f);
        }
    }
}
