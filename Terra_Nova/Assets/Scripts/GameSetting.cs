using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CREW_MOVEMENT
{
    SKI,
    SKI_UPGRADE,
    WALK,
    SNOWMOBILE,
    DOG_SLED,
    BEAR_SLED,
    PONY

}
public enum CREW_CLOTH
{
    FUR,
    WOOL
}

public enum EQUIPMENT_TRAIT
{
    SUNGLASS,
    EQUIP_SKI,
    EQUIP_SKI_UPGRADE
}
public enum EVENT_TRAIT
{

}
public enum CREW_TRAIT
{

}
public struct ExpeditionCrew
{
    public int CREW_ID;
    public CREW_MOVEMENT crew_move;
    public CREW_CLOTH crew_cloth;
    public EQUIPMENT_TRAIT[] equipment_trait;
    public EVENT_TRAIT[] event_trait;
    public CREW_TRAIT[] crew_trait;
    public string CREW_NAME;
    public float HPMAX, HP, hungerMAX, hunger, moraleMAX, morale, temperature;
    
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
