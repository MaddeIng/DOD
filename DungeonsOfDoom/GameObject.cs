using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class GameObject
    {
        public string Name { get; }
        public string Icon { get; }

        public GameObject(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
