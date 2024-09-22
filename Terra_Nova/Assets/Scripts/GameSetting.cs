using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum geograph
{
    ROSS_SEA,//로스해
    TRANSANTARTIC,//남극횡단산맥
    BEARDMORE,//비어드모어
    AXEL_HEIBERG,//악셀하이베르크
    POLAR_PLATEAU,//남극 고원

    GLACIER,//빙붕
    MOUNTAIN,//산
    PLATEAU,//고원
    GOAL,//남극점 부근

    TEKELI_LI//광기의 산맥
}
public enum CREW_MOVEMENT
{
    WALK,//0.25
    SKI,//0.4
    PONY,//0.5
    SKI_UPGRADE,//0.55
    DOG_SLED,//0.55
    SNOWMOBILE,//0.75
    BEAR_SLED//0.75


}
public enum CREW_CLOTH
{
    FUR1,
    FUR2,//모피
    FUR3,
    WOOL1,
    WOOL2,//모직
    WOOL3,

    NAKED
}
/// <summary>
/// 열거형 ID, (주석)트레잇 이름 - 세부 설명 형식
/// </summary>
public enum EQUIPMENT_TRAIT//장비 트레잇 - 가진 장구류 기반
{
    SUNGLASS,//편광 선글라스
    EQUIP_SKI,//스키 착용
    EQUIP_SKI_UPGRADE,//비욜란스키
    CONTAINED_SNOWMOBILE,//스노모빌 운반중 - 스콧 탐험대는 고장난 스노모빌을 버리기 아깝다고 직접 끌고다녔음
    WOOL_CLOTH,//여분 양모옷
    FUR_CLOTH//여분 모피옷
}
public enum EVENT_TRAIT//이벤트 트레잇 - 질병 등 상태이상 기반
{
    EXHAUSTED,//탈진
    FROSTBITE, //동상
    CRITICAL_FROST_BITE, //치명적 동상
    POWERFUL,
    STUMBLED,

    UNREST,
    SATISFIED,
    FULLNESS,
    TONE_CLOTH,
    SNOW_BLIND,
    SWEAT,
    SWEAT_2,
    DYSTROPHY,
    // 역사적 트레잇
    EVANS_CRITICAL_ACCIDENT //에드워드 에반스의 뇌손상 - 크레바스 추락 이후 뇌사, 2일 후 신체적으로도 사망
}
public enum CREW_TRAIT//대원 트레잇 - 대원 자체의 특징(ex. 소속, 경력)에 기반
{
    
    SKI_ENABLE,//스키 사용자
    SKI_CHAMP,//스키 챔피언
    MARINE,//
    HORSE_1,//말 사육사

