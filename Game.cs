using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamRpg
{
    // Game 클래스 - 게임 전체를 관리하고 통제하는 클래스입니다.
    public class Game
    {
        // 싱글톤 패턴 구현
        private static Game instance = null;

        // 외부에서 Game 인스턴스에 접근할 수 있는 정적 속성
        public static Game Instance
        {
            get
            {
                if (instance == null)
                    instance = new Game();

                return instance;
            }
        }

        // 게임에 필요한 주요 객체들
        public Player player;        // 플레이어 객체
        public Monster monster;      // 몬스터 객체
        public Battle battle;        // 전투 관리 객체
        public Shop shop;            // 상점 객체
        public List<Item> items;     // 게임 내 존재하는 모든 아이템 목록
        private Random random;       // 랜덤 요소를 위한 난수 생성기

        // 게임 실행 상태
        private bool isRunning;      // 게임이 현재 실행 중인지 여부

        // 생성자
        private Game()
        {
            // 게임에 필요한 객체들을 초기화
            InitializeGame();
        }

        // 게임 초기화 메서드
        private void InitializeGame()
        {
            // 랜덤 객체 초기화
            random = new Random();

            // 아이템 목록 초기화
            items = ItemManager.Instance.Items;

            // 게임 상태 초기화
            isRunning = false;

            Console.WriteLine("게임 시스템이 초기화되었습니다.");
        }

        // 게임 시작 메서드
        public void Start()
        {
            // 이미 게임이 실행 중이면 중복 실행 방지
            if (isRunning)
            {
                Console.WriteLine("게임이 이미 실행 중입니다.");
                return;
            }

            // 게임 실행 상태로 변경
            isRunning = true;

            // 게임 시작 화면 표시
            DisplayStartScreen();

            // 플레이어 생성
            CreatePlayer();

            // 게임 객체 초기화
            InitializeGameObjects();

            // 메인 게임 루프 실행
            GameLoop();

            // 게임 종료 시 실행될 코드
            Console.WriteLine("게임이 종료되었습니다. 다음에 또 뵙겠습니다!");
        }

        // 시작 화면 표시 메서드
        private void DisplayStartScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.WriteLine("          TEAM RPG ADVENTURE            ");
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine("\n스파르타 던전에 오신 것을 환영합니다!");
            Console.WriteLine("이곳에서 던전을 탐험하고, 몬스터를 물리치며 성장해보세요.");
            Console.WriteLine("\n게임을 시작하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // 플레이어 생성 메서드
        // 플레이어 생성 메서드
        private void CreatePlayer()
        {
            Console.Clear();
            Console.WriteLine("캐릭터 생성을 시작합니다.");

            // 이름 입력 받기
            Console.Write("당신의 이름을 입력하세요: ");
            string name = Console.ReadLine();

            // 이름이 비어있으면 기본 이름 설정
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "용사";
                Console.WriteLine("이름이 입력되지 않아 '용사'로 설정되었습니다.");
            }

            // 직업 선택
            string job = "";
            bool validJob = false;

            while (!validJob)
            {
                Console.WriteLine("\n직업을 선택하세요:");
                Console.WriteLine("1. 검투사 - 체력과 방어력이 높은 전사입니다.");
                Console.WriteLine("콜로세움에서 전투를 즐기던 전사. 적들과의 전투에서 물러서지 않는 투지를 발휘합니다.");
                Console.WriteLine();
                Console.WriteLine("2. 수렵꾼 - 균형 잡힌 능력치를 가진 궁수입니다.");
                Console.WriteLine("숲을 누비던 청부사냥꾼. 사냥꾼으로서의 직감과 경험을 바탕으로 적들을 물리치는 데 능숙합니다.");
                Console.WriteLine();
                Console.WriteLine("3. 암살자 - 공격력이 높은 도적입니다.");
                Console.WriteLine("돈만 주면 누구든 암살하는 암부의 에이스 암살자. 어둠 속에서 타겟을 조용히 처리하는 능력을 가지고 있습니다.");
                Console.WriteLine();

                Console.Write("\n선택 (1-3): ");
                string jobChoice = Console.ReadLine();

                switch (jobChoice)
                {
                    case "1":
                        job = "검투사";
                        validJob = true;
                        break;
                    case "2":
                        job = "수렵꾼";
                        validJob = true;
                        break;
                    case "3":
                        job = "암살자";
                        validJob = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 1부터 3까지의 숫자를 입력해주세요.");
                        break;
                }
            }

            // 화면 지우기
            Console.Clear();

            // 플레이어 객체 생성
            player = new Player(name, job);

            // 기본 장비 지급
            GiveStartingItems();

            Console.WriteLine("\n캐릭터 생성이 완료되었습니다! 게임을 시작합니다.");
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);

            // 직업별 스토리 표시
            ShowJobStory(job);

            // 키 입력 후 화면 다시 지우기
            Console.Clear();
        }

        // 직업별 스토리 표시 메서드
        private void ShowJobStory(string job)
        {
            switch (job)
            {
                case "검투사":
                    // 1부: 시작
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        검투사로서의 삶        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine(@"
   _____      _                               
  / ____|    | |                              
 | |     ___ | | ___  ___ ___ _   _ _ __ ___  
 | |    / _ \| |/ _ \/ __/ __| | | | '_ ` _ \ 
 | |___| (_) | | (_) \__ \__ \ |_| | | | | | |
  \_____\___/|_|\___/|___/___/\__,_|_| |_| |_|
            ");
                    Console.WriteLine("\n승리. 그저 승리만을 추구하던 시절이 있었다.");
                    Console.WriteLine("영광한 전투에서 적들을 베어내고, 자신의 힘을 증명하는 것.");
                    Console.WriteLine("그것이 검투사로서의 삶이었다.");
                    Console.WriteLine("\n하지만 언제부터 였을까.");
                    Console.WriteLine("\n베고 죽이는데에...즐거움도 명예도 목표도 없다는것을 알게된다.");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 2부: 전환점
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        삶의 2부가 시작된다.        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine("\n그렇다면 이제는 무엇을 위해 싸워야 할까?");
                    Console.WriteLine("\n이제는 더 이상 싸우지 않겠다고 결심한다.");
                    Console.WriteLine("\n하지만 그 결심은 오래가지 못했다.");
                    Console.WriteLine("\n검투사로서의 삶을 끝내고 농사도 지어보고, 대장간에도 취직해봤지만");
                    Console.WriteLine("\n몸 속에 남은 이상한 갈망이 끊임없이 나를 굶주리게 했다.");
                    Console.WriteLine();
                    Console.WriteLine("\n집 구석에 박혀있는 검이 언제나 나를 부르듯 속삭인다.");
                    Console.WriteLine("\n넌 내게서 도망칠 수 없어...라고.");
                    Console.WriteLine("\n결국 나는....다시 검을 잡고 용병의 길을 선택했다.");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 3부: 현재
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        용병으로 살아가던 나날        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine("\n꽤 적성에 맞았다. 그저 돈을 받고 죽였다.");
                    Console.WriteLine("\n명예는 없었지만, 금화와 보물이 있었다.");
                    Console.WriteLine("\n그렇게 나는 용병으로서의 삶을 살았다.");
                    Console.WriteLine("\n갑자기 생긴 던전의 이야기로 떠들석했지만, 난 신경쓰지 않았다.");
                    Console.WriteLine("\n그저 돈이 되는 일이라면 뭐든지 했다.");
                    Console.WriteLine("\n그러던 어느날. 선발대라는 것이 던전으로 들어갔고");
                    Console.WriteLine("\n그들은 돌아오지 않았다.");
                    Console.WriteLine("\n그렇게 일자리가 점점 줄어들던 어느날");
                    Console.WriteLine("\n왕의 칙서가 내 손에 쥐어졌다.");
                    break;

                case "수렵꾼":
                    // 1부: 시작
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        수렵꾼의 이야기 - 1부        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine(@"
  ______                  _   
 |  ____|                | |  
 | |__ ___  _ __ ___  ___| |_ 
 |  __/ _ \| '__/ _ \/ __| __|
 | | | (_) | | |  __/\__ \ |_ 
 |_|  \___/|_|  \___||___/\__|
            ");
                    Console.WriteLine("\n스토리 텍스트");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 2부: 전환점
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        수렵꾼의 이야기 - 2부        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine("\n스토리 텍스트");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 3부: 현재
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        수렵꾼의 이야기 - 3부        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine("\n스토리 텍스트");
                    break;

                case "암살자":
                    // 1부: 시작
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        암살자의 이야기 - 1부        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine(@"
   _____ _               _               
  / ____| |             | |              
 | (___ | |__   __ _  __| | _____      __
  \___ \| '_ \ / _` |/ _` |/ _ \ \ /\ / /
  ____) | | | | (_| | (_| | (_) \ V  V / 
 |_____/|_| |_|\__,_|\__,_|\___/ \_/\_/  
            ");
                    Console.WriteLine("\n스토리 텍스트");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 2부: 전환점
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        암살자의 이야기 - 2부        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine("\n스토리 텍스트");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 3부: 현재
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        암살자의 이야기 - 3부        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine("\n스토리 텍스트");
                    break;

                default:
                    Console.WriteLine("\n스토리 텍스트");
                    break;
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
        }

        // 시작 장비 지급 메서드
        // 시작 장비 지급 메서드
        private void GiveStartingItems()
        {
            // 플레이어 직업에 따른 기본 무기 찾기
            Item startingWeapon = null;
            Item startingArmor = null;

            // 모든 아이템을 검사하며 직업에 맞는 기본 장비 찾기
            foreach (Item item in items)
            {
                // 기본 방어구는 "패디드 아머" (공용, 가장 낮은 방어력)
                if (item.Job == "공용" && item.Name == "패디드 아머" && item.Gold == 0)
                {
                    startingArmor = item;
                    item.isBuy = true; // 구매한 것으로 표시
                }

                // 기본 무기는 직업별로 다름 (Gold가 0인 가장 기본 무기)
                if (item.Job == player.Job && item.Gold == 0 && item.Attack > 0)
                {
                    startingWeapon = item;
                    item.isBuy = true; // 구매한 것으로 표시
                }
            }

            // 기본 장비 인벤토리에 추가 (자동 장착 없음)
            if (startingWeapon != null)
            {
                player.inventory.Add(startingWeapon);
                Console.WriteLine($"기본 무기를 지급받았습니다: {startingWeapon.Name}");
            }

            if (startingArmor != null)
            {
                player.inventory.Add(startingArmor);
                Console.WriteLine($"기본 방어구를 지급받았습니다: {startingArmor.Name}");
            }

            // 시작 골드
            player.Gold = 0;
            Console.WriteLine($"던전을 탐험하며, 선발대의 흔적을 찾고 더 많은 골드와 아이템을 모아보세요!");
            Console.WriteLine("\n인벤토리에서 장비를 장착하여 던전에 도전해보세요!");
        }



        // 게임 객체 초기화 메서드
        private void InitializeGameObjects()
        {
            // 상점 객체 생성
            shop = new Shop(items, player);

            // 전투 시스템 객체 생성
            battle = new Battle();

            Console.WriteLine("게임 객체 초기화가 완료되었습니다.");
        }

        // 메인 게임 루프 메서드
        private void GameLoop()
        {
            // 게임 종료 전까지 계속 반복
            while (isRunning)
            {
                // 메인 메뉴 표시
                DisplayMainMenu();

                // 사용자 선택에 따라 처리
                string choice = Console.ReadLine();
                ProcessMainMenuChoice(choice);

                // 플레이어 사망 시 게임 종료
                if (player.Health <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("게임 오버! 당신은 사망했습니다.");
                    Console.ResetColor();
                    Console.WriteLine("\n아무 키나 누르면 게임이 종료됩니다...");
                    Console.ReadKey(true);
                    isRunning = false;
                }
            }
        }

        // 메인 메뉴 표시 메서드
        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("              메인 메뉴                 ");
            Console.WriteLine("========================================");
            Console.ResetColor();

            // 플레이어 기본 정보 간단 표시
            Console.WriteLine($"\n{player.Name} ({player.Job}) | 체력: {player.Health}/{player.MaxHealth} | 골드: {player.Gold}G");

            // 메뉴 옵션 표시
            Console.WriteLine("\n원하시는 행동을 선택하세요:");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("0. 게임 종료");

            Console.Write("\n선택: ");
        }

        // 메인 메뉴 선택 처리 메서드
        private void ProcessMainMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1": // 상태 보기
                    Console.Clear();
                    player.DisplayStatus();
                    WaitForKeyPress();
                    break;

                case "2": // 인벤토리
                    player.inventory.MainInventory();
                    break;

                case "3": // 상점
                    shop.ShopMenu();
                    break;

                case "4": // 던전 입장
                    EnterDungeon();
                    break;

                case "5": // 휴식하기
                    Rest();
                    break;

                case "0": // 게임 종료
                    ConfirmExit();
                    break;

                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                    WaitForKeyPress();
                    break;
            }
        }

        // 던전 입장 메서드
        private void EnterDungeon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("========================================");
            Console.WriteLine("              던전 입장                 ");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Console.WriteLine("\n입장할 던전을 선택하세요:");
            Console.WriteLine("1. 쉬운 던전 (레벨 1-5)");
            Console.WriteLine("2. 보통 던전 (레벨 6-10)");
            Console.WriteLine("3. 어려운 던전 (레벨 11-15)");
            Console.WriteLine("0. 뒤로 가기");

            Console.Write("\n선택: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // 쉬운 던전
                    StartBattle(DungeonDifficulty.Easy);
                    break;

                case "2": // 보통 던전
                    StartBattle(DungeonDifficulty.Normal);
                    break;

                case "3": // 어려운 던전
                    StartBattle(DungeonDifficulty.Hard);
                    break;

                case "0": // 뒤로 가기
                    return;

                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                    WaitForKeyPress();
                    break;
            }
        }

        // 던전 난이도 열거형
        private enum DungeonDifficulty
        {
            Easy,
            Normal,
            Hard
        }

        // 던전 난이도에 따른 몬스터 생성
        public string monsterName;
        public int monsterHealth;
        public int monsterMana;
        public int monsterAttack;
        public int monsterDefense;
        public int rewardGold;

        public List<Monster> monstersInBattle = new List<Monster>();
        // 전투 시작 메서드
        private void StartBattle(DungeonDifficulty difficulty)
        {
            int monsterCount = random.Next(1, 5);

            for (int i = 0; i < monsterCount; i++)
            {
                switch (difficulty)
                {
                    case DungeonDifficulty.Easy:
                        monsterName = GetRandomMonsterName(difficulty);
                        monsterHealth = random.Next(15, 30);
                        monsterMana = random.Next(5, 15);
                        monsterAttack = random.Next(3, 8);
                        monsterDefense = random.Next(1, 4);
                        rewardGold = random.Next(100, 300);
                        break;

                    case DungeonDifficulty.Normal:
                        monsterName = GetRandomMonsterName(difficulty);
                        monsterHealth = random.Next(30, 50);
                        monsterMana = random.Next(15, 30);
                        monsterAttack = random.Next(8, 15);
                        monsterDefense = random.Next(4, 8);
                        rewardGold = random.Next(300, 600);
                        break;

                    case DungeonDifficulty.Hard:
                        monsterName = GetRandomMonsterName(difficulty);
                        monsterHealth = random.Next(50, 80);
                        monsterMana = random.Next(30, 50);
                        monsterAttack = random.Next(15, 25);
                        monsterDefense = random.Next(8, 12);
                        rewardGold = random.Next(600, 1000);
                        break;

                    default:
                        monsterName = "알 수 없는 몬스터";
                        monsterHealth = 20;
                        monsterMana = 10;
                        monsterAttack = 5;
                        monsterDefense = 2;
                        rewardGold = 100;
                        break;
                }
                monstersInBattle.Add(new Monster(monsterName, monsterHealth, monsterMana, monsterAttack, monsterDefense, rewardGold));
            }

            // 몬스터 객체 생성
            //monster = new Monster(monsterName, monsterHealth, monsterMana, monsterAttack, monsterDefense);

            // 전투 시작 메시지
            Console.Clear();
            Console.WriteLine($"\n총 {monstersInBattle.Count}마리의 몬스터가 등장했습니다!\n");

            // 전투 실행
            battle.Start();
        }

        // 랜덤 몬스터 이름 생성 메서드
        private string GetRandomMonsterName(DungeonDifficulty difficulty)
        {
            string[] easyMonsters = { "슬라임", "고블린", "쥐", "박쥐", "뱀" };
            string[] normalMonsters = { "오크", "좀비", "스켈레톤", "거미", "거대 거미" };
            string[] hardMonsters = { "트롤", "오우거", "미노타우루스", "드래곤", "데몬" };

            string[] monsters;

            // 난이도에 따라 몬스터 목록 선택
            switch (difficulty)
            {
                case DungeonDifficulty.Easy:
                    monsters = easyMonsters;
                    break;

                case DungeonDifficulty.Normal:
                    monsters = normalMonsters;
                    break;

                case DungeonDifficulty.Hard:
                    monsters = hardMonsters;
                    break;

                default:
                    monsters = easyMonsters;
                    break;
            }

            // 랜덤한 몬스터 이름 선택
            return monsters[random.Next(monsters.Length)];
        }

        // 휴식 메서드
        private void Rest()
        {
            Console.Clear();
            Console.WriteLine("휴식을 취합니다...");

            // 휴식 비용
            int restCost = 100;

            // 보유 골드 확인
            if (player.Gold < restCost)
            {
                Console.WriteLine($"휴식을 취하기 위해 {restCost}G가 필요하지만, 보유 골드가 부족합니다.");
                WaitForKeyPress();
                return;
            }

            // 휴식 비용 지불
            bool paymentSuccess = player.SpendGold(restCost);

            if (paymentSuccess)
            {
                // 체력과 마나 완전 회복
                int oldHealth = player.Health;
                int oldMana = player.Mana;

                player.Health = player.MaxHealth;
                player.Mana = player.MaxMana;

                Console.WriteLine("충분한 휴식을 취했습니다!");
                Console.WriteLine($"체력: {oldHealth} -> {player.Health}/{player.MaxHealth}");
                Console.WriteLine($"마나: {oldMana} -> {player.Mana}/{player.MaxMana}");
            }

            WaitForKeyPress();
        }

        // 게임 종료 확인 메서드
        private void ConfirmExit()
        {
            Console.Clear();
            Console.WriteLine("정말로 게임을 종료하시겠습니까?");
            Console.WriteLine("1. 예");
            Console.WriteLine("2. 아니오");

            Console.Write("\n선택: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                isRunning = false;
                Console.WriteLine("게임을 종료합니다. 이용해 주셔서 감사합니다!");
            }
            // 다른 입력은 모두 취소로 처리
        }

        // 키 입력 대기 메서드
        public void WaitForKeyPress()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
        }
    }
}
