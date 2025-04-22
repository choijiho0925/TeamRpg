using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Shop

    {
        private List<Item> items;
        private Player player;
        //private Inventory inventory;
        public Shop(List<Item> items, Player player)
        {
            this.items = items;
            this.player = player;
        }

        public void MainShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("스파르타 상점입니다.");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"보유 골드:{player.Gold} Gold");
                Console.WriteLine();
                Console.WriteLine("아이템 목록");
                for (int i = 0; i < items.Count; i++)
                {
                    Item item = items[i];

                    if (item.isBuy)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray; // 이미 구매한 아이템은 회색
                        Console.WriteLine($"{i + 1}. {item.Name} - {item.Gold}G (공격력: +{item.Attack}, 방어력: +{item.Defense}) [구매 완료]");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // 기본 색상
                        Console.WriteLine($"{i + 1}. {item.Name} - {item.Gold}G (공격력: +{item.Attack}, 방어력: +{item.Defense})");
                    }

                    Console.ResetColor(); // 색상 초기화
                }


                Console.WriteLine("0. 나가기");
                Console.WriteLine("1. 아이템 판매");
                Console.WriteLine();
                Console.Write("원하시는 아이템 번호를 입력해주세요. :");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Count)
                {
                    Item item = items[choice - 1];


                    if (item.isBuy)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                        Console.ReadKey();
                        continue;
                    }

                    if (item.Job != null && item.Job != player.Job)
                    {
                        Console.WriteLine($"{player.Job}는 이 아이템을 구매할 수 없습니다.");// 특정 직업만 구입가능 아이템

                        Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                        Console.ReadKey();
                        continue;
                    }

                    if (player.Gold >= item.Gold) // 플레이어 골드가 아이템 가격보다 많거나 같으면
                    {
                        player.Gold -= item.Gold; //플레이어 골드에 아이템 가격을 뺀후 플레이어 골드저장
                        item.isBuy = true;
                        player.inventory.AddItem(item);

                        Console.WriteLine($"{item.Name}를 구매했습니다!");
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                    Console.WriteLine(" 엔터를 누르면 상점으로 돌아갑니다.");
                    Console.ReadKey();

                }
                else if (input == "1")
                {

                    SellShop();
                }

                else if (input == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                    Console.ReadKey();
                }
            }
        }
        private void SellShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 판매");
                Console.ResetColor();

                List<Item> buyItems = player.inventory.Where(item => item.isBuy && !item.IsEquipped).ToList();

                if (buyItems.Count == 0)
                {
                    Console.WriteLine("보유중인 아이템이 없습니다");
                    Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                    Console.ReadKey();
                    return;
                }
                for (int i = 0; i < buyItems.Count; i++)
                {
                    var item = buyItems[i];
                    Console.WriteLine($"{i + 1}. {item.Name}  - 판매가: {item.Gold / 2} Gold");
                }
                Console.WriteLine("0. 나가기");
                Console.Write("판매할 아이템 번호 입력: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int sellchoice))
                {
                    if (sellchoice == 0)
                    {
                        return;
                    }

                    if (sellchoice >= 1 && sellchoice <= buyItems.Count)
                    {

                        Item sellitem = buyItems[sellchoice - 1];
                        if (sellitem.isEquipped)
                        {
                            Console.WriteLine("장착 중인 아이템은 판매할 수 없습니다.");
                            Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                            Console.ReadKey();
                            continue;
                        }
                        int sellgold = sellitem.Gold / 2;

                        player.Gold += sellgold;
                        player.inventory.Remove(sellitem);
                        sellitem.isBuy = false;

                        Console.WriteLine($"{sellitem.Name}을(를) {sellgold} Gold에 판매했습니다.");
                        Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 번호입니다.");
                        Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                    Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                    Console.ReadKey();
                }
            }
        }
    }
}
