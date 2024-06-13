using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Expedition : MonoBehaviour
{
    public TextMeshProUGUI day, currentday, time,terrain_text,prog_persentage;
    public Image terrain,sun,progressbar;
    public Sprite[] sprites;
    public Camera cameras;
    private int currentdays = 1, days = 1, month = 11;
    private bool currenttime = true;
    private float progress = 0.0f;
    [SerializeField]
    CanvasGroup Curtain;
    [SerializeField]
    GameObject BottomUI;
    // Start is called before the first frame update
    void Start()
    {
        day.text = month + "/" + days;
        currentday.text = currentdays + "days"; 
        time.text = "DayTime";
        terrain_text.text = "Bearedmore Glacier";
        ProgressbarUpdate();

        
        Core.core.BGM.AudioPlay(0);
    }

    private void DayUpdate()
    {
        currentdays++;
        //days++;
        
        string dayornight = "";
        if(currenttime)
        {
            dayornight = "DayTime";
            sun.sprite = sprites[0];
            cameras.backgroundColor = new Color32(255, 255, 255, 0);
            days++;
        }
        else
        {
            dayornight = "Night";
            sun.sprite = sprites[1];
            cameras.backgroundColor = new Color32(100, 100, 100, 0);
        }
        day.text = month + "/" + days;
        currentday.text = currentdays + "days";
        time.text = dayornight;
    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private float SpdCalc(ExpeditionCrew crew)
    {
        float finalspd;
        float movestylespd = 0;


        CREW_MOVEMENT movestyle = crew.CREW_MOVE;
        switch(movestyle)
        {
            case CREW_MOVEMENT.SKI:
                movestylespd = 0.4f;
                break;

            case CREW_MOVEMENT.SKI_UPGRADE:
                movestylespd = 0.55f;
                break;

            case CREW_MOVEMENT.WALK:
                movestylespd = 0.25f;
                break;

            case CREW_MOVEMENT.SNOWMOBILE:
                movestylespd = 0.75f;
                break;

            case CREW_MOVEMENT.DOG_SLED:
                movestylespd = 0.55f;
                break;

            case CREW_MOVEMENT.BEAR_SLED:
                movestylespd = 0.75f;
                break;

            case CREW_MOVEMENT.PONY:
                movestylespd = 0.5f;
                break;
        }
        finalspd = movestylespd;

        //여기부터 최종값 꼬는 변수들 덕지덕지 장착
        return finalspd;
    }
    private void ProgressFuction()
    {
        List<ExpeditionCrew> crew_list = new List<ExpeditionCrew>();
        crew_list = Core.core.Crew_Read_All();
        List<float> spd = new List<float>();
        foreach(ExpeditionCrew crew in crew_list)
        {
            spd.Add(SpdCalc(crew));
        }
        spd.Sort();
        float progresslength = spd[0];
        progress += progresslength;

        ProgressbarUpdate();
        List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
        foreach(ExpeditionCrew crew in crew_list)
        {
            ExpeditionCrew newone = crew;
            newone.HP -= 15f;
            newone.hunger -= 20f;
            new_crew_list.Add(newone);
            //advent_damage_fuction(crew);
            //new_crew_list.Add(crew);
        }
        int crew_count = new_crew_list.Count;
        for(int i = 0; i < crew_count; i++)
        {
            Core.core.Crew_update(i, new_crew_list[i]);
        }
        
    }

    //private void advent_damage_fuction(ExpeditionCrew crew)
    //{
    //    crew.HP -= 15f;
    //}
    private void Expedition_round_ends()
    {
        List<ExpeditionCrew> old_crew_list = new List<ExpeditionCrew>();
        List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
        old_crew_list = Core.core.Crew_Read_All();
        foreach(ExpeditionCrew crew in old_crew_list)
        {
            ExpeditionCrew newone = ExpeditionRoundEndFuction(crew);
            new_crew_list.Add(newone);
        }
        int crew_count = new_crew_list.Count;

        for(int i = 0; i < crew_count; i++)
        {
            Core.core.Crew_update(i, new_crew_list[i]);
        }
        Core.core.Crew_Statue_Changed();
        currenttime = !currenttime;
        DayUpdate();

        //#앵커
        EventPopUp_Test(Random.Range(1, 3));
    }
    private ExpeditionCrew ExpeditionRoundEndFuction(ExpeditionCrew crew)
    {
        ExpeditionCrew new_crew = crew;
        new_crew.hunger -= 5f;
        if(new_crew.hunger / new_crew.hungerMAX > 0.8f)
        {
            new_crew.HP += 10;
            if (new_crew.HP > new_crew.HPMAX)
                new_crew.HP = new_crew.HPMAX;
        }

        if(new_crew.hunger / new_crew.hungerMAX < 0.5f && new_crew.hunger / new_crew.hungerMAX > 0.26f)
        {
            new_crew.morale -= 10f;
        }
        else if (new_crew.hunger / new_crew.hungerMAX < 0.25f && new_crew.hunger / new_crew.hungerMAX > 0.16f)
        {
            new_crew.morale -= 25f;
        }
        else if (new_crew.hunger / new_crew.hungerMAX < 0.15f)
        {
            new_crew.morale -= 50f;
        }

        return new_crew;
    }
    

    //public void ExpeditionEvent()
    //{
        
    //}



    [SerializeField]
    private TextMeshProUGUI names2, hp2, hunger2, trust2, temperature, trait1, trait2, trait3,trait4;
    [SerializeField]
    GameObject CrewPopUp;
    public void CrewPopUpActive(int crewnumber)
    {
        CrewPopUp.SetActive(true);
        ExpeditionCrew Selected_Crew = Core.core.Crew_Read(crewnumber);

        names2.text = Selected_Crew.CREW_NAME;
        hp2.text = "HP : " + Selected_Crew.HP.ToString() + "/" + Selected_Crew.HPMAX.ToString();
        hunger2.text = "Hunger : " + Selected_Crew.hunger.ToString() + "/" + Selected_Crew.hungerMAX.ToString();
        trust2.text = "Trust : " + Selected_Crew.morale.ToString() + "/" + Selected_Crew.moraleMAX.ToString();
        temperature.text = "Temperature : " + Selected_Crew.temperature.ToString() + "°C";

        trait1.text = CLOTH_string_Localisation(Selected_Crew.CREW_CLOTH) + "\n" + MOVE_string_Localisation(Selected_Crew.CREW_MOVE);
        string traittext = "";

        if(Selected_Crew.EQUIP_TRAIT != null)
        {
            foreach (EQUIPMENT_TRAIT trait in Selected_Crew.EQUIP_TRAIT)
            {
                traittext += EQT_string_Localisation(trait) + " ";
            }
            trait2.text = traittext;
            traittext = "";
        }
        else
        {
            trait2.text = "";
        }

        if (Selected_Crew.CREW_TRAIT != null)
        {
            foreach (CREW_TRAIT trait in Selected_Crew.CREW_TRAIT)
            {
                traittext += CRT_string_Localisation(trait) + " ";
            }
            trait3.text = traittext;
            traittext = "";
        }
        else
        {
            trait3.text = "";
        }

        if (Selected_Crew.EVENT_TRAIT != null)
        {
            foreach (EVENT_TRAIT trait in Selected_Crew.EVENT_TRAIT)
            {
                traittext += EVT_string_Localisation(trait) + " ";
            }
            trait4.text = traittext;
        }
        else
        {
            trait4.text = "";
        }
        Core.core.aud.AudioPlay(0);
    }
    public string CLOTH_string_Localisation(CREW_CLOTH CLT)
    {
        string rt = "";
        switch (CLT)
        {
            case CREW_CLOTH.FUR:
                rt += "모피";
                break;

            case CREW_CLOTH.WOOL:
                rt += "양모";
                break;
        }
        return rt;
    }

    public string MOVE_string_Localisation(CREW_MOVEMENT MVT)
    {
        string rt = "";
        switch (MVT)
        {
            case CREW_MOVEMENT.WALK:
                rt += "걸어다님";
                break;

            case CREW_MOVEMENT.SKI:
                rt += "스키";
                break;
            case CREW_MOVEMENT.SKI_UPGRADE:
                rt += "스키(증강됨)";
                break;
            case CREW_MOVEMENT.DOG_SLED:
                rt += "개 썰매";
                break;
            case CREW_MOVEMENT.PONY:
                rt += "말";
                break;
            case CREW_MOVEMENT.SNOWMOBILE:
                rt += "스노모빌";
                break;
            case CREW_MOVEMENT.BEAR_SLED:
                rt += "곰 썰매";
                break;
        }
        return rt;
    }
    public string EQT_string_Localisation(EQUIPMENT_TRAIT EQT)
    {
        string rt = "";
            switch(EQT)
            {
                case EQUIPMENT_TRAIT.SUNGLASS:
                rt += "선글라스";
                    break;

                case EQUIPMENT_TRAIT.EQUIP_SKI:
                rt += "스키";
                break;

                case EQUIPMENT_TRAIT.EQUIP_SKI_UPGRADE:
                rt += "스키(증강됨)";
                    break;

                case EQUIPMENT_TRAIT.CONTAINED_SNOWMOBILE:
                rt += "고장난 스노모빌";
                break;

                case EQUIPMENT_TRAIT.WOOL_CLOTH:
                rt += "여분의 양모 옷";
                break;

            case EQUIPMENT_TRAIT.FUR_CLOTH:
                rt += "여분의 모피 옷";
                break;
        }
        return rt;
    }

    public string EVT_string_Localisation(EVENT_TRAIT EVT)
    {
        string rt = "";
        switch (EVT)
        {
            case EVENT_TRAIT.FROSTBITE:
                rt += "동상";
                break;

            case EVENT_TRAIT.EXHAUSTED:
                rt += "탈진";
                break;

            case EVENT_TRAIT.CRITICAL_FROST_BITE:
                rt += "심각한 동상";
                break;

            case EVENT_TRAIT.EVANS_CRITICAL_ACCIDENT:
                rt += "치명적 뇌손상";
                break;
        }
        return rt;
    }

    public string CRT_string_Localisation(CREW_TRAIT CRT)
    {
        string rt = "";
        switch (CRT)
        {
            case CREW_TRAIT.SKI_ENABLE:
                rt += "스키 사용자";
                break;

            case CREW_TRAIT.OATES_POLAR_CAVARLY:
                rt += "극점의 기병대장";
                break;

            case CREW_TRAIT.SKI_CHAMP:
                rt += "스키 챔피언";
                break;

            case CREW_TRAIT.OLAV_SKI_CHAMPION:
                rt += "노르웨이 스키 챔피언";
                break;
        }
        return rt;
    }
    public void CrewPopUpExit()
    {
        CrewPopUp.SetActive(false);
        //Curtain.alpha = 0f;
        Core.core.aud.AudioPlay(0);
    }

    private void ProgressbarUpdate()
    {
        progressbar.fillAmount = progress / 100f;
        prog_persentage.text = progress.ToString() + "%";
    }

    [SerializeField]
    GameObject Adv_PopUp;
    [SerializeField]
    TextMeshProUGUI Adv_text;
    public void Adv_PopUP_Active()
    {
        Adv_PopUp.SetActive(true);

        List<ExpeditionCrew> crew_list = new List<ExpeditionCrew>();
        crew_list = Core.core.Crew_Read_All();
        List<float> spd = new List<float>();
        foreach (ExpeditionCrew crew in crew_list)
        {
            spd.Add(SpdCalc(crew));
        }
        spd.Sort();
        float progresslength = spd[0];

        Adv_text.text = "예상 진행 거리 : " + progresslength.ToString();
        Curtain.alpha = 0.75f;
        Core.core.aud.AudioPlay(0);
    }

    public void Adv_Bt(bool bt)
    {
        if(bt)
        {
            ProgressFuction();
            Expedition_round_ends();
            Adv_PopUp.SetActive(false);
            Core.core.aud.AudioPlay(0);
            Curtain.alpha = 0f;
        }
        else
        {
            Adv_PopUp.SetActive(false);
            Core.core.aud.AudioPlay(0);
            Curtain.alpha = 0f;
        }
    }
    [SerializeField]
    private GameObject RestingPopUp;
    [SerializeField]
    private TextMeshProUGUI[] RestingTxt;
    public void RestingPopUpActive()
    {
        Curtain.alpha = 0.75f;
        RestingPopUp.SetActive(true);

        List<ExpeditionCrew> old_crew_list = new List<ExpeditionCrew>();
        List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
        old_crew_list = Core.core.Crew_Read_All();
        foreach (ExpeditionCrew crew in old_crew_list)
        {
            
            
            new_crew_list.Add(RestingFunction(crew));
            
        }
        
        int crew_count = new_crew_list.Count;

        for (int i = 0; i < crew_count; i++)
        {
            
            RestingTxt[i].text = new_crew_list[i].CREW_NAME + "\n" + old_crew_list[i].morale + "/" + old_crew_list[i].moraleMAX + "->" + new_crew_list[i].morale + "/" + new_crew_list[i].moraleMAX;
        }
        Core.core.aud.AudioPlay(0);
        //RestingTxt.text = "예상 진행 거리 : " + progresslength.ToString();
    }

    public void RestingPopUpBt(bool bt)
    {
        if (bt)
        {
            List<ExpeditionCrew> old_crew_list = new List<ExpeditionCrew>();
            List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
            old_crew_list = Core.core.Crew_Read_All();
            foreach (ExpeditionCrew crew in old_crew_list)
            {
                ExpeditionCrew newone = RestingFunction(crew);
                new_crew_list.Add(newone);

            }

            int crew_count = new_crew_list.Count;

            for (int i = 0; i < crew_count; i++)
            {
                Core.core.Crew_update(i, new_crew_list[i]);

            }
            Expedition_round_ends();
            
            Curtain.alpha = 0f;
            RestingPopUp.SetActive(false);
            EventPopUp_Test(0);
            Core.core.aud.AudioPlay(0);
        }
        else
        {
            Curtain.alpha = 0f;
            RestingPopUp.SetActive(false);
            Core.core.aud.AudioPlay(0);
        }
    }
    private ExpeditionCrew RestingFunction(ExpeditionCrew crew)
    {
        ExpeditionCrew new_crew = crew;
        new_crew.morale += 20f;
        if(new_crew.morale >= new_crew.moraleMAX)
        {
            new_crew.morale = new_crew.moraleMAX;
        }

        return new_crew;
    }

    public GameObject Item_Popup;
    [SerializeField]
    private GameObject Usage_Bt;
    public void ItemPopUp_Active()
    {
        Item_Popup.SetActive(true);
        popupcheck();
        Core.core.aud.AudioPlay(0);
    }
    public void popupcheck()
    {
        Bottom_UI_Sc sc = BottomUI.GetComponent<Bottom_UI_Sc>();
        if (sc.UI_pos == true && Item_Popup.activeSelf == true)
        {
            Usage_Bt.SetActive(true);
        }
    }
    public void ItemPopUp_Tab_Toggle(int i)
    {

        Item_Popup.GetComponent<ItemPopUp_Sc>().Bkgswap(i);
        Core.core.aud.AudioPlay(0);
    }
    public void ItemPopUp_Exit()
    {
        Item_Popup.SetActive(false);
        if(Usage_Bt.activeSelf == true)
        {
            Usage_Bt.SetActive(false);
        }
        Core.core.aud.AudioPlay(0);
    }
    [SerializeField]
    private GameObject Event_Popup,Event_Popup2,Event_Popup3;
    public void EventPopUp_Test(int i)
    {
        //Curtain.alpha = 0.75f;
        switch(i)
        {
            case 0:
                Event_Popup.SetActive(true);
                break;
            case 1:
                Event_Popup2.SetActive(true);
                break;
            case 2:
                Event_Popup3.SetActive(true);
                break;
        }
        
    }
    [SerializeField]
    private GameObject Menu_Popup;
    public void MenuPopUp_Test()
    {
        Curtain.alpha = 0.75f;
        Menu_Popup.SetActive(true);
        Core.core.aud.AudioPlay(0);
    }

    public void Menu_Script(int i)
    {
        switch(i)
        {
            case 0:
                Menu_Popup.SetActive(false);
                Curtain.alpha = 0f;
                break;

            case 1:
                SceneManager.LoadScene("Title_Scene");
                break;

            case 2:
                break;

            case 3:
                break;
        }
        Core.core.aud.AudioPlay(0);
    }

    /////////////////////////이벤트

    //public void Createevent()
    //{
    //    List<Event> eventpool;

    //}

    //void MSEventpoolcreation()
    //{
    //    List<float> eventpool;
    //}
    //public void EventSetting()
    //{

    //}
    [SerializeField]
    private GameObject eventpopup;
    private void MS_event_creator()
    {
        Event_Popup.SetActive(true);
    }

    public void MS_Event_fuction()
    {
        EffectVariations MS_effect = new EffectVariations(EffectVariationEnum.HUN, 50f);
        for(int i = 0; i < 4; i++)
        {
            EffectFunction(i, MS_effect);
            Core.core.aud.AudioPlay(0);
            Event_Popup.SetActive(false);
        }
    }
    public void MS_Event_fuction2()
    {
        Core.core.aud.AudioPlay(0);
        
        Event_Popup2.SetActive(false);
    }
    public void MS_Event_fuction3()
    {
        Core.core.aud.AudioPlay(0);

        Event_Popup.SetActive(false);
    }
    public void MS_Event_fuction4()
    {
        Core.core.aud.AudioPlay(0);

        Event_Popup3.SetActive(false);
    }

    public void OptionBt(int i, List<EffectVariations> option)
    {
        foreach(EffectVariations eft in option)
        {
            EffectFunction(i, eft);
            Core.core.aud.AudioPlay(0);
        }
    }
    ///
    ///이벤트 처리기
    ///

    private void EffectFunction(int i, EffectVariations effect)
    {
        ExpeditionCrew Selected_Crew = Core.core.Crew_Read(i);
        

        switch(effect.effect)
        {
            case EffectVariationEnum.HP:
                Selected_Crew.HP += effect.var;
                break;
            case EffectVariationEnum.HUN:
                Selected_Crew.hunger += effect.var;
                break;
            case EffectVariationEnum.TRU:
                Selected_Crew.morale += effect.var;
                break;
        }
        
        Core.core.Crew_update(i, Selected_Crew);
    }
}

