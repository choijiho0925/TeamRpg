using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Inventory : List<Item>
    {

        private Player player; // Player 참조 추가

        // 생성자에서 플레이어 참조 설정
        public Inventory(Player playerRef)
        {
            player = playerRef;
        }
        private void MainInventory()
        {
            while (true)
            {


                Console.Clear();
                Console.WriteLine("인벤토리");

                if (player.inventory.Count == 0)
                {
                    Console.WriteLine("아무것도없음");
                    Console.WriteLine("엔터누르면계속");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < player.inventory.Count; i++)
                {
                    var item = player.inventory[i];

                    string equipMark = "   ";
                    ConsoleColor? color = null;

                    if (player.equippedWeapon == item)
                    {
                        equipMark = "[E]";
                        color = ConsoleColor.Green;
                    }
                    else if (player.equippedArmor == item)
                    {
                        equipMark = "[E]";
                        color = ConsoleColor.Cyan;
                    }

                    string stat = item.Attack > 0 ? $"공격력+{item.Attack}" : $"방어력+{item.Defense}";

                    Console.Write($"{i + 1}.");

                    if (color.HasValue)
                        Console.ForegroundColor = color.Value;

                    Console.Write(equipMark);

                    if (color.HasValue)
                        Console.ResetColor();

                    Console.WriteLine($"{item.Name,-10}|{stat,-10}|{item.Description,-30}");
                }
                Console.WriteLine("0.메인메뉴");
                Console.Write("장착할 아이템 번호");
                string? input = Console.ReadLine();

                if (input == "0") return;

                if (int.TryParse(input, out var index) && index >= 1 && index <= _player.inventory.Count)
                {
                    EquipItem(player.inventory[index - 1]);
                    Console.WriteLine($"{player.inventory[index - 1].Name}장착됨!");
                }
                else
                {

                    Console.WriteLine("다시 입력하세요");


                }
                Console.WriteLine("엔터 누르면 계속");
                Console.ReadLine();



            }


        }
        private void EquipItem(Item item)
        {
            if (item.Attack > 0)
            {
                if (player.equippedWeapon != null)
                {
                    Console.WriteLine($"무기{player.equippedWeapon.Name}해제");
                    player.attack -= player.equippedWeapon.Attack;
                }
                player.equippedWeapon = item;
                player.attack += item.Attack;
                Console.WriteLine($"{item.Name}를 장착 공격력+{item.Attack}");
            }
            else if (item.Defense > 0)
            {
                if (player.equippedArmor != null)
                {
                    Console.WriteLine($"방어구 {player.equippedArmor.Name}를 해제");
                    player.defense -= player.equippedArmor.Defense;
                }
                player.equippedArmor = item;
                player.defense += item.Defense;
                Console.WriteLine($"{item.Name}를 장착 방어력+{item.Defense}");
            }
        }

        private void UnequipItem()
        {
            Console.Clear();
            Console.WriteLine("장비 해제");

            if (player.equippedWeapon == null && player.equippedArmor == null)
            {
                Console.WriteLine("장착 중인 아이템이 없음");
                Console.WriteLine("엔터를 누르면 돌아갑니다");
                Console.ReadLine();
                return;
            }
            if (player.equippedWeapon != null)
            {
                Console.WriteLine($"1. 무기 해제 - {player.equippedWeapon.Name}");
            }
            if (player.equippedArmor != null)
            {
                Console.WriteLine($"2.방어구 해제- {player.equippedArmor.Name}");
            }
            Console.WriteLine("0. 뒤로");
            Console.Write("선택:");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    if (player.equippedWeapon != null)
                    {
                        Console.WriteLine($"{player.equippedWeapon.Name}를 해제");
                        player.attack -= player.equippedWeapon.Attack;
                        player.equippedWeapon = null;
                    }
                    break;

                case "2":
                    if (player.equippedArmor != null)
                    {
                        Console.WriteLine($"{player.equippedArmor.Name}를 해제");
                        player.defense -= player.equippedArmor.Defense;
                        player.equippedArmor = null;
                    }
                    break;

                case "0":
                default:
                    break;
            }

            Console.WriteLine("엔터를 누르면 돌아갑니다.");
            Console.ReadLine();
        }
    }
}
