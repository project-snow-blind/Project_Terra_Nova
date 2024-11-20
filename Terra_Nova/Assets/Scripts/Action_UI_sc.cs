using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Action_UI_sc : MonoBehaviour
{
    [SerializeField]
    Crew_Sc crew;
    [SerializeField]
    private Image Portrait, Advent, Rest, Scout, Craft, Check;
    [SerializeField]
    private TextMeshProUGUI Name, Desc;


    [SerializeField]
    private int crewnumber = 0;
    // Start is called before the first frame update

    public ACTION_STATUE statues;
    public enum ACTION_STATUE
    {
        IDLE = 0,
        ADVENT = 1,
        REST = 2,
        SCOUT = 3,
        CRAF = 4
    }
    void Start()
    {
        Core.core.OnCrewChanged += crew_statue_changed;
        Core.core.On_Round_Ends += Action_Invoke;
    }
    private void crew_statue_changed(object sender, EventArgs eventArgs)
    {
        
        ExpeditionCrew check = Core.core.Crew_Read(crewnumber);

        if (check.CREW_ID == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Action_UI_Update();
        }
    }
    // Update is called once per frame
    //private void OnEnable()
    //{
    //    ExpeditionCrew check = Core.core.Crew_Read(crewnumber);
    //    if (check.CREW_ID == 0)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    Action_UI_Update();
    //}
    public void Action_UI_Update()
    {
        ExpeditionCrew crew = Core.core.Crew_Read(crewnumber);
        if (crew.portrait < 100)
        {
            Portrait.sprite = Core.core.Crew_Portrait_Set[crew.portrait];
        }
        else
        {
            Portrait.sprite = Core.core.Unique_Crew_Portrait_Set[crew.portrait - 100];
        }
        Name.text = crew.CREW_NAME;

        desc_tooltip_update();
    }

    public void Actionbutton_function(int i)
    {
        switch(i)
        {
            case 0:
                statues = ACTION_STATUE.IDLE;
                break;
            case 1:
                statues = ACTION_STATUE.ADVENT;
                Check.rectTransform.position = Advent.rectTransform.position;
                
                break;
            case 2:
                statues = ACTION_STATUE.REST;
                Check.rectTransform.position = Rest.rectTransform.position;
                break;
            case 3:
                statues = ACTION_STATUE.SCOUT;
                Check.rectTransform.position = Scout.rectTransform.position;
                break;
            case 4:
                statues = ACTION_STATUE.CRAF;
                Check.rectTransform.position = Craft.rectTransform.position;
                break;
        }
        Check.gameObject.SetActive(true);

        Core.core.Action_UI_Changed();
        Core.core.aud.AudioPlay(0);
        Action_UI_Update();
    }

    private void Action_Invoke(object sender, EventArgs eventArgs)
    {
        Stat_st effect = new Stat_st();
        float var = 0;
        switch(statues)
        {
            case ACTION_STATUE.ADVENT:
                var = -15f;
                effect = new Stat_st("HP", var);


                List<ExpeditionCrew> crewlist = Core.core.Crew_Read_All();
                List<float> spdlist = new List<float>();
                foreach (ExpeditionCrew crews in crewlist)
                {
                    spdlist.Add(SpdCalc(crews));
                }
                spdlist.Sort();
                float spd = spdlist[0];
                //spd *= 10f;//�ӽ÷� ����
                spd *= 40f;
                Expedition.instance.ProgressFuction(spd);

                List<Crew_Sc> crew_new_list = Core.core.Crew_Read_All_New();
                Core.core.Crew_update_export();
                Trait_data check = new Trait_data();
                foreach (Crew_Sc crew in crew_new_list)
                {
                    check = Trait_Library.instance.GetTrait("���� ��Ʈ ���");
                    ExpeditionCrew crew_old = crew.export;
                    if (crew_old.Traits.Contains(check))
                    {
                        var += 5f;
                    }

                    check = Trait_Library.instance.GetTrait("Ż��");
                    if (crew_old.Traits.Contains(check))
                    {
                        var *= 2f;
                    }
                    check = Trait_Library.instance.GetTrait("Ȱ�� ��ħ");
                    if(crew_old.Traits.Contains(check))
                    {
                        var = 0;
                    }
                    crew.CrewEffect_Function(effect);
                }
                
                
                Expedition.instance.Random_event_Creator(false);
                break;

            case ACTION_STATUE.REST:
                var = 10f;
                effect = new Stat_st("HP", var);
                //float heal = 10f;
                //Stat_st effect = new Stat_st("HP", heal);
                crew.CrewEffect_Function(effect);
                effect.stat = "morale";
                crew.CrewEffect_Function(effect);
                Expedition.instance.Random_event_Creator(true);
                break;

            case ACTION_STATUE.SCOUT:
                Expedition.instance.Event_add("����");
                break;

            case ACTION_STATUE.CRAF:
                Expedition.instance.Event_add("����");
                break;
        }
        statues = ACTION_STATUE.IDLE;
        Check.gameObject.SetActive(false);
    }
    private void desc_tooltip_update()
    {
        Crew_Sc crew = Core.core.Crew_Read_New(crewnumber);
        ExpeditionCrew crew_old = Core.core.Crew_Read(crewnumber);
        
        
        switch (Convert.ToInt32(statues))
        {
            case 0:
                Desc.text = "������ ����� ���� ����";
                break;
            case 1://����
                List<ExpeditionCrew> crewlist = Core.core.Crew_Read_All();
                List<float> spdlist = new List<float>();
                foreach (ExpeditionCrew crews in crewlist)
                {
                    spdlist.Add(SpdCalc(crews));
                }
                spdlist.Sort();
                float spd = spdlist[0];
                //spd *= 100f;
                spd *= 40f;
                Desc.text = "��ǥ�� ���� ���弭�� ������\n ���� �̵� �Ÿ� : " + spd + "%" + "\n" + "�̵� �� ��� ����� 15�� ���ظ� ����";
                break;
            case 2://�޽�
                float tempmorale = crew_old.morale + 20f;
                if (tempmorale > crew_old.moraleMAX)
                    tempmorale = crew_old.moraleMAX;
                float temphp = crew_old.HP + 20f;
                if (temphp > crew_old.HPMAX)
                    temphp = crew_old.HPMAX;
                Desc.text = crew_old.CREW_NAME += "��(��) �޽��� ����\n��� ȸ�� �� ü�� ȸ��\n" + crew_old.morale + "/" + crew_old.moraleMAX + " -> " + tempmorale + "/" + crew_old.moraleMAX + "\n" +  crew_old.HP + "/" + crew_old.HPMAX + " -> " + temphp + "/" + crew_old.HPMAX;
                break;
            case 3://����
                Desc.text = crew_old.CREW_NAME += "��(��) �ֺ��� �ѷ������ ��\n" + "������ �̺�Ʈ �߻�";
                break;
            case 4://����
                Desc.text = crew_old.CREW_NAME += "��(��) ķ������ ��� �����ϱ�� ��\n" + "���� �̺�Ʈ �߻�";
                break;

        }
    }
    private float SpdCalc(ExpeditionCrew crew)
    {
        float finalspd;
        float movestylespd = 0;
        float minimum_advent_range = 0;

        CREW_MOVEMENT movestyle = crew.CREW_MOVE;
        switch (movestyle)
        {
            case CREW_MOVEMENT.SKI:
                movestylespd = 0.04f;


                if (crew.Traits.Contains(Trait_Library.instance.GetTrait("��Ű è�Ǿ�")))
                {
                    movestylespd += 0.01f;
                }
                else if (crew.Traits.Contains(Trait_Library.instance.GetTrait("��Ű è�Ǿ�")))
                {
                    movestylespd += 0.01f;
                    minimum_advent_range += 0.01f;
                }

                break;

            case CREW_MOVEMENT.SKI_UPGRADE:
                movestylespd = 0.055f;

                if (crew.Traits.Contains(Trait_Library.instance.GetTrait("��Ű è�Ǿ�")))
                {
                    movestylespd += 0.01f;
                }
                else if (crew.Traits.Contains(Trait_Library.instance.GetTrait("��Ű è�Ǿ�")))
                {
                    movestylespd += 0.01f;
                    minimum_advent_range += 0.01f;
                }
                break;

            case CREW_MOVEMENT.WALK:
                movestylespd = 0.025f;

                if (crew.Traits.Contains(Trait_Library.instance.GetTrait("������ ���")))
                {
                    movestylespd -= 0.005f;
                }
                break;

            case CREW_MOVEMENT.SNOWMOBILE:
                movestylespd = 0.075f;
                break;

            case CREW_MOVEMENT.DOG_SLED:
                movestylespd = 0.055f;
                break;

            case CREW_MOVEMENT.BEAR_SLED:
                movestylespd = 0.075f;
                break;

            case CREW_MOVEMENT.PONY:
                movestylespd = 0.05f;
                break;
        }
        finalspd = movestylespd * crew.speed;
        finalspd += minimum_advent_range;
        //������� ������ ���� ������ �������� ����
        return finalspd;
    }
    //    List<ExpeditionCrew> crew_list = new List<ExpeditionCrew>();
    //    crew_list = Core.core.Crew_Read_All();
    //        List<float> spd = new List<float>();
    //        foreach(ExpeditionCrew crew in crew_list)
    //        {
    //            spd.Add(SpdCalc(crew));
    //        }
    //spd.Sort();
    //float progresslength = spd[0];
    //progress += progresslength;

    //ProgressbarUpdate();
    //List<ExpeditionCrew> new_crew_list = new List<ExpeditionCrew>();
    //foreach (ExpeditionCrew crew in crew_list)
    //{
    //    ExpeditionCrew newone = crew;
    //    newone.HP -= 15f;
    //    newone.hunger -= 20f;
    //    new_crew_list.Add(newone);
    //    //advent_damage_fuction(crew);
    //    //new_crew_list.Add(crew);
    //}
    //int crew_count = new_crew_list.Count;
    //for (int i = 0; i < crew_count; i++)
    //{
    //    Core.core.Crew_update(i, new_crew_list[i]);
    //}
}
