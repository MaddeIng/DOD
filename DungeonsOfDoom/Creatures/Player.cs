using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Player : Creature
    {
        //public List<Item> Backpack { get; } = new List<Item>();

        public int X { get; set; }
        public int Y { get; set; }
        public List<Item> Backpack { get; private set; }

        public Player(string name, string icon, int health, int attack, int x, int y) : base(name, icon, health, attack)
        {
            X = x;
            Y = y;
            Backpack = new List<Item>();
        }
    }
}
