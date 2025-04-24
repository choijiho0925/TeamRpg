using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Battle
    {
        //객체 가져옴
        Player player = Game.Instance.player;
        Monster monster = Game.Instance.monster;

        Random random = new Random();

        bool allMonstersDefeated = Game.Instance.monstersInBattle.All(m => m.Health <= 0);

        public void Start()
        {
            monster = new Monster(Game.Instance.monsterName, Game.Instance.monsterHealth, Game.Instance.monsterMana, Game.Instance.monsterAttack, Game.Instance.monsterDefense, Game.Instance.rewardGold);

            //플레이어 혹은 몬스터의 체력이 없어질 때 까지 진행됨
            while (player.Health > 0 && !Game.Instance.monstersInBattle.All(monster => monster.Health <= 0))
            {
                //선택된 몬스터 인덱스
                int selectedIndex = 0;
                ConsoleKey key;

                // 첫 번째 살아있는 몬스터 인덱스 설정
                for (int i = 0; i < Game.Instance.monstersInBattle.Count; i++)
                {
                    if (Game.Instance.monstersInBattle[i].Health > 0)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
                    do
                {
                    Console.Clear();
                    Console.WriteLine("공격할 몬스터를 선택하세요 (↑ ↓, 엔터):\n");

                    
                    for (int i = 0; i < Game.Instance.monstersInBattle.Count; i++)
                    {
                        Monster monster = Game.Instance.monstersInBattle[i];
                        // 체력이 0이하인 몬스터는 회색으로 표시
                        if (monster.Health <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        //선택 색상 변경
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("▶ ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }

                        Console.WriteLine($"{monster.Name} (체력: {monster.Health}, 공격력: {monster.Attack})");
                        Console.ResetColor();
                    }

                    // 키 입력 대기
                    key = Console.ReadKey(true).Key;

                    //방향키대로 입력
                    //아래키
                    if (key == ConsoleKey.DownArrow)
                    {
                        int nextIndex = selectedIndex;
                        do
                        {
                            //임시변수로 저장
                            nextIndex++;
                        } while (nextIndex < Game.Instance.monstersInBattle.Count &&
                                 Game.Instance.monstersInBattle[nextIndex].Health <= 0);

                        // 살아있는 몬스터면 이동
                        if (nextIndex < Game.Instance.monstersInBattle.Count)
                            selectedIndex = nextIndex;
                    }

                    //위키
                    else if (key == ConsoleKey.UpArrow)
                    {
                        int prevIndex = selectedIndex;
                        do
                        {
                            prevIndex--;
                        } while (prevIndex >= 0 &&
                                 Game.Instance.monstersInBattle[prevIndex].Health <= 0);

                        // 살아있는 몬스터면 이동
                        if (prevIndex >= 0)
                            selectedIndex = prevIndex;
                    }

                } while (key != ConsoleKey.Enter);

                // 선택된 몬스터
                Monster target = Game.Instance.monstersInBattle[selectedIndex];
                Console.WriteLine($"\n{target.Name}을(를) 공격합니다!\n");

                Console.WriteLine($"{player.Name}의 체력: {player.Health}, {target.Name}의 체력: {target.Health}\n");

                PlayerTurn(target);
                if (target.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"\n{target.Name}");
                    Console.ResetColor();
                    Console.Write("이(가) 사망했습니다.\n");
                }
                if (Game.Instance.monstersInBattle.Any(monster => monster.Health > 0)) EnemyTurn();
            }

            Game.Instance.WaitForKeyPress();
            Console.Clear();

            // 모든 몬스터가 죽었는지 다시 확인
            if (player.Health > 0 && Game.Instance.monstersInBattle.All(monster => monster.Health <= 0))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("축하합니다! 전투에서 승리했습니다.");
                Console.ResetColor();

                player.EarnGold(Game.Instance.rewardGold);
                //Console.WriteLine($"{Game.Instance.rewardGold}G를 획득했습니다!");

                int healthRecovery = (int)(player.MaxHealth * 0.3);
                int manaRecovery = (int)(player.MaxMana * 0.3);
                player.Heal(healthRecovery);
                player.RecoverMana(manaRecovery);

                Game.Instance.monstersInBattle.Clear();
                Game.Instance.WaitForKeyPress();
            }
            else if (player.Health <= 0)
            {
                // 전투 패배
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("전투에서 패배했습니다...");
                Console.ResetColor();

                Game.Instance.monstersInBattle.Clear();
                Game.Instance.WaitForKeyPress();
            }
        }

        //플레이어 턴
        private void PlayerTurn(Monster target)
        {
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 특수행동"); // 새로운 옵션 추가

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // 일반 공격
                    Console.Clear();
                    player.Attack(target);
                    break;
                case "2":
                    // 스킬
                    Console.Clear();
                    player.UseSpecialSkill(target);
                    break;
                case "3":
                    // 특수행동 - 포션 사용
                    Console.Clear();
                    UseSpecialAction();
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }

        // ============== 특수행동 메서드 ==============
        // 전투 중 특수행동(포션 사용 등)을 처리합니다.
        private void UseSpecialAction()
        {
            Console.Clear();
            Console.WriteLine("===== 특수행동 =====");
            Console.WriteLine("1. 엘릭서 사용 [HP 회복] - 남은 개수: " + player.GetPotionCount("HP"));
            Console.WriteLine("2. 무미야 사용 [MP 회복] - 남은 개수: " + player.GetPotionCount("MP"));
            Console.WriteLine("0. 돌아가기");

            Console.Write("\n선택: ");
            string actionChoice = Console.ReadLine();

            switch (actionChoice)
            {
                case "1":
                    // HP 포션 사용
                    if (player.UsePotion("HP"))
                    {
                        // 포션 사용 성공 - 턴 종료
                        Game.Instance.WaitForKeyPress();
                    }
                    else
                    {
                        // 포션 없음 - 다시 특수행동 메뉴로
                        Game.Instance.WaitForKeyPress();
                        UseSpecialAction();
                        return;
                    }
                    break;

                case "2":
                    // MP 포션 사용
                    if (player.UsePotion("MP"))
                    {
                        // 포션 사용 성공 - 턴 종료
                        Game.Instance.WaitForKeyPress();
                    }
                    else
                    {
                        // 포션 없음 - 다시 특수행동 메뉴로
                        Game.Instance.WaitForKeyPress();
                        UseSpecialAction();
                        return;
                    }
                    break;

                case "0":
                    // 돌아가기 (턴은 소비하지 않음)
                    Console.WriteLine("특수행동을 취소합니다.");
                    Game.Instance.WaitForKeyPress();
                    // 다시 행동 선택으로 돌아감
                    PlayerTurn(Game.Instance.monstersInBattle.FirstOrDefault(m => m.Health > 0));
                    return;

                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    Game.Instance.WaitForKeyPress();
                    UseSpecialAction();
                    return;
            }
        }
        //적 턴
        private void EnemyTurn()
        {
            Console.WriteLine($"\n적의 턴입니다.");
            monster.EnemyAttack(player);

            Game.Instance.WaitForKeyPress();
        }
    }
}
