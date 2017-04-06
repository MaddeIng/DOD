using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Creature : GameObject
    {
        public int Health { get; set; }
        public int Attack { get; set; }

        public Creature(string name, string icon, int health, int attack) : base(name, icon)
        {
            Health = health;
            Attack = attack;
        }

        public virtual int Fight(Creature creatureOpponent)
        {
            int power = RandomUtils.Randomizer(0, this.Attack + 1);

            creatureOpponent.Health -= power;

            return power;
        }
    }
}
