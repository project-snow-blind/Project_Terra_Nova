using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
//using System.IO;
public class Crew_Sc : MonoBehaviour
{

    [SerializeField]
    private int CREW_ID;
    [SerializeField]
    protected CREW_MOVEMENT CREW_MOVE;
    [SerializeField]
    protected CREW_CLOTH CREW_CLOTH;
    //[SerializeField]
    //private List<EQUIPMENT_TRAIT> EQUIP_TRAIT;
    //[SerializeField]
    //private List<EVENT_TRAIT> EVENT_TRAIT;
    //[SerializeField]
    //private List<CREW_TRAIT> CREW_TRAIT;
    [SerializeField]
    //private List<Trait> traits = new List<Trait>();
    private List<Trait_data> traits = new List<Trait_data>();
    [SerializeField]
    private string CREW_NAME;
    [SerializeField]
    protected float HPMAX, HP, hungerMAX, hunger, moraleMAX, morale, speed;
    [SerializeField]
    protected int temperature, Portrait;
    //private bool trait_resort = false;
    //[SerializeField]
    //private List<Trait_Function> trait_Functions = new List<Trait_Function>();
    //[SerializeField]
    //int CrewPortraitSet;
    public ExpeditionCrew export;
    public Crew_Sc export_new;

    private void Start()
    {
        Core.core.On_Round_Ends_2 += RoundChecker;
        Core.core.On_Day_Ends += DailyChecker;
    }

    public void Testing_Normalize_Crew(ExpeditionCrew listcrew)
    {
        StopAllCoroutines();    
        CREW_ID = listcrew.CREW_ID;
        CREW_MOVE = listcrew.CREW_MOVE;
        CREW_CLOTH = listcrew.CREW_CLOTH;
        //EQUIP_TRAIT = listcrew.EQUIP_TRAIT;
        //EVENT_TRAIT = listcrew.EVENT_TRAIT;
        //CREW_TRAIT = listcrew.CREW_TRAIT;
        traits = listcrew.Traits;
        CREW_NAME = listcrew.CREW_NAME;
        HPMAX = listcrew.HPMAX;
        hungerMAX = listcrew.hungerMAX;
        moraleMAX = listcrew.moraleMAX;
        temperature = listcrew.temperature;
        Portrait = listcrew.portrait;
        speed = listcrew.speed;
        
        Trait_Reorg();
        HP = HPMAX;
        hunger = hungerMAX;
        morale = moraleMAX;
    }
    public void Replace_crew(ExpeditionCrew listcrew)
    {
        StopAllCoroutines();
        CREW_ID = listcrew.CREW_ID;
        CREW_MOVE = listcrew.CREW_MOVE;
        CREW_CLOTH = listcrew.CREW_CLOTH;
        //EQUIP_TRAIT = listcrew.EQUIP_TRAIT;
        //EVENT_TRAIT = listcrew.EVENT_TRAIT;
        //CREW_TRAIT = listcrew.CREW_TRAIT;
        traits = listcrew.Traits;
        CREW_NAME = listcrew.CREW_NAME;
        HPMAX = 100;
        hungerMAX = 100;
        moraleMAX = 100;
        temperature = listcrew.temperature;
        Portrait = listcrew.portrait;
        speed = listcrew.speed;

        Trait_Reorg();
        HP = listcrew.HP;
        hunger = listcrew.hunger;
        morale = listcrew.hunger;
    }
    public void crew_remove()
    {
        StopAllCoroutines();
        CREW_ID = 0;
        Core.core.Crew_Statue_Changed();
    }


    public void Export_Crew_Const()
    {
        ExpeditionCrew Output = new ExpeditionCrew(CREW_ID, CREW_MOVE, CREW_CLOTH, CREW_NAME, HP, hunger, morale, traits, temperature, Portrait,speed, HPMAX, moraleMAX, hungerMAX);
        //ExpeditionCrew Output = new ExpeditionCrew(CREW_ID, CREW_MOVE, CREW_CLOTH, CREW_NAME, HP, hunger, morale, traits, temperature, Portrait);
        export = Output;

        //Crew_Sc output_new = this.GetComponent<Crew_Sc>();

        //export_new = output_new;
    }
    IEnumerator effecttest(Modifier modifier, Crew_Sc obj, Trait_data trait)
    {
        var fi = obj.GetType().GetTypeInfo().GetDeclaredField(modifier.statname);
        float v = (float)fi.GetValue(obj);
        float buffed;
        if (modifier.mult)
        {
            float var = 1f + modifier.value;
            buffed = v * var;
        }
        else
        {
            buffed = v + modifier.value;
        }
        fi.SetValue(obj, buffed);

        while(obj.traits.Contains(trait))
        {
            yield return null;
        }
        if (modifier.mult)
        {
            float var = 1f - modifier.value;
            buffed = v / var;
        }
        else
        {
            buffed = v - modifier.value;
        }

        fi.SetValue(obj, buffed);
    }
    public void Trait_Reorg()
    {
        //Trait_data trait;
        foreach(Trait_data trait_ in traits)
        {
            //Debug.Log(trait_.name);
            if (trait_.modifiers.Any())
            {
                foreach (Modifier modifier in trait_.modifiers)
                {
                    StartCoroutine(effecttest(modifier, this.GetComponent<Crew_Sc>(), trait_));
                }
            }
        }
        //foreach(Trait_data trait in traits)
        //{
        //    if(trait.modifiers.Any())
        //    {
        //        Debug.Log("test");
        //        //foreach(Modifier mod in trait.modifiers)
        //        //{
        //        //    Debug.Log(mod.statname);
        //        //    //var fi = GetType().GetTypeInfo().GetDeclaredEvent(mod.statname);

        //        //    //Debug.Log(fi);
        //        //}
                
                
        //    }
        //}
        //foreach(Trait Trait in traits)
        //{
        //    //Debug.Log(Trait.Name + "이름");
        //    //Trait_Add(Trait);
        //    //GameObject.Instantiate(Trait);
        //}
        
        //foreach(Trait_data trait in traits)
        //{
        //    foreach(Modifier mod in trait.modifiers)
        //    {
        //        Debug.Log(mod.statname + mod.value);
                
        //    }
        //    //if(trait.modifiers != null)
        //    //{
        //    //    StartCoroutine(Trait_effect(trait));
        //    //}

        //}
    }
    //public void modifiermakeshift(Modifier mod)
    //{
    //    switch(mod.statname)
    //    {
    //        case "HPMAX":

