using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew_Sc : MonoBehaviour
{

    [SerializeField]
    private int CREW_ID;
    [SerializeField]
    private CREW_MOVEMENT CREW_MOVE;
    [SerializeField]
    private CREW_CLOTH CREW_CLOTH;
    [SerializeField]
    private List<EQUIPMENT_TRAIT> EQUIP_TRAIT;
    [SerializeField]
    private List<EVENT_TRAIT> EVENT_TRAIT;
    [SerializeField]
    private List<CREW_TRAIT> CREW_TRAIT;
    [SerializeField]
    private string CREW_NAME;
    [SerializeField]
    private float HPMAX, HP, hungerMAX, hunger, moraleMAX, morale, temperature;

    public ExpeditionCrew export;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void Testing_Normalize_Crew(ExpeditionCrew listcrew)
    {
        CREW_ID = listcrew.CREW_ID;
        CREW_MOVE = listcrew.CREW_MOVE;
        CREW_CLOTH = listcrew.CREW_CLOTH;
        EQUIP_TRAIT = listcrew.EQUIP_TRAIT;
        EVENT_TRAIT = listcrew.EVENT_TRAIT;
        CREW_TRAIT = listcrew.CREW_TRAIT;
        CREW_NAME = listcrew.CREW_NAME;
        HPMAX = listcrew.HPMAX;
        HP = listcrew.HP;
        hungerMAX = listcrew.hungerMAX;
        hunger = listcrew.hunger;
        morale = listcrew.morale;
        moraleMAX = listcrew.moraleMAX;
        temperature = listcrew.temperature;
    }
    
    public void Export_Crew_Const()
    {
        ExpeditionCrew Output = new ExpeditionCrew(CREW_ID, CREW_MOVE, CREW_CLOTH, CREW_NAME, HP, hunger, morale, temperature, EQUIP_TRAIT, EVENT_TRAIT, CREW_TRAIT);
        export = Output;
    }
    
}
