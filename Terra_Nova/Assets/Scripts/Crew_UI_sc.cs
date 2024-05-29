using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Crew_UI_sc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI names, hp, hunger, trust;
    //public TextMeshProUGUI text2;
    //[SerializeField]
    //private GameObject PopUp;
    [SerializeField]
    private int crewnumber = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        List<ExpeditionCrew> check = Core.core.Crew_Read_All();
        
        if(check.Count - 1 < crewnumber)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            UIupdatecrewinfo();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIupdatecrewinfo()
    {
        ExpeditionCrew crew = Core.core.Crew_Read(crewnumber);
        names.text = crew.CREW_NAME;
        hp.text = "HP : " + crew.HP.ToString() + "/" + crew.HPMAX.ToString();
        hunger.text = "HUN : " + crew.hunger.ToString() + "/" + crew.hungerMAX.ToString();
        trust.text = "TRU : " + crew.morale.ToString() + "/" + crew.moraleMAX.ToString();
    }
    
}
