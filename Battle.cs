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
                    if (key == ConsoleKey.DownArrow && selectedIndex < Game.Instance.monstersInBattle.Count - 1)
                    {
                        // 체력이 0인 몬스터는 넘어가기
                        do
                        {
                            selectedIndex++;
                        } while (selectedIndex < Game.Instance.monstersInBattle.Count && Game.Instance.monstersInBattle[selectedIndex].Health <= 0);
                    }
                    else if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                    {
                        // 체력이 0인 몬스터는 넘어가기
                        do
                        {
                            selectedIndex--;
                        } while (selectedIndex >= 0 && Game.Instance.monstersInBattle[selectedIndex].Health <= 0);
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

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //일반 공격
                    Console.Clear();
                    player.Attack(target);
                    break;
                case "2":
                    //스킬
                    Console.Clear();
                    player.UseSpecialSkill(target);
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    Thread.Sleep(1000);
                    break;
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
