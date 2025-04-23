using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Monster
    {
        private string name;
        private int health;
        private int mana;
        private int attack;
        private int defense;


        public Monster(string Name, int Health, int Mana, int Attack, int Defense) //생성자 사용
        {
            name = Name;
            health = Health;
            mana = Mana;
            attack = Attack;
            defense = Defense;
        }

        //읽기 전용
        public string Name => name;
        public int Health => health;
        public int Mana => mana;
        public int Attack => attack;
        public int Defense => defense;

        //적의 능력치 출력
        public void PrintInfo(int number)
        {
            Console.WriteLine($"{number}. {name} (체력: {health}, 마나: {mana}, 공격력: {attack}, 방어력: {defense})");
        }
        //공격을 받을 때
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0) health = 0;
        }
        //공격할 때
        public void EnemyAttack(Player player)
        {
            int AttackPower = Game.Instance.monsterAttack;

            if (AttackPower >= 0)
            {
                Console.WriteLine($"\n{name}가 {player.Name}을(를) 공격합니다! {AttackPower}의 피해를 입혔습니다.");
                player.TakeDamage(AttackPower);
            }
            else
            {
                Console.WriteLine("적의 공격을 방어했습니다!");
            }
        }
    }
}
