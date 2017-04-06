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

        public override string PickUpItem(Player player, Room room)
        {
            player.Backpack.Add(room.Item);
            player.Attack += 5;
            string message = $"{room.Item.Name} added. Attack power increased to: {player.Attack}";
            room.Item = null;
            return message;
        }
    }
}
