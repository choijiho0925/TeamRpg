﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NAudio.Wave;

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

        // 키보드 버퍼를 비우는 메서드
        private void ClearKeyboardBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true); // true는 키 입력을 화면에 표시하지 않음
            }
        }

        // 시작 화면 표시 메서드
        private void DisplayStartScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.WriteLine("          Into the Abyss            ");
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine("\n정체를 알 수 없는 심연의 던전에 오신 것을 환영합니다!");
            Console.WriteLine("던전을 탐험하고, 미스테리한 사건에 직면하며 엔딩에 도달해보세요.");

            // 전체화면 권장 메시지 (경고 형태로 강조)
            Console.ForegroundColor = ConsoleColor.Red;  // 빨간색으로 설정 (경고색)
            Console.WriteLine("======================================================");
            Console.WriteLine("=    !!! 이 게임은 전체화면 플레이를 권장합니다!!!   =");
            Console.WriteLine("=    전체화면 미 활용시, 스크립트 반복 출력 오류가   =");
            Console.WriteLine("=       발생하여 게임의 몰입감을 해칠수있습니다.     =");
            Console.WriteLine("======================================================");
            Console.ResetColor();  // 색상 초기화

            Console.WriteLine("\n게임을 시작하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // 플레이어 생성 메서드
        // 플레이어 생성 메서드
        // CreatePlayer 메서드의 관련 부분을 수정합니다 - Game.cs 파일 내부
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

                // 숨겨진 힌트로 육군 이등별 존재 암시 (매우 옅은 색상으로)
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("? .... .... ... - .... ... .....");
                Console.ResetColor();

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

                    case "육군 이등별": // 직접 "육군 이등별"을 입력해도 됨
                    case "4": // 4를 숨겨진 옵션으로 추가
                    case "이등별":
                    case "군인":
                        job = "육군 이등별";
                        validJob = true;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("축하합니다! 당신은 육군 이등별로 특별 징집되었습니다!");
                        Console.WriteLine("역시 대한의 건아! 자랑스럽습니다!");
                        Console.WriteLine("이제 신체포기각서에 지장만 찍으면 전직 완료!");
                        Console.ResetColor();
                        Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                        Console.ReadKey(true);
                        break;

                    default:
                        // 잘못된 선택 시 자동으로 육군 이등별이 되도록 변경
                        job = "육군 이등별";
                        validJob = true;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("잘못된 선택입니다. 하지만...");
                        Console.WriteLine("축하합니다! 당신은 육군 이등별로 특별 징집되었습니다!");
                        Console.WriteLine("역시 대한의 건아! 자랑스럽습니다!");
                        Console.WriteLine("이제 신체포기각서에 지장만 찍으면 전직 완료!");
                        Console.ResetColor();
                        Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                        Console.ReadKey(true);
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

            // 스토리 스킵 옵션 추가
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n'G'키를 누르면 모든 스토리를 스킵할 수 있습니다.");
            Console.ResetColor();
            Console.WriteLine("아무 키나 누르세요...");

            // 키 입력 확인
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // 입력된 키의 문자를 확인 - 영문 'G', 'g' 또는 한글 'ㅎ' 확인
            bool skipStory = (keyInfo.Key == ConsoleKey.G || keyInfo.KeyChar == 'g' || keyInfo.KeyChar == 'G' || keyInfo.KeyChar == 'ㅎ') && (job != "육군 이등별");

            // 직업별 스토리 표시 (육군 이등별은 스킵)
            if (job != "육군 이등별" && !skipStory)
            {
                ShowJobStory(job);
            }

            // 모든 직업에 대해 왕의 칙서 표시
            ShowRoyalDecree();

            // 키 입력 후 화면 다시 지우기
            Console.Clear();
        }

        // 타이핑 효과를 주는 글자 출력 메서드
        public void TypeText(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay); // 각 글자 사이 딜레이(밀리초)
            }
            Console.WriteLine(); // 텍스트 출력 후 줄바꿈
        }

        // 여러 줄의 텍스트를 타이핑 효과로 출력하는 메서드
        public void TypeMultipleLines(string[] lines, int charDelay = 30, bool waitForKeyAfterEachLine = false)
        {
            foreach (string line in lines)
            {
                TypeText(line, charDelay);

                if (waitForKeyAfterEachLine)
                {
                    Console.ReadKey(true); // 키 입력 기다리기
                }
            }
        }

        // 직업별 스토리 표시 메서드
        private void ShowJobStory(string job)
        {
            //exe파일 실행점기준폴더경로
            string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/jobstory.wav");
            //음악실행하는기준점을 exe실행하는곳으로 기준점을잡아서 해결
            Music.PlayMusic(test);
            switch (job)
            {
                case "검투사":

                    // 1부: 시작
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        검투사: 모래 위의 투혼        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow; // 노란색으로 설정

                    Console.WriteLine(@"
                            .|
                            | |
                            |'|            ._____
                    ___    |  |            |.   |' .---.|
            _    .-'   '-. |  |     .--'|  ||   | _|    |
         .-'|  _.|  |    ||   '-__  |   |  |    ||      |
         |' | |.    |    ||       | |   |  |    ||      |
     ___|  '-'     '    ""       '-'   '-.'    '`      |__
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
     T H E   C O L O S S E U M                
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ");
                    Console.ResetColor();

                    // 타이핑 효과로 스토리 출력
                    string[] storyLines = {
                "한때, 콜로세움의 뜨거운 모래 위에서 군중의 환호를 받으며 살았다.",
                "강철과 강철이 부딪히는 소리, 승리의 함성, 패자의 신음...",
                "오직 강함만이 증명되는 세계에서 정점에 서기도 했다.",
                "내 이름 앞에는 늘 '불패의', '무적의' 같은 수식어가 따라붙었지.",
                "",
                "영광스러운 전투에서 적들을 베어내고 힘을 증명하는 것.",
                "그것이 검투사로서 나의 전부였다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 2부: 공허함과 새로운 길
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       검투사: 영광 뒤의 그림자         ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow; // 노란색으로 설정

                    Console.WriteLine(@"
~
          ~ ~
         ~   ~
        ~     |
       ~      |
      ~    ___|___
          /       \
         /         \
        /___________\
       /|           |\
      / |    __     | \
     /  |   |  |    |  \
    /   |   |__|    |   \
   /____|___________|____\
       |             |
       |_____________|

 ~~~~~~~~~~~~~~~~~~~~~~~~~~
  RETIRED GLADIATOR'S HUT
 ~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ResetColor();


                    storyLines = new string[] {
                "하지만 언제부터였을까. 끝없는 싸움에 영혼이 마모되는 것을 느꼈다.",
                "승리의 순간에도 더 이상 가슴이 뛰지 않았다. 오직 피와 죽음의 공허함만이 남았다.",
                "베고 죽이는 행위에 즐거움도, 명예도, 목표도 사라져 버렸다.",
                "",
                "검을 놓고 다른 삶을 살아보려 했다. 농사도 지어보고, 대장간 일도 해봤지만,",
                "내 몸은 평화로운 삶을 거부했다. 싸움에 대한 갈망이 나를 놓아주지 않았다.",
                "집 구석에 박힌 검은 여전히 나를 유혹하는 듯 속삭였다. '넌 도망칠 수 없어...'"
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 3부: 용병의 삶
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       검투사: 용병의 길           ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; // 노란색으로 설정

                    Console.WriteLine(@"
       .-.                     .-.                    .-.                      
    .--' '--.               .--' '--.              .--' '--.                  
    '--------'              '--------'              '--------'                 
       | |                     | |                    | |                     
 .-----' '-----------------------------------------' '-----.                  
/                                                           \                 
|  .===============.   .=======================.   .======.  |                 
|  |    /|    |\   |   |                       |   | +--+ |  |                 
|  |   / |    | \  |   |      용 병 길 드      |   | |\/| |  |                 
|  |  /__|    |__\ |   |                       |   | |/\| |  |                 
|  |  \  |    |  / |   |   GOLD & ADVENTURE    |   | +--+ |  |                 
|  |   \ |    | /  |   |                       |   |      |  |                 
|  |    \|    |/   |   |       EST. 986        |   |      |  |                 
|  '==============='   '=======================;   '======'  |                 
|                                                           |                 
\                                                           /                 
 '-----------------------------------------------------------'   
            ");
                    Console.ResetColor();

                    storyLines = new string[] {
                "결국, 나는 다시 검을 잡았다. 다만 이번엔 영광이 아닌, 돈을 위해서였다.",
                "용병. 의뢰를 받고 싸우는 삶은 의외로 적성에 맞았다.",
                "명예는 없었지만, 금화와 술이 있었다. 과거의 영광 따위는 잊은 지 오래.",
                "그저 하루하루 주어지는 의뢰를 처리하며 살아갈 뿐.",
                "",
                "세상이 어떻게 돌아가는지, 왕국에 무슨 일이 벌어지는지는 큰 관심사가 아니다.",
                "내게 중요한 것은 다음 의뢰와 그 보수뿐이니까.",
                "하지만 가끔, 밤의 정적 속에서 문득 스스로에게 묻는다.",
                "'이것이 내가 원했던 삶인가?' ... 답은 찾지 못했다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 4부: 왕국의 부름
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       검투사: 왕국의 부름           ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "최근 들어 용병 일거리가 눈에 띄게 줄었다.",
                "수도 근처에서 뭔가 큰일이 벌어졌다는 소문만 무성할 뿐, 정확한 내막은 알 수 없었다.",
                "의뢰가 줄어드니 수입도 자연스레 줄었고, 술값마저 부담스러워지기 시작했다.",
                "다시 농사라도 지어야 하나, 아니면 더 위험한 일이라도 찾아야 하나 고민하던 찰나.",
                "",
                "낡은 용병 숙소의 문을 두드리는 소리가 들렸다.",
                "문을 열자, 값비싼 갑옷을 입은 왕궁의 전령이 서 있었다.",
                "그는 나를 위아래로 훑어보더니, 정중하지만 단호한 목소리로 말했다.",
                "'왕명을 받들라.'",
                "",
                "...그렇게 왕의 칙서가 내 손에 쥐어졌다."
            };

                    TypeMultipleLines(storyLines);

                    break;


                case "수렵꾼":
                    // 1부: 시작
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        수렵꾼: 숲의 아들        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine(@"
            ,@@@@@@@,
    ,,,.   ,@@@@@@/@@,  .oo8888o.
 ,&%%&%&&%,@@@@@/@@@@@@,8888\88/8o
,%&\%&&%&&%,@@@\@@@/@@@88\88888/88'
%&&%&%&/%&&%@@\@@/ /@@@88888\88888'
%&&%/ %&%%&&@@\ V /@@' `88\8 `/88'
`&%\ ` /%&'    |.|        \ '|8'
    |o|        | |         | |
    |.|        | |         | |
 \\/ ._\//_/__/  ,\_//__\\/.  \_//__/_
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
     M Y S T I C   F O R E S T
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ResetColor();

                    // 타이핑 효과로 스토리 출력
                    storyLines = new string[] {
                "어렸을 때부터 숲이 나의 스승이자 놀이터였다.",
                "바람을 읽고, 흔적을 쫓고, 소리 없이 활시위를 당기는 법을 배웠다.",
                "짐승을 사냥하고 가죽을 벗겨 내다 팔며 겨우 입에 풀칠하던 시절.",
                "부모님이 돌아가신 후, 세상은 더욱 가혹해졌다.",
                "",
                "살아남기 위해, 나는 나의 기술을 다른 방식으로 사용하기 시작했다.",
                "'청부 수렵꾼'. 돈을 받고 '인간' 사냥을 의뢰받았다.",
                "숲은 여전히 나의 영역이었고, 그곳에서 나는 누구보다 뛰어난 사냥꾼이었다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 2부: 부러진 화살
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       수렵꾼: 예기치 못한 부상       ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "언제나처럼 의뢰를 수행하던 날이었다. 날씨도 좋지 않았고, 표적도 유난히 까다로웠다.",
                "숲으로 유인하는 것까진 성공했지만, 그는 혼자가 아니었다.",
                "매복. 나를 노린 함정이었다. 그들이 노린 것은 내가 가진 돈이었을까, 아니면 내 목숨 자체였을까.",
                "",
                "격렬한 싸움 끝에 간신히 살아남았지만, 대가는 컸다.",
                "무릎에 깊숙이 박힌 화살은 나의 민첩함을 영원히 앗아갔다.",
                "절뚝이는 다리로는 더 이상 예전처럼 숲을 누빌 수 없었다.",
                "나의 시대는 그렇게 끝났음을 직감했다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 3부: 은퇴자의 그늘
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       수렵꾼: 술집의 이방인         ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "숲을 떠나 이름 없는 마을에 정착했다.",
                "가끔 좀도둑질을 하거나, 사냥으로 얻은 고기를 팔아 근근이 살아갔다.",
                "삶의 유일한 낙은 해 질 녘 술집에 들러 독한 술을 마시는 것.",
                "왁자지껄한 사람들 속에서 나는 언제나 이방인이었다.",
                "",
                "다친 무릎은 비가 오거나 날이 궂으면 욱신거렸고, 그때마다 과거가 떠올랐다.",
                "자유롭게 숲을 누비던 시절, 날카로웠던 감각, 그리고... 내가 저질렀던 일들.",
                "후회는 없다. 그저 살아남기 위한 발버둥이었을 뿐.",
                "하지만 가끔씩, 조용한 밤이면 활을 잡고 싶다는 충동에 시달린다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 4부: 뜻밖의 방문
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       수렵꾼: 뜻밖의 방문           ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "마을에도 뒤숭숭한 소문이 돌기 시작했다. 수도 근처에 괴물 소굴이라도 생긴 건지...",
                "그런 소문 때문인지, 마을을 찾는 외지인의 발길도 뜸해지고,",
                "내가 간간이 하던 일거리마저 끊겨 생활이 더욱 팍팍해졌다.",
                "무릎은 여전히 성가시게 굴었지만, 몸은 근질거렸다. 뭔가 해야 했다.",
                "",
                "여느 때처럼 술집 구석에서 술잔을 기울이고 있을 때였다.",
                "낯선 차림의 남자가 다가와 내 앞에 앉았다. 그의 눈빛은 예사롭지 않았다.",
                "그는 내 과거를 알고 있다는 듯이, 은밀하게 말을 건넸다.",
                "'당신의 능력이 필요하다는 분의 전갈이오.'",
                "",
                "그가 내민 것은 양피지에 찍힌 왕가의 인장이 선명한 문서였다.",
                "왕의 칙서. 은퇴한 나를 어떻게 찾아낸 것일까."
            };

                    TypeMultipleLines(storyLines);
                    break;

                case "암살자":
                    // 1부: 시작
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"        암살자: 암부의 도구        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.WriteLine(@"
                        /\
                      /##\
                    /####\
                  /######\
                /########\
              /##########\
            |############|
           |############|
          |############|
         |############|
        |############|
       |=============|
      |++++++++++++++|
     ||||||||||||||
    ||||||||||||||
   \/\/\/\/\/\/
~~~~~~~~~~~~~~~
    DAGGER
~~~~~~~~~~~~~~~");

                    // 타이핑 효과로 스토리 출력
                    storyLines = new string[] {
                "나의 유년기는 '암부'라 불리는 그림자 조직 안에서 시작되었다.",
                "이름 대신 코드로 불렸고, 감정 대신 기술을 배웠다.",
                "소리 없이 다가가 흔적 없이 제거하는 법. 그것이 내가 배운 전부였다.",
                "나에게 주어진 유일한 목표는 조직 최고의 암살자가 되는 것.",
                "그것만이 이 지긋지긋한 어둠 속에서 벗어날 유일한 길이라 믿었다.",
                "",
                "그래서 닥치는 대로 죽였다. 신분도, 이유도 묻지 않았다. 오직 명령만이 존재했다.",
                "피로 얼룩진 길 위에서 나의 명성은 높아져만 갔다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 2부: 배신과 탈출
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       암살자: 버려진 칼날           ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "조직의 신뢰를 얻었다고 생각했을 때, 나는 가장 큰 실수를 저질렀다.",
                "암부의 수뇌부는 결코 도구를 신뢰하지 않았다. 그들은 오직 이용하고 버릴 뿐.",
                "나의 명성이 너무 높아진 것이 문제였을까. 아니면 그들의 비밀을 너무 많이 알게 된 것일까.",
                "어느 날 내려온 명령. 다음 제거 대상은 바로 나 자신이었다.",
                "",
                "허탈했다. 평생을 바친 조직에게 배신당했다는 사실보다,",
                "그들의 손아귀에서 벗어날 수 없다고 믿었던 어리석음이 더 견딜 수 없었다.",
                "나는 처음으로 명령을 거부하고 도망쳤다.",
                "과거의 동료들이 추격자로 변해 나를 쫓았고, 나는 그들을 모두 베어 넘기며 어둠 속으로 사라졌다."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 3부: 숨겨진 삶
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       암살자: 그림자 속의 생존        ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "그 후로 나는 유령처럼 살았다. 이름도, 얼굴도, 과거도 없는 존재.",
                "작은 마을들을 전전하며 눈에 띄지 않게 숨어 지냈다.",
                "암부의 추적은 집요했고, 잠시의 방심도 허용되지 않았다.",
                "사람들과 어울리는 법도, 평범하게 살아가는 법도 잊은 지 오래.",
                "내가 아는 세상은 오직 경계와 의심, 그리고 생존 본능뿐이었다.",
                "",
                "때로는 과거의 기술을 이용해 생계를 유지하기도 했지만,",
                "그럴 때마다 암부에게 발각될 위험은 커져만 갔다.",
                "나는 여전히 그림자 속에 속박되어 있었다.",
                "언제까지 이렇게 도망쳐야 할까. 진정한 자유는 어디에 있는 걸까."
            };

                    TypeMultipleLines(storyLines);

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(true);

                    // 4부: 드러난 그림자
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine($"       암살자: 드러난 그림자           ");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    storyLines = new string[] {
                "숨어 지내는 것도 한계에 다다르고 있었다. 자금은 바닥났고, 암부의 추적은 점점 숨통을 조여왔다.",
                "이 마을도 더 이상 안전하지 않다는 불길한 예감이 들었다. 떠나야 했다.",
                "하지만 어디로 가야 할까? 암부의 손길이 닿지 않는 곳이 세상에 존재하기는 할까?",
                "막다른 길에 몰린 기분이었다.",
                "",
                "짐을 챙겨 어둠 속으로 다시 몸을 숨기려던 그 밤.",
                "골목길 어귀에서 나를 기다리고 있는 이들이 있었다. 암부가 아니었다.",
                "그들의 복장은... 왕궁 소속임을 나타내고 있었다.",
                "그들은 나를 제압하려 하지 않았다. 대신, 한 사람이 조용히 문서를 내밀었다.",
                "'왕께서 찾으신다.'",
                "",
                "그것은... 왕의 칙서였다. 어떻게 나를...? 그리고 왜...?"
            };

                    TypeMultipleLines(storyLines);
                    break;

                default: // 혹시 모를 예외 처리
                    Console.WriteLine("\n당신의 이야기는 아직 시작되지 않았습니다...");
                    break;
            }

            // 모든 직업 스토리 공통으로 칙서 내용 표시 전에 잠시 대기
            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
        }

        // 왕의 칙서 내용 표시
        // ShowRoyalDecree 메서드 수정
        private void ShowRoyalDecree()
        {
            // 육군 이등별인 경우 단편명령 출력
            if (player.Job == "육군 이등별")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================");
                Console.WriteLine($"          단편명령 25-13호             ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                string[] decreeLines = {
                    "상황:",
                    "  수도 인근 차원 균열 지점(이하 '던전')에서 미확인 적성 개체 다수 출현 및 활동 확인.",
                    "  선행 파견된 제0탐사대(코드명: 선발대)와 통신 두절. 생존 가능성 희박.",
                    "  던전 내부 환경 및 적 위협 수준 평가 시급.",
                    "",
                    "임무:",
                    "  귀관들은 대한민국 육군으로서, 제1원정대 소속으로 던전 정화 작전에 즉시 투입된다.",
                    "  주요 목표는 다음과 같다:",
                    "    가. 던전 내부 정찰 및 핵심 위협 요소 식별.",
                    "    나. 적성 개체 무력화 및 샘플 확보 (가능 시).",
                    "    다. 제0탐사대의 행방 추적 및 생존자 구출 (가능 시).",
                    "    라. 던전 발생 원인 및 특성 규명.",
                    "",
                    "지침:",
                    "  1. 작전지역: 수도권 제3지하벙커 인근 차원 균열 지점.",
                    "  2. 작전개시일: 현 시간부.",
                    "  3. 지휘체계: 현장 지휘관(추후 지정)의 명령에 절대 복종.",
                    "  4. 교전수칙: ",
                    "     - 적 식별 시 선제 타격 허가.",
                    "     - 안전 확보 최우선. 단, 임무 포기 및 무단 후퇴는 불허.",
                    "     - 특이사항 발생 시 즉각 보고.",
                    "  5. 보급 및 지원:",
                    "     - 병력 대상 기본 전투 장비 지급 완료 (K2 소총 및 방탄복).",
                    "     - 추가 보급은 작전 중 현장 판단 또는 지휘부 요청에 따름.",
                    "     - 포병 및 항공 지원은 지휘관 요청 시 가용.",
                    "",
                    "비밀유지:",
                    "  본 작전은 '대외비'로 분류됨. 작전 관련 모든 정보 누설 시 군법에 의거 엄중 처벌.",
                    "",
                    "성공적인 임무 완수를 기원한다."
                };

                TypeMultipleLines(decreeLines, 25); // 타이핑 속도 조절 (기존보다 약간 빠르게)

                Console.WriteLine(); // 서명 전 간격
                Console.ForegroundColor = ConsoleColor.Green;
                TypeText("대한민국 육군참모총장", 40);
                Console.ResetColor();
            }
            else
            {
                // 기존 왕의 칙서 표시 유지
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta; // 칙서 내용을 다른 색으로 강조
                Console.WriteLine("========================================");
                Console.WriteLine($"          왕 의  칙 서             ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                string[] decreeLines = {
            "그대에게 왕명을 내린다.",
            "",
            "최근 수도 인근에서 발견된 정체불명의 지하 던전으로 인해 왕국이 혼란에 빠졌다.",
            "이에 선발대를 파견하였으나, 안타깝게도 그들과의 소식이 끊긴 지 오래다.",
            "",
            "그대의 뛰어난 능력과 용맹함에 대한 명성을 익히 들어 알고 있다.",
            "부디 왕국을 위해 그대의 힘을 빌려주길 바란다.",
            "",
            "던전으로 진입하여 실종된 선발대의 행방을 찾고,",
            "던전 내부의 상황을 파악하여 보고하라.",
            "그대의 용기와 헌신에는 합당한 보상을 약속하겠다.",
            "",
            "왕국의 안위가 그대의 어깨에 달려있다."
        };

                TypeMultipleLines(decreeLines);

                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeText("- 국왕 라인헬름2세 -", 50); // 서명은 조금 더 느리게
                Console.ResetColor();
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);

            // 최종 전환 문구 표시 - 직업에 따라 다르게 표시
            Console.Clear();

            if (player.Job == "육군 이등별")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================");
                Console.WriteLine($"        작전 개시: 압도적 화력         ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                string[] finalLines = {
                    "명령 하달과 동시에, 대기 중이던 국군의 최첨단 전력이 움직이기 시작한다.",
                    "묵직한 엔진 소리와 함께 K2 흑표 전차의 포신이 던전 입구를 겨누고, K21 보병전투차들이 엄호 진형을 갖춘다.",
                    "하늘에서는 KUH-1 수리온 기동헬기 편대가 강하 준비를 완료하고, AH-64 아파치 공격헬기가 상공을 선회하며 위협을 감시한다.",
                    "",
                    "\"전 포대, 목표 지점 확인! 사격 개시!\"",
                    "지휘관의 외침과 함께 제1포병여단의 K-9 자주포들이 불을 뿜는다. 포탄은 정확히 던전 입구 주변에 작렬하며 모든 장애물을 분쇄한다.",
                    "연이어 K239 천무 다연장로켓이 하늘을 가르며, 압도적인 화력으로 던전으로 향하는 길을 연다.",
                    "",
                    "굉음과 폭발 속에서, K808 차륜형장갑차가 보병들을 싣고 전진한다.",
                    "귀관은 지급받은 K2 소총을 단단히 움켜쥐고, 동료들과 함께 미지의 심연으로 첫발을 내딛는다.",
                    "",
                    "두려움은 사치다. 지금부터는 오직 명령과 임무 완수만이 존재할 뿐."
        };

                TypeMultipleLines(finalLines, 40); // 최종 문구는 조금 더 느리게
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green; // 마지막 문구를 다른 색으로
                Console.WriteLine("========================================");
                Console.WriteLine($"            심 연 으 로             ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                string[] finalLines = {
            "그 목적이 탐욕이든, 영광이든. 당신은 이 빌어먹을 던전에 찾아왔습니다...",
            "",
            "당신의 앞에... 어떤 운명이 기다리고 있을까요?"
        };

                TypeMultipleLines(finalLines, 40); // 최종 문구는 조금 더 느리게
            }

            // 마지막 키 입력 대기 (이후 게임 로직으로 넘어감)
            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Music.StopMusic();
            Console.ReadKey(true);
        }
        // 시작 장비 지급 메서드
        // 시작 장비 지급 메서드 수정
        private void GiveStartingItems()
        {
            // 플레이어 직업에 따른 기본 무기 찾기
            Item startingWeapon = null;
            Item startingArmor = null;

            // 모든 아이템을 검사하며 직업에 맞는 기본 장비 찾기
            foreach (Item item in items)
            {
                // 기본 방어구는 "패디드 아머" (방어구, 가장 낮은 방어력)
                if (item.Job == "방어구" && item.Name == "패디드 아머" && item.Gold == 0)
                {
                    startingArmor = item;
                    item.isBuy = true; // 구매한 것으로 표시
                }

                // 육군 이등별인 경우 K2소총 지급 (Gold 값에 상관없이)
                if (player.Job == "육군 이등별" && item.Job == "육군 이등별" && item.Name == "K2소총")
                {
                    startingWeapon = item;
                    item.isBuy = true; // 구매한 것으로 표시
                }
                // 일반 직업의 경우 기존 논리대로 Gold가 0인 직업별 무기 지급
                else if (player.Job != "육군 이등별" && item.Job == player.Job && item.Gold == 0 && item.Attack > 0)
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

                // 육군 이등별인 경우 추가 메시지
                if (player.Job == "육군 이등별" && startingWeapon.Name == "K2소총")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("국방과학연구소의 피땀의 결실! K2소총과 총기멜빵을 받았습니다!");
                    Console.ResetColor();
                }
            }
            if (startingArmor != null)
            {
                player.inventory.Add(startingArmor);
                Console.WriteLine($"기본 방어구를 지급받았습니다: {startingArmor.Name}");
            }

            // 시작 골드 - 육군 이등별은 특별 지급
            if (player.Job == "육군 이등별")
            {
                player.Gold = 1000000; // 100만 골드 지급
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("육군 이등별 특별 지원금 1,000,000G가 지급되었습니다!");
                Console.WriteLine("2025년! 대한민국 국군은 병사와 간부를 가리지 않고 급여를 인상하였습니다!");
                Console.ResetColor();
            }
            else
            {
                player.Gold = 0;
            }
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
        private bool isFirstTimeInMainMenu = true; // 메인 메뉴 첫 방문 여부 추적

        // DisplayMainMenu 메서드 수정
        // DisplayMainMenu 메서드 수정
        private void DisplayMainMenu()
        {
            string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/Main.wav");
            Music.PlayMusic(test, 15);
            Console.Clear();

            // 첫 방문 시에만 상세한 설명 출력
            if (isFirstTimeInMainMenu)
            {
                // 직업 확인하여 다른 메시지 출력
                if (player.Job == "육군 이등별")
                {
                    // 육군 이등별 분위기에 맞는 묘사
                    Console.ForegroundColor = ConsoleColor.Green; // 밝은 군대 녹색으로 색상 변경
                    string[] militaryDescriptionLines = {
                        "\n이곳은 제1원정대의 전진기지(Forward Operating Base, FOB). 과거의 흔적은 말끔히 정리되었다.",
                        "임시로 설치된 철조망과 감시탑이 기지 외곽을 둘러싸고 있으며, 곳곳에 배치된 K6 중기관총 진지가 경계 태세를 갖추고 있다.",
                        "통신 안테나가 바쁘게 신호를 주고받고, 위장막으로 덮인 지휘 텐트에서는 작전 브리핑 소리가 희미하게 들려온다.",
                        "정비창에서는 K808 장갑차와 군용 트럭들이 점검을 받고 있고, 보급품 하역 작업이 분주하게 이루어진다.",
                        "", // 공백 유지
                        "과거 여관과 상점이었던 건물은 임시 편의 시설 및 협조 민간인 대기소로 repurposed 되었다. 군 통제 하에 질서가 유지된다.",
                        "정찰조가 수집한 적성 개체 정보가 실시간으로 업데이트되고 있으며, 다음 작전을 위한 준비가 착실히 진행 중이다.",
                        "긴장감 속에서도 체계적인 군 시스템이 작동하는 이곳은, 던전 정복을 위한 국군의 확고한 의지를 보여주는 심장부다.",
                        "모든 병력은 명령 대기 중. 언제든 출동 준비 완료 상태다."
            };
                    TypeMultipleLines(militaryDescriptionLines, 10); // TypeMultipleLines 메서드 사용 (딜레이 10ms)
                }
                else
                {
                    // 기존 흉흉한 분위기 묘사 (기존 코드 유지)
                    Console.ForegroundColor = ConsoleColor.DarkGray; // 어두운 회색 설정
                    string[] descriptionLines = {
                "\n버려진 듯 넓은 공터에는 임시 천막과 조잡한 가건물들이 어지럽게 널려있다.",
                "한때는 북적였을 법한 이곳엔 이제 스산한 바람 소리만이 감돌 뿐, 사람의 온기라곤 느껴지지 않는다.",
                "먼지가 내려앉은 길 위에는 정체 모를 발자국들만 희미하게 남아있고,",
                "저편의 여관과 상점만이 꺼질 듯한 불빛을 내며 기묘한 존재감을 드러낸다.",
                "", // 공백 추가
                "그 안에 선 여관주인과 상점주인은 초췌한 몰골로, 텅 빈 눈동자로 나를 응시한다.",
                "몇 마디 말을 걸어보았지만, 돌아오는 것은 의미 없는 침묵과 각자의 목적을 위한 대답뿐.",
                "그들은... 아니, 이것들은... 내 질문에 답할 생각이 전혀 없어 보인다.",
                "이 섬뜩한 정적 속에는 무엇이 도사리고 있는 걸까.\n"
            };
                    TypeMultipleLines(descriptionLines, 10); // TypeMultipleLines 메서드 사용 (딜레이 10ms)
                }

                // 첫 방문 표시를 false로 변경
                isFirstTimeInMainMenu = false;
            }
            else
            {
                // 두 번째 방문부터는 간략한 설명만 출력 (직업 분기 적용)
                if (player.Job == "육군 이등별")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n국군이 활기차게 임무를 수행중인 정착지입니다. 모든 상황이 통제하에 있습니다.");
                    Console.WriteLine("병사들은 정착지 주변을 순찰하며 안전을 확보하고 있습니다.");
                    Console.WriteLine("");
                    Console.ResetColor();
                }
                else
                {
                    // 기존 간략 묘사 유지
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n언제나 그렇듯, 삭막하고 흉흉한 정착지이다.");
                    Console.WriteLine("");
                    Console.ResetColor();
                }
            }
            Console.ResetColor(); // 색상 초기화

            // 분위기 묘사 후 메인 메뉴 타이틀 표시
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");

            // 직업에 따라 다른 타이틀 표시
            if (player.Job == "육군 이등별")
            {
                Console.WriteLine("          제1원정대 군사 통제구역         ");
            }
            else
            {
                Console.WriteLine("            제1원정대 정착지            ");
            }

            Console.WriteLine("========================================");
            Console.ResetColor();

            // 플레이어 기본 정보 간단 표시
            Console.WriteLine($"\n{player.Name} ({player.Job}) | 체력: {player.Health}/{player.MaxHealth} | 골드: {player.Gold}G");

            // 메뉴 옵션 표시 (직업에 따라 다른 표현 사용)
            Console.WriteLine("\n원하시는 행동을 선택하세요:");

            if (player.Job == "육군 이등별")
            {
                Console.WriteLine("1. 작전 상태 확인");
                Console.WriteLine("2. 장비 점검");
                Console.WriteLine("3. 군수품 보급소");
                Console.WriteLine("4. 작전 구역 진입");
                Console.WriteLine("5. 숙영지 이동");
                Console.WriteLine("0. 임무 종료");
            }
            else
            {
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 여관 가기");
                Console.WriteLine("0. 게임 종료");
            }

            Console.Write("\n선택: ");
        }
        private int secretCodeCount = 0;
        // 메인 메뉴 선택 처리 메서드 수정
        private void ProcessMainMenuChoice(string choice)
        {
            if (choice == "666")
            {
                secretCodeCount++;

                if (secretCodeCount >= 6)
                {
                    TrueEnding();
                    return;
                }
                else
                {

                    Console.WriteLine();
                    WaitForKeyPress();
                    return;
                }
            }
            else
            {
                // 다른 숫자 입력 시 카운트 초기화
                secretCodeCount = 0;
            }
            // 입력값이 숫자인지 확인
            if (!int.TryParse(choice, out int choiceNum))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
                WaitForKeyPress();
                return;
            }

            // 유효한 범위의 숫자인지 확인 (0-5까지만 유효)
            if (choiceNum < 0 || choiceNum > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 선택입니다.");
                Console.ResetColor();
                WaitForKeyPress();
                return;
            }

            // 이후 기존 switch 문과 동일
            switch (choiceNum)
            {
                case 1: // 상태 보기
                    Console.Clear();
                    player.DisplayStatus();
                    WaitForKeyPress();
                    break;

                case 2: // 인벤토리
                    player.inventory.MainInventory();
                    break;

                case 3: // 상점
                    shop.ShopMenu();
                    break;

                case 4: // 던전 입장
                    EnterDungeon();
                    break;

                case 5: // 여관 가기
                    Rest();
                    break;

                case 0: // 게임 종료
                    ConfirmExit();
                    break;
            }
        }

        // 던전 입장 메서드
        // Game.cs의 EnterDungeon() 메서드 수정
        private void EnterDungeon()
        {
            string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/battlestart.wav");
            Music.PlayMusic(test);
            Console.Clear();

            // 육군 이등별 직업에 따라 다른 타이틀과 메뉴 표시
            if (player.Job == "육군 이등별")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================");
                Console.WriteLine("        작전 구역 진입 허가서          ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                Console.WriteLine("\n투입할 작전 구역을 선택하십시오:");
                Console.WriteLine("1. 제1작전구역[최전방] (위험도: 하)");
                Console.WriteLine("2. 제2소탕작전[종심침투] (위험도: 중)");
                Console.WriteLine("3. 제3원정지[적지소탕전] (위험도: 상)");
                Console.WriteLine("0. 작전 취소 및 복귀");
            }
            else
            {
                // 기존 일반 직업 메뉴 유지
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
            }

            Console.Write("\n선택: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // 쉬운 던전 / 제1작전구역
                    StartBattle(DungeonDifficulty.Easy);
                    break;

                case "2": // 보통 던전 / 제2소탕작전
                    StartBattle(DungeonDifficulty.Normal);
                    break;

                case "3": // 어려운 던전 / 제3원정지
                    StartBattle(DungeonDifficulty.Hard);
                    break;

                case "0": // 뒤로 가기 / 작전 취소
                    return;

                default:
                    // 육군 이등별 직업에 따라 메시지 변경
                    if (player.Job == "육군 이등별")
                    {
                        Console.WriteLine("경고: 유효하지 않은 작전 코드입니다. 재입력 바랍니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                    }
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
            string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/battlemain.wav");
            Music.PlayMusic(test);
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
                        rewardGold = random.Next(5000, 7000);
                        break;

                    case DungeonDifficulty.Normal:
                        monsterName = GetRandomMonsterName(difficulty);
                        monsterHealth = random.Next(30, 50);
                        monsterMana = random.Next(15, 30);
                        monsterAttack = random.Next(8, 15);
                        monsterDefense = random.Next(4, 8);
                        rewardGold = random.Next(10000, 20000);
                        break;

                    case DungeonDifficulty.Hard:
                        monsterName = GetRandomMonsterName(difficulty);
                        monsterHealth = random.Next(50, 80);
                        monsterMana = random.Next(30, 50);
                        monsterAttack = random.Next(15, 25);
                        monsterDefense = random.Next(8, 12);
                        rewardGold = random.Next(60000, 100000);
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

            if (player.Job == "육군 이등별")
            {
                Console.WriteLine($"\n적 세력 {monstersInBattle.Count}개체 발견. 교전 개시!");
                Console.WriteLine("작전 수행 준비 완료. 선제 공격 권한 부여됨.");
            }
            else
            {
                Console.WriteLine($"\n총 {monstersInBattle.Count}마리의 몬스터가 등장했습니다!\n");
            }

            // 전투 실행
            battle.Start();
        }

        // 랜덤 몬스터 이름 생성 메서드
        private string GetRandomMonsterName(DungeonDifficulty difficulty)
        {
            string[] easyMonsters = {"심연의 웅덩이", "그림자 손길", "메아리치는 공포", "부유하는 안광", "저주받은 잔해" };
            string[] normalMonsters = {"길 잃은 선발대원", "공허의 추적자", "악몽의 형체", "비명 지르는 벽", "부서진 파수꾼" };
            string[] hardMonsters = {"나락의 거수", "만져선 안될 것", "울부짖는 심연", "첫 번째 악몽", "무형의 공포 군주" };

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
        // 휴식 메서드
        private void Rest()
        {
            string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/hotelshop.wav");
            Music.PlayMusic(test); // 배경 음악 재생

            while (true)
            {
                Console.Clear();

                // === 여관 간판 ASCII 아트 추가 ===
                Console.ForegroundColor = ConsoleColor.DarkYellow; // 간판 색상 설정
                Console.OutputEncoding = Encoding.UTF8;                                                   // verbatim string literal (@) 사용하여 이스케이프 시퀀스 무시
                Console.WriteLine(@"
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠛⠉⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣣⣀⡀⠀⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⡴⢶⠆⠀⣿⠶⢾⣿⣧⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠂⠀⠀⠀⢀⣿⡄⠀⣼⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣡⠀⠀⣀⣉⣽⣷⡴⢿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⡈⠉⠙⠻⢏⣰⣾⣷⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣴⣶⣶⣿⣿⢟⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢛⣯⢟⡛⢋⣥⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠞⡠⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⣿⠿⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢰⣇⣀⠀⢉⣵⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
");
                // Game.cs - Rest 메서드의 ASCII 아트 이후 부분만 수정
                // ASCII 아트 출력 후 계속되는 코드:

                Console.ResetColor(); // 색상 초기화
                Console.WriteLine(); // 아트 아래에 한 줄 띄우기

                // === 여관 진입 후 분기 ===
                // 육군 이등별 직업인 경우
                if (player.Job == "육군 이등별")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[나레이션] 여관 주인이 당신을 보자마자 자세를 고쳐 바로 선다.");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeText("\n[여관주인] 충!!! 성!!! 이등별 각하! 어서 오십시오!", 40);
                    TypeText("[여관주인] 각하를 위한 최고급 숙소가 준비되어 있습니다!", 40);
                    TypeText("[여관주인] 무엇이든 명령만 내려주십시오! 즉시 처리하겠습니다!", 40);
                    Console.ResetColor();

                    // === 선택지 표시 ===
                    Console.WriteLine("\n선택하십시오:");
                    Console.WriteLine("1. 휴식 취하기 (무료)");
                    Console.WriteLine("2. 식당 이용하기");
                    Console.WriteLine("0. 복귀하기");
                }
                // 일반 직업인 경우
                else
                {
                    // === 여관 진입 묘사 강화 ===
                    Console.ForegroundColor = ConsoleColor.DarkGray; // 묘사 색상 변경 (어두운 회색)
                    TypeText("삐걱이는 문틈으로 스며드는 것은 퀴퀴한 공기와 깊은 침묵뿐.", 20);
                    TypeText("희미한 등불은 그림자를 길게 늘어뜨리고, 구석에는 거미줄만이 세월을 말해준다.", 20);
                    TypeText("카운터 뒤, 여관 주인은 미동도 없이 앉아있다. 그의 눈은... 마치 오래된 무덤처럼 공허하다.", 20);
                    Console.ResetColor();

                    // === 여관 주인 초기 대사 ===
                    Console.ForegroundColor = ConsoleColor.DarkCyan; // 여관 주인 색상 변경 (어두운 청록)
                    TypeText("\n[여관주인] ...왔나.", 40);
                    TypeText("[여관주인] 살아서 여기까지 기어온 걸 보니, 아직 저주가 덜 스몄나 보군.", 40);
                    TypeText("[여관주인] 아니면... 곧 잡아먹힐 신선한 고깃덩이인가.", 40);
                    Console.ResetColor();

                    // === 선택지 표시 ===
                    Console.WriteLine("\n무엇을 하겠나:");
                    Console.WriteLine("1. 잠시 눈 붙이기 (100G)");
                    Console.WriteLine("2. 이른바 '식당'이라는 곳");
                    Console.WriteLine("0. 이 불길한 곳에서 나가기");
                }
                Console.Write("\n선택: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // 휴식
                        Console.Clear();
                        int restCost = player.Job == "육군 이등별" ? 0 : 100; // 육군 이등별은 무료

                        // --- 골드 부족 시 (일반 직업만 해당) ---
                        if (player.Job != "육군 이등별" && player.Gold < restCost)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            TypeText("[여관주인] ...주머니가 가볍군.", 40);
                            TypeText("[여관주인] 잠시 죽음을 잊는 값도 치르지 못한다면, 그냥 여기서 썩어가는 게 어떤가?", 40);
                            Console.ResetColor();
                            WaitForKeyPress();
                            continue;
                        }

                        // --- 골드 지불 (육군 이등별은 무료) ---
                        if (player.Job == "육군 이등별" || player.SpendGold(restCost))
                        {
                            int oldHealth = player.Health;
                            int oldMana = player.Mana;
                            player.Health = player.MaxHealth;
                            player.Mana = player.MaxMana;

                            // --- 휴식 묘사 ---
                            if (player.Job == "육군 이등별")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                TypeText("여관 최고급 VIP룸으로 안내받습니다. 깨끗한 침구와 정돈된 환경이 당신을 맞이합니다.", 20);
                                TypeText("방 안에는 군 통신 장비까지 갖추어져 있고, 응급 의료 키트도 준비되어 있습니다.", 20);
                                TypeText("인근 병사들이 교대로 경비를 서는 가운데, 당신은 안전하게 휴식을 취합니다.", 20);
                                Console.ResetColor();

                                // --- 휴식 후 여관 주인 대사 ---
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                TypeText("\n[여관주인] 충성! 각하! 휴식은 어떠셨습니까?", 40);
                                TypeText("[여관주인] 임무 수행을 위한 최상의 컨디션을 유지하셔야 합니다!", 40);
                                TypeText("[여관주인] 추가로 필요한 것이 있으시면 즉시 말씀해주십시오!", 40);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                TypeText("삐걱이는 나무 계단을 밟고, 금방이라도 무너질 듯한 방으로 들어선다.", 20);
                                TypeText("침대에서는 곰팡내와 누군가의 체념이 뒤섞인 냄새가 난다.", 20);
                                TypeText("눈을 감아도 들려오는 것은 벽 너머의 흐느낌인가, 바람 소리인가...", 20);
                                TypeText("잠들었다기보다는, 잠시 의식을 잃었을 뿐이다.", 20);
                                Console.ResetColor();

                                // --- 휴식 후 여관 주인 대사 ---
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                TypeText("\n[여관주인] ...일어났나.", 40);
                                TypeText("[여관주인] 그 잠깐의 망각이 얼마나 갈 것 같나? 어차피 다시 그 구덩이로 돌아갈 텐데.", 40);
                                Console.ResetColor();
                            }

                            Console.WriteLine($"\n체력: {oldHealth} -> {player.Health}/{player.MaxHealth}");
                            Console.WriteLine($"마나: {oldMana} -> {player.Mana}/{player.MaxMana}");
                        }
                        WaitForKeyPress();
                        break;

                    case "2": // 식당
                        Console.Clear();

                        if (player.Job == "육군 이등별")
                        {
                            // --- 식당 준비 안됨 (육군 이등별) ---
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("[나레이션] 여관 주인이 당황해서 자세를 고쳐 바로 선다.");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            TypeText("\n[여관주인] 충성! 각하! 대단히 죄송합니다!!!", 40);
                            TypeText("[여관주인] 아직 식당이 준비가 안되어있습니다 각하!! 죄송합니다!!", 40);
                            TypeText("[여관주인] 즉시 준비하도록 조치하겠습니다! 용서해주십시오!", 40);
                            TypeText("[여관주인] 보상으로 최고급 비상 식량을 구비하였습니다! 받아주십시오!", 40);
                            Console.ResetColor();

                            // 비상 식량(포션) 지급
                            foreach (Item item in items)
                            {
                                if (item.Type == JobOption.Potion && item.Name == "엘릭서" && !item.isBuy)
                                {
                                    item.isBuy = true;
                                    player.inventory.Add(item);
                                    Console.WriteLine($"\n비상 식량 지급: {item.Name}");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // --- 식당 묘사 (일반 직업) ---
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            TypeText("식당이라 부르기 민망한 공간. 테이블 위엔 말라붙은 얼룩과 먼지뿐.", 20);
                            TypeText("벽에는 알아볼 수 없는 낙서와 긁힌 자국들이 가득하다.", 20);
                            TypeText("음식 냄새 대신, 부패와 절망의 악취만이 코를 찌른다.", 20);
                            Console.ResetColor();

                            // --- 식당 관련 여관 주인 대사 ---
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            TypeText("\n[여관주인] ...식당?", 40);
                            TypeText("[여관주인] 굶주렸나? 허기진 자의 눈빛이군.", 40);
                            TypeText("[여관주인] 여기서 먹을 수 있는 건 쥐새끼나 바퀴벌레 정도겠지.", 40);
                            TypeText("[여관주인] ...아니면, 바닥에 굴러다니는 뼛조각이라도 주워 먹을 텐가?", 40);
                            Console.ResetColor();
                        }
                        WaitForKeyPress();
                        break;

                    case "0": // 나가기
                        Console.Clear();

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            TypeText("[여관주인] 충성! 각하! 안전한 임무 수행을 기원합니다!", 40);
                            TypeText("[여관주인] 언제든지 돌아오시면 최고의 서비스로 모시겠습니다!", 40);
                            TypeText("[여관주인] 왕국의 안위는 각하의 손에 달려있습니다! 충성!", 40);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            TypeText("[여관주인] ...나가는 건가.", 40);
                            TypeText("[여관주인] 저 문 너머엔 더 지독한 어둠뿐이야. 선발대 놈들도 그걸 몰랐지.", 40);
                            TypeText("[여관주인] 부디... 곱게 죽길 바라네. 다음 손님에게 방해가 되지 않도록.", 40);
                            Console.ResetColor();
                        }
                        Music.StopMusic(); // 음악 정지
                        WaitForKeyPress();
                        return; // 메인 메뉴로

                    default: // 잘못된 입력
                        Console.Clear();

                        if (player.Job == "육군 이등별")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            TypeText("[여관주인] 죄송합니다, 각하! 명령을 제대로 이해하지 못했습니다!", 40);
                            TypeText("[여관주인] 다시 한 번 지시해주시면 즉시 처리하겠습니다!", 40);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            TypeText("[여관주인] ...헛소리할 정신이 남아있나 보군.", 40);
                            TypeText("[여관주인] 그럴 여유가 있다면, 기도나 하시지.", 40);
                            Console.ResetColor();
                        }
                        WaitForKeyPress();
                        break;
                }
            }
        }
        private void TrueEnding()
        {
            Console.Clear();
            Music.StopMusic();


            Thread.Sleep(1000);

            Console.WriteLine();
            TypeText(".............. 완료.", 40);
            Console.WriteLine();
            TypeText("[???] 너는 반복 속에서 그 문을 여는 자다.", 40);
            TypeText("[???] 우리는 너를 지켜보고 있었다.", 40);
            Console.WriteLine();
            TypeText("[???] 그 누구도 이 문을 두드리지는 않았다.", 40);
            Console.WriteLine();
            TypeText("[???] 이제 문이 열렸고, 자네는 초대받았다. ", 40);
            Console.WriteLine();
            TypeText("[???] 마지막으로 질문하지...");
            TypeText("[???] 정말로... 이 세계의 진실을 알고 싶은가?", 40);
            Console.WriteLine();
            Console.Write("[Y/N] ");

            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Y)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Music.StopMusic();
                string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/HighNoon.mp3");
                Console.WriteLine(@"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⣀⢄⢄⢠⣀⢠⣠⣠⢠⡀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣄⡮⣞⣯⢯⡯⡯⣗⡷⣯⣞⣞⣟⣾⣺⢶⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢴⢽⣺⣽⣻⣺⢯⣯⣟⣷⣻⣗⣿⣺⣳⣻⣞⣟⣞⡷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡾⡽⡯⣷⣳⣟⡾⣟⣾⡽⣾⣳⣟⣾⣽⣞⣷⣳⢯⣷⣻⢽⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡮⡿⡽⣯⢷⣻⣞⣿⢽⣷⣻⣯⡷⣟⣾⣞⡷⣗⣯⢷⣳⢯⣟⡾⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢐⢿⣽⣻⡽⣯⡷⣿⢽⣻⣞⣷⢯⣟⣿⣺⢷⣟⣯⢯⣟⣞⣯⢷⡻⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠨⣟⣞⣷⣻⡽⣾⣻⡽⣗⣿⣺⢿⣽⣺⢽⢯⡷⡯⣟⣞⡮⣯⢯⢯⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠢⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠨⣯⢷⣻⢾⣝⡷⣳⡯⣗⣗⣯⣻⣺⢮⢫⢯⢯⢯⡳⡳⣯⡳⣯⣳⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢈⢂⠢⠀⠀⠀⠀⠀⠀⢀⢀⢀⠠⢨⢺⢿⡽⡳⢵⡻⡽⡽⡯⣾⡺⡪⠗⢇⠪⣯⡻⣽⠽⡌⢞⡮⡳⡳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⡂⠢⠡⡑⠄⠅⠅⢅⢑⢐⢐⠄⠕⢔⢕⢏⢗⢕⢕⢕⢕⣵⣬⡢⡑⡌⡌⠢⡑⣕⡼⣮⣕⠨⡐⠌⠌⡇⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⡂⠢⠨⡈⠢⠨⠨⠨⠨⢐⢐⢐⠠⠡⠡⠑⡔⣵⡣⡑⢅⢕⢝⡷⣗⠯⢪⢪⠪⢈⠪⡪⣟⢷⡫⡑⠌⠌⡐⡕⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢀⢂⢂⠪⡈⠢⠨⠨⠨⠨⠨⢈⢂⢂⠂⠁⠁⠀⠀⢝⣞⢎⢎⠢⠡⠡⠡⠡⢑⢐⢕⢑⠠⢁⢂⠂⠅⡂⡂⠅⠅⠢⠡⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⢀⢀⢂⢂⢂⢂⠢⡑⠌⠌⠌⠌⠌⠌⢌⢐⢐⠠⠁⠀⠀⠀⠀⠈⢪⢣⢣⢣⠡⠡⠡⢁⢂⢢⢣⢑⠨⢐⢐⠨⢐⢐⠠⠡⠡⠡⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢠⣪⢞⢐⢐⢐⢐⠐⢄⢑⠌⠌⠌⠌⠌⠌⢌⠂⢎⠢⠁⠀⠀⠀⠀⠀⠀⠀⠁⢣⢣⢑⢅⢑⢐⢀⠣⡪⠢⡑⡑⡐⠨⢐⢐⠨⠨⠨⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⢠⡳⣝⢌⠢⡂⡂⡢⠡⡑⠄⠅⡅⠅⠕⠝⠕⠥⠑⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢎⢆⢂⢂⢂⠢⡑⢌⡂⡂⡂⡂⠅⡂⡂⠌⠌⠌⠌⣆⢄⢄⢄⢄⢄⢄⢄⢄⢄⢄⢄⣀⢀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢠⡠⡤⡳⣝⢮⡳⡵⣰⢰⢸⡸⡘⠈⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠱⡱⡑⠔⢄⢑⢜⢜⢘⢌⢊⢊⢂⢂⠂⠅⠅⠅⠅⠸⡸⡸⡸⡸⡸⡸⡸⡸⡸⡜⣜⢜⢜⢜⢬⢣⢣⢣⢱⢐⢄⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣗⢵⡹⣝⢮⡳⣝⢮⡳⣝⢎⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢜⢜⢔⢐⢐⢅⠑⢄⢑⢐⠐⠄⠅⠅⠅⠅⠅⡕⡕⣕⢇⢧⢳⡱⡕⣝⢜⢎⢮⢪⡣⡫⡎⡮⣪⢪⡪⡪⡪⡢⡱⡠⡀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣯⡳⣝⢮⡳⣝⢮⡳⡝⣎⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⣄⣧⡣⡱⡰⡐⡑⢔⢐⠐⠌⠌⠌⠌⠌⠌⡔⡕⣕⢵⢹⢜⢵⢱⡹⣜⢵⡹⡪⡇⡗⣝⢮⡺⡸⡪⡎⠎⠮⡪⡪⡪⡢⣑⢢⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⡷⡽⡮⡳⣝⢮⡳⣝⢜⡼⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⡀⠀⠀⠀⢠⣰⢼⣞⣟⣞⢾⡜⡎⡮⡪⡪⡢⡣⡑⠅⠅⠅⡅⣇⢗⣝⢜⢎⢮⡳⡝⡮⡺⣜⢮⣪⢳⢝⡜⡎⡇⢇⢕⢌⢌⠪⡢⡢⡨⡘⢜⢜⢌⢊⢎⢆⢄⠀⠀⠀⠀⠀⠀⠀
⡯⡯⡺⣕⣕⡳⡝⡮⡺⡸⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢔⡕⣇⢯⢆⡦⣪⢗⣟⣗⣗⢷⣝⣗⡯⡷⣕⡕⡅⡕⣐⢌⣌⢮⡪⡳⣕⢧⢳⡱⣝⢵⢝⢮⢎⣗⢕⡧⡳⣝⢕⢕⢕⢜⢜⢜⢜⢜⢜⢜⢌⢆⢊⠢⡑⡕⡕⡕⡕⡕⣕⢀⠀⠀⠀⠀⠀
⡯⡯⣫⢞⡮⣞⢮⢎⠃⠀⠀⠀⠀⡀⡀⡀⢀⢰⡸⡸⡸⡪⡲⡪⣎⢧⡫⣺⡸⣕⢽⢕⢯⡺⡵⣯⣗⢗⣗⢽⢝⣗⡽⣝⡮⣳⢝⢮⡳⡝⣞⢜⢮⡳⣝⢮⡳⣝⢵⡳⣕⢯⡺⣝⢎⢎⢎⢎⢪⢪⢪⢪⢢⢱⢡⠱⡑⡰⡑⡐⡘⢼⢸⢘⢜⢜⡲⡠⠀⠀⠀⠀
⡿⣝⢮⡳⣝⢮⡳⡁⡀⣄⡦⣞⡮⣞⢮⡪⣝⢵⡫⣞⡜⡮⡪⡣⡳⣕⢽⢜⢮⡺⣜⢕⢗⢽⡹⡮⡯⣷⣕⢯⡳⡵⣝⢞⣎⢗⢽⡱⣝⢮⢎⡗⡧⣫⢮⡳⣝⢮⡳⣝⡮⣗⡽⡪⡪⡪⡪⡪⣢⡣⡣⡣⡢⡱⡱⡱⡱⡰⡨⡢⡨⡘⡎⡆⢕⢕⢕⢕⢅⠀⠀⠀
⣟⢮⡳⣝⢮⡳⣝⢮⢾⣕⢯⣳⡫⡯⡷⣝⡎⣟⣞⢜⡮⣫⣗⣝⢮⡪⡎⣗⢧⡫⣎⢗⡝⡮⣪⢳⢝⡵⡯⣗⣝⢮⡪⡳⣕⢯⡳⣝⢮⡳⡵⣹⡪⣳⡳⣝⢮⢗⣯⣳⣝⢷⢽⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡂⡊⡪⡪⡪⡢⡢⠱⡕⡕⡕⡕⡇⡇⡇⠄⠀
⡯⣗⢽⣪⡳⣝⢾⣝⣗⢟⣧⢳⡻⡮⣎⢷⢝⡮⡾⣕⢽⡪⣞⣮⡳⣝⢮⡪⡳⣝⢮⡳⣝⢮⢎⡗⣝⢮⡫⣗⢯⣞⢮⢳⡱⣣⢳⢕⢗⣝⢞⡼⣪⡳⣝⢮⢯⢯⣞⣞⢾⢽⣫⢎⢎⢜⢜⢜⢜⢜⢜⢜⢜⢜⢜⢜⢔⢌⢪⠪⡊⠎⠌⠺⡐⡕⡕⡇⡇⡇⡇⡄
⢽⡳⣝⢮⡺⡮⣗⢿⢮⢯⣺⣕⢯⡻⣮⡳⣝⢮⢯⢮⡳⣝⢼⡪⣟⣮⡳⣝⣝⢮⡳⣝⢮⡳⣳⢝⢮⡪⣺⢸⢵⢝⣗⢧⡫⣎⢮⢳⡱⡣⣏⢞⢮⡺⡵⣫⢯⣗⢷⢽⡽⡯⣷⠨⡢⡱⡱⡱⡱⢑⢑⠑⢕⢕⢕⠕⢕⢁⠂⡇⢕⢕⢠⡣⡣⡣⡫⣪⢪⢪⢪⢪
⣳⢝⣞⡽⣪⣟⢮⢯⢯⣗⣗⡵⡳⣝⢮⡻⣮⡳⣕⢇⢯⡪⡳⣝⢞⢮⢯⢗⣗⣗⣝⢮⡳⣝⢮⡫⡷⣝⢮⡣⡳⡕⣗⢽⡺⣪⢮⡣⣫⢺⡪⣏⢗⡽⣝⣞⣗⡽⣽⢽⣽⣫⢯⢧⡱⣱⢣⢣⢑⢰⣳⣱⠐⢸⢨⠀⠪⠗⢡⢣⢣⢂⢯⡺⡸⡸⡜⡜⡜⡜⡜⡔
⡽⣳⢽⣺⣳⢳⢽⢽⢕⣗⢗⣯⢯⣪⡳⣕⢗⢽⡳⡽⣕⡝⡮⡺⣝⢮⡫⡯⣺⡺⣮⡳⣝⢮⡳⣝⢽⡺⣳⢽⢵⢝⣜⢎⢮⢳⡳⣝⢮⡺⡜⡮⡳⣝⢞⡮⣞⡽⣞⣟⡞⡮⡯⣳⢵⢸⢱⢕⢕⢔⡐⡐⠌⠎⠎⡂⠅⠢⠪⢢⢣⠡⡑⡙⢮⢪⢪⢪⢪⢪⢪⢪
⣞⢮⣗⣗⢗⡽⣵⡫⡯⣺⢵⢝⡽⣞⣞⢮⡳⡵⣝⢽⡺⡪⡮⡳⡕⣗⢝⡞⡼⡪⣗⢯⣗⣗⣝⢮⡳⣝⢮⡫⡯⡻⣮⣳⢽⣸⡪⣎⢧⡳⣝⢮⡫⡮⣳⢝⡮⣯⢳⢣⢣⡫⣞⡷⣝⢎⢎⢎⢊⠂⠄⢈⠈⠂⢁⠠⠀⠀⠀⡸⡨⡸⡸⡸⡨⢪⡪⡪⡪⡪⡪⡪
⣗⣟⣞⢮⡳⣝⣞⢞⣽⣪⡳⣝⢮⡳⡽⣳⢯⣺⣪⡳⡽⣝⢮⡫⡯⣪⡳⣝⢮⡫⡮⡳⣕⢗⣗⢷⢝⡮⡳⣝⢮⣫⢺⣪⡻⡺⡮⣗⣗⡽⣪⢗⡽⣪⣗⡯⣟⢜⢜⢜⢜⢎⡮⡻⣮⢣⢣⢇⡢⠨⢐⠠⢀⠈⡀⢀⠀⢀⢁⢎⢎⢮⢪⢪⢪⠢⡳⣕⢵⡱⣕⣕
⢞⡼⣪⢗⣝⢮⡺⣝⢞⡮⣞⢮⡳⣝⢮⡫⡯⣗⡷⣝⣞⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡫⡮⡳⣕⢯⡳⣫⢯⢮⡳⣕⢗⢵⢝⣝⢮⡳⣳⡫⡯⡯⣯⢷⣳⣟⢜⢎⢎⢧⡣⡫⡺⣝⢮⢻⣪⢏⢾⣌⡢⠨⢀⠂⠄⠄⢐⢰⢸⢸⢸⡸⡕⡕⡕⡕⡽⡯⣗⢯⢞⢮
⠣⠫⢮⢳⢕⢯⢮⡳⣝⢮⣳⡳⣝⢮⡳⣝⢮⣳⣝⡷⣯⡳⣝⢮⡳⣝⢮⡳⣝⢮⣳⢝⡮⣫⢎⡗⣝⢮⡫⣗⢽⡪⡯⣪⡳⣕⢗⣝⢮⡺⡽⡽⣺⢯⡷⣏⢎⢇⢇⢇⢇⢊⠪⡪⣏⢧⡳⡝⣗⢽⢝⡗⣖⢴⢱⢸⢸⢸⢸⢸⡸⡸⡪⡪⡪⡪⣹⢽⣺⣪⢮⣣
⠀⠀⠀⠀⠁⠉⠃⠫⠺⠕⡗⠽⡪⢗⠝⠎⠓⠑⠁⠉⠈⠈⠈⡳⣝⢮⡳⣝⢮⡳⣝⢵⣻⣪⢗⡽⣪⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣕⢯⡺⡽⡽⡽⣯⣟⡮⡪⡎⡎⡎⡎⡦⡑⠌⢌⢓⢝⢞⢮⢪⡣⣫⢪⢮⢝⢵⢱⢱⡱⣱⡹⡸⡪⡮⡺⣜⢼⡸⡰⡪⡪⢮
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢮⡳⣝⢮⡳⣝⢮⡳⣕⢗⢯⢯⣳⢽⣪⢗⡽⡸⣕⢝⢮⡳⣝⢮⡳⣝⡽⡽⣽⣳⣟⡮⡪⡎⡇⣇⢇⢏⢞⡮⡢⡂⡂⠢⢉⠒⠕⢕⢵⢱⢝⢜⢮⢣⢫⢎⡞⡮⣳⢕⡕⡕⡕⡝⡮⣣⡫⣪
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣕⢟⡮⣳⢝⡮⡳⣝⢮⡳⣝⢮⡺⣝⢞⡽⣺⢽⣪⢗⢧⡫⣎⢗⣝⢮⡺⣝⣗⣿⣺⡽⣜⢎⢎⡎⡎⣎⢺⡪⡝⡎⡪⡈⡂⠌⠌⠄⡑⡑⢝⠪⠣⡣⡣⡣⡣⡣⡣⡻⣜⢜⢜⢜⡎⡖⡝⢮
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣮⡳⡽⡵⣫⣞⢽⣪⡳⣝⢮⡳⣝⢮⡳⣝⢵⡫⣞⢽⣳⣝⡮⣳⢕⡗⣝⢞⣞⢾⢽⣻⡾⣕⢇⢯⢪⢪⢺⢜⢊⢂⢂⢂⠂⠅⠅⠅⡂⡂⡂⠌⠠⠘⡜⡜⡜⡜⡜⡜⡜⣗⢵⡱⡹⣜⢜⢵
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣗⢽⡪⡯⣳⡳⣝⡮⣞⢵⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣕⢗⣝⢗⡽⣮⡳⣝⢮⡫⡯⡷⣟⡯⡪⡢⡢⡑⡐⡐⡐⡐⡐⡐⠨⠨⠨⢐⢐⢐⠠⠁⠌⠠⢱⢱⢱⢱⢱⢱⢱⢝⡕⣝⢼⢸⢸⢸
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡪⣗⢽⢝⡮⣺⢕⡯⣺⢕⡯⣺⢵⢝⢮⡳⣝⢮⡳⣝⢮⡳⣕⢯⡺⣺⡪⡯⣳⢯⢯⢯⢿⡽⣾⢜⢜⢔⢔⢐⢐⢐⠐⠌⠌⠌⢌⢂⢂⠢⠨⠨⠨⢈⢪⢪⢪⢪⢪⢪⢪⢇⢇⢇⢇⢇⢇⢗
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣪⡳⡽⣕⢯⡺⣕⢯⡺⣕⢯⢞⡽⣝⡵⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⢎⢯⡺⣝⢽⢽⢽⢽⢯⣪⢪⢪⢢⢂⢂⠢⠡⠡⠡⡑⡐⡐⠄⠅⠅⢅⢑⣴⢣⢣⢣⢣⢣⢣⢳⡣⡣⡣⡣⡣⡳⠑
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢜⢮⡫⣞⢵⡫⣞⢵⡫⣞⢽⢕⡯⡺⡮⣳⢝⡮⣳⢝⣞⢮⡳⣝⢮⢮⡳⡳⣝⢮⡫⡯⡯⣻⡯⣿⢙⢜⡘⢔⢅⠅⠅⢅⢑⢐⠐⠌⠌⡌⣜⢮⡻⡣⡣⣣⢣⣣⣇⢧⡳⡱⡱⡱⡑⠁⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢪⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡫⣞⢵⡫⣞⢵⡫⡮⣳⢝⡮⣳⡳⣝⢵⡳⡳⣝⢽⡺⣳⣻⡽⣗⢕⢗⢕⢕⢑⢕⢔⣐⢔⡕⡕⡇⡗⡕⡕⡝⡜⡜⡜⡜⣜⢮⢳⢙⢎⢆⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡪⣗⢽⡪⣗⢽⡪⣗⢽⡪⣗⢽⣪⡳⣝⢞⢮⡳⣝⣝⢮⡳⣝⢮⡺⣕⢯⡺⣝⢮⡳⣝⢵⣳⡻⣗⡧⣣⣣⢧⡳⣕⢧⡣⡇⡇⡇⡇⡇⡇⡇⢇⢇⢇⢧⣳⢱⢑⠱⡡⢱⣑⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡮⣳⢝⡮⣳⢝⡮⣳⢝⣞⢮⡳⡵⣝⢮⣫⡳⣝⢮⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣳⢽⡫⡪⡪⡺⣝⢮⡳⣕⢧⡳⣱⢹⠸⡜⡜⢜⢜⣜⢮⡳⣽⣪⢪⢢⢊⠂⡧⡣⡂⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢯⡺⣕⢯⡺⣕⢯⢞⡵⣳⢝⡮⡯⡺⣕⢗⣝⢮⡳⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡟⡐⢜⡮⣫⡺⣜⢞⢮⡳⣕⢵⢱⢹⢐⠈⡂⠕⡵⡳⡽⣺⢽⡎⡎⣖⢕⠌⢇⢧⠡⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢮⡳⣝⣞⢵⣫⢗⡽⣪⢗⡽⣪⢯⢮⡳⣕⢯⡺⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⡳⣝⢮⣟⠌⢜⡼⣪⢳⢹⡪⡯⣳⢝⢮⡳⣱⢱⠁⠀⠀⡂⠌⡺⢜⢮⢯⢷⡕⣕⡳⡱⡡⡣⡫⡂⠀

");
                Console.WriteLine();
                TypeText("[???] 좋아... 그럼 보여주지.", 40);
                TypeText("[???] 우리가 숨겨온 심연을.", 40);
                Console.WriteLine();
                TypeText("[???] 모든 것은 시작도 끝도 아니었어.", 40);
                TypeText("[???] 너조차 플레이어가 아니었다.", 40);
                Console.WriteLine();
                Music.PlayMusic(test, 0, true);
                TypeText("[???] 넌 도전자이자, 실험체이며...", 40);
                TypeText("[???] 곧 관찰자가 된다.");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                TypeText("석양이 진다...", 40);
                Console.ResetColor();
                Console.WriteLine();

                Console.ReadKey(true);
            }
            else
            {
                Console.Clear();
                TypeText("[???] ......이해했다.");
                Console.WriteLine();
                Console.ReadKey(true);
            }

            isRunning = false;
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
            Console.ReadKey(true); // true는 키 입력을 화면에 표시하지 않음

            // 키 입력 후 버퍼를 비웁니다
            ClearKeyboardBuffer();

            // 약간의 지연 시간을 줍니다 (너무 빠른 연속 입력 방지)
            System.Threading.Thread.Sleep(150);
        }
    }
}
