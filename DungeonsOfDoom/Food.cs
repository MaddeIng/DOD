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

        public override string PickUpItem(Player player, Room room)
        {
            player.Backpack.Add(room.Item);
            player.Health += RandomUtils.Randomizer(-2, 6);
            string message = $"{room.Item.Name} added. Health increased to: {player.Health}";
            room.Item = null;
            return message;

            //string message = $"{room.Item.Name} added. Health decreased to: {player.Health}";
        }

    }
}
