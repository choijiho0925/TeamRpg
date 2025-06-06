﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRpg
{
    public class ItemManager
    {
        private static ItemManager instance;

        public static ItemManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ItemManager();
                return instance;
            }
        }

        private ItemManager()
        {
            Items = new List<Item>()
            {
                new Item(JobOption.Gladiator,"검투사", "무딘 강철검","훈련용으로 널리 쓰이는 기본 강철검. 세월의 흔적으로 날은 닳았지만, 무게감은 여전하다.\n초심자의 첫 전투를 함께하는 가장 현실적인 무기.", 3, 0, 0, false, false),
                new Item(JobOption.Gladiator,"검투사", "균형 잡힌 롱소드","잘 다듬어진 칼날과 안정적인 무게 중심 덕분에, 전투 중 흔들림이 적다.\n중급 전사들이 가장 선호하는 표준 장비 중 하나.", 6, 0, 1500, false, false),
                new Item(JobOption.Gladiator,"검투사", "쯔바이핸더","양손으로 휘두르도록 설계된 독일식 대검. 그 압도적인 길이와 무게는 한 번의 일격으로 전열을 무너뜨릴 수 있으며, \n숙련된 전사만이 온전히 제어할 수 있다. 수많은 전장에서 '거인을 무찌르는 칼'이라 불려왔다.", 10, 0, 4000, false, false),
                new Item(JobOption.Hunter,"수렵꾼", "낡은 짧은 활","저렴한 재료와 단순한 구조. 그러나 꾸준히 수리하고 조정하면, 입문자에겐 좋은 동반자가 되어준다.", 3, 0, 0, false, false),
                new Item(JobOption.Hunter,"수렵꾼", "중형 활","다양한 거리에서의 전투를 커버할 수 있도록 설계된 활. 다용도이며 상황에 따라 다양한 방식으로 활용 가능하다.", 6, 0, 1500, false, false),
                new Item(JobOption.Hunter,"수렵꾼", "잉글리시 롱보우","중세 전장에서 무수한 전설을 남긴 장궁. 뛰어난 사거리와 관통력을 자랑하며,\n강한 팔힘을 가진 궁수만이 그 잠재력을 끌어낼 수 있다. 훈련된 궁병은 이 활 하나로 전세를 바꾸기도 했다.\n군 정찰대와 궁병 부대에서 채택한 장궁. 장거리 사격은 물론, 적의 약점을 정확히 관통할 수 있는 정밀도를 자랑한다.", 10, 0, 4000, false, false),
                new Item(JobOption.Assassin,"암살자", "녹슨 단검","너무 오래 방치되어 녹이 슬었지만, 급박한 상황에서는 여전히 유용하다. 언제나 여분으로 들고 다니는 단검.", 3, 0, 0, false, false),
                new Item(JobOption.Assassin,"암살자", "경량 단검","빠르게 움직이며 공격을 주고받아야 하는 전투에서 빛을 발한다. 짧은 사거리지만 기동성은 매우 우수하다.", 6, 0, 1500, false, false),
                new Item(JobOption.Assassin,"암살자", "스틸 스틸레토","날카롭고 가느다란 찌르기 전용 단검. 갑옷의 약점을 노리기 위한 무기로, 은신과 기습에 최적화되어 있다.\n한 번 찔러 넣으면 빠지지 않는 구조 덕분에, 짧지만 치명적인 순간을 만든다. 기습, 은신, 교란에 최적화된 전술용 무기.\n도시 전투나 특수 작전에서 널리 사용되며, 숙련자의 손에 들렸을 때 그 진가를 발휘한다.", 10, 0, 4000, false, false),
                new Item(JobOption.Gladiator,"육군 이등별", "K2소총","현대적인 화력과 정밀성을 자랑하는 군용 소총. 멀리서도 적을 정확히 타격할 수 있는 우수한 성능을 자랑한다.\n국방과학연구소에서 개발한 대한민국 국군의 표준 소총으로, 던전의 어떤 몬스터도 감당할 수 없는 파괴력을 지녔다.", 100, 0, 1000000000, false, false),
                new Item(JobOption.Amor,"방어구", "패디드 아머","여러 겹의 천과 가죽을 덧댄 경량 방어구. 착용이 간편하고 이동에 불편함이 없다.\n주로 초보 모험가나 금전이 부족한 자들이 애용한다. 강력한 공격엔 취약하지만, 아무것도 안 입는 것보단 낫다.", 0, 2, 0, false, false),
                new Item(JobOption.Amor,"방어구", "체인메일","수천 개의 금속 고리를 정교하게 엮어 만든 방어구. 칼날과 화살을 효과적으로 막아내며, 가성비가 뛰어나 널리 보급되었다.\n민첩성을 어느 정도 유지할 수 있어 다양한 직업이 사용 가능하다.", 0, 5, 1500, false, false),
                new Item(JobOption.Amor,"방어구", "플레이트 아머","전신을 두르는 강철판 방어구. 압도적인 방어력을 자랑하며, 전장의 정예병 혹은 기사단의 상징이다. 무게로 인해 체력과 숙련도가 요구되지만, 생존률을 극대화할 수 있다.", 0, 10, 4000, false, false),
                new Item(JobOption.Amor,"방어구", "피 묻고 낡은 갑옷","왜인지 모르는 피묻고 낡은 갑옷. 1차 원정대를 이끌던 귀족 가문의 인장이 새겨져있다.", 0, 0, 0, false, false),
                new Item(JobOption.Potion,"포션", "엘릭서", "이곳저곳 떠도는 연금술사들이 만든, 죽음을 상대로한 승리 라고 칭해지는 약. HP를 50회복한다.", 0, 0, 1000, false, false),
                new Item(JobOption.Potion,"포션", "무미야", "오스트리아 공국의 연금술 상인 길드에서 만들어낸 만병통치약. 먹으면 정신이 멍해지며 기력이 오른다. MP를 50회복한다.", 0, 0, 1000, false, false)
            };
        }

        public List<Item> Items;
    }
}