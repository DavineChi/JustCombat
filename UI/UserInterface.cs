using JustCombat.Entities;
using JustCombat.Panels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JustCombat.UI
{
    public class UserInterface
    {
        private static UserInterface _userInterface = null;

        private Player _player;
        private Actor _target;
        private Texture2D _topBarBackpanel;
        private ActorInfoCard _playerInfoCard;
        private ActorInfoCard _targetInfoCard;
        private InfoPanel _cursorInfoPanel;
        private SpriteFont _fontConsolas13;
        private bool _debug;

        //public CooldownTimer CoolDownTimer = new CooldownTimer();

        private UserInterface()
        {
            _player = Player.Instance();
            _topBarBackpanel = JustCombat.GameContent.TopBarBackpanel;
            _playerInfoCard = new ActorInfoCard(_player, new Vector2(16.0f, 4.0f));
            _targetInfoCard = new ActorInfoCard(null);
            _cursorInfoPanel = new InfoPanel("", 998, (Constants.SCREEN_HEIGHT - 26), 200, 24, new Color(0.0f, 0.004f, 0.125f, 0.5f), true);
            _fontConsolas13 = JustCombat.GameContent.FontConsolas13;
            _debug = false;
        }

        public static UserInterface Instance()
        {
            if (_userInterface == null)
            {
                _userInterface = new UserInterface();
            }

            return _userInterface;
        }

        public void ClearTarget()
        {
            _targetInfoCard.SetActor(null);
        }

        public ActorInfoCard GetTargetInfoCard()
        {
            return _targetInfoCard;
        }

        public InfoPanel GetCursorInfoPanel()
        {
            return _cursorInfoPanel;
        }

        public bool InDebugMode()
        {
            return _debug;
        }

        public void SetDebug(bool debug)
        {
            _debug = debug;
        }

        public void Update(GameTime gameTime)
        {
            _target = (Actor)(JustCombat.TargetingSystem.GetCurrentTarget());

            _playerInfoCard.Update(gameTime);

            if (_target != null)
            {
                _targetInfoCard.Update(gameTime);
            }

            //CoolDownTimer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Collision.BoundingBox bb = Player.Instance().GetBoundingBox();
            bool intersectsResult = !(bb.Intersects(JustCombat.MapTransformBounds));
            
            MouseState mouseState = Mouse.GetState();

            int screenMouseX = mouseState.X;
            int screenMouseY = mouseState.Y;

            Vector2 mouseVectorScreen = new Vector2(screenMouseX, screenMouseY);
            Vector2 mouseVectorWorld = JustCombat.WorldCamera.ScreenToWorld(mouseVectorScreen);

            int worldMouseX = (int)(mouseVectorWorld.X);
            int worldMouseY = (int)(mouseVectorWorld.Y);

            string target = JustCombat.TargetingSystem.ToString();

            spriteBatch.Draw(_topBarBackpanel, new Vector2(0.0f, 0.0f), Color.White);

            _playerInfoCard.Draw(spriteBatch);

            if (_target != null)
            {
                _targetInfoCard.Draw(spriteBatch);
            }

            if (_cursorInfoPanel.IsDisplayed())
            {
                _cursorInfoPanel.Draw(spriteBatch);
            }

            if (_debug)
            {
                spriteBatch.DrawString(_fontConsolas13, DateTime.Now.ToString(), new Vector2(500.0f, 10.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "X: " + _player.GetX() + ", Y: " + _player.GetY(), new Vector2(500.0f, 30.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "Camera: " + intersectsResult, new Vector2(500.0f, 50.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, target, new Vector2(500.0f, 70.0f), Color.White);

                //spriteBatch.DrawString(_font, "Health " + _player.GetHitPoints().ToString() + " / " + _player.GetMaxHitPoints().ToString(), new Vector2(744.0f, 6.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "   Player State: " + _player.GetState().ToString(), new Vector2(750.0f, 10.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "HealthBar State: " + _player.GetHealthBarState().ToString(), new Vector2(750.0f, 30.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "        Heading: " + _player.GetDirection().GetHeading().ToString(), new Vector2(750.0f, 50.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "  Taking Damage: " + _player._takingDamage.ToString(), new Vector2(750.0f, 70.0f), Color.White);

                //spriteBatch.DrawString(_font, "CD: " + CoolDownTimer.ToString(), new Vector2(550.0f, 50.0f), Color.White);
                
                spriteBatch.DrawString(_fontConsolas13, "Screen Mouse: (" + screenMouseX + "," + screenMouseY + ")", new Vector2(screenMouseX + 50, screenMouseY + 50), Color.White);
                spriteBatch.DrawString(_fontConsolas13, " World Mouse: (" + worldMouseX + "," + worldMouseY + ")", new Vector2(screenMouseX + 50, screenMouseY + 70), Color.White);
            }
        }
    }
}
