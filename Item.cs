using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Item
    {
        public string Job { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }

        public bool isBuy;
        public bool isEquipped;

        public Item(string job, string name, string description, int attack, int defense, int gold, bool isBuyItem, bool isEquippedItem)
        {
            Job = job;
            Name = name;
            Description = description;

            Attack = attack;
            Defense = defense;
            Gold = gold;

            isBuy = isBuyItem;
            isEquipped = isEquippedItem;
        }
    }
}
