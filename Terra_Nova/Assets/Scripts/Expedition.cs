using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Reflection;


//using BIRPToURPConversionExtensions;
public class Expedition : MonoBehaviour
{
    public TextMeshProUGUI day, currentday, time,terrain_text,prog_persentage;
    public Image terrain,sun,progressbar,checkpoint;
    public Sprite[] sprites;
    public Camera cameras;
    private geograph terrain_info = geograph.GLACIER;
    private int currentdays = 1, days = 1, month = 11;
    private bool currenttime = true;
    private float progress = 0.000f;
    [SerializeField]
    CanvasGroup Curtain;

    [SerializeField]
    private GameObject Event_Container,General_Info_Container,Action_Container,Undo_Bookmark;
    [SerializeField]
    Volume volume;
    [SerializeField]
    private GameObject Menu_Popup;
    [SerializeField]
    private TextMeshProUGUI Menu_Title;
    [SerializeField]
    private GameObject menu_1st, menu_sound;
    [SerializeField]
    private Slider slide1, slide2;
    [SerializeField]
    private GameObject eventpopup;
    [SerializeField]
    private GameObject ItemContainer;
    [SerializeField]
    private GameObject ItemBookMark;
    [SerializeField]
    private GameObject Crew_Info_container;


    [SerializeField]
    private List<Event_data> stacked_events = new List<Event_data>();



    [SerializeField]
    private GameObject Action_Accept_Bt;

    [SerializeField]
    private GameObject[] Options;
    public static Expedition instance;


    [SerializeField]
    private GameObject itemcontainercontext,description_obj,target_container;

    [SerializeField]
    private Image[] itemtargetcrew;
    [SerializeField]
    private TextMeshProUGUI item_name, item_desc;
    [SerializeField]
    private Image item_picture;
    [SerializeField]
    private Item_Obj_Sc prefab;

    [SerializeField]
    private string targetitemname;
    [SerializeField]
    private List<Item_data> changgo = new List<Item_data>();

    [SerializeField]
    private bool[] progresscheckerarray = new bool[19];
    enum expedition_state
    {
        action,
        events,
        item,
        crewinfo
    }

    private expedition_state statue = expedition_state.action;
    void Start()
    {
        
        Core.core.OnActionChanged += Action_UI_accept_bt_function;
        Core.core.On_Round_Ends += Round_Ends_func_ex;
        day.text = month + "/" + days;
        currentday.text = currentdays + "일째"; 
        time.text = "일";
        terrain_text.text = "Bearedmore Glacier";
        ProgressbarUpdate();

        
        Core.core.BGM.AudioPlay(0);
    }
    

    public void Round_Ends_func_ex(object sender, EventArgs eventArgs)
    {
        Core.core.Crew_update_export();

        List<Crew_Sc> crews = Core.core.Crew_Read_All_New();

        round_storage_check();
        foreach (Crew_Sc crew in crews)
        {
            ExpeditionCrew export = crew.export;

            float var = export.hunger / export.hungerMAX;
            Stat_st stat = new Stat_st("HP", 10f);
            if (var > 0.8)
            {
                crew.CrewEffect_Function(stat);
            }
        }
        foreach (Crew_Sc crew in crews)
        {
            ExpeditionCrew export = crew.export;

            float var = export.hunger / export.hungerMAX;
            
            if (var < 0.5 && var > 0.25)
            {
                Stat_st stat = new Stat_st("morale", -10f);
                crew.CrewEffect_Function(stat);
            }
            else if(var < 0.25 && var > 0.15)
            {
                Stat_st stat = new Stat_st("morale", -25f);
                crew.CrewEffect_Function(stat);
            }
            else if(var < 0.15)
            {
                Stat_st stat = new Stat_st("morale", -50f);
                crew.CrewEffect_Function(stat);
            }
        }
        foreach (Crew_Sc crew in crews)
        {
            Stat_st stat = new Stat_st("hunger", -20f);
            Trait_data check = Trait_Library.instance.GetTrait("포만감");
            if(crew.export.Traits.Contains(check))
            {
                stat.value += 10f;
            }
            crew.CrewEffect_Function(stat);
        }
        crew_death_checker();
        progresschecker();
        DayUpdate();


    }
    
