using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Expedition : MonoBehaviour
{
    public TextMeshProUGUI day;

    private int currentdays = 1, days = 1, month = 11;
    private float progress = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        day.text = month + "/" + days + "\n" + currentdays + "days";
    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExpeditionRoundEnd(ExpeditionCrew crew)
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



    public void TtemBbang()
    {

    }
}

