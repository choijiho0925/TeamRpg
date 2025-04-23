using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace TeamRpg
{

    public class Shop

    {

        public void ShopMenu()

        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("낯선 얼굴이군. 자네도 목숨을 걸고 싸우다 온 건가?");
                Console.WriteLine();
                Console.WriteLine("여긴 성도, 마을도 아니야. 누구든, 똑같지..");
                Console.WriteLine();
                Console.WriteLine("뭐 하나 골라보게 뭘 원하는가?");
                Console.WriteLine("1. 장비구매");
                Console.WriteLine("2. 장비판매");
                Console.WriteLine("3. 의뢰 ");
                Console.WriteLine("0. 나가기");
                Console.ResetColor();

                string input = Console.ReadLine();

                if (input == "1")
                {
                    MainShop();

                }
                else if (input == "2")

                {

                    SellShop();

                }


                else if (input == "3")
                {

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[상인] 의뢰라...");
                    Console.WriteLine("[상인] 맘에 드는거 있으면 하나 골라보게");
                    Console.WriteLine("1.슬라임 처치");
                    Console.WriteLine("2.고블린 처치 ");
                    Console.WriteLine("3.오우거 처치");
                    Console.WriteLine("0. 나가기");
                    Console.ResetColor();


                    string quest = Console.ReadLine();

                    if (quest == "1")
                    {
                        Console.WriteLine("[상인] 슬라임이라.. 너같은놈한테 딱이군 5마리 정도만 처치해 오게");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("수락하겠네...");
                        Console.ResetColor();
                        Console.ReadKey();

                    }

                    else if (quest == "2")
                    {
                        Console.WriteLine("[상인] 고블린이라… 걔넨 머리 하나 자르고 나면 셋이 튀어나오지.");
                        Console.WriteLine("[상인] 자네가 과연 그 악취를 견딜 수 있을지 모르겠군.");
                        Console.WriteLine("[상인] 10마리정도 처치해 오게.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("수락하겠네...");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else if (quest == "3")
                    {

                        Console.WriteLine("[상인] 오우거? 하하… 자네, 목숨이 두 개라도 되나?");
                        Console.WriteLine("[상인] 그 괴물은 칼 한 자루로는 안 쓰러지오. 그래도 가겠다는 건가?");
                        Console.WriteLine("[상인] 할수만 있다면 3마리정도 처치해 오게.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("수락하겠네...");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else if (quest == "0")
                    {
                        Console.WriteLine("[상인] 행운을 빈다네");
                        Console.ReadKey();

                    }


                }

                else if (input == "0")
                {
                    break;
                }
                else
                {

                    Console.WriteLine("그럴 여유는 없을 텐데.");
                    Console.ReadKey();
                }


            }


        }


        private CancellationTokenSource idleTalkTokenSource; //비동기 작업을 중간에 멈추기 위해 사용하는 객체
        private Random random = new Random();
        private List<Item> items;
        private Player player;
        //private Inventory inventory;

        public Shop(List<Item> items, Player player)
        {
            this.items = items;
            this.player = player;
        }
        private async Task IdleTalkAsync(CancellationToken token)
        {
            string[] talk = new string[]
            {
        "이 조용함, 폭풍이 오기 전의 정적 같군.",
        "살 돈은 있지만… 그 물건을 감당할 용기는 있소?",
         "그 칼을 쥐고 죽은 자가 셋이오. 넷째는 당신일지도 모르지.",
        "나는 그냥 물건을 팔 뿐이오. 피는 당신 손에 묻히시오.",
        "요즘은 누구도 믿을 수 없지. 특히 돈이 없는 자들은…",
        "그 검은, 주인을 가리는 법이 없소. 다만 누구나 피를 보게 되지.",
        "운명이란 건 어쩌면, 우리가 무엇을 사느냐에 따라 정해지는 걸지도 모르지.",
        "자네가 이걸 들고 떠나면... 다시는 돌아오지 못할 수도 있소."
            };

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(7000, token); //7초마다

                    if (!token.IsCancellationRequested)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"\n[상인] {talk[random.Next(talk.Length)]}");
                        Console.ResetColor();
                    }
                }
                catch (TaskCanceledException)
                {
                    Console.Clear();
                    break;
                }
            }
        }
        public void MainShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("물건이라...내일 다시 돌아오겠군.");
                Console.WriteLine();
                Console.WriteLine("여긴 성도, 마을도 아니야. 누구든, 돈만 있으면 상대해주지.");
                Console.WriteLine();
                Console.WriteLine("얼른 고르시오. 그대가 써봤자 다 의미 없으니");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"보유 Groshen:{player.Gold} Groshen");
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
                        Console.WriteLine($"{i + 1}. {item.Name} - {item.Gold}G (공격력: +{item.Attack}, 방어력: +{item.Defense}\n{item.Description}");
                    }
                    Console.WriteLine();
                    Console.ResetColor(); // 색상 초기화
                }

                Console.WriteLine("0. 나가기");

                Console.WriteLine();
                Console.Write("원하시는 장비를 말해보게... ");
                idleTalkTokenSource = new CancellationTokenSource();// Task 시작할 때 token을 전달
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token); //중단 신호를 받을 수 있게 함

                string input = Console.ReadLine();

                // 입력 받았으므로 혼잣말 멈추기
                idleTalkTokenSource.Cancel();
                idleTask.Wait();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Count)
                {
                    Item item = items[choice - 1];

                    if (item.isBuy)
                    {
                        Console.WriteLine("다시 보러 온 건가, 아니면 잊은 건가. 어쨌든 거래는 끝났소.");
                        Console.WriteLine("돌아가시게");
                        Console.ReadKey();
                        continue;
                    }

                    if (item.Job != null && item.Job != "공용" && item.Job != player.Job)

                    {
                        Console.WriteLine("자네가 그걸 든다면, 웃음거리가 될 뿐이오.");// 특정 직업만 구입가능 아이템

                        Console.WriteLine("그 물건은 자네 길이 아니오");
                        Console.ReadKey();
                        continue;
                    }

                    // Shop 클래스 내 MainShop() 메서드의 구매 부분 수정
                    if (player.Gold >= item.Gold) // 플레이어 골드가 아이템 가격보다 많거나 같으면
                    {
                        player.Gold -= item.Gold; //플레이어 골드에 아이템 가격을 뺀후 플레이어 골드저장
                        item.isBuy = true;
                        player.inventory.Add(item);
                        Console.Clear();
                        Console.WriteLine($"{item.Name}를 구매했습니다!");
                        Console.WriteLine("좋소. 그대의 생명을 조금은 연장시켜줄 테지.");
                        Console.ReadKey();
                        // 구매 확인 후 다시 화면 클리어 추가
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("꿈만 꾸지 마시오. 여긴 자선을 베푸는 곳이 아니니까.");
                        Console.WriteLine(" 이 물건의 값어치를 모른다면, 다시 돌아오시오.");
                        Console.ReadKey();
                        // 부족한 금액 메시지 확인 후 다시 화면 클리어 추가
                        Console.Clear();
                    }
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
                    Console.WriteLine("귀가 멀었소? 아니면 숫자도 못 읽는 건가.");
                    Console.WriteLine("그럴 여유는 없을 텐데.");
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
                Console.WriteLine("아이템 판매 메뉴");
                Console.ResetColor();

                List<Item> buyItems = player.inventory.Where(item => item.isBuy && !item.isEquipped).ToList();

                if (buyItems.Count == 0)
                {
                    Console.WriteLine("비었소. 마음도, 가방도. 지금은 돌아가시오.");
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
                            Console.WriteLine("몸에 붙어 있는 걸 파는 자는… 아직 결심이 덜 선 거요.");
                            Console.WriteLine("아직 자네가 버릴 준비가 안 된 것 같군. 나중에 다시 말하시오.");
                            Console.ReadKey();
                            continue;
                        }
                        int sellgold = sellitem.Gold / 2;

                        player.Gold += sellgold;
                        player.inventory.Remove(sellitem);
                        sellitem.isBuy = false;

                        Console.WriteLine($"({sellitem.Name}을(를) {sellgold} Gold에 판매했습니다.)");
                        Console.WriteLine("이걸 판다고 과거가 사라지진 않소.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("이런 자들 때문에 시간만 낭비되는 거지.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("숫자조차 제대로 못 고르는 자에겐, 거래란 무리요.");
                    Console.ReadKey();
                }
            }
        }
    }
}
