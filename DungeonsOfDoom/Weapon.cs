using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Weapon : Item
    {
        public int ChangeAttackStats { get; set; }

        public Weapon(string name, string icon, int weight, int changeAttackStats) : base(name, icon, weight)
        {
            ChangeAttackStats = changeAttackStats;
        }
    }
}
