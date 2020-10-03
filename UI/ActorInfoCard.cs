using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class ActorInfoCard : IDrawable
    {
        protected HealthBar _healthBar;
        protected ManaBar _manaBar;

        private const int FILL_BAR_WIDTH  = 182;
        private const int FILL_BAR_HEIGHT = 8;

        private int _xPosition;
        private int _yPosition;

        private Vector2 _position;
        private Texture2D _card;
        private SpriteFont _font;

        private Actor _actor;

        public ActorInfoCard(Actor actor) :
            this(actor, new Vector2(16.0f, 4.0f))
        {
        }

        public ActorInfoCard(Actor actor, Vector2 position)
        {
            _actor = actor;
            _position = position;

            _xPosition = (int)(position.X);
            _yPosition = (int)(position.Y);

            _card = JustCombat.gameContent.ActorInfoCard;
            _healthBar = new HealthBar((_xPosition + 8), 66, FILL_BAR_WIDTH, FILL_BAR_HEIGHT, actor);
            _manaBar = new ManaBar((_xPosition + 8), 80, FILL_BAR_WIDTH, FILL_BAR_HEIGHT, actor);
            _font = JustCombat.gameContent.FontConsolas13;
        }

        public HealthBar GetHealthBar()
        {
            return _healthBar;
        }

        public void Update(GameTime gameTime)
        {
            _healthBar.Update(gameTime);
            _manaBar.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string name = _actor.GetName();
            string level = _actor.GetLevel().ToString();
            string health = _actor.GetHitPoints().ToString() + " / " + _actor.GetMaxHitPoints().ToString();

            spriteBatch.Draw(_card, _position, Color.White);

            spriteBatch.DrawString(_font, name, new Vector2((_xPosition + 14.0f), (13.0f)), Color.White);
            spriteBatch.DrawString(_font, level, new Vector2((_xPosition + 13.0f), (40.0f)), Color.White);
            spriteBatch.DrawString(_font, health, new Vector2((_xPosition + 59.0f), (40.0f)), Color.White);

            _healthBar.Draw(spriteBatch);
            _manaBar.Draw(spriteBatch);
        }
    }
}
