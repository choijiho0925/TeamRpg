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

        // 상점 방문 여부 확인 변수
        private bool isFirstVisit = true;

        public void ShopMenu()

        {

            while (true)
            {


                string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/hotelshop.wav");
                Music.PlayMusic(test);

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
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                // 육군 이등별 여부에 따라 다른 대사 출력
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[나레이션] 상점 주인이 당신을 보자마자 자세를 고쳐 바로 선다.");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Game.Instance.TypeText("[상점 주인] 충!!! 성!!! 각하께서 오시다니! 영광입니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 이등별 각하! 필요한 물품은 무엇이든 준비되어 있습니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 모든 물자는 최상의 품질로 준비했습니다! 명령만 내려주십시오!", 30);
                    Console.ResetColor();
                }
                else // 일반 직업인 경우 기존 대사
                {
                    // 첫 방문 여부에 따라 다른 대사 출력
                    if (isFirstVisit)
                    {
                        Game.Instance.TypeText("[상점 주인] ...? 이런 곳에 아직 숨 쉬는 게 남아있었나. 놀랍군...", 30);
                        isFirstVisit = false; // 첫 방문 후 플래그 변경
                    }
                    else
                    {
                        Game.Instance.TypeText("[상점 주인] ...또 왔군. 아직 숨은 붙어 있는 모양이지?", 30);
                    }

                    // 공통 대사
                    Game.Instance.TypeText("[상점 주인] 뭘 기대하고 온 건진 모르겠지만, 여긴 기적 따윈 없어. 그저 거래가 있을 뿐.", 30);
                    Game.Instance.TypeText("[상점 주인] 살아서 돌아온 것만 해도 용하군 그래. 선발대 놈들은 소식도 없는데 말이야.", 30);
                    Game.Instance.TypeText("[상점 주인] 뭘 원하는가? 시간 없어. 빨리 고르게.", 30);
                }
                Console.ResetColor();

                // 메뉴 옵션 표시 - 육군 이등별인 경우 다른 텍스트
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine("\n1. 무기고");
                    Console.WriteLine("2. 방어구 창고");
                    Console.WriteLine("3. 보급품");
                    Console.WriteLine("4. 특수 작전 정보");
                    Console.WriteLine("0. 복귀하기");
                }
                else
                {
                    Console.WriteLine("\n1. 무기 상점");
                    Console.WriteLine("2. 방어구 상점");
                    Console.WriteLine("3. 잡화 상점");
                    Console.WriteLine("4. 의뢰...? (낡은 전단지)");
                    Console.WriteLine("0. 나가기");
                }

                Console.Write("\n선택: ");
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
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Game.Instance.TypeText("[상점 주인] 의뢰...? 아아, 이 낡아빠진 전단지 말인가.", 30);
                    Game.Instance.TypeText("[상점 주인] 쯧... 보다시피, 찾는 사람 하나 없는 쓰레기지.", 30);
                    Game.Instance.TypeText("[상점 주인] 가져가서 불쏘시개로나 쓰던가. 여기선 아무짝에도 쓸모없으니.", 30);
                    Console.ResetColor();
                    Console.WriteLine("\n(상점 주인이 낡은 의뢰 전단지를 무시한다.)");
                    // Game.Instance.WaitForKeyPress(); // WaitForKeyPress() 호출은 Game 클래스에 public으로 있어야 함
                    Console.ReadKey(); // 임시로 ReadKey 사용
                    continue;

                    /* --- 기존 의뢰 코드 (비활성화) --- */
                }
                else if (input == "0")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Game.Instance.TypeText("[상점 주인] ...잘 가게. 다시 볼 일 없기를 바라지.", 30);
                    Console.ResetColor();
                    Music.StopMusic();
                    // Game.Instance.WaitForKeyPress(); // WaitForKeyPress() 호출은 Game 클래스에 public으로 있어야 함
                    Console.ReadKey(); // 임시로 ReadKey 사용
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Game.Instance.TypeText("[상점 주인] 딴생각할 시간 있나? 정신 차리게.", 30);
                    Console.ResetColor();
                    // Game.Instance.WaitForKeyPress(); // WaitForKeyPress() 호출은 Game 클래스에 public으로 있어야 함
                    Console.ReadKey(); // 임시로 ReadKey 사용
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
                "[상점 주인] ...쯧, 또 누가 죽어나갔나 보군.",
                "[상점 주인] 이딴 곳에서 장사하는 것도 지긋지긋해...",
                "[상점 주인] 저 문 너머는 지옥이나 마찬가지지.",
                "[상점 주인] 왕국이 망하든 말든, 내 알 바 아니야.",
                "[상점 주인] ...오늘은 또 어떤 멍청이가 비싼 값을 치를까.",
                "[상점 주인] 여기서 산 물건이... 무덤까지 함께 갈 수도 있겠지.",
                "[상점 주인] ...선발대 놈들은 지금쯤 뭘 하고 있을까. 아니, 뭘 당하고 있을까.",
                "[상점 주인] 결국 다 똑같아. 죽거나, 미치거나."
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
                Console.ForegroundColor = ConsoleColor.DarkGreen; // 상점 주인 색상

                // 육군 이등별 직업 여부에 따라 다른 대사 출력
                if (player.Job == "육군 이등별")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[나레이션] 상점 주인이 거수경례를 하며 바짝 긴장한 표정으로 서 있다.");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Game.Instance.TypeText("[상점 주인] 충성! 이등별 각하! 최고급 무기들이 준비되어 있습니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 각하의 K2소총도 좋지만, 추가 화력이 필요하시다면 말씀만 하십시오!", 30);
                    Game.Instance.TypeText("[상점 주인] 비밀 무기고에서 특별히 준비했습니다! 명령을 기다리겠습니다!", 30);
                    Console.ResetColor();

                    Console.WriteLine("\n1. 무기 지급받기");
                    Console.WriteLine("2. 무기 반납하기");
                    Console.WriteLine("0. 돌아가기");
                }
                else // 일반 직업 대사
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Game.Instance.TypeText("[상점 주인] 무기라... 결국 이걸로 누군가를 죽이거나, 네놈이 죽거나 둘 중 하나겠지.", 30);
                    Game.Instance.TypeText("[상점 주인] 여기 걸린 것들은 전부 피맛을 본 놈들이야. 자, 뭘로 생을 마감하고 싶나?", 30);
                    Console.ResetColor();

                    Console.WriteLine("\n1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("0. 돌아가기");
                }

                Console.Write("\n선택: ");
                string input1 = Console.ReadLine();

                if (input1 == "1") { WeaponBuy(); }
                else if (input1 == "2") { WeaponSell(); }
                else if (input1 == "0") { return; }
                else
                {
                    Console.Clear();

                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 죄송합니다, 각하! 올바른 선택을 부탁드립니다!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...정신 똑바로 차리게. 여긴 어물쩡거릴 곳이 못 돼.", 30);
                        Console.ResetColor();
                    }
                    Console.ReadKey(); // 임시
                }
            }
        }




        public void WeaponBuy()
        {
            string currentPrompt = player.Job == "육군 이등별" ?
                "\n지급받을 무기 번호: " : "\n구매할 아이템 번호: "; // 메시지 변경

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"보유 골드:{player.Gold} G");
                Console.WriteLine("\n[아이템 목록]");

                var weaponItems = items
                    .Where(item => item.Type == JobOption.Gladiator ||
                                   item.Type == JobOption.Hunter ||
                                   item.Type == JobOption.Assassin).ToList();

                // 아이템 목록 표시 (이전 답변 포맷 유지)
                for (int i = 0; i < weaponItems.Count; i++)
                {
                    Item item = weaponItems[i];
                    Console.ForegroundColor = item.isBuy ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.Write($"{i + 1}. ");
                    Console.Write($"{item.Name,-15}");
                    Console.ResetColor();
                    Console.Write($"| ");
                    if (item.Attack > 0) Console.ForegroundColor = ConsoleColor.Red;
                    else if (item.Defense > 0) Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{(item.Attack > 0 ? $"공격력 +{item.Attack}" : $"방어력 +{item.Defense}"),-12}");
                    Console.ResetColor();
                    Console.Write($"| ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{item.Gold + " G",-10}");
                    Console.ResetColor();
                    Console.WriteLine($"{(item.isBuy ? (player.Job == "육군 이등별" ? " [지급 완료]" : " [구매 완료]") : "")}");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"   └ {item.Description}");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine("0. 나가기");
                Console.Write(currentPrompt); // 저장된 프롬프트 출력

                // IdleTalkAsync 시작하기
                idleTalkTokenSource = new CancellationTokenSource();
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token);

                // 사용자 입력 대기
                string input = Console.ReadLine();

                // 사용자 입력 후 IdleTalkAsync 취소
                idleTalkTokenSource.Cancel();

                // --- 이하 입력 처리 로직 ---
                if (input == "0") break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= weaponItems.Count)
                {
                    Item item = weaponItems[choice - 1];

                    if (item.isBuy)
                    {
                        Console.Clear();
                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText("[상점 주인] 죄송합니다, 각하! 해당 무기는 이미 지급 완료되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 다른 무기를 원하신다면 말씀해주십시오!", 30);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Game.Instance.TypeText("[상점 주인] ...이미 팔린 물건이야. 눈은 장식인가?", 30);
                            Game.Instance.TypeText("[상점 주인] 아니면, 미련이라도 남은 건가?", 30);
                            Console.ResetColor();
                        }
                        Console.ReadKey(); // 임시
                        continue;
                    }

                    if ((item.Type == JobOption.Gladiator || item.Type == JobOption.Hunter || item.Type == JobOption.Assassin) &&
                        item.Job != player.Job && item.Job != "공용" && player.Job != "육군 이등별") // 육군 이등별은 모든 무기 사용 가능
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText($"[상점 주인] ...자네같은 {player.Job}가 그걸 휘두를 수 있을 것 같나?", 30);
                        Game.Instance.TypeText("[상점 주인] 꼴사나운 꼴 보이기 싫으면 다른 걸 알아보게.", 30);
                        Console.ResetColor();
                        Console.ReadKey(); // 임시
                        continue;
                    }

                    // 육군 이등별은 무기를 공짜로 받음
                    if (player.Job == "육군 이등별" || player.SpendGold(item.Gold))
                    {
                        Console.Clear();
                        item.isBuy = true;
                        player.inventory.Add(item);

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText($"[상점 주인] 충성! 각하! {item.Name} 무기가 성공적으로 지급되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 군 최고의 무기입니다! 각하의 임무 수행에 도움이 되길 바랍니다!", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({item.Name} 지급 완료)");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Game.Instance.TypeText($"[상점 주인] ...{item.Name}. 좋아. 이걸로 베든 베이든, 이제 네놈 책임이야.", 30);
                            Game.Instance.TypeText("[상점 주인] 부디 죽 더라도 입구 가까이에서 죽게나. 물건 값은 해야지.", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({item.Name} 구매 완료)");
                        }
                        Console.ReadKey(); // 임시
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...그만한 돈도 없으면서 뭘 사겠다는 건가?", 30);
                        Game.Instance.TypeText("[상점 주인] 목숨 값은 벌고 와야지 않겠나.", 30);
                        Console.ResetColor();
                        Console.ReadKey(); // 임시
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 죄송합니다, 각하! 올바른 번호를 선택해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...숫자도 제대로 못 읽나? 시간 없어.", 30);
                        Console.ResetColor();
                    }
                    Console.ReadKey(); // 임시
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

                // 직업에 따라 다른 타이틀 표시
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine("무기 반납 메뉴");
                }
                else
                {
                    Console.WriteLine("아이템 판매 메뉴");
                }
                Console.ResetColor();

                List<Item> buyItems =
                player.inventory.Where(item => item.isBuy && !item.isEquipped && (item.Type == JobOption.Gladiator || item.Type == JobOption.Hunter || item.Type == JobOption.Assassin)).ToList();

                if (buyItems.Count == 0)
                {
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 현재 반납 가능한 무기가 없습니다!", 30);
                        Game.Instance.TypeText("[상점 주인] 필요하신 장비가 있으시면 즉시 말씀해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("비었소. 마음도, 가방도. 지금은 돌아가시오.");
                    }
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < buyItems.Count; i++)
                {
                    var item = buyItems[i];
                    if (player.Job == "육군 이등별")
                    {
                        Console.WriteLine($"{i + 1}. {item.Name}  - 반납 가능");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {item.Name}  - 판매가: {item.Gold / 2} Gold");
                    }
                }

                Console.WriteLine("0. 나가기");

                // 직업에 따라 다른 프롬프트 표시
                if (player.Job == "육군 이등별")
                {
                    Console.Write("반납할 무기 번호 입력: ");
                }
                else
                {
                    Console.Write("판매할 아이템 번호 입력: ");
                }

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
                            if (player.Job == "육군 이등별")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Game.Instance.TypeText("[상점 주인] 충성! 각하! 현재 장착 중인 장비는 반납이 어렵습니다!", 30);
                                Game.Instance.TypeText("[상점 주인] 먼저 장비를 해제해주시기 바랍니다!", 30);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("몸에 붙어 있는 걸 파는 자는… 아직 결심이 덜 선 거요.");
                                Console.WriteLine("아직 자네가 버릴 준비가 안 된 것 같군. 나중에 다시 말하시오.");
                            }
                            Console.ReadKey();
                            continue;
                        }

                        int sellgold = sellitem.Gold / 2;

                        // 육군 이등별은 골드를 추가로 얻지 않음 (무료로 받았으므로)
                        if (player.Job != "육군 이등별")
                        {
                            player.Gold += sellgold;
                        }

                        player.inventory.Remove(sellitem);
                        sellitem.isBuy = false;

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText($"[상점 주인] 충성! 각하! {sellitem.Name} 무기가 성공적으로 반납되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 즉시 군 물자 체계에 등록하겠습니다!", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({sellitem.Name} 반납 완료)");
                        }
                        else
                        {
                            Console.WriteLine($"({sellitem.Name}을(를) {sellgold} Gold에 판매했습니다.)");
                            Console.WriteLine("이걸 판다고 과거가 사라지진 않소.");
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText("[상점 주인] 충성! 각하! 올바른 번호를 선택해주십시오!", 30);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("이런 자들 때문에 시간만 낭비되는 거지.");
                        }
                        Console.ReadKey();
                    }
                }
                else
                {
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 올바른 형식으로 입력해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("숫자조차 제대로 못 고르는 자에겐, 거래란 무리요.");
                    }
                    Console.ReadKey();
                }
            }
        }

        public void ArmorShop()
        {
            while (true)
            {
                Console.Clear();

                // 육군 이등별 직업 여부에 따라 다른 대사 출력
                if (player.Job == "육군 이등별")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[나레이션] 상점 주인은 방어구 선반을 급히 정돈하며 어깨를 펴고 경례를 한다.");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Game.Instance.TypeText("[상점 주인] 충성! 이등별 각하! 최고급 방어구들이 준비되어 있습니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 각하의 안전이 최우선입니다! 특수부대용 방탄 장비도 구비해 놓았습니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 모든 물자는 군 보급품 기준을 충족합니다! 명령만 내려주십시오!", 30);
                    Console.ResetColor();

                    Console.WriteLine("\n1. 방어구 지급받기");
                    Console.WriteLine("2. 방어구 반납하기");
                    Console.WriteLine("0. 돌아가기");
                }
                else // 일반 직업 대사
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Game.Instance.TypeText("[상점 주인] 방어구? 흥, 그런 걸 입는다고 죽음이 비껴갈 것 같나?", 30);
                    Game.Instance.TypeText("[상점 주인] 뭐, 없는 것보단 낫겠지. 조금 더 고통스럽게 죽을 수 있을 테니.", 30);
                    Game.Instance.TypeText("[상점 주인] 흠집 하나하나가 누군가의 마지막 비명이야. 마음에 드는 걸로 골라보게.", 30);
                    Console.ResetColor();

                    Console.WriteLine("\n1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("0. 돌아가기");
                }

                Console.Write("\n선택: ");
                string input1 = Console.ReadLine();

                if (input1 == "1") { ArmorBuy(); }
                else if (input1 == "2") { ArmorSell(); }
                else if (input1 == "0") { return; }
                else
                {
                    Console.Clear();

                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 죄송합니다, 각하! 명령을 잘 이해하지 못했습니다! 다시 말씀해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...결정 못 했으면 이만 가게. 시간 없어.", 30);
                        Console.ResetColor();
                    }
                    Console.ReadKey(); // 임시
                }
            }
        }
        public void ArmorBuy()
        {
            string currentPrompt = player.Job == "육군 이등별" ?
                "\n지급받을 방어구 번호: " : "\n구매할 아이템 번호: "; // 메시지 변경

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"보유 골드:{player.Gold} G");
                Console.WriteLine("\n[아이템 목록]");

                var armorItems = items
                    .Where(item => item.Type == JobOption.Amor).ToList();

                // 아이템 목록 표시
                for (int i = 0; i < armorItems.Count; i++)
                {
                    Item item = armorItems[i];
                    Console.ForegroundColor = item.isBuy ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.Write($"{i + 1}. ");
                    Console.Write($"{item.Name,-15}");
                    Console.ResetColor();
                    Console.Write($"| ");
                    if (item.Attack > 0) Console.ForegroundColor = ConsoleColor.Red;
                    else if (item.Defense > 0) Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{(item.Attack > 0 ? $"공격력 +{item.Attack}" : $"방어력 +{item.Defense}"),-12}");
                    Console.ResetColor();
                    Console.Write($"| ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{item.Gold + " G",-10}");
                    Console.ResetColor();
                    Console.WriteLine($"{(item.isBuy ? (player.Job == "육군 이등별" ? " [지급 완료]" : " [구매 완료]") : "")}");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"   └ {item.Description}");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine("0. 나가기");
                Console.Write(currentPrompt);

                // IdleTalkAsync 시작
                idleTalkTokenSource = new CancellationTokenSource();
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token);

                // 사용자 입력 대기
                string input = Console.ReadLine();

                // 사용자 입력 후 IdleTalkAsync 취소
                idleTalkTokenSource.Cancel();

                if (input == "0") break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= armorItems.Count)
                {
                    Item item = armorItems[choice - 1];

                    if (item.isBuy)
                    {
                        Console.Clear();
                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText("[상점 주인] 충성! 각하! 해당 방어구는 이미 지급 완료되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 추가 장비가 필요하시면 말씀해주십시오!", 30);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Game.Instance.TypeText("[상점 주인] ...그건 이미 자네 옷장 어딘가에 처박혀 있을 텐데.", 30);
                            Game.Instance.TypeText("[상점 주인] 뭘 또 사려는 건가?", 30);
                            Console.ResetColor();
                        }
                        Console.ReadKey();
                        continue;
                    }

                    // 방어구는 직업 제한이 없으므로 해당 확인 생략

                    // 육군 이등별은 방어구를 공짜로 받음
                    if (player.Job == "육군 이등별" || player.SpendGold(item.Gold))
                    {
                        Console.Clear();
                        item.isBuy = true;
                        player.inventory.Add(item);

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText($"[상점 주인] 충성! 각하! {item.Name} 방어구가 성공적으로 지급되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 최고급 방어구로 각하의 안전을 보장하겠습니다!", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({item.Name} 지급 완료)");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Game.Instance.TypeText($"[상점 주인] ...{item.Name}. 그래, 조금이라도 더 버텨 보시게.", 30);
                            Game.Instance.TypeText("[상점 주인] 어차피 뚫릴 갑옷이지만... 없는 것보단 낫겠지.", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({item.Name} 구매 완료)");
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...맨몸으로 버틸 셈인가? 아니면 돈이 없는 건가?", 30);
                        Game.Instance.TypeText("[상점 주인] 구걸은 여기서 통하지 않아.", 30);
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 죄송합니다, 각하! 올바른 번호를 선택해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...내 시간 뺏지 말고 숫자나 제대로 입력하게.", 30);
                        Console.ResetColor();
                    }
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

                // 직업에 따라 다른 타이틀 표시
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine("방어구 반납 메뉴");
                }
                else
                {
                    Console.WriteLine("아이템 판매 메뉴");
                }
                Console.ResetColor();

                List<Item> buyItems = player.inventory.Where(item => item.isBuy && !item.isEquipped && item.Type == JobOption.Amor).ToList();

                if (buyItems.Count == 0)
                {
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 현재 반납 가능한 방어구가 없습니다!", 30);
                        Game.Instance.TypeText("[상점 주인] 필요하신 장비가 있으시면 즉시 말씀해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("비었소. 마음도, 가방도. 지금은 돌아가시오.");
                    }
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < buyItems.Count; i++)
                {
                    var item = buyItems[i];
                    if (player.Job == "육군 이등별")
                    {
                        Console.WriteLine($"{i + 1}. {item.Name}  - 반납 가능");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {item.Name}  - 판매가: {item.Gold / 2} G");
                    }
                }

                Console.WriteLine("0. 나가기");

                // 직업에 따라 다른 프롬프트 표시
                if (player.Job == "육군 이등별")
                {
                    Console.Write("반납할 방어구 번호 입력: ");
                }
                else
                {
                    Console.Write("판매할 아이템 번호 입력: ");
                }

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
                            if (player.Job == "육군 이등별")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Game.Instance.TypeText("[상점 주인] 충성! 각하! 현재 착용 중인 방어구는 반납이 어렵습니다!", 30);
                                Game.Instance.TypeText("[상점 주인] 먼저 장비를 해제해주시기 바랍니다!", 30);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("몸에 붙어 있는 걸 파는 자는… 아직 결심이 덜 선 거요.");
                                Console.WriteLine("아직 자네가 버릴 준비가 안 된 것 같군. 나중에 다시 말하시오.");
                            }
                            Console.ReadKey();
                            continue;
                        }

                        int sellgold = sellitem.Gold / 2;

                        // 육군 이등별은 골드를 추가로 얻지 않음 (무료로 받았으므로)
                        if (player.Job != "육군 이등별")
                        {
                            player.Gold += sellgold;
                        }

                        player.inventory.Remove(sellitem);
                        sellitem.isBuy = false;

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText($"[상점 주인] 충성! 각하! {sellitem.Name} 방어구가 성공적으로 반납되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 다른 장비로 교체를 원하시면 말씀해주십시오!", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({sellitem.Name} 반납 완료)");
                        }
                        else
                        {
                            Console.WriteLine($"({sellitem.Name}을(를) {sellgold} Gold에 판매했습니다.)");
                            Console.WriteLine("이걸 판다고 과거가 사라지진 않소.");
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText("[상점 주인] 충성! 각하! 올바른 번호를 선택해주십시오!", 30);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("이런 자들 때문에 시간만 낭비되는 거지.");
                        }
                        Console.ReadKey();
                    }
                }
                else
                {
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 올바른 형식으로 입력해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("숫자조차 제대로 못 고르는 자에겐, 거래란 무리요.");
                    }
                    Console.ReadKey();
                }
            }
        }


        public void HaberDasheryShop() // 잡화 상점 (포션 등)
        {
            while (true)
            {
                Console.Clear();

                // 육군 이등별 직업 여부에 따라 다른 대사 출력
                if (player.Job == "육군 이등별")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[나레이션] 상점 주인이 군대식 경례를 하며 바짝 얼어붙은 자세로 서 있다.");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Game.Instance.TypeText("[상점 주인] 충성! 이등별 각하! 최고급 의료 보급품이 준비되어 있습니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 생존 키트, 응급 처치약, 에너지 보충제까지 완벽하게 갖추었습니다!", 30);
                    Game.Instance.TypeText("[상점 주인] 특수부대 작전을 위한 모든 물자는 즉시 지급 가능합니다! 명령을 기다리겠습니다!", 30);
                    Console.ResetColor();

                    Console.WriteLine("\n1. 보급품 지급받기");
                    Console.WriteLine("2. 보급품 반납하기");
                    Console.WriteLine("0. 돌아가기");
                }
                else // 일반 직업 대사
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Game.Instance.TypeText("[상점 주인] ...약이라. 이걸 찾는 걸 보니 상태가 말이 아니군 그래.", 30);
                    Game.Instance.TypeText("[상점 주인] 연금술사들이 만든 수상한 물건들이지. 마시면... 글쎄, 뭐가 달라질지는 모르겠군.", 30);
                    Game.Instance.TypeText("[상점 주인] 잠깐의 위안일 뿐이야. 결국엔 다시 고통스러워질 텐데.", 30);
                    Console.ResetColor();

                    Console.WriteLine("\n1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("0. 돌아가기");
                }

                Console.Write("\n선택: ");
                string input1 = Console.ReadLine();

                if (input1 == "1") { haberdasheryBuy(); }
                else if (input1 == "2") { haberdasherySell(); }
                else if (input1 == "0") { return; }
                else
                {
                    Console.Clear();

                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 죄송합니다, 각하! 명령을 제대로 이해하지 못했습니다!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Game.Instance.TypeText("[상점 주인] ...필요 없으면 길 막지 말고 비키게.", 30);
                        Console.ResetColor();
                    }
                    Console.ReadKey(); // 임시
                }
            }
        }

        private void haberdasheryBuy()
        {
            while (true) // 아이템 구매 전용 루프
            {
                Console.Clear();

                // 직업에 따라 다른 화면 표시
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine($"보급품 신청 허가액: {player.Gold} G");
                }
                else
                {
                    Console.WriteLine($"보유 골드: {player.Gold} G");
                }

                Console.WriteLine();

                // 타이틀도 직업에 따라 다르게
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine("사용 가능 보급품 목록");
                }
                else
                {
                    Console.WriteLine("아이템 목록");
                }

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

                // 직업에 따라 다른 프롬프트 표시
                if (player.Job == "육군 이등별")
                {
                    Console.Write("지급받을 보급품 번호: ");
                }
                else
                {
                    Console.Write("원하는 엘릭서를 말해보게... ");
                }

                // 상점 주인 혼잣말 시작
                idleTalkTokenSource = new CancellationTokenSource();
                var idleTask = IdleTalkAsync(idleTalkTokenSource.Token);

                string input = Console.ReadLine();

                // 혼잣말 중지
                idleTalkTokenSource.Cancel();
                idleTask.Wait();

                if (input == "0")
                    break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= potionItems.Count)
                {
                    Item item = potionItems[choice - 1];

                    // 육군 이등별은 무료, 일반 직업은 골드 소비
                    if (player.Job == "육군 이등별" || player.Gold >= item.Gold)
                    {
                        Console.Clear();

                        if (player.Job != "육군 이등별")
                        {
                            player.Gold -= item.Gold;
                        }

                        item.isBuy = true;
                        player.inventory.Add(item);

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText($"[상점 주인] 충성! 각하! {item.Name} 보급품이 성공적으로 지급되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 특수 작전 수행에 최적화된 의약품입니다!", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({item.Name} 지급 완료)");
                        }
                        else
                        {
                            Console.WriteLine($"{item.Name}를 구매했습니다!");
                            Console.WriteLine("좋소. 그대의 생명을 조금은 연장시켜줄 테지.");
                        }
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
                    if (player.Job == "육군 이등별")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 정확한 보급품 번호를 선택해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("그런 번호는 목록에 없소. 당신이 본 게… 진짜였을까?");
                        Console.WriteLine("그럴 여유는 없을 텐데.");
                    }
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

                // 직업에 따라 다른 타이틀 표시
                if (player.Job == "육군 이등별")
                {
                    Console.WriteLine("보급품 반납 메뉴");
                }
                else
                {
                    Console.WriteLine("아이템 판매 메뉴");
                }
                Console.ResetColor();

                List<Item> buyItems = player.inventory.Where(item => item.isBuy && item.Type == JobOption.Potion).ToList();

                if (buyItems.Count == 0)
                {
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 현재 반납 가능한 보급품이 없습니다!", 30);
                        Game.Instance.TypeText("[상점 주인] 필요하신 물자가 있으시면 즉시 말씀해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("비었소. 마음도, 가방도. 지금은 돌아가시오.");
                    }
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < buyItems.Count; i++)
                {
                    var item = buyItems[i];
                    if (player.Job == "육군 이등별")
                    {
                        Console.WriteLine($"{i + 1}. {item.Name}  - 반납 가능");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {item.Name}  - 판매가: {item.Gold / 2} G");
                    }
                }

                Console.WriteLine("0. 나가기");

                // 직업에 따라 다른 프롬프트 표시
                if (player.Job == "육군 이등별")
                {
                    Console.Write("반납할 보급품 번호 입력: ");
                }
                else
                {
                    Console.Write("판매할 아이템 번호 입력: ");
                }

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
                            if (player.Job == "육군 이등별")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Game.Instance.TypeText("[상점 주인] 충성! 각하! 현재 사용 중인 보급품은 반납이 어렵습니다!", 30);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("몸에 붙어 있는 걸 파는 자는… 아직 결심이 덜 선 거요.");
                                Console.WriteLine("아직 자네가 버릴 준비가 안 된 것 같군. 나중에 다시 말하시오.");
                            }
                            Console.ReadKey();
                            continue;
                        }

                        int sellgold = sellitem.Gold / 2;

                        // 육군 이등별은 골드를 추가로 얻지 않음 (무료로 받았으므로)
                        if (player.Job != "육군 이등별")
                        {
                            player.Gold += sellgold;
                        }

                        player.inventory.Remove(sellitem);
                        sellitem.isBuy = false;

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText($"[상점 주인] 충성! 각하! {sellitem.Name} 보급품이 성공적으로 반납되었습니다!", 30);
                            Game.Instance.TypeText("[상점 주인] 미사용 의약품은 다른 전투원들에게 지급하겠습니다!", 30);
                            Console.ResetColor();
                            Console.WriteLine($"\n({sellitem.Name} 반납 완료)");
                        }
                        else
                        {
                            Console.WriteLine($"({sellitem.Name}을(를) {sellgold} Gold에 판매했습니다.)");
                            Console.WriteLine("이걸 판다고 과거가 사라지진 않소.");
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Game.Instance.TypeText("[상점 주인] 충성! 각하! 올바른 번호를 선택해주십시오!", 30);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("이런 자들 때문에 시간만 낭비되는 거지.");
                        }
                        Console.ReadKey();
                    }
                }
                else
                {
                    if (player.Job == "육군 이등별")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Game.Instance.TypeText("[상점 주인] 충성! 각하! 올바른 형식으로 입력해주십시오!", 30);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("숫자조차 제대로 못 고르는 자에겐, 거래란 무리요.");
                    }
                    Console.ReadKey();
                }
            }
        }
    }
}