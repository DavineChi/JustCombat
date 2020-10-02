using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JustCombat.UI
{
    public class UserInterface
    {
        private static UserInterface _userInterface = null;

        private Player _player;
        private Texture2D _topBarBackpanel;
        private ActorInfoCard _playerInfoCard;
        private ActorInfoCard _targetInfoCard;
        private SpriteFont _font;

        //public CooldownTimer CoolDownTimer = new CooldownTimer();

        protected UserInterface()
        {
            _player = Player.Instance();
            _topBarBackpanel = JustCombat.gameContent.TopBarBackpanel;
            _playerInfoCard = new ActorInfoCard(_player);
            _font = JustCombat.gameContent.FontConsolas13;
        }

        public static UserInterface Instance()
        {
            if (_userInterface == null)
            {
                _userInterface = new UserInterface();
            }

            return _userInterface;
        }

        public ActorInfoCard GetPlayerInfoCard()
        {
            return _playerInfoCard;
        }

        public void Update(GameTime gameTime)
        {
            _playerInfoCard.Update(gameTime);

            //CoolDownTimer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string target = JustCombat.TargetingSystem.ToString();

            spriteBatch.Draw(_topBarBackpanel, new Vector2(0.0f, 0.0f), Color.White);

            _playerInfoCard.Draw(spriteBatch);

            JustCombat.TargetingSystem.Draw(spriteBatch);

            spriteBatch.DrawString(_font, DateTime.Now.ToString(), new Vector2(550.0f, 10.0f), Color.White);
            spriteBatch.DrawString(_font, "X: " + _player.GetX() + ", Y: " + _player.GetY(), new Vector2(550.0f, 30.0f), Color.White);

            //spriteBatch.DrawString(_font, "Health " + _player.GetHitPoints().ToString() + " / " + _player.GetMaxHitPoints().ToString(), new Vector2(744.0f, 6.0f), Color.White);
            spriteBatch.DrawString(_font, "   Player State: " + _player.GetState().ToString(), new Vector2(800.0f, 10.0f), Color.White);
            spriteBatch.DrawString(_font, "HealthBar State: " + _playerInfoCard.GetHealthBar().GetState().ToString(), new Vector2(800.0f, 30.0f), Color.White);
            spriteBatch.DrawString(_font, "        Heading: " + _player.GetDirection().GetHeading().ToString(), new Vector2(800.0f, 50.0f), Color.White);

            //spriteBatch.DrawString(_font, "CD: " + CoolDownTimer.ToString(), new Vector2(550.0f, 50.0f), Color.White);
            spriteBatch.DrawString(_font, target, new Vector2(550.0f, 70.0f), Color.White);
        }
    }
}
