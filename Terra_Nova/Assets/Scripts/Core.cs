using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using System.IO;
public class Core : MonoBehaviour
{
    public event EventHandler OnCrewChanged, OnActionChanged, On_Round_Ends, On_Day_Ends, On_Weak_Ends;


    public static Core core;
    public AudioSc aud;
    public AudioSc BGM;
    private int IDpool = 1;
    
    //[SerializeField]
    public Crew_Sc[] crews;
    //[SerializeField]
    public Sprite[] Crew_Portrait_Set;
    //[SerializeField]
    public Sprite[] Unique_Crew_Portrait_Set;
    [SerializeField]
    private int crewcount = 0;

    [SerializeField]
    private List<ExpeditionCrew> Unique_Crew_Library = new List<ExpeditionCrew>();

    
    //private List<ExpeditionCrew> Core_Crew = new List<ExpeditionCrew>();
    private void Awake()
    {
        core = this;
        
        //Crew_Add(GetCrewinfo("Lawrence Oates"));
    }

    private void difficulty_generate()
    {
        int i = Option_Setting_script.settings.difficult_output();

        switch(i)
        {
            case 0:
                for (i = 0; i < 4; i++)
                    Crew_Add(Create_Crew());
                break;

            case 1:
                for (i = 0; i < 4; i++)
                    Crew_Add(Create_Crew());
                break;

            case 2:
                for (i = 0; i < 4; i++)
                    Crew_Add(Create_Crew());
                break;

            case 3:
                Crew_Add(GetCrewinfo("Robert F. Scott"));
                Crew_Add(GetCrewinfo("Lawrence Oates"));
                Crew_Add(GetCrewinfo("Henry R. Bowers"));
                Crew_Add(GetCrewinfo("Edgar Evans"));
                break;
        }
    }

    private void Start()
    {

        //ExpeditionCrew dummy = new ExpeditionCrew(0, CREW_MOVEMENT.WALK, CREW_CLOTH.WOOL, "Dummy", 0, 0, 0, 0f);
        //Crew_Add(dummy);

        Unique_crew_Generator();
        difficulty_generate();





        GameObject audioobj = GameObject.Find("Audio Source");
        aud = audioobj.GetComponent<AudioSc>();
        GameObject audioobj2 = GameObject.Find("BGM Source");
        BGM = audioobj2.GetComponent<AudioSc>();
        DontDestroyOnLoad(transform.gameObject);

        StartCoroutine(startsetting());
    }

    private IEnumerator startsetting()
    {
        yield return new WaitForSeconds(0.01f);
        Crew_Statue_Changed();
        Expedition.instance.Itemstorage_input("통조림", 200);
        Expedition.instance.Itemstorage_input("사료", 75);
        Expedition.instance.Itemstorage_input("비스킷", 35);
        Expedition.instance.Itemstorage_input("홍차", 50);
        Expedition.instance.Itemstorage_input("연료", 1000);
        Expedition.instance.Itemstorage_input("조랑말", 10);
        Expedition.instance.Itemstorage_input("썰매 개", 5);
        Expedition.instance.Itemstorage_input("양모 옷", 4);
        Expedition.instance.Itemstorage_input("카메라", 2);
        Expedition.instance.Itemstorage_input("국기", 1);
        Expedition.instance.Itemstorage_input("말 사료", 125);
        Expedition.instance.Itemstorage_input("초콜릿", 8);

    }


    public void Crew_Add(ExpeditionCrew newcrew)
    {
        //Core_Crew.Add(newcrew);
        if(crewcount <= 4)
        {
            crews[crewcount].Testing_Normalize_Crew(newcrew);
            crewcount++;
        }
        //crewcount++;
    }
    public void Crew_update(int i, ExpeditionCrew newcrew)
    {
        //ExpeditionCrew[] arr = Core_Crew.OfType<ExpeditionCrew>().ToArray();
        crews[i].Testing_Normalize_Crew(newcrew);
        //List<ExpeditionCrew> newone = arr.OfType<ExpeditionCrew>().ToList();
        //Core_Crew = newone;
    }

    public void Crew_update_export()
    {
        foreach(Crew_Sc crew in crews)
        {
            crew.Export_Crew_Const();
        }
    }
    public ExpeditionCrew Crew_Read(int i)
    {
        Crew_Sc dummy = crews[i];
        dummy.Export_Crew_Const();
        
        ExpeditionCrew Output = dummy.export;

        return Output;
    }

    public Crew_Sc Crew_Read_New(int i)
    {
        Crew_Sc dummy = crews[i];
        return dummy;
    }
    public List<ExpeditionCrew> Crew_Read_All()
    {
        List<ExpeditionCrew> Output = new List<ExpeditionCrew>();
        int length = crews.Length;
        for(int i = 0; i < length; i++)
        {
            Crew_Sc dummy = crews[i];
            dummy.Export_Crew_Const();
            ExpeditionCrew newone = dummy.export;
            Output.Add(newone);
        }
        //Output = Core_Crew;

        return Output;
    }

