using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Core : MonoBehaviour
{
    public static Core core;
    public AudioSc aud;
    public AudioSc BGM;
    private int IDpool = 0;
    private List<ExpeditionCrew> Core_Crew = new List<ExpeditionCrew>();
    private void Awake()
    {
        core = this;
        ExpeditionCrew Oates = new ExpeditionCrew(0, CREW_MOVEMENT.WALK, CREW_CLOTH.WOOL, "L. Oates", 100, 100, 100, 36f);
        Crew_Add(Oates);
    }

    private void Start()
    {

        //ExpeditionCrew dummy = new ExpeditionCrew(0, CREW_MOVEMENT.WALK, CREW_CLOTH.WOOL, "Dummy", 0, 0, 0, 0f);
        //Crew_Add(dummy);
        
        GameObject audioobj = GameObject.Find("Audio Source");
        aud = audioobj.GetComponent<AudioSc>();
        GameObject audioobj2 = GameObject.Find("BGM Source");
        BGM = audioobj2.GetComponent<AudioSc>();
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Crew_Add(ExpeditionCrew newcrew)
    {
        Core_Crew.Add(newcrew);
    }
    public void Crew_update(int i, ExpeditionCrew newcrew)
    {
        ExpeditionCrew[] arr = Core_Crew.OfType<ExpeditionCrew>().ToArray();
        arr[i] = newcrew;
        List<ExpeditionCrew> newone = arr.OfType<ExpeditionCrew>().ToList();
        Core_Crew = newone;
    }
    public ExpeditionCrew Crew_Read(int i)
    {
        ExpeditionCrew Output = Core_Crew[i];

        return Output;
    }
    public List<ExpeditionCrew> Crew_Read_All()
    {
        List<ExpeditionCrew> Output = new List<ExpeditionCrew>();
        Output = Core_Crew;

        return Output;
    }

    public ExpeditionCrew Create_Crew()
    {
        IDpool++;
        CREW_CLOTH cloth;
        if (Random.Range(0, 1) == 0)
        {
            cloth = CREW_CLOTH.WOOL;
        }
        else
            cloth = CREW_CLOTH.FUR;

        List<EQUIPMENT_TRAIT> eq_list = Eq_TraitGen();
        List<CREW_TRAIT> crew_trait = Crew_TraitGen();
        ExpeditionCrew newcrew = new ExpeditionCrew(IDpool, CREW_MOVEMENT.WALK, cloth, NameGen(), 100, 100, 100, 36f, eq_list, default, crew_trait) ;

        return newcrew;
    }

    public string NameGen()
    {
        string name = "";
        name = namepool[Random.Range(0, namepool.Length)] + " " + namepool2[Random.Range(0,namepool2.Length)];

        return name;
    }

    public string[] namepool =
    {
        "Robert",
        "Edward",
        "Henry",
        "Lawrence",
        "Victor",
        "George",
        "Frank",
        "Raymond",
        "Charles",
        "Herbert",
        "Cecil",
        "Bernard",
        "Apsley",
        "Tryggve"
    };

    public string[] namepool2 =
    {
        "Evans",
        "Bowers",
        "Oates",
        "Atkinson",
        "Cambell",
        "Levick",
        "Wilson",
        "Simpson",
        "Taylor"
    };

    public List<EQUIPMENT_TRAIT> Eq_TraitGen(int count = default, int max = 2, List<EQUIPMENT_TRAIT> traitpool = default, List<EQUIPMENT_TRAIT> fixed_trait = default)
    {
        int trait_count = 0;
        List<EQUIPMENT_TRAIT> OutPut = new List<EQUIPMENT_TRAIT>();
        List<EQUIPMENT_TRAIT> trait_list = new List<EQUIPMENT_TRAIT>();
        if(traitpool == default)
        {
            trait_list = Enum.GetValues(typeof(EQUIPMENT_TRAIT)).Cast<EQUIPMENT_TRAIT>().ToList();
        }
        else
        {
            trait_list = traitpool;
        }
        if(count > 0)
        {
            trait_count = Random.Range(count, max);
        }
        else
        {
            trait_count = Random.Range(0, max);
        }
        for(int i = 0; i < trait_count; i++)
        {
            ShuffleList(trait_list);
            OutPut.Add(trait_list[0]);
            trait_list.Remove(0);
        }
        if(fixed_trait != default)
        {
            for (int i = 0; i < fixed_trait.Count; i++)
            {
                OutPut.Add(fixed_trait[i]);
            }
        }
        return OutPut;
    }

    public List<CREW_TRAIT> Crew_TraitGen(int count = default, int max = 3, List<CREW_TRAIT> traitpool = default, List<CREW_TRAIT> fixed_trait = default)
    {
        int trait_count = 0;
        List<CREW_TRAIT> OutPut = new List<CREW_TRAIT>();
        List<CREW_TRAIT> Traitlist = new List<CREW_TRAIT>();
        if(traitpool == default)
        {
            Traitlist = normal_crew_trait_list_gen();
        }
        else
        {
            Traitlist = traitpool;
        }

        if (count > 0)
        {
            trait_count = Random.Range(count, max);
        }
        else
        {
            trait_count = Random.Range(0, max);
        }
        for (int i = 0; i < trait_count; i++)
        {
            ShuffleList(Traitlist);
            OutPut.Add(Traitlist[0]);
            Traitlist.Remove(0);
        }
        if (fixed_trait != default)
        {
            for (int i = 0; i < fixed_trait.Count; i++)
            {
                OutPut.Add(fixed_trait[i]);
            }
        }
        return OutPut;

    }
    //public List<EVENT_TRAIT> Ev_TraitGen(int count = default)
    //{
    //    List<EventTra>
    //}
    private List<CREW_TRAIT>normal_crew_trait_list_gen()
    {
        List<CREW_TRAIT> normal_trait_list = new List<CREW_TRAIT>();
        normal_trait_list.Add(CREW_TRAIT.SKI_ENABLE);
        normal_trait_list.Add(CREW_TRAIT.SKI_CHAMP);

        return normal_trait_list;
    }
        


    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }

    public bool IDCheck(ExpeditionCrew arr, int id)
    {
        bool output = false;
        if (arr.CREW_ID.Equals(id))
        {
            output = true;
        }
        return output;
    }
    public bool EquipCheck(ExpeditionCrew arr, EQUIPMENT_TRAIT trait)
    {
        bool output = false;
        foreach (EQUIPMENT_TRAIT traits in arr.EQUIP_TRAIT)
        {
            if (traits.Equals(trait))
            {
                output = true;
            }
        }
        return output;

    }

    public bool Ev_traitCheck(ExpeditionCrew arr, EVENT_TRAIT trait)
    {
        bool output = false;
        foreach (EVENT_TRAIT traits in arr.EVENT_TRAIT)
        {
            if (traits.Equals(trait))
            {
                output = true;
            }
        }
        return output;

    }
    public bool CrewTraitCheck(ExpeditionCrew arr, CREW_TRAIT trait)
    {
        bool output = false;
        foreach (CREW_TRAIT traits in arr.CREW_TRAIT)
        {
            if (traits.Equals(trait))
            {
                output = true;
            }
        }
        return output;

    }
}
