using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Item
    {
        public JobOption Type { get; set; }

        public string Job { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }

        public bool isBuy;
        public bool isEquipped;

        public Item(JobOption type, string job, string name, string description, int attack, int defense, int gold, bool isBuyItem, bool isEquippedItem)
        {
            Job = job;
            Name = name;
            Description = description;
            Type = type;

            Attack = attack;
            Defense = defense;
            Gold = gold;

            isBuy = isBuyItem;
            isEquipped = isEquippedItem;
        }

        public static string StringOption(JobOption type)
        {
            if (type == JobOption.Gladiator)
            {
                return "검투사";
            }
            else if (type == JobOption.Hunter)
            {
                return "수렵꾼";
            }
            else if (type == JobOption.Assassin)
            {
                return "암살자";
            }
            else if (type == JobOption.Amor)
            {
                return "방어구";
            }
            else
            {
                return "물약";
            }
        }
    }

    public enum JobOption
    {
        Gladiator, Hunter, Assassin, Amor, Potion
    }
}

