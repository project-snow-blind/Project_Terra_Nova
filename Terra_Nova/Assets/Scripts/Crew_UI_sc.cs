using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Crew_UI_sc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI names, hp, hunger, trust,movesetdesc,clothdesc;
    [SerializeField]
    private Image Portrait,hpbar,hgbar,trustbar,tempbar,moveset,cloth;
    [SerializeField]
    private Sprite[] Temp,movesetsprite,clothsprite;

    [SerializeField]
    private GameObject[] Trait_List;
    //[SerializeField]
    //private Image Bkg;
    //public TextMeshProUGUI text2;
    //[SerializeField]
    //private GameObject PopUp;
    [SerializeField]
    private int crewnumber = 0;


    [SerializeField]
    private Crew_Info_UI_sc info_ui;
    //public event EventHandler Crew_Changed;
    // Start is called before the first frame update
    void Start()
    {
        Core.core.OnCrewChanged += crew_statue_changed;
        
    }


    public void Crew_Info_UI_Open()
    {
        Expedition.instance.UI_form_change(2);
        Crew_Sc crew = Core.core.Crew_Read_New(crewnumber);
        info_ui.UIupdate(crew);
        Core.core.aud.AudioPlay(0);
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
            UIupdatecrewinfo();
        }
    }
    public void UIupdatecrewinfo()
    {
        ExpeditionCrew crew = Core.core.Crew_Read(crewnumber);
        names.text = crew.CREW_NAME;


        float temp = crew.HP / crew.HPMAX;
        temp = MathF.Floor(temp * 100f);
        hp.text = temp + "%";
        temp = crew.hunger / crew.hungerMAX;
        temp = MathF.Floor(temp * 100f);
        hunger.text = temp + "%";
        temp = crew.morale / crew.moraleMAX;
        temp = MathF.Floor(temp * 100f);
        trust.text = temp + "%";

        hpbar.fillAmount = crew.HP / crew.HPMAX;
        hgbar.fillAmount = crew.hunger / crew.hungerMAX;
        trustbar.fillAmount = crew.morale / crew.moraleMAX;
        if (crew.portrait < 100)
        {
            Portrait.sprite = Core.core.Crew_Portrait_Set[crew.portrait];
        }
        else
        {
            Portrait.sprite = Core.core.Unique_Crew_Portrait_Set[crew.portrait - 100];
        }
        //Portrait_Set(crew);
        tempbar.sprite = Temp[crew.temperature];

        for(int i = crew.Traits.Count; i < Trait_List.Length; i++)
        {
            Trait_List[i].SetActive(false);
        }


        for(int i = 0; i < crew.Traits.Count; i++)
        {
            Image trait_image = Trait_List[i].GetComponent<Image>();

            trait_image.sprite = crew.Traits[i].icon;
        }

        switch(crew.CREW_MOVE)
        {
            case CREW_MOVEMENT.WALK:
                moveset.sprite = movesetsprite[0];

                movesetdesc.text = "°È±â\n<color=blue>ÀÌµ¿¼Óµµ 0.25</color>";
                break;

            case CREW_MOVEMENT.SKI:
                moveset.sprite = movesetsprite[1];

                movesetdesc.text = "½ºÅ°\n<color=blue>ÀÌµ¿¼Óµµ 0.4</color>";
                break;

            case CREW_MOVEMENT.SKI_UPGRADE:
                moveset.sprite = movesetsprite[3];

                movesetdesc.text = "½ºÅ°(°­È­µÊ)\n<color=blue>ÀÌµ¿¼Óµµ 0.55</color>";
                break;
            case CREW_MOVEMENT.PONY:
                moveset.sprite = movesetsprite[2];

                movesetdesc.text = "Á¶¶û¸»\n<color=blue>ÀÌµ¿¼Óµµ 0.5</color>";
                break;
            case CREW_MOVEMENT.DOG_SLED:
                moveset.sprite = movesetsprite[4];
                movesetdesc.text = "°³½ä¸Å\n<color=blue>ÀÌµ¿¼Óµµ 0.55</color>";
                break;
            case CREW_MOVEMENT.BEAR_SLED:
                moveset.sprite = movesetsprite[6];
                movesetdesc.text = "°õ½ä¸Å\n<color=blue>ÀÌµ¿¼Óµµ 0.75</color>";
                break;
            case CREW_MOVEMENT.SNOWMOBILE:
                moveset.sprite = movesetsprite[5];
                movesetdesc.text = "½º³ë¸ðºô\n<color=blue>ÀÌµ¿¼Óµµ 0.75</color>";
                break;
        }

        switch(crew.CREW_CLOTH)
        {
            case CREW_CLOTH.NAKED:
                cloth.sprite = null;
                clothdesc.text = "<color=red>¹æÇÑº¹ ¾øÀ½!</color>";
                break;

            case CREW_CLOTH.FUR1:
                cloth.sprite = clothsprite[1];
                clothdesc.text = "¸ðÇÇ¿Ê\n<color=orange>¼Õ»óµÊ</color>";
                break;

            case CREW_CLOTH.FUR2:
                cloth.sprite = clothsprite[1];
                clothdesc.text = "¸ðÇÇ¿Ê\n<color=yellow>±¦ÂúÀ½</color>";
                break;
            case CREW_CLOTH.FUR3:
                cloth.sprite = clothsprite[1];
                clothdesc.text = "¸ðÇÇ¿Ê\n<color=blue>¾çÈ£ÇÔ</color>";
                break;

            case CREW_CLOTH.WOOL1:
                cloth.sprite = clothsprite[0];
                clothdesc.text = "¾ç¸ð¿Ê\n<color=orange>¼Õ»óµÊ</color>";
                break;
            case CREW_CLOTH.WOOL2:
                cloth.sprite = clothsprite[0];
                clothdesc.text = "¾ç¸ð¿Ê\n<color=yellow>±¦ÂúÀ½</color>";
                break;
            case CREW_CLOTH.WOOL3:
                cloth.sprite = clothsprite[0];
                clothdesc.text = "¾ç¸ð¿Ê\n<color=blue>¾çÈ£ÇÔ</color>";
                break;
        }
    }
}
