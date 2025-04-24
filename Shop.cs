using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using NAudio.Wave;
using System.ComponentModel.Design;

namespace TeamRpg
{

    public class Shop

    {
        //private WaveOutEvent shopDevice; 
        //private AudioFileReader shopAudio;
        public void ShopMenu()

        {
            while (true)
            {
                //                //shopAudio = new AudioFileReader(@"Audio\Shop.mp3");
                //                shopDevice = new WaveOutEvent();
                //                shopDevice.Init(shopAudio);
                //                shopDevice.Play();
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                string asciiArt = @"

//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@&##B#&&#BBBB#&@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@&@BGPG#GYJ5BGBBBG#&@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@&P?~?J!~~7YY5GB5G#@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@&J^^::^~^~JY55?5P&@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@5~7J5J!JGPGBGJYG&@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@Y!77J?^Y#J?Y5P?YB#@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@B~:::^^Y#G??5P!JB&@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@J~^^~?PBB55P57Y&@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@5~7??JPBGGGY75@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@@5?!!?Y55PGPG5#@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&Y~?55G##&&###GBGGB@@@@@@@@&@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@&##G!75GPGBBB#G5GPPYJPPB##&&@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@&BB57YPB7!777JJ?5G5G5YJY5YY5PGGPB&@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@##GP5PP7JGGJ~~!~!!7PG5GYY?J55GBBBBBBBGP&@@@@@@@@@
//@@@@@@@@@@@@@@@@@G?Y5PGGBJ?PPP~~~~~~?BBYYBPJ?YGBB###GYBGBG5@@@@@@@@@
//@@@@@@@@@@@@@@@B?~~5GBBBG7?G5?^~~^!YGG5J5#PYYBGB#PJ7~?BBB#PB@@@@@@@@
//@@@@@@@@@@@@@GJ~::!5G#B#J!JG5J^^~?PPPY7YPGPG#GBP!^::7BBG#BBP&@@@@@@@
//@@@@@@@@@@@@Y!!~:7!YB#BG!~YP55~75PPGY7?5GGGBGGJ^:^::G&GG#B#B&@@@@@@@
//@@@@@@@@@@@P:Y!::Y~J#G#P~~YGYG5PPGBB77?PGBGP?~.:^:~5&#PB#B###@@@@@@@
//@@@@@@@@@@@!:??.:?J5#B#G!~JG5GGGBB#B7??PG#5~::..^?P#BGYGGB#&B@@@@@@@
//@@@@@@@@@@P^:~Y!:!5GBBBB5?JPBBBBBB#G7??5BB!.::^~!GGBG?Y#5P#&#&@@@@@@
//@@@@@@@@@Y~7~^!J?!7B####G5GP5GBB##B5?YYJ7^:::.?YYGBG?J#B7G&#B#@@@@@@
//@@@@@@@@#~^5?JYY55YPGGBBBBBBB#BB5!!!??~~::^^::75PB5?JBB7?&&#B#@@@@@@
//@@@@@@@5~^:!YP5Y!7PY55JJY5PPGGBJ::^^?~::^^:?!!7BB?!PBBJ7B@&#GB@@@@@@
//@@@@@@B^!7?~~7??JPGGPJ7?YPYYY5B~.::^~?J!::~~75GB?!P#PJ!5&@&#BB@@@@@@
//@@@@@@B~!?7^7!?5GGBGBBGYPP555G5^::^~^~YY^^?7?BB77PB5Y?JB&&&&BB&@@@@@
//@@@@@@@?:^^~~5GPPP57?5BGBBGGPB5!^:^~!~!YJJJ?J#Y7GGPPY!5&&&&#BB#@@@@@
//@@&@@@@P~^~?GP5BBBG????YG&&&#BBY~^~~77JJ??PBGPJGGBB5!!B@@&&##B#@@@@@
//@@@@@@@@P7Y#&P5B###PY5PG#&#&&@@&G777GP?!~5&&B5PB##57!G@@@&&B###@@@@@
//@@@@@@@@@@BJPBGB#&@&&@@@&&&&&@@&@BY7YG5Y!Y&&#GG#B577G@@@@&&B##B@@@@@
//@@@@@@@@@@@57?YB&@&BBBBGPGGGB#BGGB&YJ5PPYG&##BGGY7!G@@@@@&#B&#B@@@@@";

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(asciiArt);
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("낯선 얼굴이군. 자네도 목숨을 걸고 싸우다 온 건가?");
                Console.WriteLine();
                Console.WriteLine("여긴 성도, 마을도 아니야. 누구든, 똑같지..");
                Console.WriteLine();
                Console.WriteLine("뭐 하나 골라보게 뭘 원하는가?");
                Console.WriteLine("1. 무기 상점");
                Console.WriteLine("2. 방어구 상점");
                Console.WriteLine("3. 잡화 상점");
                Console.WriteLine("4. 의뢰 ");
                Console.WriteLine("0. 나가기");
                Console.ResetColor();

                string input = Console.ReadLine();

                if (input == "1")
                {
                    WeaponShop();

                }
                else if (input == "2")

                {
                    ArmorShop();

                }
                else if (input == "3")
                {
                    HaberDasheryShop();

                }


                else if (input == "4")
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
                    //shopDevice.Stop();
                    //shopDevice.Dispose();
                    //shopAudio.Dispose();
                    //shopDevice = null;
                    //shopAudio = null;
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
        private Inventory inventory;

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
                    await Task.Delay(7000, token);//7초마다

                    if (!token.IsCancellationRequested)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"\n[상인] {talk[random.Next(talk.Length)]}");
                        Console.ResetColor();
                    }
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }
        public void WeaponShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("[상인] 여기 있는 것들은 그저 쇳덩어리가 아니오.");
                Console.WriteLine();
                Console.WriteLine("[상인] 각각의 무기엔, 한 명의 죽음이 담겨 있지...");
                Console.WriteLine();
                Console.WriteLine("[상인] 얼른 고르시오. 그대가 써봤자 다 의미 없으니");
                Console.ResetColor();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 돌아가기");
                string input1 = Console.ReadLine();
                if (input1 == "1")
                {

                    WeaponBuy();

                }
                else if (input1 == "2")
                {
                    WeaponSell();
                }
                else if (input1 == "0")
                {
                    // 상점 나갈때 대사 출력 선택
                    // Console.WriteLine("잘 가시오, 잊힌 자여. 다음 만남은… 아마 더 어두울 것이오.");
                    // Console.ReadKey();
                    return;
                }


            }
        }



        public void WeaponBuy()
        {

            while (true) // 아이템 구매 전용 루프
            {
                Console.Clear();
                Console.WriteLine($"보유 Groshen:{player.Gold} Groshen");
                Console.WriteLine();
                Console.WriteLine("아이템 목록");

                var weaponItems = items
                    .Where(item => item.Type == JobOption.Gladiator ||
                                   item.Type == JobOption.Hunter ||
                                   item.Type == JobOption.Assassin).ToList();//무기 직업별

                for (int i = 0; i < weaponItems.Count; i++)
                {
                    Item item = weaponItems[i];
                    Console.ForegroundColor = item.isBuy ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {item.Name} - {item.Gold}G (공격력: +{item.Attack}, 방어력: +{item.Defense}){(item.isBuy ? " [구매 완료]" : "")}");
                    Console.WriteLine(item.Description);
                    Console.ResetColor();
                }

                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 무기를 말해보게... ");
                idleTalkTokenSource = new CancellationTokenSource();
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token);
                string input = Console.ReadLine();
                idleTalkTokenSource.Cancel();
                idleTask.Wait();

                if (input == "0")
                    break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= weaponItems.Count)
                {
                    Item item = weaponItems[choice - 1];

                    if (item.isBuy)
                    {
                        Console.Clear();
                        Console.WriteLine("다시 보러 온 건가, 아니면 잊은 건가. 어쨌든 거래는 끝났소.");
                        Console.WriteLine("돌아가시게");
                        Console.ReadKey();
                        continue;
                    }

                    if (item.Job != player.Job)
                    {
                        Console.Clear();
                        Console.WriteLine("자네가 그걸 든다면, 웃음거리가 될 뿐이오.");// 특정 직업만 구입가능 아이템

                        Console.WriteLine("그 물건은 자네 길이 아니오");
                        Console.ReadKey();
                        continue;
                    }

                    if (player.Gold >= item.Gold)
                    {
                        Console.Clear();
                        player.Gold -= item.Gold;
                        item.isBuy = true;
                        player.inventory.Add(item);
                        Console.WriteLine($"{item.Name}를 구매했습니다!");
                        Console.WriteLine("좋소. 그대의 생명을 조금은 연장시켜줄 테지.");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("꿈만 꾸지 마시오. 여긴 자선을 베푸는 곳이 아니니까.");
                        Console.WriteLine(" 이 물건의 값어치를 모른다면, 다시 돌아오시오.");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("귀가 멀었소? 아니면 숫자도 못 읽는 건가.");
                    Console.WriteLine("그럴 여유는 없을 텐데.");
                    Console.ReadKey();
                    continue;
                }
            }
        }



        private void WeaponSell()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 판매 메뉴");
                Console.ResetColor();

                List<Item> buyItems =
                player.inventory.Where(item => item.isBuy && !item.isEquipped && (item.Type == JobOption.Gladiator || item.Type == JobOption.Hunter || item.Type == JobOption.Assassin)).ToList();

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

        public void ArmorShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("[상인] 여기 있는 것들은 그저 쇳덩어리가 아니오.");
                Console.WriteLine();
                Console.WriteLine("[상인] 각각의 무기엔, 한 명의 죽음이 담겨 있지...");
                Console.WriteLine();
                Console.WriteLine("[상인] 얼른 고르시오. 그대가 써봤자 다 의미 없으니");
                Console.ResetColor();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 돌아가기");
                string input1 = Console.ReadLine();
                if (input1 == "1")
                {

                    ArmorBuy();

                }
                else if (input1 == "2")
                {
                    ArmorSell();
                }
                else if (input1 == "0")
                {
                    // 상점 나갈때 대사 출력 선택
                    // Console.WriteLine("잘 가시오, 잊힌 자여. 다음 만남은… 아마 더 어두울 것이오.");
                    // Console.ReadKey();
                    return;
                }


            }


        }
        public void ArmorBuy()
        {
            while (true) // 아이템 구매 전용 루프
            {
                Console.Clear();
                Console.WriteLine($"보유 Groshen:{player.Gold} Groshen");
                Console.WriteLine();
                Console.WriteLine("아이템 목록");

                var armorItems = items
                    .Where(item => item.Type == JobOption.Amor).ToList(); //방어구만 


                for (int i = 0; i < armorItems.Count; i++)
                {
                    Item item = armorItems[i];
                    Console.ForegroundColor = item.isBuy ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {item.Name} - {item.Gold}G (공격력: +{item.Attack}, 방어력: +{item.Defense}){(item.isBuy ? " [구매 완료]" : "")}");
                    Console.WriteLine(item.Description);
                    Console.ResetColor();
                }

                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 방어구를 말해보게... ");
                idleTalkTokenSource = new CancellationTokenSource();
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token);
                string input = Console.ReadLine();
                idleTalkTokenSource.Cancel();
                idleTask.Wait();

                if (input == "0")
                    break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= armorItems.Count)
                {
                    Item item = armorItems[choice - 1];

                    if (item.isBuy)
                    {
                        Console.Clear();
                        Console.WriteLine("다시 보러 온 건가, 아니면 잊은 건가. 어쨌든 거래는 끝났소.");
                        Console.WriteLine("돌아가시게");
                        Console.ReadKey();
                        continue;
                    }


                    if (player.Gold >= item.Gold)
                    {
                        Console.Clear();
                        player.Gold -= item.Gold;
                        item.isBuy = true;
                        player.inventory.Add(item);
                        Console.WriteLine($"{item.Name}를 구매했습니다!");
                        Console.WriteLine("좋소. 그대의 생명을 조금은 연장시켜줄 테지.");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("꿈만 꾸지 마시오. 여긴 자선을 베푸는 곳이 아니니까.");
                        Console.WriteLine(" 이 물건의 값어치를 모른다면, 다시 돌아오시오.");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("귀가 멀었소? 아니면 숫자도 못 읽는 건가.");
                    Console.WriteLine("그럴 여유는 없을 텐데.");
                    Console.ReadKey();
                    continue;
                }
            }


        }
        private void ArmorSell()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 판매 메뉴");
                Console.ResetColor();

                List<Item> buyItems = player.inventory.Where(item => item.isBuy && !item.isEquipped && item.Type == JobOption.Amor).ToList();

                if (buyItems.Count == 0)
                {
                    Console.WriteLine("비었소. 마음도, 가방도. 지금은 돌아가시오.");
                    Console.ReadKey();
                    return;
                }
                for (int i = 0; i < buyItems.Count; i++)
                {
                    var item = buyItems[i];
                    Console.WriteLine($"{i + 1}. {item.Name}  - 판매가: {item.Gold / 2} G");
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


        private void HaberDasheryShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("[상인] 이 물건들은 단순한 약이 아니오. 기억을 지우고, 생명을 비틀기도 하니…");
                Console.WriteLine();
                Console.WriteLine("[상인] 이건 연금술사의 피와 악마의 숨결로 만들어졌소. 흔한 물건은 아니지.");
                Console.WriteLine();
                Console.WriteLine("[상인] 얼른 고르시오. 그대가 써봤자 다 의미 없으니");
                Console.ResetColor();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 돌아가기");
                string input1 = Console.ReadLine();
                if (input1 == "1")
                {

                    haberdasheryBuy();

                }
                else if (input1 == "2")
                {
                    haberdasherySell();
                }
                else if (input1 == "0")
                {
                    // 상점 나갈때 대사 출력 선택
                    // Console.WriteLine("잘 가시오, 잊힌 자여. 다음 만남은… 아마 더 어두울 것이오.");
                    // Console.ReadKey();
                    return;
                }


            }
        }
        private void haberdasheryBuy()
        {
            while (true) // 아이템 구매 전용 루프
            {
                Console.Clear();
                Console.WriteLine($"보유 Groshen:{player.Gold} Groshen");
                Console.WriteLine();
                Console.WriteLine("아이템 목록");

                var potionItems = items
                    .Where(item => item.Type == JobOption.Potion).ToList(); //포션만 

                for (int i = 0; i < potionItems.Count; i++)
                {
                    var item = potionItems[i];
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {item.Name} - {item.Gold}G");
                    Console.WriteLine(item.Description);
                    Console.ResetColor();
                }


                Console.WriteLine("0. 나가기");
                Console.Write("원하는 엘릭서를 말해보게... ");
                idleTalkTokenSource = new CancellationTokenSource();
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token);
                string input = Console.ReadLine();
                idleTalkTokenSource.Cancel();
                idleTask.Wait();

                if (input == "0")

                    break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= potionItems.Count)
                {
                    Item item = potionItems[choice - 1];




                    if (player.Gold >= item.Gold)
                    {
                        Console.Clear();
                        player.Gold -= item.Gold;
                        item.isBuy = true;
                        player.inventory.Add(item);
                        Console.WriteLine($"{item.Name}를 구매했습니다!");
                        Console.WriteLine("좋소. 그대의 생명을 조금은 연장시켜줄 테지.");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("재산이란 허상이지. 하지만 이 허상 없이는… 아무것도 얻을 수 없소");
                        Console.WriteLine("당신이 가진 건 욕망뿐인가 보오.");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("그런 번호는 목록에 없소. 당신이 본 게… 진짜였을까?");
                    Console.WriteLine("그럴 여유는 없을 텐데.");
                    Console.ReadKey();
                    continue;
                }
            }
        }


        private void haberdasherySell()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 판매 메뉴");
                Console.ResetColor();

                List<Item> buyItems = player.inventory.Where(item => item.isBuy && item.Type == JobOption.Potion).ToList();

                if (buyItems.Count == 0)
                {
                    Console.WriteLine("비었소. 마음도, 가방도. 지금은 돌아가시오.");
                    Console.ReadKey();
                    return;
                }
                for (int i = 0; i < buyItems.Count; i++)
                {
                    var item = buyItems[i];
                    Console.WriteLine($"{i + 1}. {item.Name}  - 판매가: {item.Gold / 2} G");
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
