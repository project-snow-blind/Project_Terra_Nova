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

        
        hp.text = "<color=red>ü��</color>\n" + MathF.Round(crew.HP) + " / " + MathF.Round(crew.HPMAX);
        hunger.text = "<color=#B47021>���</color>\n" + MathF.Round(crew.hunger) + " / " + MathF.Round(crew.hungerMAX);
        morale.text = "<color=#008600>�ŷڵ�</color>\n" + MathF.Round(crew.morale) + " / " + MathF.Round(crew.moraleMAX);
        speed.text = "<color=#8290DB>�̵��ӵ�</color>\n" + crew.speed;

        switch(crew.temperature)
        {
            case 1:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "���� �������� ����(1)";

                break;

            case 2:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "�ɰ��ϰ� ��������(2)";

                break;

            case 3:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "���� ���� ������(3)";

                break;

            case 4:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "�߿��ϴ� ��(4)";

                break;

            case 5:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "����(5)";

                break;

            case 6:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "�����ϴ� ��(6)";

                break;

            case 7:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "���� �긲(7)";

                break;
            case 8:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "���� �޾ƿ���(8)";

                break;
            case 9:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "�ҵ��̰���(9)";

                break;
            case 10:
                temperature.text = "<color=#EC1F85>ü��</color>\n" + "���� �������� ����(10)";

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

                movesetname.text = "�ȱ�";
                break;

            case CREW_MOVEMENT.SKI:
                Moveset.sprite = movesetandclothsprite[1];

                movesetname.text = "��Ű";
                break;

            case CREW_MOVEMENT.SKI_UPGRADE:
                Moveset.sprite = movesetandclothsprite[3];

                movesetname.text = "��Ű(��ȭ��)";
                break;
            case CREW_MOVEMENT.PONY:
                Moveset.sprite = movesetandclothsprite[2];

                movesetname.text = "������";
                break;
            case CREW_MOVEMENT.DOG_SLED:
                Moveset.sprite = movesetandclothsprite[4];
                movesetname.text = "�����";
                break;
            case CREW_MOVEMENT.BEAR_SLED:
                Moveset.sprite = movesetandclothsprite[6];
                movesetname.text = "�����";
                break;
            case CREW_MOVEMENT.SNOWMOBILE:
                Moveset.sprite = movesetandclothsprite[5];
                movesetname.text = "������";
                break;
        }

        switch (crew.CREW_CLOTH)
        {
            case CREW_CLOTH.NAKED:
                cloth.sprite = null;
                clothname.text = "<color=red>���Ѻ� ����!</color>";
                break;

            case CREW_CLOTH.FUR1:
                cloth.sprite = movesetandclothsprite[8];
                clothname.text = "���ǿ�";
                break;

            case CREW_CLOTH.FUR2:
                cloth.sprite = movesetandclothsprite[8];
                clothname.text = "���ǿ�";
                break;
            case CREW_CLOTH.FUR3:
                cloth.sprite = movesetandclothsprite[8];
                clothname.text = "���ǿ�";
                break;

            case CREW_CLOTH.WOOL1:
                cloth.sprite = movesetandclothsprite[7];
                clothname.text = "����";
                break;
            case CREW_CLOTH.WOOL2:
                cloth.sprite = movesetandclothsprite[7];
                clothname.text = "����";
                break;
            case CREW_CLOTH.WOOL3:
                cloth.sprite = movesetandclothsprite[7];
                clothname.text = "����";
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
                trait_list_text.text = "��� Ư��";
                foreach (Trait_data traits in crew.Traits)
                {
                    if (traits.categrory == trait_category.CR)
                    {
                        traitlist.Add(traits);
                    }
                }
                break;

            case 1://eq
                trait_list_text.text = "��� Ư��";
                foreach (Trait_data traits in crew.Traits)
                {
                    if (traits.categrory == trait_category.EQ)
                    {
                        traitlist.Add(traits);
                    }
                }
                break;

            case 2://ev
                trait_list_text.text = "�̺�Ʈ Ư��";
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
            stylename.text = "�̵� ���";
            stylecontainerarr[0].SetActive(true);

            if (!crew.Traits.Exists(x => x.name == "������ ������"))
            {
                if (crew.Traits.Exists(x => x.name == "��Ű �����") || crew.Traits.Exists(x => x.name == "��Ű è�Ǿ�") || crew.Traits.Exists(x => x.name == "�븣���� ��Ű è�Ǿ�"))
                {
                    if (crew.Traits.Exists(x => x.name == "��Ű"))
                    {
                        stylecontainerarr[1].SetActive(true);
                    }
                    else if (crew.Traits.Exists(x => x.name == "��Ű(��ȭ��)"))
                    {
                        stylecontainerarr[2].SetActive(true);
                    }
                }
                if (crew.Traits.Exists(x => x.name == "������"))
                {
                    stylecontainerarr[6].SetActive(true);
                }
            }
            if (crew.Traits.Exists(x => x.name == "������") && Expedition.instance.storage_check("���", 2))
            {
                if (Expedition.instance.trait_checker("�� ������") || Expedition.instance.trait_checker("������ ���"))
                {
                    stylecontainerarr[3].SetActive(true);
                }
            }

            if (crew.Traits.Exists(x => x.name == "��� ��") && Expedition.instance.storage_check("���", 1))
            {
                if (Expedition.instance.trait_checker("�� ������"))
                {
                    stylecontainerarr[4].SetActive(true);
                }
            }
            if (crew.Traits.Exists(x => x.name == "��� ��") && Expedition.instance.storage_check("���", 5))
            {
                if (Expedition.instance.trait_checker("�� ���û�"))
                {
                    stylecontainerarr[5].SetActive(true);
                }
            }

        }
        else
        {
            stylename.text = "���Ѻ�";
            if (crew.Traits.Exists(x => x.name == "������ ��� ��"))
            {
                stylecontainerarr[8].SetActive(true);
            }
            if (crew.Traits.Exists(x => x.name == "������ ���� ��"))
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
                    Trait_st trait = new Trait_st("������ ���ǿ�", false);
                    target_crew.CrewTrait_Effect_Function(trait);
                }
                
                break;
            case 8:
                if(export.CREW_CLOTH != CREW_CLOTH.WOOL3)
                {
                    target_crew.cloth_change(CREW_CLOTH.WOOL3);
                    Trait_st trait = new Trait_st("������ ����", false);
                    target_crew.CrewTrait_Effect_Function(trait);
                }
                
                break;
        }
        UIupdate(target_crew);
        Core.core.aud.AudioPlay(0);
        Core.core.Crew_Statue_Changed();
    }
}
