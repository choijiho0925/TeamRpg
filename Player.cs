using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    // Player 클래스 - 게임 내 플레이어 캐릭터를 나타냅니다.
    public class Player
    {
        // ============== 인벤토리 - 다른 클래스에서 접근할 수 있도록 public ==============
        public Inventory inventory;

        // ============== 플레이어의 기본 속성들 ==============
        private string name;        // 플레이어 이름
        private string job;         // 플레이어 직업 (전사, 궁수, 도적 중 하나)
        private int health;         // 현재 체력
        private int maxHealth;      // 최대 체력
        private int mana;           // 현재 마나
        private int maxMana;        // 최대 마나
        private int level;       // 플레이어 레벨
        private int experience;  // 누적 경험치 (비공개, 출력 X)

        // 기존 코드와의 호환성을 위해 public으로 변경
        public int attack;          // 전체 공격력 (기본 + 장비 보너스)
        public int defense;         // 전체 방어력 (기본 + 장비 보너스)

        // 내부적으로 사용할 속성들
        private int baseAttack;     // 기본 공격력 (장비 효과 제외)
        private int baseDefense;    // 기본 방어력 (장비 효과 제외)
        private int bonusAttack;    // 장비로부터 얻는 추가 공격력
        private int bonusDefense;   // 장비로부터 얻는 추가 방어력
        private int gold;           // 보유 골드

        // ============== 장착 아이템 관련 속성들 - 기존 코드와 호환되도록 public ==============
        public Item equippedWeapon; // 현재 장착된 무기
        public Item equippedArmor;  // 현재 장착된 방어구
        private List<Item> equippedItems; // 장착된 모든 아이템 목록

        // ============== 생성자 - 플레이어 객체를 생성할 때 이름과 직업을 지정합니다. ==============
        public Player(string name, string job)
        {
            // 인벤토리 초기화 (다른 클래스에서 접근 가능하도록 먼저 생성)
            inventory = new Inventory(this);

            // 입력받은 이름을 저장합니다.
            this.name = name;

            // 직업을 설정하고 유효한지 확인합니다.
            SetJob(job);

            // 직업에 맞는 초기 능력치를 설정합니다.
            InitializeStats();

            // 현재 체력과 마나를 최대치로 설정합니다.
            this.health = this.maxHealth;
            this.mana = this.maxMana;

            // 초기 골드를 0으로 설정합니다.
            this.gold = 0;

            // 장비 효과 초기화 (시작 시 장비 없음)
            this.bonusAttack = 0;
            this.bonusDefense = 0;

            // 총 공격력과 방어력 초기화
            this.attack = this.baseAttack + this.bonusAttack;
            this.defense = this.baseDefense + this.bonusDefense;

            // 레벨과 경험치 초기화
            this.level = 1;      // 레벨 1로 고정
            this.experience = 0; // 경험치 0으로 시작

            // 장착된 아이템 리스트 초기화
            this.equippedItems = new List<Item>();
            this.equippedWeapon = null;
            this.equippedArmor = null;

            // 인벤토리에 플레이어 참조 설정 - 이 부분을 추가
            inventory.SetPlayer(this);

            // 플레이어 생성 메시지를 출력합니다.
            Console.WriteLine($"{name} {job}(이)가 생성되었습니다!");
            Console.WriteLine("초기 스탯:");
            DisplayStatus();
        }


        // ============== 직업 설정 메서드 - 유효한 직업인지 검사하고 설정합니다. ==============
        // 유효하지 않으면 다시 입력받도록 false를 반환합니다.
        public bool SetJob(string job)
        {
            // 입력된 직업이 유효한지 확인 (전사, 궁수, 도적만 가능)
            if (job == "검투사" || job == "수렵꾼" || job == "암살자")
            {
                this.job = job;
                return true; // 유효한 직업
            }
            else
            {
                // 잘못된 직업 입력 시 메시지 출력
                Console.WriteLine("잘못된 직업입니다. 검투사, 수렵꾼, 암살자 중에서 선택해주세요.");
                return false; // 유효하지 않은 직업
            }
        }

        // ============== 직업별 초기 능력치 설정 메서드 ==============
        // 각 직업마다 다른 능력치를 가집니다.
        private void InitializeStats()
        {
            // job 변수에 저장된 직업명에 따라 다른 초기값을 설정합니다.
            switch (job)
            {
                case "검투사":
                    // 검투사는 체력과 방어력이 높지만 마나가 적습니다.
                    this.maxHealth = 150;  // 최대 체력 150
                    this.maxMana = 50;     // 최대 마나 50
                    this.baseAttack = 15;  // 기본 공격력 15
                    this.baseDefense = 10; // 기본 방어력 10
                    break;

                case "수렵꾼":
                    // 수렵꾼은 중간 수준의 균형잡힌 능력치를 가집니다.
                    this.maxHealth = 120;  // 최대 체력 120
                    this.maxMana = 80;     // 최대 마나 80
                    this.baseAttack = 18;  // 기본 공격력 18
                    this.baseDefense = 6;  // 기본 방어력 6
                    break;

                case "암살자":
                    // 암살자는 공격력이 높지만 체력과 방어력이 낮습니다.
                    this.maxHealth = 100;  // 최대 체력 100
                    this.maxMana = 70;     // 최대 마나 70
                    this.baseAttack = 20;  // 기본 공격력 20
                    this.baseDefense = 4;  // 기본 방어력 4
                    break;

                default:
                    // 이 부분은 SetJob 메서드에서 이미 검증되었으므로 실행되지 않아야 합니다.
                    // 하지만 예외 상황에 대비해 기본값 설정
                    Console.WriteLine("오류: 직업이 올바르게 설정되지 않았습니다. 개발자에게 문의하세요.");
                    this.maxHealth = 120;
                    this.maxMana = 60;
                    this.baseAttack = 12;
                    this.baseDefense = 8;
                    break;
            }

            // 기본값으로 총 공격력과 방어력 초기화
            this.attack = this.baseAttack;
            this.defense = this.baseDefense;
        }

        // ============== 플레이어 상태 표시 메서드 ==============
        // 현재 플레이어의 모든 능력치와 상태를 화면에 출력합니다.
        public void DisplayStatus()
        {
            // 구분선과 함께 플레이어 정보 표시 시작
            Console.WriteLine("\n===== 플레이어 정보 =====");

            // 각 속성별로 정보 출력
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"직업: {job}");
            Console.WriteLine($"레벨: {level}");  // 레벨 정보 출력 추가
            Console.WriteLine($"체력: {health}/{maxHealth}");
            Console.WriteLine($"마나: {mana}/{maxMana}");
            Console.WriteLine($"기본 공격력: {baseAttack} (장비 보너스: +{attack - baseAttack})");
            Console.WriteLine($"총 공격력: {attack}");
            Console.WriteLine($"기본 방어력: {baseDefense} (장비 보너스: +{defense - baseDefense})");
            Console.WriteLine($"총 방어력: {defense}");
            Console.WriteLine($"보유 골드: {gold}G");

            // 장착 중인 아이템이 있으면 표시
            if (equippedItems.Count > 0)
            {
                Console.WriteLine("\n[장착 중인 아이템]");
                foreach (Item item in equippedItems)
                {
                    Console.WriteLine($"- {item.Name} (공격력: +{item.Attack}, 방어력: +{item.Defense})");
                }
            }
            else
            {
                Console.WriteLine("\n[장착 중인 아이템이 없습니다]");
            }

            // 마무리 구분선
            Console.WriteLine("========================\n");
        }


        public void EarnExperience(int amount)
        {
            experience += amount;
            // 현재는 레벨업 기능이 없으므로 단순히 누적만 함
        }

        public int Level
        {
            get { return level; }
        }

        public int Experience
        {
            get { return experience; }
        }

        // ============== 데미지 처리 메서드 ==============
        // 몬스터나 기타 요인으로부터 받은 데미지를 처리합니다.
        public void TakeDamage(int damage)
        {
            // 실제 받는 데미지 계산 (방어력으로 감소, 최소 1)
            int actualDamage = Math.Max(1, damage - defense);

            // 현재 체력에서 데미지 감소 (최소 0)
            int oldHealth = health;  // 이전 체력 저장
            health = Math.Max(0, health - actualDamage);

            // 데미지 받음 메시지 출력
            Console.WriteLine($"{name}이(가) {actualDamage}의 데미지를 받았습니다. ({oldHealth} -> {health})");

            // 체력이 0 이하로 떨어지면 쓰러짐 메시지 출력
            if (health <= 0)
            {
                Console.WriteLine($"{name}이(가) 쓰러졌습니다!");
                Console.WriteLine("Game Over...");
            }
        }

        // ============== 공격 메서드 ==============
        // 플레이어가 공격할 때 호출되며, 공격력 값을 반환합니다.
        public int Attack(Monster target)
        {
            // 랜덤 요소를 위한 난수 생성기
            Random rand = new Random();

            // 공격력의 90~110% 범위에서 실제 공격력 결정 (기본+장비 보너스 포함)
            int actualAttack = (int)(attack * (0.9 + rand.NextDouble() * 0.2));

            // 공격 메시지 출력
            Console.WriteLine($"{name}이(가) 공격합니다! (공격력: {actualAttack})");

            //적 체력 감소
            target.Health -= actualAttack;

            // 계산된 공격력 반환
            return actualAttack;

        }

        // ============== 골드 획득 메서드 ==============
        // 전투 승리나 아이템 판매 등으로 골드를 획득할 때 호출됩니다.
        public void EarnGold(int amount)
        {
            // 이전 골드 저장
            int oldGold = gold;

            // 골드 증가
            gold += amount;

            // 골드 획득 메시지 출력
            Console.WriteLine($"{amount}G를 획득했습니다. ({oldGold}G -> {gold}G)");
        }

        // ============== 골드 소비 메서드 ==============
        // 아이템 구매나 기타 요인으로 골드를 소비할 때 호출됩니다.
        // 골드가 충분하면 true, 부족하면 false를 반환합니다.
        public bool SpendGold(int amount)
        {
            // 보유 골드가 충분한지 확인
            if (gold >= amount)
            {
                // 충분하면 골드 감소
                int oldGold = gold;  // 이전 골드 저장
                gold -= amount;

                // 골드 사용 메시지 출력
                Console.WriteLine($"{amount}G를 사용했습니다. ({oldGold}G -> {gold}G)");
                return true;  // 골드 사용 성공
            }
            else
            {
                // 부족하면 메시지 출력 후 false 반환
                Console.WriteLine($"골드가 부족합니다. 필요: {amount}G, 보유: {gold}G");
                return false;  // 골드 사용 실패
            }
        }

        // ============== 아이템 장착 가능 여부 확인 메서드 ==============
        // 해당 아이템을 플레이어가 장착할 수 있는지 확인합니다.
        public bool CanEquipItem(Item item)
        {
            // 1. 직업 제한 확인
            // 아이템에 직업 제한이 없거나(빈 문자열), "공용" 표시가 있거나, 플레이어 직업과 일치하면 장착 가능
            bool jobMatches = string.IsNullOrEmpty(item.Job) || item.Job == "공용" || item.Job == job;

            if (!jobMatches)
            {
                Console.WriteLine($"{name}의 직업({job})은 이 아이템({item.Name})을 장착할 수 없습니다. (필요 직업: {item.Job})");
                return false;
            }

            // 2. 이미 장착된 아이템인지 확인
            if (item.isEquipped)
            {
                Console.WriteLine($"{item.Name}은(는) 이미 장착되어 있습니다.");
                return false;
            }

            // 모든 조건 충족 - 장착 가능
            return true;
        }

        // ============== 아이템 장착 메서드 ==============
        // 아이템을 장착하고 능력치에 효과를 적용합니다.
        public bool EquipItem(Item item)
        {
            // 아이템 장착 가능 여부 확인
            if (!CanEquipItem(item))
            {
                return false; // 장착 불가
            }

            // 이전 능력치 저장 (변화량 표시용)
            int oldAttack = attack;
            int oldDefense = defense;

            // 아이템 종류에 따라 다르게 처리
            if (item.Attack > 0)
            {
                // 무기인 경우
                if (equippedWeapon != null)
                {
                    // 이미 무기를 장착 중이면 해제
                    UnequipItem(equippedWeapon);
                }
                equippedWeapon = item;
                // 공격력 증가
                bonusAttack += item.Attack;
                attack += item.Attack;
            }
            else if (item.Defense > 0)
            {
                // 방어구인 경우
                if (equippedArmor != null)
                {
                    // 이미 방어구를 장착 중이면 해제
                    UnequipItem(equippedArmor);
                }
                equippedArmor = item;
                // 방어력 증가
                bonusDefense += item.Defense;
                defense += item.Defense;
            }

            // 아이템을 장착 상태로 변경
            item.isEquipped = true;

            // 장착된 아이템 리스트에 추가
            equippedItems.Add(item);

            // 능력치 변화 메시지 출력
            Console.WriteLine($"{item.Name}을(를) 장착했습니다.");

            // 공격력 변화가 있으면 표시
            if (item.Attack != 0)
            {
                Console.WriteLine($"공격력: {oldAttack} -> {attack} (+{item.Attack})");
            }

            // 방어력 변화가 있으면 표시
            if (item.Defense != 0)
            {
                Console.WriteLine($"방어력: {oldDefense} -> {defense} (+{item.Defense})");
            }

            return true; // 장착 성공
        }

        // ============== 아이템 해제 메서드 ==============
        // 장착된 아이템을 해제하고 능력치 효과를 제거합니다.
        public bool UnequipItem(Item item)
        {
            // 장착되지 않은 아이템은 해제할 수 없음
            if (!item.isEquipped)
            {
                Console.WriteLine($"{item.Name}은(는) 장착되어 있지 않습니다.");
                return false;
            }

            // 이 아이템이 플레이어의 장착 목록에 있는지 확인
            if (!equippedItems.Contains(item))
            {
                Console.WriteLine($"{item.Name}은(는) 이 플레이어가 장착한 아이템이 아닙니다.");
                return false;
            }

            // 이전 능력치 저장 (변화량 표시용)
            int oldAttack = attack;
            int oldDefense = defense;

            // 아이템 종류에 따라 참조 제거 및 능력치 감소
            if (equippedWeapon == item)
            {
                equippedWeapon = null;
                bonusAttack -= item.Attack;
                attack -= item.Attack;
            }
            else if (equippedArmor == item)
            {
                equippedArmor = null;
                bonusDefense -= item.Defense;
                defense -= item.Defense;
            }

            // 아이템 장착 상태 변경
            item.isEquipped = false;

            // 장착된 아이템 리스트에서 제거
            equippedItems.Remove(item);

            // 능력치 변화 메시지 출력
            Console.WriteLine($"{item.Name}을(를) 해제했습니다.");

            // 공격력 변화가 있으면 표시
            if (item.Attack != 0)
            {
                Console.WriteLine($"공격력: {oldAttack} -> {attack} (-{item.Attack})");
            }

            // 방어력 변화가 있으면 표시
            if (item.Defense != 0)
            {
                Console.WriteLine($"방어력: {oldDefense} -> {defense} (-{item.Defense})");
            }

            return true; // 해제 성공
        }

        // ============== 체력 회복 메서드 ==============
        // 아이템 사용이나 휴식 등으로 체력을 회복할 때 호출됩니다.
        public void Heal(int amount)
        {
            // 이전 체력 저장
            int oldHealth = health;

            // 체력 회복 (최대치 초과 방지)
            health = Math.Min(maxHealth, health + amount);

            // 실제 회복된 양 계산
            int actualHeal = health - oldHealth;

            // 체력 회복 메시지 출력
            if (actualHeal > 0)
            {
                Console.WriteLine($"{name}의 체력이 {actualHeal}만큼 회복되었습니다. ({oldHealth} -> {health}/{maxHealth})");
            }
            else
            {
                Console.WriteLine($"{name}의 체력이 이미 최대치입니다. ({health}/{maxHealth})");
            }
        }

        // ============== 마나 회복 메서드 ==============
        // 아이템 사용이나 휴식 등으로 마나를 회복할 때 호출됩니다.
        public void RecoverMana(int amount)
        {
            // 이전 마나 저장
            int oldMana = mana;

            // 마나 회복 (최대치 초과 방지)
            mana = Math.Min(maxMana, mana + amount);

            // 실제 회복된 양 계산
            int actualRecover = mana - oldMana;

            // 마나 회복 메시지 출력
            if (actualRecover > 0)
            {
                Console.WriteLine($"{name}의 마나가 {actualRecover}만큼 회복되었습니다. ({oldMana} -> {mana}/{maxMana})");
            }
            else
            {
                Console.WriteLine($"{name}의 마나가 이미 최대치입니다. ({mana}/{maxMana})");
            }
        }

        // ============== 마나 사용 메서드 ==============
        // 스킬 사용 등으로 마나를 소비할 때 호출됩니다.
        // 마나가 충분하면 true, 부족하면 false를 반환합니다.
        public bool UseMana(int amount)
        {
            // 마나가 충분한지 확인
            if (mana >= amount)
            {
                // 충분하면 마나 감소
                int oldMana = mana;  // 이전 마나 저장
                mana -= amount;

                // 마나 사용 메시지 출력
                Console.WriteLine($"{name}이(가) {amount}의 마나를 사용했습니다. ({oldMana} -> {mana}/{maxMana})");
                return true;  // 마나 사용 성공
            }
            else
            {
                // 부족하면 메시지 출력 후 false 반환
                Console.WriteLine($"마나가 부족합니다. 필요: {amount}, 보유: {mana}/{maxMana}");
                return false;  // 마나 사용 실패
            }
        }

        // ============== 생존 확인 메서드 ==============
        // 플레이어가 살아있는지 확인합니다. 체력이 0보다 크면 true, 아니면 false를 반환합니다.
        public bool IsAlive()
        {
            // 체력이 0보다 크면 살아있음
            return health > 0;
        }

        // ============== 직업별 특수 기술 사용 메서드 ==============
        // 각 직업마다 다른 특수 기술을 사용할 수 있습니다.
        // 주석 처리된 코드를 살림
        public int UseSpecialSkill(Monster target)
        {
            // 직업별로 다른 특수 기술 구현
            int skillDamage = 0;
            int manaCost = 0;
            string skillName = "";

            // 직업에 따라 다른 스킬 효과 설정
            switch (job)
            {
                case "검투사":
                    skillName = "콜로세움의 생존자";
                    skillDamage = (int)(attack * 2);  // 총 공격력의 2배 데미지
                    manaCost = 20;  // 마나 20 소비
                    break;

                case "수렵꾼":
                    skillName = "야생의 감각";
                    skillDamage = (int)(attack * 1.5);  // 총 공격력의 1.5배 데미지
                    manaCost = 15;  // 마나 15 소비
                    break;

                case "암살자":
                    skillName = "암습";
                    skillDamage = (int)(attack * 1.8);  // 총 공격력의 1.8배 데미지
                    manaCost = 10;  // 마나 10 소비
                    break;

                default:
                    skillName = "기본 스킬";
                    skillDamage = attack;  // 기본 공격력과 동일한 데미지
                    manaCost = 5;  // 마나 5 소비
                    break;
            }

            // 마나 사용 시도
            if (UseMana(manaCost))
            {
                // 마나가 충분하면 스킬 사용 메시지 출력 후 데미지 반환
                Console.WriteLine($"{name}이(가) {skillName} 기술을 사용했습니다!(데미지: {skillDamage})");
                target.Health -= skillDamage;
                return skillDamage;
            }
            else
            {
                // 마나가 부족하면 0 반환
                Console.WriteLine("마나가 부족하여 특수 기술을 사용할 수 없습니다!");
                return 0;  // 마나 부족으로 스킬 사용 실패
            }
        }


        // ============== 속성(Property) 정의 - C#의 getter/setter ==============
        // 다른 클래스에서 플레이어의 속성을 읽거나 수정할 때 사용됩니다.

        // 플레이어 이름 - 읽기만 가능
        public string Name
        {
            get { return name; }
        }

        // 플레이어 직업 - 읽기만 가능
        public string Job
        {
            get { return job; }
        }

        // 현재 체력 - 읽기/쓰기 가능
        public int Health
        {
            get { return health; }
            set
            {
                // 체력이 0보다 작아지지 않도록 방지
                health = Math.Max(0, value);
                // 최대 체력을 넘지 않도록 방지
                health = Math.Min(maxHealth, health);
            }
        }

        // 최대 체력 - 읽기만 가능
        public int MaxHealth
        {
            get { return maxHealth; }
        }

        // 현재 마나 - 읽기/쓰기 가능
        public int Mana
        {
            get { return mana; }
            set
            {
                // 마나가 0보다 작아지지 않도록 방지
                mana = Math.Max(0, value);
                // 최대 마나를 넘지 않도록 방지
                mana = Math.Min(maxMana, mana);
            }
        }

        // 최대 마나 - 읽기만 가능
        public int MaxMana
        {
            get { return maxMana; }
        }

        // 기본 공격력 - 읽기/쓰기 가능
        public int BaseAttack
        {
            get { return baseAttack; }
            set
            {
                baseAttack = Math.Max(1, value);  // 최소 1의 공격력 보장
                // 총 공격력도 함께 업데이트
                attack = baseAttack + bonusAttack;
            }
        }

        // 기본 방어력 - 읽기/쓰기 가능
        public int BaseDefense
        {
            get { return baseDefense; }
            set
            {
                baseDefense = Math.Max(0, value);  // 최소 0의 방어력 보장
                // 총 방어력도 함께 업데이트
                defense = baseDefense + bonusDefense;
            }
        }

        // 보유 골드 - 읽기/쓰기 가능
        public int Gold
        {
            get { return gold; }
            set { gold = Math.Max(0, value); }  // 음수 골드 방지
        }
    }
}