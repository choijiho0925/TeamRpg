using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class Monster
    {
        public string Name;
        public int Health;
        public int Mana;
        public int Attack;
        public int Defense;
        public int RewardGold;

        Random rand = new Random();

        // 생성자
        public Monster(string name, int health, int mana, int attack, int defense, int rewardGold)
        {
            Name = name;
            Health = health;
            Mana = mana;
            Attack = attack;
            Defense = defense;
            RewardGold = rewardGold;
        }

        //적의 능력치 출력
        //public void PrintInfo(int number)
        //{
        //    Console.WriteLine($"{number}. {Name} (체력: {Health}, 마나: {Mana}, 공격력: {Attack}, 방어력: {Defense})");
        //}
        //공격을 받을 때
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }
        //공격할 때
        public void EnemyAttack(Player player)
        {
            int attack = Game.Instance.monsterAttack;
            int actualAttack = (int)(attack * (0.9 + rand.NextDouble() * 0.2));

            if (actualAttack >= 0)
            {

                Console.WriteLine($"\n{Name}가 {player.Name}을(를) 공격합니다! {actualAttack}의 피해를 입혔습니다.");
                player.TakeDamage(actualAttack);
            }
            else
            {
                Console.WriteLine("적의 공격을 방어했습니다!");
            }
        }
    }
}
