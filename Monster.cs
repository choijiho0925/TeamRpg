using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    internal class Monster
    {
        public string Name;
        public int HP;
        public int MP;
        public int Attack;
        public int Defense;

        //적 종류들
        public class Goblin : Monster
        {
            public Goblin() : base("고블린", 50, 50, 10, 3) { } // 이름, HP, MP, 공격력, 방어력
        }

        public class Dragon : Monster
        {
            public Dragon() : base("드래곤", 200, 50, 30, 10) { }
        }

        public class Slime : Monster
        {
            public Slime() : base("슬라임", 30, 20, 5, 1) { }
        }

        public Monster(string name, int hp, int mp, int attack, int defense) //생성자 사용했어요
        {
            Name = name;
            HP = hp;
            MP = mp;
            Attack = attack;
            Defense = defense;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }

        public void EnemyAttack(Player player)
        {
            //str = 공격력이라 생각하고 만들었어요
            int AttackPower = str;

            if (AttackPower >= 0)
            {
                Console.WriteLine($"\n{Name}가 {player.name}을(를) 공격합니다! {AttackPower}의 피해를 입혔습니다.");
                player.TakeDamage(AttackPower);
            }
            else
            {
                Console.WriteLine("적의 공격을 방어했습니다!");
            }

        }
    }
}
