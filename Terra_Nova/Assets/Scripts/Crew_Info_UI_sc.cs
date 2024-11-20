using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Crew_Info_UI_sc : MonoBehaviour
{
    [SerializeField]
    private Image Portrait, Moveset, cloth,trait_image;
    [SerializeField]
    private TextMeshProUGUI Name, hp, hunger, morale,speed,temperature, movesetname, clothname,trait_list_text,trait_name,trait_desc,stylename;
    [SerializeField]
    private Sprite[] movesetandclothsprite;
    [SerializeField]
    private GameObject contents,traitcontainer,style_container;
    [SerializeField]
    private GameObject[] stylecontainerarr;
    [SerializeField]
    private Trait_Info_Button prefab;

    private int statue = 0;
    //private bool trait_or_style = false;//default = trait
    
    private ExpeditionCrew backup;

    [SerializeField]
    private Crew_Sc target_crew;
    //private Trait_data save;
    //private List<Trait_Info_Button> backup2 = new List<Trait_Info_Button>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIupdate(Crew_Sc crew_new)
    {
        target_crew = crew_new;
        crew_new.Export_Crew_Const();
        ExpeditionCrew crew = crew_new.export;
        Name.text = crew.CREW_NAME;

        
        hp.text = "<color=red>체력</color>\n" + MathF.Round(crew.HP) + " / " + MathF.Round(crew.HPMAX);
        hunger.text = "<color=#B47021>허기</color>\n" + MathF.Round(crew.hunger) + " / " + MathF.Round(crew.hungerMAX);
        morale.text = "<color=#008600>신뢰도</color>\n" + MathF.Round(crew.morale) + " / " + MathF.Round(crew.moraleMAX);
        speed.text = "<color=#8290DB>이동속도</color>\n" + crew.speed;

        switch(crew.temperature)
        {
            case 1:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "더는 움직이지 않음(1)";

                break;

            case 2:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "심각하게 떨고있음(2)";

                break;

            case 3:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "몸을 떨기 시작함(3)";

                break;

            case 4:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "추워하는 중(4)";

                break;

            case 5:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "정상(5)";

                break;

            case 6:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "더워하는 중(6)";

                break;

            case 7:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "땀을 흘림(7)";

                break;
            case 8:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "몸이 달아오름(8)";

                break;
            case 9:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "불덩이같음(9)";

                break;
            case 10:
                temperature.text = "<color=#EC1F85>체온</color>\n" + "더는 움직이지 않음(10)";

                break;
        }

        if (crew.portrait < 100)
        {
            Portrait.sprite = Core.core.Crew_Portrait_Set[crew.portrait];
        }
        else
        {
            Portrait.sprite = Core.core.Unique_Crew_Portrait_Set[crew.portrait - 100];
        }

        //for (int i = crew.Traits.Count; i < Trait_List.Length; i++)
        //{
        //    Trait_List[i].SetActive(false);
        //}


        //for (int i = 0; i < crew.Traits.Count; i++)
        //{
        //    Image trait_image = Trait_List[i].GetComponent<Image>();

        //    trait_image.sprite = crew.Traits[i].icon;
        //}

        switch (crew.CREW_MOVE)
        {
            case CREW_MOVEMENT.WALK:
                Moveset.sprite = movesetandclothsprite[0];

                movesetname.text = "걷기";
                break;

            case CREW_MOVEMENT.SKI:
                Moveset.sprite = movesetandclothsprite[1];

                movesetname.text = "스키";
                break;

            case CREW_MOVEMENT.SKI_UPGRADE:
                Moveset.sprite = movesetandclothsprite[3];

                movesetname.text = "스키(강화됨)";
                break;
            case CREW_MOVEMENT.PONY:
                Moveset.sprite = movesetandclothsprite[2];

                movesetname.text = "조랑말";
                break;
            case CREW_MOVEMENT.DOG_SLED:
                Moveset.sprite = movesetandclothsprite[4];
                movesetname.text = "개썰매";
                break;
            case CREW_MOVEMENT.BEAR_SLED:
                Moveset.sprite = movesetandclothsprite[6];
                movesetname.text = "곰썰매";
                break;
            case CREW_MOVEMENT.SNOWMOBILE:
                Moveset.sprite = movesetandclothsprite[5];
                movesetname.text = "스노모빌";
                break;
        }

        switch (crew.CREW_CLOTH)
        {
            case CREW_CLOTH.NAKED:
                cloth.sprite = null;
                clothname.text = "<color=red>방한복 없음!</color>";
                break;

            case CREW_CLOTH.FUR1:
                cloth.sprite = movesetandclothsprite[8];
                clothname.text = "모피옷";
                break;

            case CREW_CLOTH.FUR2:
                cloth.sprite = movesetandclothsprite[8];
                clothname.text = "모피옷";
                break;
            case CREW_CLOTH.FUR3:
                cloth.sprite = movesetandclothsprite[8];
                clothname.text = "모피옷";
                break;

            case CREW_CLOTH.WOOL1:
                cloth.sprite = movesetandclothsprite[7];
                clothname.text = "양모옷";
                break;
            case CREW_CLOTH.WOOL2:
                cloth.sprite = movesetandclothsprite[7];
                clothname.text = "양모옷";
                break;
            case CREW_CLOTH.WOOL3:
                cloth.sprite = movesetandclothsprite[7];
                clothname.text = "양모옷";
                break;
        }
        backup = crew;
        trait_list_gene(crew);
        
    }
    public void tab_swap_button()
    {
        Core.core.aud.AudioPlay(0);
        statue++;
        if(statue == 3)
        {
            statue = 0;
        }
        trait_list_gene(backup);
    }
    private void trait_list_gene(ExpeditionCrew crew)
    {
        if (contents.transform.childCount != 0)
        {
            foreach (Transform child in contents.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List<Trait_data> traitlist = new List<Trait_data>();
        switch (statue)
        {
            case 0://cr
                trait_list_text.text = "대원 특성";
                foreach (Trait_data traits in crew.Traits)
                {
                    if (traits.categrory == trait_category.CR)
                    {
                        traitlist.Add(traits);
                    }
                }
                break;

            case 1://eq
                trait_list_text.text = "장비 특성";
                foreach (Trait_data traits in crew.Traits)
                {
                    if (traits.categrory == trait_category.EQ)
                    {
                        traitlist.Add(traits);
                    }
                }
                break;

            case 2://ev
                trait_list_text.text = "이벤트 특성";
                foreach (Trait_data traits in crew.Traits)
                {
                    if (traits.categrory == trait_category.EV)
                    {
                        traitlist.Add(traits);
                    }
                }
                break;
        }
        foreach (Trait_data traits in traitlist)
        {
            Trait_Info_Button newtrait = Instantiate(prefab, contents.transform);
            newtrait.set_trait(traits);
            
        }
    }

    public void trait_info_button(string name)
    {
        Core.core.aud.AudioPlay(0);
        traitcontainer.SetActive(true);
        style_container.SetActive(false);
        Trait_data trait = Trait_Library.instance.GetTrait(name);
        //save = trait;
        //Debug.Log(save.name);
        trait_name.text = "";
        trait_desc.text = "";
        trait_name.text += trait.name;
        
        trait_image.sprite = trait.big_icon;
        trait_desc.text += trait.description;

        //trait_name.text.Replace("\n", "\n");
        //trait_desc.text.Replace("\\n", "<br>");
    }

    public void trait_or_style_swap(bool b)
    {
        Core.core.aud.AudioPlay(0);
        style_container.SetActive(true);
        traitcontainer.SetActive(false);
        foreach (GameObject obj in stylecontainerarr)
        {
            obj.SetActive(false);
        }
        ExpeditionCrew crew = backup;

        if (b == false)
        {
            stylename.text = "이동 방식";
            stylecontainerarr[0].SetActive(true);

            if (!crew.Traits.Exists(x => x.name == "망가진 스노모빌"))
            {
                if (crew.Traits.Exists(x => x.name == "스키 사용자") || crew.Traits.Exists(x => x.name == "스키 챔피언") || crew.Traits.Exists(x => x.name == "노르웨이 스키 챔피언"))
                {
                    if (crew.Traits.Exists(x => x.name == "스키"))
                    {
                        stylecontainerarr[1].SetActive(true);
                    }
                    else if (crew.Traits.Exists(x => x.name == "스키(강화됨)"))
                    {
                        stylecontainerarr[2].SetActive(true);
                    }
                }
                if (crew.Traits.Exists(x => x.name == "스노모빌"))
                {
                    stylecontainerarr[6].SetActive(true);
                }
            }
            if (crew.Traits.Exists(x => x.name == "조랑말") && Expedition.instance.storage_check("사료", 2))
            {
                if (Expedition.instance.trait_checker("말 사육사") || Expedition.instance.trait_checker("극지의 기사"))
                {
                    stylecontainerarr[3].SetActive(true);
                }
            }

            if (crew.Traits.Exists(x => x.name == "썰매 개") && Expedition.instance.storage_check("사료", 1))
            {
                if (Expedition.instance.trait_checker("개 사육사"))
                {
                    stylecontainerarr[4].SetActive(true);
                }
            }
            if (crew.Traits.Exists(x => x.name == "썰매 곰") && Expedition.instance.storage_check("사료", 5))
            {
                if (Expedition.instance.trait_checker("곰 조련사"))
                {
                    stylecontainerarr[5].SetActive(true);
                }
            }

        }
        else
        {
            stylename.text = "방한복";
            if (crew.Traits.Exists(x => x.name == "여분의 양모 옷"))
            {
                stylecontainerarr[8].SetActive(true);
            }
            if (crew.Traits.Exists(x => x.name == "여분의 모피 옷"))
            {
                stylecontainerarr[7].SetActive(true);
            }
        }
    }

    public void style_change_working(int i)
    {
        target_crew.Export_Crew_Const();
        ExpeditionCrew export = target_crew.export;
        
        switch(i)
        {
            case 0:
                target_crew.move_style_change(CREW_MOVEMENT.WALK);
                break;
            case 1:
                target_crew.move_style_change(CREW_MOVEMENT.SKI);
                break;
            case 2:
                target_crew.move_style_change(CREW_MOVEMENT.SKI_UPGRADE);
                break;
            case 3:
                target_crew.move_style_change(CREW_MOVEMENT.PONY);
                break;
            case 4:
                target_crew.move_style_change(CREW_MOVEMENT.DOG_SLED);
                break;
            case 5:
                target_crew.move_style_change(CREW_MOVEMENT.BEAR_SLED);
                break;
            case 6:
                target_crew.move_style_change(CREW_MOVEMENT.SNOWMOBILE);
                break;
            case 7:
                if (export.CREW_CLOTH != CREW_CLOTH.FUR3)
                {
                    target_crew.cloth_change(CREW_CLOTH.FUR3);
                    Trait_st trait = new Trait_st("여분의 모피옷", false);
                    target_crew.CrewTrait_Effect_Function(trait);
                }
                
                break;
            case 8:
                if(export.CREW_CLOTH != CREW_CLOTH.WOOL3)
                {
                    target_crew.cloth_change(CREW_CLOTH.WOOL3);
                    Trait_st trait = new Trait_st("여분의 양모옷", false);
                    target_crew.CrewTrait_Effect_Function(trait);
                }
                
                break;
        }
        UIupdate(target_crew);
        Core.core.aud.AudioPlay(0);
        Core.core.Crew_Statue_Changed();
    }
}
