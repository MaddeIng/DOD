﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Monster : Creature, IBackpackable
    {
        public static int numberOfMonster;

        public int Weight { get; set; }

        public Monster(string name, string icon, int health, int attack) : base(name, icon, health, attack)
        {
            numberOfMonster++;
        }
    }
}
