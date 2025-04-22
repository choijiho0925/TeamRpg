using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    internal class Battle
    {
        internal class Battle
        {
            //게임매니저에서 객체 생성
            Player player = new Player();
            Monster monster = new Monster();


            public void Start()
            {
                Console.Clear();
                while (player.health > 0 && enemy.hp > 0)
                {
                    // 플레이어 턴
                    Console.WriteLine($"{player.name}의 체력: {player.hp}, {enemy.name}의 체력: {enemy.hp}");
                    PlayerTurn();

                    if (enemy.hp <= 0)
                    {
                        Console.WriteLine($"{enemy.name}를 처치했습니다!");

                        //보상 넣기
                        Console.WriteLine("1000G를 획득했습니다!");
                        player.exp += 1;
                        player.gold += 1000;
                        break;
                    }

                    // 적 턴
                    EnemyTurn();
                    if (player.health <= 0)
                    {
                        Console.WriteLine($"{player.name}이 쓰러졌습니다. 게임 오버!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    }
                }
            }

            private void PlayerTurn()
            {
                Thread.Sleep(1000);
                Console.WriteLine("\n당신의 턴입니다. 어떤 행동을 하시겠습니까?");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 도망");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        player.Attack(enemy);
                        Thread.Sleep(1000);
                        break;
                    case "2":
                        Console.Clear();
                        player.SkillAttack(enemy);
                        Thread.Sleep(1000);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("도망쳤습니다.");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다.");
                        Thread.Sleep(1000);
                        break;
                }
            }

            private void EnemyTurn()
            {
                Console.WriteLine($"\n{monster.Name}의 턴입니다.");
                monster.EnemyAttack(player);
            }
        }
    }
}