    //            break;

    //        case ""
    //    }
    //}
    public void CrewEffect_Function(Stat_st stat)
    {
        //Debug.Log(stat.stat);
        var fi = GetType().GetTypeInfo().GetDeclaredField(stat.stat);

        float v = (float)fi.GetValue(this);

        float changed = v + stat.value;

        fi.SetValue(this, changed);


        if(HP > HPMAX)
        {
            HP = HPMAX;
        }
        else if(hunger > hungerMAX)
        {
            hunger = hungerMAX;
        }
        else if(morale > moraleMAX)
        {
            morale = moraleMAX;
        }
    }

    public void CrewTrait_Effect_Function(Trait_st trait)
    {
        Trait_data trait_data = Trait_Library.instance.GetTrait(trait.name);
        if(trait.b == true)
        {
            if(traits.Contains(trait_data) == false)
            {
                traits.Add(trait_data);
                foreach (Modifier modifier in trait_data.modifiers)
                {
                    StartCoroutine(effecttest(modifier, this.GetComponent<Crew_Sc>(), trait_data));
                }
                
            }
        }
        else
        {
            if(traits.Contains(trait_data) == true)
            {
                traits.Remove(trait_data);
            }
        }
    }
    protected IEnumerator Trait_effect(Trait_data trait)
    {
        foreach(Modifier modifier in trait.modifiers)
        {            var fi = GetType().GetTypeInfo().GetField(modifier.statname);
            Debug.Log(fi);
        }
        while (traits.Contains(trait) == true)
        {

            yield return null;

        }
    }

    private void RoundChecker(object sender, EventArgs eventArgs)
    {
        Stat_st st = new Stat_st();
        foreach(Trait_data trait in traits)
        {
            switch(trait.name)
            {
                case "카메라":
                    st.stat = "morale";
                    st.value = 5;
                    CrewEffect_Function(st);

                    break;

                case "극지의 기사":
                    morale = moraleMAX;

                    break;

                case "지나친 베테랑":
                    st.stat = "morale";
                    st.value = -15;
                    Core.core.Crew_effect_to_all(st);

                    break;
                case "빠른 회복":
                    st.stat = "HP";
                    st.value = 3;
                    CrewEffect_Function(st);

                    break;
                case "충성스러움":
                    st.stat = "morale";
                    st.value = 5;
                    CrewEffect_Function(st);

                    break;
                case "심한 동상":
                    st.stat = "morale";
                    st.value = -5;
                    CrewEffect_Function(st);
                    st.stat = "HP";
                    CrewEffect_Function(st);
                    break;
                case "만족스러움":
                    st.stat = "morale";
                    st.value = 5;
                    CrewEffect_Function(st);

                    break;
                case "저체온증":
                    st.stat = "HP";
                    st.value = -1;
                    CrewEffect_Function(st);
                    st.stat = "morale";
                    st.value = -10;
                    CrewEffect_Function(st);
                    break;
                case "영양실조":
                    st.stat = "HP";
                    st.value = -10;
                    CrewEffect_Function(st);
                    st.stat = "hunger";
                    CrewEffect_Function(st);
                    break;

                case "조랑말":
                    Expedition.instance.Itemstorage_output("말 사료", 2);
                    
                    
                    break;
                case "썰매 개":
                    Expedition.instance.Itemstorage_output("사료", 1);


                    break;
                default:
                    break;
            }
        }
    }
    private void DailyChecker(object sender, EventArgs eventArgs)
    {
        Stat_st st = new Stat_st();
        foreach (Trait_data trait in traits)
        {
            switch (trait.name)
            {
                case "탈진":
                    st.stat = "morale";
                    st.value = 5;
                    CrewEffect_Function(st);

                    break;

                case "동상":
                    st.stat = "morale";
                    st.value = 5;
                    CrewEffect_Function(st);
                    st.stat = "HP";
                    CrewEffect_Function(st);

                    break;

                case "부상":
                    st.stat = "HP";
                    st.value = -5;
                    Core.core.Crew_effect_to_all(st);

                    break;
                case "불만":
                    st.stat = "morale";
                    st.value = -5;
                    CrewEffect_Function(st);

                    break;
                case "심한 땀":
                    st.stat = "morale";
                    st.value = -10;
                    CrewEffect_Function(st);

                    break;
                default:
                    break;
            }
        }
    }
    private void WeeklyChecker(object sender, EventArgs eventArgs)
    {
        Stat_st st = new Stat_st();
        foreach (Trait_data trait in traits)
        {
            switch (trait.name)
            {
                case "친절":
                    st.stat = "morale";
                    st.value = 5;
                    Core.core.Crew_effect_to_all(st);

                    break;
                default:
                    break;
            }
        }
    }
    public void move_style_change(CREW_MOVEMENT moveset)
    {
        CREW_MOVE = moveset;
    }

    public void cloth_change(CREW_CLOTH clothset)
    {
        CREW_CLOTH = clothset;
    }
}