using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Orc : Monster
    {
        public Orc(string name, string icon, int health, int attack) : base(name, icon, health, attack)
        {
        }

        public override int Fight(Creature creatureOpponent)
        {
            if (this.Health < 0.5 * creatureOpponent.Health)
            {
                this.Health = 0;
                return 0;
            }
            else
            {
                return base.Fight(creatureOpponent);
            }

        }
    }
}
