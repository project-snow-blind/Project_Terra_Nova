using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trait_Info_Button : MonoBehaviour
{
    [SerializeField]
    public Trait_data trait_info;
    [SerializeField]
    public Image trait_image;
    //public int i = 0;
    // Start is called before the first frame update
    public void set_trait(Trait_data crewinfo)
    {
        trait_info = crewinfo;
        trait_image.sprite = trait_info.icon;
    }

    public void trait_info_button()
    {
        Crew_Info_UI_sc parents = GetComponentInParent<Crew_Info_UI_sc>();
        string trait_name = trait_info.name;
        parents.trait_info_button(trait_name);
    }
}
