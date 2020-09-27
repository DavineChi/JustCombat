using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class ActorInfoCard
    {
        protected string _name;
        protected string _level;
        protected HealthBar _healthBar;
        protected ManaBar _manaBar;

        private Vector2 _position = new Vector2(16.0f, 4.0f);
        private Texture2D _card;
        private SpriteFont _font;

        private Actor _actor;

        public ActorInfoCard(Actor actor)
        {
            _actor = actor;

            _card = JustCombat.gameContent.ActorInfoCard;
            _name = actor.GetName();
            _level = actor.GetLevel().ToString();
            _healthBar = new HealthBar(23, 57, 172, 6);
            _manaBar = new ManaBar(23, 69, 172, 6);
            _font = JustCombat.gameContent.GameFont;
        }

        public Texture2D GetTexture()
        {
            return _card;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetLevel()
        {
            return _level;
        }

        public void SetLevel(int level)
        {
            SetLevel(level.ToString());
        }

        public void SetLevel(string level)
        {
            _level = level;
        }

        public HealthBar GetHealthBar()
        {
            return _healthBar;
        }

        public void Update(GameTime gameTime)
        {
            _level = _actor.GetLevel().ToString();

            _healthBar.Update(_actor, gameTime);
            _manaBar.Update(_actor, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string name = _actor.GetName();
            string level = _actor.GetLevel().ToString();
            string health = _actor.GetHitPoints().ToString() + " / " + _actor.GetMaxHitPoints().ToString();

            spriteBatch.Draw(_card, _position, Color.White);
            spriteBatch.DrawString(_font, name, new Vector2(28.0f, 9.0f), Color.White);
            spriteBatch.DrawString(_font, level, new Vector2(29.0f, 33.0f), Color.White);
            spriteBatch.DrawString(_font, health, new Vector2(75.0f, 33.0f), Color.White);

            _healthBar.Draw(spriteBatch);
            _manaBar.Draw(spriteBatch);
        }
    }
}
