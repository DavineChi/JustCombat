using System;

namespace JustCombat.Spells
{
    public class Fireball : Spell
    {
        public Fireball()
        {
            _rank = 1;
            _castTime = 1.5f;
            _timer = new CooldownTimer(_castTime);
            _manaCost = 10.0f; // TODO: change to 7% base mana
            _damage = 27;
        }

        // TODO: implementation
        public override void Cast()
        {
            throw new NotImplementedException();
        }
    }
}
