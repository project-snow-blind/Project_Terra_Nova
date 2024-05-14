using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core core;

    ExpeditionCrew[] Core_Crew = new ExpeditionCrew[4];
    private void Awake()
    {
        core = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Crew_update(int i, ExpeditionCrew newcrew)
    {
        Core_Crew[i] = newcrew;
    }
    public ExpeditionCrew Ttembang3(int i)
    {
        ExpeditionCrew ttembangcrew = Core_Crew[i];

        return ttembangcrew;
    }
}