    DOG_1,
    BEARER,
    GUIDE,
    USELESS_VETERAN,
    MAINTENANCE,
    COOK,
    HUNTER,
    SCIENTIST,
    GEOLOGIST,
    GOOD_HEALTH,
    GLASS_JAW,
    FAST_RECOVER,
    LOYAL,
    KIND,
    // 역사적 트레잇
    OLAV_SKI_CHAMPION,//노르웨이 스키 챔피언 / 올라프 비욜란 - 당대 노르웨이 스키 챔피언, 아문센 탐험대의 경량스키를 제작함
    OATES_POLAR_CAVARLY,//극지의 기사 / 로렌스 오츠 - 스콧 탐험대 극점팀 말 운용 담당, 탐험대 내부에서 가장 이성적인 인물
    PARATROOPER,//낙하산
    ARROGANT_BRITISH,//스콧
}
[System.Serializable]
public struct ExpeditionCrew
{
    public int CREW_ID;
    public CREW_MOVEMENT CREW_MOVE;
    public CREW_CLOTH CREW_CLOTH;
    public List<Trait_data> Traits;
    //public List<EQUIPMENT_TRAIT> EQUIP_TRAIT;
    ////public EQUIPMENT_TRAIT[] EQUIP_TRAIT;
    //public List<EVENT_TRAIT> EVENT_TRAIT;
    ////public EVENT_TRAIT[] EVENT_TRAIT;
    //public List<CREW_TRAIT> CREW_TRAIT;
    ////public CREW_TRAIT[] CREW_TRAIT;
    public string CREW_NAME;
    public float HPMAX, HP, hungerMAX, hunger, moraleMAX, morale;
    public int temperature,portrait;
    public float speed;
    public ExpeditionCrew(int id, CREW_MOVEMENT move, CREW_CLOTH cloth, string crew_name, float hp, float hunger, float morale, List<Trait_data> traits = default, int temperature = 5, int portrait = 0, float spd = 1, float HPMAX = 100, float moraleMAX = 100, float hungerMAX = 100)
    //public ExpeditionCrew(int id, CREW_MOVEMENT move, CREW_CLOTH cloth, string crew_name, float hp, float hunger, float morale, int temperature = 5, List<EQUIPMENT_TRAIT> eq_trait = default, List<EVENT_TRAIT> ev_trait = default, List<CREW_TRAIT> crew_trait = default, int portrait = 0)
    {
        
        CREW_ID = id;
        CREW_MOVE = move;
        CREW_CLOTH = cloth;
        //EQUIP_TRAIT = new List<EQUIPMENT_TRAIT>();
        //EQUIP_TRAIT = eq_trait;
        //EVENT_TRAIT = new List<EVENT_TRAIT>();
        //EVENT_TRAIT = ev_trait;
        //CREW_TRAIT = new List<CREW_TRAIT>();
        //CREW_TRAIT = crew_trait;
        this.hungerMAX = hungerMAX;
        this.HPMAX = HPMAX;
        this.moraleMAX = moraleMAX;
        Traits = traits;
        CREW_NAME = crew_name;
        HP = hp;
        this.hunger = hunger;
        this.morale = morale;
        this.temperature = temperature;
        this.portrait = portrait;
        speed = spd;
    }

    //public ExpeditionCrew(int id, CREW_MOVEMENT move, CREW_CLOTH cloth, string crew_name, float hp, float hunger, float morale, float temperature)
    //{

    //    CREW_ID = id;
    //    CREW_MOVE = move;
    //    CREW_CLOTH = cloth;
    //    EQUIP_TRAIT = new List<EQUIPMENT_TRAIT>();
    //    //EQUIP_TRAIT.Add(eq_trait);
    //    EVENT_TRAIT = new List<EVENT_TRAIT>();
    //    //EVENT_TRAIT.Add(ev_trait);
    //    CREW_TRAIT = new List<CREW_TRAIT>();
    //    //CREW_TRAIT.Add(crew_trait);
    //    CREW_NAME = crew_name;
    //    HPMAX = hp;
    //    HP = hp;
    //    hungerMAX = hunger;
    //    this.hunger = hunger;
    //    this.morale = morale;
    //    moraleMAX = morale;
    //    this.temperature = temperature;
    //}
}


//[System.Serializable]
//public struct Trait
//{
//    public trait_category category;
//    public List<Modifier> modifiers;
//    public Sprite icon, Big_Icon;
//    public string Name;
//    public string description;
//    public bool Unique;

//    public Trait(Sprite icon, Sprite big, string name, string desc, bool unique, trait_category category, List<Modifier> modifier)
//    {
//        this.icon = icon;
//        Big_Icon = big;
//        Name = name;
//        description = desc;
//        Unique = unique;
//        this.category = category;
//        modifiers = modifier;
//    }
//}

//public struct EventOptions
//{
//    public string option_title, option_desc;
//    public int eventtarget;
//    public List<EffectVariations> eventoption;

//    public EventOptions(string title, string desc, int target, List<EffectVariations> effect = default)
//    {
//        option_title = title;
//        option_desc = desc;
//        eventtarget = target;
//        eventoption = new List<EffectVariations>();
//        eventoption = effect;
//    }
//}

public enum EffectVariationEnum
{
    HP,
    HUN,
    TRU
}

public struct EffectVariations
{
    public EffectVariationEnum effect;
    public float var;

    public EffectVariations(EffectVariationEnum eff, float var)
    {
        effect = eff;
        this.var = var;
    }
}
public class GameSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
