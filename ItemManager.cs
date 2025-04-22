using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class ItemManager
    {
        private static ItemManager instance;

        public static ItemManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ItemManager();
                return instance;
            }
        }
        private ItemManager()
        {
            Items = new List<Item>()
            {
                new Item("전사", "목검","1", 3, 0, 0, false, false),
                new Item("전사", "철검","2", 6, 0, 1500, false, false),
                new Item("전사", "부러지지 않는 신념의 검","3", 10, 0, 4000, false, false),
                new Item("궁수", "나무 활","1", 3, 0, 0, false, false),
                new Item("궁수", "탐험가의 활","2", 6, 0, 1500, false, false),
                new Item("궁수", "아케인셰이드 보우","3", 10, 4000, 0, false, false),
                new Item("도적", "초보 도적의 단검","1", 3, 0, 0, false, false),
                new Item("도적", "파프니르 다마스커스","2", 6, 0, 1500, false, false),
                new Item("도적", "아케인셰이드 대거","3", 10, 0, 4000, false, false),
                new Item("공용", "견습복","1", 0, 2, 0, false, false),
                new Item("공용", "요리사복","2", 0, 5, 1500, false, false),
                new Item("공용", "블랙슈트","3", 0, 10, 4000, false, false),
            };
        }

        public List<Item> Items;

    }
}
