using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Monster : Creature
    {
        public Monster(string name, string icon, int health, int attack) : base(name, icon, health, attack)
        {
        }

        //private void RandomizeMonster()
        //{
        //    Random random = new Random();

        //    int monsterType = random.Next(1, 3);

        //    if (monsterType == 1)
        //        new Monster("Ogre", "O", 30, 3);

        //    else
        //        new Monster("Orc", "0", 30, 3);
        //}
    }
}
