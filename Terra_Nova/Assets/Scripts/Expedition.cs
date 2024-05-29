using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        days++;
        
        string dayornight = "";
        if(currenttime)
        {
            dayornight = "DayTime";
            sun.sprite = sprites[0];
            cameras.backgroundColor = new Color32(255, 255, 255, 0);
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
        //체력까는거 넣어야함
    }
    private void Expedition_round_ends()
    {
        List<ExpeditionCrew> old_crew_list = new List<ExpeditionCrew>();
        List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
        old_crew_list = Core.core.Crew_Read_All();
        foreach(ExpeditionCrew crew in old_crew_list)
        {
            ExpeditionRoundEndFuction(crew);
            new_crew_list.Add(crew);
        }
        int crew_count = new_crew_list.Count;

        for(int i = 0; i < crew_count; i++)
        {
            Core.core.Crew_update(i, new_crew_list[i]);
        }

        currenttime = !currenttime;
        DayUpdate();
    }
    private void ExpeditionRoundEndFuction(ExpeditionCrew crew)
    {
        crew.hunger -= 20f;
        if(crew.hunger / crew.hungerMAX > 0.8f)
        {
            crew.HP += 10;
            if (crew.HP > crew.HPMAX)
                crew.HP = crew.HPMAX;
        }

        if(crew.hunger / crew.hungerMAX < 0.5f && crew.hunger / crew.hungerMAX > 0.26f)
        {
            crew.morale -= 10f;
        }
        else if (crew.hunger / crew.hungerMAX < 0.25f && crew.hunger / crew.hungerMAX > 0.16f)
        {
            crew.morale -= 25f;
        }
        else if (crew.hunger / crew.hungerMAX < 0.15f)
        {
            crew.morale -= 50f;
        }
    }
    

    public void ExpeditionEvent()
    {
        
    }



    [SerializeField]
    private TextMeshProUGUI names2, hp2, hunger2, trust2, temperature, trait1, trait2, trait3;
    [SerializeField]
    GameObject CrewPopUp;
    public void CrewPopUpActive(int crewnumber)
    {
        CrewPopUp.SetActive(true);


        names2.text = Core.core.Crew_Read(crewnumber).CREW_NAME;
        hp2.text = "HP : " + Core.core.Crew_Read(crewnumber).HP.ToString() + "/" + Core.core.Crew_Read(crewnumber).HPMAX.ToString();
        hunger2.text = "Hunger : " + Core.core.Crew_Read(crewnumber).hunger.ToString() + "/" + Core.core.Crew_Read(crewnumber).hungerMAX.ToString();
        trust2.text = "Trust : " + Core.core.Crew_Read(crewnumber).morale.ToString() + "/" + Core.core.Crew_Read(crewnumber).moraleMAX.ToString();
        temperature.text = "Temperature : " + Core.core.Crew_Read(crewnumber).temperature.ToString() + "°C";

        Core.core.aud.AudioPlay(0);
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
    private GameObject Item_Popup;
    public void ItemPopUp_Active()
    {

    }
    public void EventPopUp_Test()
    {

    }
}

