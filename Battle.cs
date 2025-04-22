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
        List<Monster> monstersInBattle = new List<Monster>();
        List<Monster> monsterTypes = new List<Monster>();


        public void Start()
        {
            Console.Clear();

            //생성되는 몬스터의 수 결정
            int monsterCount = random.Next(1, 5);
            //플레이어 혹은 몬스터의 체력이 없어질 때 까지 진행됨
            while (player.Health > 0 && monster.Health > 0)
            {
                Console.WriteLine("");

                //랜덤 몬스터 생성
                for (int i = 0; i < monsterCount; i++)
                {
                    int index = random.Next(monsterTypes.Count);
                    Monster randomMonster = monsterTypes[index];

                    Monster copy = new Monster(randomMonster.Name, randomMonster.Health, randomMonster.Mana, randomMonster.Attack, randomMonster.Defense);
                    monstersInBattle.Add(copy);
                }

                // 몬스터 클래스에서 출력
                for (int i = 0; i < monstersInBattle.Count; i++)
                {
                    monstersInBattle[i].PrintInfo(i + 1);
                }

                Console.WriteLine($"{player.Name}의 체력: {player.Health}, {monster.Name}의 체력: {monster.Health}");
                PlayerTurn();

                //몬스터가 죽었을 때
                if (monster.Health <= 0)
                {
                    Console.WriteLine($"{monster.Name}를 처치했습니다!");

                    //보상 넣기
                    Console.WriteLine("를 획득했습니다!");
                    //player.gold += {amount};
                    break;
                }

                EnemyTurn();

                //플레이어가 죽었을 때
                if (player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name}이 쓰러졌습니다. 게임 오버!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                }
            }
        }

        //플레이어 턴
        private void PlayerTurn()
        {
            Thread.Sleep(1000);
            Console.WriteLine("\n당신의 턴입니다. 어떤 행동을 하시겠습니까?");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //일반 공격
                    Console.Clear();
                    player.Attack();
                    Thread.Sleep(1000);
                    break;
                case "2":
                    //스킬
                    Console.Clear();
                    //player.SkillAttack();
                    Thread.Sleep(1000);
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
            Console.WriteLine($"\n{monster.Name}의 턴입니다.");
            monster.EnemyAttack(player);
        }
    }
}