    public List<Crew_Sc> Crew_Read_All_New()
    {
        List<Crew_Sc> Output = new List<Crew_Sc>();
        foreach(Crew_Sc crew in crews)
        {
            Output.Add(crew);
        }

        return Output;
    }
    public ExpeditionCrew Create_Crew()
    {
        IDpool++;
        CREW_CLOTH cloth = CREW_CLOTH.WOOL2;
        switch(Random.Range(0,5))
        {
            case 0:
                cloth = CREW_CLOTH.WOOL1;
                break;
            case 1:
                cloth = CREW_CLOTH.WOOL2;
                break;
            case 2:
                cloth = CREW_CLOTH.WOOL3;
                break;
            case 3:
                cloth = CREW_CLOTH.FUR1;
                break;
            case 4:
                cloth = CREW_CLOTH.FUR2;
                break;
            case 5:
                cloth = CREW_CLOTH.FUR3;
                break;
        }

        List<Trait_data> trait_pool = TraitGenerator();
        ///ExpeditionCrew newcrew = new ExpeditionCrew(IDpool, CREW_MOVEMENT.WALK, cloth, NameGen(), 100, 100, 100, 5, eq_list, default, crew_trait) ;
        ExpeditionCrew newcrew = new ExpeditionCrew(IDpool, CREW_MOVEMENT.WALK, cloth, NameGen(), 100, 100, 100, trait_pool, 5, 0) ;

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



    public List<Trait_data> TraitGenerator()
    {
        List<Trait_data> all_pool = Trait_Library.instance.Traits;
        List<Trait_data> temppool = new List<Trait_data>();
        List<Trait_data> newpool = new List<Trait_data>();
        List<Trait_data> equip = new List<Trait_data>();
        List<Trait_data> crew = new List<Trait_data>();


        foreach(Trait_data trait in all_pool)
        {
            if (!trait.unique)
                temppool.Add(trait);

        }

        foreach(Trait_data trait in temppool)
        {
            switch (trait.categrory)
            {
                case trait_category.CR:
                    crew.Add(trait);
                    break;

                case trait_category.EQ:
                    equip.Add(trait);

                    break;
                default:

                    break;
            }

        }

        for(int i = 0; i < 4; i++)
        {
            int v = Random.Range(0, crew.Count);
            newpool.Add(crew[v]);
            crew.RemoveAt(v);
        }
        
        for(int i = 0; i < 2; i++)
        {
            int v = Random.Range(0, equip.Count);
            newpool.Add(equip[v]);
            equip.RemoveAt(v);
        }
        return newpool;
    }

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

    public void Crew_Statue_Changed()
    {
        OnCrewChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Action_UI_Changed()
    {
        OnActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Action_do()
    {
        On_Round_Ends?.Invoke(this, EventArgs.Empty);

    }

    public void Next_Day()
    {
        On_Day_Ends?.Invoke(this, EventArgs.Empty);
    }

    public void Next_Week()
    {
        On_Weak_Ends?.Invoke(this, EventArgs.Empty);
    }
    
    public void Unique_crew_Generator()
    {
        LoadCrewData();
        foreach(Crew_data data in crew.crewdata)
        {
            List<Trait_data> traitlist = new List<Trait_data>();

            foreach(string str in data.traits)
            {
                traitlist.Add(Trait_Library.instance.GetTrait(str));
                
            }


            ExpeditionCrew crew = new ExpeditionCrew(data.CREW_ID, CREW_MOVEMENT.WALK, data.CREW_CLOTH, data.CREW_NAME, 100, 100, 100, traitlist, data.temperature, data.Portrait);
            Unique_Crew_Library.Add(crew);
        }
    }

    public ExpeditionCrew GetCrewinfo(string name)
    {
        return Unique_Crew_Library.Where(x => x.CREW_NAME == name).FirstOrDefault();
    }

    public Crew crew;

    [ContextMenu("To Json Data")]
    void SaveCrewData()
    {
        string JsonData = JsonUtility.ToJson(crew, true);
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Crews", "UniqueCrewdata.json");
        File.WriteAllText(path, JsonData);

        path = Path.Combine(Application.dataPath + "/Resources/JsonDat/backup", "UniqueCrewdata.json");
        File.WriteAllText(path, JsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadCrewData()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Crews", "UniqueCrewdata.json");
        string jsondata = File.ReadAllText(path);

        crew = JsonUtility.FromJson<Crew>(jsondata);
    }
}



[Serializable]
public class Crew
{
    public Crew_data[] crewdata;
}
[Serializable]
public class Crew_data
{
    public int CREW_ID;
    public CREW_CLOTH CREW_CLOTH;
    public List<string> traits;
    public string CREW_NAME;
    public int temperature, Portrait;
}