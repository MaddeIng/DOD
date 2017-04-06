using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Food : Item
    {
        public int ChangeHealth { get; set; }

        public Food(string name, string icon, int weight, int changeHealth) : base(name, icon, weight)
        {
            ChangeHealth = changeHealth;
        }

        public virtual int pickUpItem(Food storeItem)
        {
            int power = RandomUtils.Randomizer(3, this.ChangeHealth + 1);
            storeItem.ChangeHealth += power;

            return power;
        }
    }
}
