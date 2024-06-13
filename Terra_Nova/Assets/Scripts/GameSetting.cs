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
    FUR,//����
    WOOL//����
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
    WOOL_CLOTH,
    FUR_CLOTH
}
public enum EVENT_TRAIT//�̺�Ʈ Ʈ���� - ���� �� �����̻� ���
{
    EXHAUSTED,//Ż��
    FROSTBITE, //����
    CRITICAL_FROST_BITE, //ġ���� ����
    // ������ Ʈ����
    EVANS_CRITICAL_ACCIDENT //������� ���ݽ��� ���ջ� - ũ���ٽ� �߶� ���� ����, 2�� �� ��ü�����ε� ���
}
public enum CREW_TRAIT//��� Ʈ���� - ��� ��ü�� Ư¡(ex. �Ҽ�, ���)�� ���
{
    
    SKI_ENABLE,//��Ű �����
    SKI_CHAMP,//��Ű è�Ǿ�
    
    // ������ Ʈ����
    OLAV_SKI_CHAMPION,//�븣���� ��Ű è�Ǿ� / �ö��� ���� - ��� �븣���� ��Ű è�Ǿ�, �ƹ��� Ž����� �淮��Ű�� ������
    OATES_POLAR_CAVARLY,//������ ��� / �η��� ���� - ���� Ž��� ������ �� ��� ���, Ž��� ���ο��� ���� �̼����� �ι�
}
public struct ExpeditionCrew
{
    public int CREW_ID;
    public CREW_MOVEMENT CREW_MOVE;
    public CREW_CLOTH CREW_CLOTH;
    public List<EQUIPMENT_TRAIT> EQUIP_TRAIT;
    //public EQUIPMENT_TRAIT[] EQUIP_TRAIT;
    public List<EVENT_TRAIT> EVENT_TRAIT;
    //public EVENT_TRAIT[] EVENT_TRAIT;
    public List<CREW_TRAIT> CREW_TRAIT;
    //public CREW_TRAIT[] CREW_TRAIT;
    public string CREW_NAME;
    public float HPMAX, HP, hungerMAX, hunger, moraleMAX, morale, temperature;
    

    public ExpeditionCrew(int id, CREW_MOVEMENT move, CREW_CLOTH cloth, string crew_name, float hp, float hunger, float morale, float temperature = 36f, List<EQUIPMENT_TRAIT> eq_trait = default, List<EVENT_TRAIT> ev_trait = default, List<CREW_TRAIT> crew_trait = default)
    {
        
        CREW_ID = id;
        CREW_MOVE = move;
        CREW_CLOTH = cloth;
        EQUIP_TRAIT = new List<EQUIPMENT_TRAIT>();
        EQUIP_TRAIT = eq_trait;
        EVENT_TRAIT = new List<EVENT_TRAIT>();
        EVENT_TRAIT = ev_trait;
        CREW_TRAIT = new List<CREW_TRAIT>();
        CREW_TRAIT = crew_trait;
        CREW_NAME = crew_name;
        HPMAX = hp;
        HP = hp;
        hungerMAX = hunger;
        this.hunger = hunger;
        this.morale = morale;
        moraleMAX = morale;
        this.temperature = temperature;
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

public enum EVENT_TAG
{
    HISTORICAL,
    GOOD,
    BAD
    
}
public struct Event
{
    public float EHV;//Event Happen Variable
    public bool event_trigger;//�̺�Ʈ ����
    public string event_title;//�̺�Ʈ ����
    public string event_desc;//�̺�Ʈ ����
    public bool event_repeatable;
    public List<EVENT_TAG> event_tag;//�̺�Ʈ �з� �±�
    public List<EventOptions> option;

    public Event(float mtth, bool trigger, string title, string desc, bool repeat, List<EVENT_TAG> tag, List<EventOptions> options)
    {
        EHV = mtth;
        event_trigger = trigger;
        event_title = title;
        event_repeatable = repeat;
        event_desc = desc;
        event_tag = tag;
        option = options;
    }
}

public struct EventOptions
{
    public string option_title, option_desc;
    public int eventtarget;
    public List<EffectVariations> eventoption;

    public EventOptions(string title, string desc, int target, List<EffectVariations> effect = default)
    {
        option_title = title;
        option_desc = desc;
        eventtarget = target;
        eventoption = new List<EffectVariations>();
        eventoption = effect;
    }
}

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
