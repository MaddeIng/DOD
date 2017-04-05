using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item : GameObject
    {
        public int Weight { get; }

        public Item(string name, string icon, int weight) : base (name, icon)
        {
            Weight = weight;
        }
    }
}
