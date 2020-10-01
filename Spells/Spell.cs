
namespace JustCombat.Spells
{
    public abstract class Spell
    {
        protected int _rank;
        protected float _castTime;
        protected CooldownTimer _timer;
        protected float _manaCost;
        protected int _damage;

        public abstract void Cast();
    }
}
