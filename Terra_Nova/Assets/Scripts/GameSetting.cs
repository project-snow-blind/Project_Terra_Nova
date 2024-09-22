using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum geograph
{
    ROSS_SEA,//�ν���
    TRANSANTARTIC,//����Ⱦ�ܻ��
    BEARDMORE,//������
    AXEL_HEIBERG,//�Ǽ����̺���ũ
    POLAR_PLATEAU,//���� ���

    GLACIER,//����
    MOUNTAIN,//��
    PLATEAU,//���
    GOAL,//������ �α�

    TEKELI_LI//������ ���
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
    FUR2,//����
    FUR3,
    WOOL1,
    WOOL2,//����
    WOOL3,

    NAKED
}
/// <summary>
/// ������ ID, (�ּ�)Ʈ���� �̸� - ���� ���� ����
/// </summary>
public enum EQUIPMENT_TRAIT//��� Ʈ���� - ���� �屸�� ���
{
    SUNGLASS,//�� ���۶�
    EQUIP_SKI,//��Ű ����
    EQUIP_SKI_UPGRADE,//������Ű
    CONTAINED_SNOWMOBILE,//������ ����� - ���� Ž���� ���峭 �������� ������ �Ʊ��ٰ� ���� ����ٳ���
    WOOL_CLOTH,//���� ����
    FUR_CLOTH//���� ���ǿ�
}
public enum EVENT_TRAIT//�̺�Ʈ Ʈ���� - ���� �� �����̻� ���
{
    EXHAUSTED,//Ż��
    FROSTBITE, //����
    CRITICAL_FROST_BITE, //ġ���� ����
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
    // ������ Ʈ����
    EVANS_CRITICAL_ACCIDENT //������� ���ݽ��� ���ջ� - ũ���ٽ� �߶� ���� ����, 2�� �� ��ü�����ε� ���
}
public enum CREW_TRAIT//��� Ʈ���� - ��� ��ü�� Ư¡(ex. �Ҽ�, ���)�� ���
{
    
    SKI_ENABLE,//��Ű �����
    SKI_CHAMP,//��Ű è�Ǿ�
    MARINE,//
    HORSE_1,//�� ������

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
    // ������ Ʈ����
    OLAV_SKI_CHAMPION,//�븣���� ��Ű è�Ǿ� / �ö��� ���� - ��� �븣���� ��Ű è�Ǿ�, �ƹ��� Ž����� �淮��Ű�� ������
    OATES_POLAR_CAVARLY,//������ ��� / �η��� ���� - ���� Ž��� ������ �� ��� ���, Ž��� ���ο��� ���� �̼����� �ι�
    PARATROOPER,//���ϻ�
    ARROGANT_BRITISH,//����
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