    public void DayUpdate()
    {
        currentdays++;
        //days++;
        
        string dayornight = "";
        if(currenttime)
        {
            dayornight = "오전";
            sun.sprite = sprites[0];
            //cameras.backgroundColor = new Color32(255, 255, 255, 0);
            days++;
            Core.core.Next_Day();
        }
        else
        {
            dayornight = "오후";
            sun.sprite = sprites[1];
            //cameras.backgroundColor = new Color32(100, 100, 100, 0);
        }
        day.text = month + "/" + days;
        currentday.text = currentdays + "일째";
        time.text = dayornight;


        Vignette vignette;
        volume.profile.TryGet<Vignette>(out vignette);
        if (currenttime)
        {
            
            vignette.intensity.value = 0f;
        }
        else
        {
            vignette.intensity.value = 0.4f;
        }
        //앵커


    }

    private void Awake()
    {
        instance = this;
        Option_Setting_script.settings.volumereorg();
    }



    public float SpdCalc(ExpeditionCrew crew)
    {
        float finalspd;
        float movestylespd = 0;
        float minimum_advent_range = 0;

        CREW_MOVEMENT movestyle = crew.CREW_MOVE;
        switch(movestyle)
        {
            case CREW_MOVEMENT.SKI:
                movestylespd = 0.4f;


                if (crew.Traits.Contains(Trait_Library.instance.GetTrait("스키 챔피언")))
                {
                    movestylespd += 0.1f;
                }
                else if(crew.Traits.Contains(Trait_Library.instance.GetTrait("스키 챔피언")))
                {
                    movestylespd += 0.1f;
                    minimum_advent_range += 0.1f;
                }

                break;

            case CREW_MOVEMENT.SKI_UPGRADE:
                movestylespd = 0.55f;

                if (crew.Traits.Contains(Trait_Library.instance.GetTrait("스키 챔피언")))
                {
                    movestylespd += 0.1f;
                }
                else if (crew.Traits.Contains(Trait_Library.instance.GetTrait("스키 챔피언")))
                {
                    movestylespd += 0.1f;
                    minimum_advent_range += 0.1f;
                }
                break;

            case CREW_MOVEMENT.WALK:
                movestylespd = 0.25f;

                if (crew.Traits.Contains(Trait_Library.instance.GetTrait("극지의 기사")))
                {
                    movestylespd -= 0.05f;
                }
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
        finalspd = movestylespd * crew.speed;
        finalspd += minimum_advent_range;
        return finalspd;
    }
    public void ProgressFuction(float adventrange)
    {
        
        progress += adventrange;

        ProgressbarUpdate();
        
    }
    //private void Expedition_round_ends()
    //{
    //    List<ExpeditionCrew> old_crew_list = new List<ExpeditionCrew>();
    //    List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
    //    old_crew_list = Core.core.Crew_Read_All();
    //    foreach(ExpeditionCrew crew in old_crew_list)
    //    {
    //        ExpeditionCrew newone = ExpeditionRoundEndFuction(crew);
    //        new_crew_list.Add(newone);
    //    }
    //    int crew_count = new_crew_list.Count;

    //    for(int i = 0; i < crew_count; i++)
    //    {
    //        Core.core.Crew_update(i, new_crew_list[i]);
    //    }
    //    Core.core.Crew_Statue_Changed();
    //    currenttime = !currenttime;
    //    DayUpdate();

    //    //#앵커
    //    //EventPopUp_Test(Random.Range(1, 3));
    //}
    //private ExpeditionCrew ExpeditionRoundEndFuction(ExpeditionCrew crew)
    //{
    //    ExpeditionCrew new_crew = crew;
    //    new_crew.hunger -= 5f;
    //    if(new_crew.hunger / new_crew.hungerMAX > 0.8f)
    //    {
    //        new_crew.HP += 10;
    //        if (new_crew.HP > new_crew.HPMAX)
    //            new_crew.HP = new_crew.HPMAX;
    //    }

    //    if(new_crew.hunger / new_crew.hungerMAX < 0.5f && new_crew.hunger / new_crew.hungerMAX > 0.26f)
    //    {
    //        new_crew.morale -= 10f;
    //    }
    //    else if (new_crew.hunger / new_crew.hungerMAX < 0.25f && new_crew.hunger / new_crew.hungerMAX > 0.16f)
    //    {
    //        new_crew.morale -= 25f;
    //    }
    //    else if (new_crew.hunger / new_crew.hungerMAX < 0.15f)
    //    {
    //        new_crew.morale -= 50f;
    //    }

    //    return new_crew;
    //}


    private void ProgressbarUpdate()
    {
        progressbar.fillAmount = progress / 100f;
        checkpoint.fillAmount = 1.01f - progressbar.fillAmount;
        prog_persentage.text = progress.ToString() + "%";
        
    }
    
    public void MenuPopUp_Test()
    {
        Curtain.alpha = 0.75f;
        Menu_Popup.SetActive(true);
        Core.core.aud.AudioPlay(0);
    }
    
    public void Menu_Script(int i)
    {
        Core.core.aud.AudioPlay(0);
        switch (i)
        {
            case 0:
                Menu_Popup.SetActive(false);
                Curtain.alpha = 0f;
                break;

            case 1:

                SceneManager.LoadScene("Title_Scene");
                Core.core.BGM.stopsound();
                break;

            case 2:
                menu_1st.SetActive(false);
                menu_sound.SetActive(true);
                Menu_Title.text = "소리 설정";
                break;

            case 3:
                menu_1st.SetActive(true);
                menu_sound.SetActive(false);
                Menu_Title.text = "메뉴";

                Core.core.aud.volumechange(slide1.value);
                Core.core.BGM.volumechange(slide2.value);
                //Option_Setting_script.settings.volume_audio(slide1.value);
                //Option_Setting_script.settings.volume_bgm(slide1.value);
                //Option_Setting_script.settings.volume_set(slide1.value, true);
                //Option_Setting_script.settings.volume_set(slide2.value, false);
                //Core.core.BGM.volumechange();
                //Core.core.aud.volumechange();
                break;
        }
        
    }


    public void Event_add(string evnt)
    {
        Event_data evntdata = Event_Library.instance.GetEvent(evnt);
        stacked_events.Add(evntdata);
    }
    public void Random_event_Creator(bool b)
    {
        List<Event_data> All_Events = Event_Library.instance.All_Events;
        List<Event_data> Temppool1 = new List<Event_data>();
        //List<Event_data> Temppool2 = new List<Event_data>();
        List<Event_data> new_pool = new List<Event_data>();

        foreach(Event_data evntcls in All_Events)
        {
            if(b)
            {
                if(evntcls.event_trigger.rest_event)
                {

                    Temppool1.Add(evntcls);
                    //All_Events.Remove(evntcls);
                }
            }
            else
            {
                if(!evntcls.event_trigger.rest_event)
                {
                    Temppool1.Add(evntcls);
                    //All_Events.Remove(evntcls);
                }
            }
        }

        
        foreach(Event_data eventcls in Temppool1)
        {
            if(trigger_checker(eventcls.event_trigger))
            {
                //if(eventcls.event_trigger.trigger_only)
                //{
                //    All_Events.Remove(eventcls);
                //}
                //else
                //{
                if(!eventcls.event_trigger.trigger_only)
                {
                    if (eventcls.event_chance_var.Any())
                    {
                        foreach (change_variable var in eventcls.event_chance_var)
                        {
                            if (trigger_checker(var.trigger))
                            {
                                eventcls.eventchance *= var.value;
                            }

                        }
                        int v = Mathf.RoundToInt(eventcls.eventchance);
                        for (int i = 0; i < v; i++)
                        {
                            new_pool.Add(eventcls);
                        }
                    }
                    else
                    {
                        int v = Mathf.RoundToInt(eventcls.eventchance);
                        for (int i = 0; i < v; i++)
                        {
                            new_pool.Add(eventcls);
                        }

                    }
                }
                //Debug.Log(eventcls.Event_Name);
                //Debug.Log(eventcls.eventchance);
                //}
                
            }
        }
        int j = UnityEngine.Random.Range(0, new_pool.Count);
        stacked_events.Add(new_pool[j]);
    }

    [SerializeField]
    TextMeshProUGUI event_title, event_desc,eventcount;
    [SerializeField]
    Image event_image;
    private void eventloader()
    {
        Event_data targetevent = eventoptionstrainer(stacked_events[0]);

        if(targetevent.fire_only_once == true)
        {
            Event_data original = Event_Library.instance.GetEvent(targetevent.Event_Name);
            Event_Library.instance.All_Events.Find(x => x == original).event_trigger.trigger_only = true;
        }

        if(targetevent.effects.Any())
        {
            foreach(Effect efct in targetevent.effects)
            {
                effectworkingmethod(efct);
            }
        }


        event_title.text = targetevent.Event_Name;
        event_desc.text = targetevent.Event_Desc;
        event_image.sprite = targetevent.Event_Picture;
        eventcount.text = stacked_events.Count.ToString();
        for (int i = targetevent.eventoptions.Count; i < Options.Length; i++)
        {
            Options[i].SetActive(false);
        }
        for (int i = 0; i < targetevent.eventoptions.Count; i++)
        {
            TextMeshProUGUI text = Options[i].GetComponent<TextMeshProUGUI>();
            text.text = targetevent.eventoptions[i].name + "<br>" + targetevent.eventoptions[i].desc;
        }
    }


    private Event_data eventoptionstrainer(Event_data eventcls)
    {
        Event_data evnt = eventcls;
        List<EventOption> newlist = new List<EventOption>();
        List<EventOption> templist = new List<EventOption>();
        foreach (EventOption option in evnt.eventoptions)
        {
            if (option.Randomized == true)
            {
                bool rand = (UnityEngine.Random.value > 0.5f);

                if (rand)
                    templist.Add(option);
            }
            else
                templist.Add(option);
        }
        foreach (EventOption option in templist)
        {
            
            if(trigger_checker(option.trigger))
            {
                newlist.Add(option);
            }
            
        }
        evnt.eventoptions = newlist;
        
        return evnt;
    }

    public void item_activate(int i)
    {
        //Core.core.aud.AudioPlay(0);
        Crew_Sc target = Core.core.Crew_Read_New(i);
        int j = changgo.FindIndex(x => x.name == targetitemname);
        
        if (trigger_checker_mini(changgo[j].trigger, target) && changgo[j].have > 0)
        {
            foreach(Item_effect efct in changgo[j].effect)
            {
                if(efct.stateffect.Any())
                {
                    foreach(Stat_st st in efct.stateffect)
                    {
                        target.CrewEffect_Function(st);
                    }
                }
                if(efct.traiteffect.Any())
                {
                    foreach(Trait_st tr in efct.traiteffect)
                    {
                        target.CrewTrait_Effect_Function(tr);
                    }
                }
                if(efct.itemeffect.Any())
                {
                    foreach (Item_st it in efct.itemeffect)
                    {
                        if(it.stack > 0)
                        {
                            Itemstorage_input(it.name, it.stack);
                        }
                        else
                        {
                            Itemstorage_output(it.name, it.stack);
                        }
                    }
                }
            }
            changgo[j].have -= 1;
            ItemcontainerSetting();
            //소리 넣기
            switch(changgo[j].category)
            {
                case item_category.equipment:
                    Core.core.aud.AudioPlay(4);
                    break;
                case item_category.food:
                    Core.core.aud.AudioPlay(2);
                    break;
                case item_category.others:
                    Core.core.aud.AudioPlay(4);
                    break;
            }
        }
        Core.core.Crew_Statue_Changed();
    }

    public bool trigger_checker(triggers trig)
    {
        List<bool> Output = new List<bool>();

        
        List<Crew_Sc> crews = new List<Crew_Sc>();
        crews = Core.core.Crew_Read_All_New();
        if (trig.stat_trigger.Any())
        {
            foreach (stat_trigger stattrig in trig.stat_trigger)
            {
                Stat_st stat = stattrig.stat;

                foreach (Crew_Sc crew in crews)
                {
                    var fi = crew.GetType().GetTypeInfo().GetDeclaredField(stat.stat);
                    float v = (float)fi.GetValue(crew);

                    if (stat.value > v)
                    {
                        Output.Add(stattrig.over_or_below);
                    }

                }
            }
        }

        if(trig.Trait_trigger.Any())
        {
            List<ExpeditionCrew> crew_old = new List<ExpeditionCrew>();

            crew_old = Core.core.Crew_Read_All();
            foreach(ExpeditionCrew crew in crew_old)
            {
                foreach(Trait_st st in trig.Trait_trigger)
                {
                    Trait_data temp = Trait_Library.instance.GetTrait(st.name);
                    if(crew.Traits.Contains(temp))
                    {
                        Output.Add(st.b);
                    }
                }
            }
            //foreach(Trait_st trait_st in trig.Trait_trigger)
            //{
            //    Trait_data temp = Trait_Library.instance.GetTrait(trait_st.name);
            //    foreach (ExpeditionCrew crew in crew_old)
            //    {
                    
            //        if(crew.Traits.Contains(temp))
            //        {
            //            Output.Add(trait_st.b);
            //        }

            //    }
            //}
        }

        if(trig.progress_trigger.Any())
        {
            float cutline = trig.progress_trigger[0];
            if (progress > cutline)
            {
                Output.Add(true);
            }
            else
                Output.Add(false);
        }

        if(trig.item_limit.Any())
        {
            foreach(Item_trigger trig2 in trig.item_limit)
            {
                //Item_data item = Item_Library.instance.GetItem(trig2.name);

                if(changgo.Exists(x => x.name == trig2.name))
                {
                    int i = changgo.FindIndex(x => x.name == trig2.name);

                    if (changgo[i].have < trig2.stack)
                    {
                        Output.Add(!trig2.b);
                    }
                    else
                        Output.Add(trig2.b);
                }
                else
                {
                    Output.Add(false);
                }
                
                
            }
        }

        if(trig.geograph_trigger.Any())
        {
            if (trig.geograph_trigger.Contains(terrain_info))
                Output.Add(true);

            else
                Output.Add(false);
        }

        if(trig.daycheck.Any())
        {
            if (trig.daycheck[0] < currentdays)
            {
                Output.Add(true);
            }
            else
                Output.Add(false);
        }

        if(trig.is_evening.Any())
        {
            if (trig.is_evening[0] != currenttime)
            {
                Output.Add(true);
            }
            else
                Output.Add(false);
        }

        //Output.Add(!trig.trigger_only);

        if(Output.Contains(false))
        {
            return false;
        }
        else
            return true;
    }

    public bool trait_checker(string name)
    {
        List<bool> Output = new List<bool>();
        List<ExpeditionCrew> crew_old = new List<ExpeditionCrew>();

        crew_old = Core.core.Crew_Read_All();
        foreach (ExpeditionCrew crew in crew_old)
        {
            if(crew.Traits.Exists(x => x.name == name))
            {
                Output.Add(true);
            }
        }

        if (Output.Contains(true))
        {
            return true;
        }
        else
            return false;
    }
    public bool trigger_checker_mini(triggers trig, Crew_Sc crew)
    {
        List<bool> Output = new List<bool>();


//        List<Crew_Sc> crews = new List<Crew_Sc>();
  //      crews = Core.core.Crew_Read_All_New();
        if (trig.stat_trigger.Any())
        {
            foreach (stat_trigger stattrig in trig.stat_trigger)
            {
                Stat_st stat = stattrig.stat;
                var fi = crew.GetType().GetTypeInfo().GetDeclaredField(stat.stat);
                float v = (float)fi.GetValue(crew);
                
                if (stat.value > v)
                {
                    Output.Add(stattrig.over_or_below);
                }
            }
        }

        if (trig.Trait_trigger.Any())
        {
            crew.Export_Crew_Const();
            List<Trait_data> traits = crew.export.Traits;
            foreach (Trait_st trait_st in trig.Trait_trigger)
            {
                foreach (Trait_data trait in traits)
                {
                    if (trait.name == trait_st.name)
                    {
                        Output.Add(trait_st.b);
                    }
                }
            }
        }

        if(trig.targetID.Any())
        {
            crew.Export_Crew_Const();
            if (!trig.targetID.Contains(crew.export.CREW_ID))
                Output.Add(false);
        }

        if (Output.Contains(false))
        {
            return false;
        }
        else
            return true;
    }

    
    public void Option_Button(int num)
    {
        Event_data eventcls = stacked_events[0];

        EventOption option = eventcls.eventoptions[num];

        

        foreach(Effect efct in option.effects)
        {
            effectworkingmethod(efct);
        }
        crew_death_checker();
        stacked_events.RemoveAt(0);
        if(stacked_events.Any())
        {
            eventloader();
        }
        else
        {
            UI_form_change(3);
        }
        Core.core.Crew_Statue_Changed();
        Core.core.aud.AudioPlay(0);
    }
    private void effectworkingmethod(Effect efct)
    {
        List<Crew_Sc> crews = Core.core.Crew_Read_All_New();
        if (!efct.randomtarget)
        {
            foreach (Crew_Sc crew in crews)
            {
                if (trigger_checker_mini(efct.target, crew))
                {
                    if (efct.traiteffect.Any())
                    {


                        foreach (Trait_st traitst in efct.traiteffect)
                        {
                            crew.CrewTrait_Effect_Function(traitst);

                        }

                    }

                    if (efct.stateffect.Any())
                    {
                        foreach (Stat_st statst in efct.stateffect)
                        {
                            crew.CrewEffect_Function(statst);
                        }
                    }
                }

            }
            if (efct.itemeffect.Any())
            {
                foreach (Item_st item in efct.itemeffect)
                {
                    if (item.stack > 0)
                    {
                        Itemstorage_input(item.name, item.stack);
                    }
                    else
                        Itemstorage_output(item.name, item.stack);
                }
            }
            if (efct.event_effect.Any())
            {
                //if(efct.event_effect[0] == "random")
                //{
                //    Random_event_Creator(false);
                //}

                Event_data nextevent = Event_Library.instance.GetEvent((efct.event_effect[0]));
                stacked_events.Insert(1, nextevent);

            }
        }
        else
        {
            int i = UnityEngine.Random.Range(0, crews.Count);
            if (efct.traiteffect.Any())
            {


                foreach (Trait_st traitst in efct.traiteffect)
                {
                    crews[i].CrewTrait_Effect_Function(traitst);

                }

            }

            if (efct.stateffect.Any())
            {
                foreach (Stat_st statst in efct.stateffect)
                {
                    crews[i].CrewEffect_Function(statst);
                }
            }

        }
    }
    public void Undo_Bt()
    {
        switch(statue)
        {
            case expedition_state.events:
                UI_form_change(0);
                break;

            //case expedition_state.item:
            //    UI_form_change(1);
            //    break;

            case expedition_state.action:

                UI_form_change(3);
                break;
        }
        Undo_Bookmark.SetActive(false);
        Core.core.aud.AudioPlay(0);
    }


    ///땜빵2
    ///
    public void UI_form_change(int form)
    {
        switch(form)
        {
            case 0://event
                Event_Container.SetActive(true);
                General_Info_Container.SetActive(true);
                Action_Container.SetActive(false);
                ItemBookMark.SetActive(false);
                Crew_Info_container.SetActive(false);
                statue = expedition_state.events;
                break;

            case 1://item
                
                Undo_Bookmark.SetActive(true);
                Action_Container.SetActive(false);
                ItemContainer.SetActive(true);
                ItemBookMark.SetActive(false);
                Crew_Info_container.SetActive(false);
                ItemcontainerSetting();
                Core.core.aud.AudioPlay(0);
                //statue = expedition_state.item;
                break;

            case 2://crewinfo
                Undo_Bookmark.SetActive(true);
                Event_Container.SetActive(false);
                General_Info_Container.SetActive(false);
                Action_Container.SetActive(false);
                ItemBookMark.SetActive(false);
                ItemContainer.SetActive(false);
                Crew_Info_container.SetActive(true);
                //statue = expedition_state.crewinfo;
                break;
            case 3://action
                Event_Container.SetActive(false);
                General_Info_Container.SetActive(false);
                Action_Container.SetActive(true);
                ItemContainer.SetActive(false);
                ItemBookMark.SetActive(true);
                Crew_Info_container.SetActive(false);
                statue = expedition_state.action;
                break;
        }
    }




    public void accept_button()
    {
        Action_Accept_Bt.SetActive(false);
        StartCoroutine(Screen_fade());
        
        //Expedition_round_ends();
    }

    private void Action_UI_accept_bt_function(object sender, EventArgs eventArgs)
    {
        Action_UI_sc[] components = Action_Container.transform.GetComponentsInChildren<Action_UI_sc>();
        List<Action_UI_sc.ACTION_STATUE> statue = new List<Action_UI_sc.ACTION_STATUE>();
        foreach(Action_UI_sc comp2 in components)
        {
            statue.Add(comp2.statues);
        }

        if(statue.Contains(Action_UI_sc.ACTION_STATUE.IDLE))
        {
            Action_Accept_Bt.SetActive(false);
        }
        else
        {
            Action_Accept_Bt.SetActive(true);
        }
    }


    private IEnumerator Screen_fade()
    {
        Core.core.aud.AudioPlay(0);
        float Timer = 0f;
        float Fading_Time = 0.75f;
        while (Curtain.alpha != 1)
        {
            Timer += Time.deltaTime;
            float Timer2 = Timer / Fading_Time;
            yield return new WaitForSeconds(0.01f);
            Curtain.alpha = Timer2;
            //Debug.Log(Timer2 + "1차");

        }
        UI_form_change(0);
        
        //Core.core.aud.AudioPlay(0);


        Core.core.Action_do();
        eventloader();
        Core.core.Crew_Statue_Changed();
        while (Curtain.alpha != 0)
        {
            Timer -= Time.deltaTime;
            float Timer2 = Timer / Fading_Time;
            yield return new WaitForSeconds(0.01f);
            Curtain.alpha = Timer2;

            //Debug.Log(Timer2 + "2차");
        }

        yield return null;
    }
    public void iteminfosetting(string name)
    {
        Core.core.aud.AudioPlay(0);
        if(description_obj.activeSelf == false)
        {
            description_obj.SetActive(true);
        }
        Item_data item = Item_Library.instance.GetItem(name);
        item_name.text = item.name;
        item_picture.sprite = item.big_icon;
        item_desc.text = item.description;
        targetitemname = item.name;


        if(item.activeable)
        {
            itemusage_func();
        }
        else
        {
            target_container.SetActive(false);
        }
    }

    private void itemusage_func()
    {
        //foreach(GameObject child in target_container.transform)
        //{

        //}
        target_container.SetActive(true);

        for(int i = 0; i < 4; i++)
        {
            ExpeditionCrew crews = Core.core.Crew_Read(i);
            if (crews.CREW_ID == 0)
                itemtargetcrew[i].gameObject.SetActive(false);
            else
                if (crews.portrait < 100)
            {
                itemtargetcrew[i].sprite = Core.core.Crew_Portrait_Set[crews.portrait];
            }
            else
            {
                itemtargetcrew[i].sprite = Core.core.Unique_Crew_Portrait_Set[crews.portrait - 100];
            }
            //itemtargetcrew[i].sprite = Core.core.Unique_Crew_Portrait_Set[crews.portrait - 100];
            //else
            //{
            //    Image img = itemtargetcrew[i].GetComponent<Image>();
            //    img.sprite = Core.core.Unique_Crew_Portrait_Set[cre.portrait - 100];
            //}
        }

    }
    public void ItemcontainerSetting()
    {
        if (itemcontainercontext.transform.childCount != 0)
        {
            foreach (Transform child in itemcontainercontext.transform)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Item_data items in changgo)
        {
            Item_Obj_Sc newtrait = Instantiate(prefab, itemcontainercontext.transform);
            newtrait.setinfo(items);

        }
    }
    public void Itemstorage_input(string name, int stack)
    {
        if (changgo.Exists(x => x.name == name))
        {
            int i = changgo.FindIndex(x => x.name == name);
            int stacks = stack;
            
            
            if (changgo[i].have < changgo[i].capacity)
            {
                while(stacks == 0)
                {
                    changgo[i].have++;
                    stacks--;
                }
                
            }
        }
        else
        {
            Item_data item = Item_Library.instance.GetItem(name);
            item.have = stack;
            changgo.Add(item);

        }
    }

    private void round_storage_check()
    {
        foreach(Item_data item in changgo)
        {
            switch (item.name)
            {
                case "썰매 개":
                    Itemstorage_output("사료", item.have);

                    break;
                case "썰매 곰":
                    Itemstorage_output("사료", item.have * 8);

                    break;
                case "조랑말":
                    Itemstorage_output("말 사료", item.have * 2);

                    break;

                default:
                    break;
            }
        }
    }


    public bool storage_check(string name, int stack)
    {
        if (changgo.Exists(x => x.name == name))
        {
            Item_data item = changgo.Find(x => x.name == name);
            if (item.have > stack)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    public void Itemstorage_output(string name, int stack)
    {
        if (changgo.Exists(x => x.name == name))
        {
            int i = changgo.FindIndex(x => x.name == name);


            if (changgo[i].have > stack)
            {
                changgo[i].have += stack;

            }
            else
            {
                changgo[i].have = 0;
            }
        }
    }


    public void expedition_ends(bool victory)
    {
        DontDestroyOnLoad(this);
        if(victory)
        {
            SceneManager.LoadScene("Title_Scene");


        }
        else
        {
            //SceneManager.LoadScene()
        }
    }
    private int deathcounter = 0;
    GameObject credit_controller;
    private void crew_death_checker()
    {
        List<ExpeditionCrew> crews = Core.core.Crew_Read_All();

        foreach(ExpeditionCrew crew in crews)
        {
            if(crew.HP <= 0)
            {
                int index = crews.IndexOf(crew);
                Core.core.Crew_reset(index);
                Core.core.aud.AudioPlay(5);
                deathcounter++;
            }
            else if(crew.hunger <= 0)
            {
                int index = crews.IndexOf(crew);
                Core.core.Crew_reset(index);
                Core.core.aud.AudioPlay(5);
                deathcounter++;
            }
        }

        List<bool> templist = new List<bool>();
        if (Core.core.crew_life_check())
        {
            creditinfo info = new creditinfo();
            info.progress = progress;
            info.currentday = currentdays;
            info.month = month;
            info.day = days;
            info.deathcount = deathcounter;
            info.difficult = Option_Setting_script.settings.difficult;
            info.additional_score = 0;
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("Credit_fail_scene");
            credit_controller = GameObject.Find("credit_controller");
            credit_controller.GetComponent<Credit_script>().getcreditinfo(info);
            Destroy(this);
        }
            
    }

    private void progresschecker()
    {
        if(progress >= 5f)
        {
            int a = Mathf.RoundToInt(progress) / 5;

            if (progresscheckerarray[a] == false)
            {
                progresscheckerarray[a] = true;
                Event_data nextevent = Event_Library.instance.GetEvent("보급창 도착");
                stacked_events.Insert(0, nextevent);
            }
        }
        
    }
}

public class creditinfo
{
    public float progress;
    public int deathcount, day, month, currentday;
    public int difficult,additional_score;

}