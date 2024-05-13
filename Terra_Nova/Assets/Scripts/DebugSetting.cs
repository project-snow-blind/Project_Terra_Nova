using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetting : MonoBehaviour
{
    private void creation()
    {
        ExpeditionCrew edward_evans;
        edward_evans.CREW_ID = 0;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //Core.core.GetComponent<Core>().Crew_update();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ///
    /*
     public struct ExpeditionCrew
{
    public int CREW_ID;
    public CREW_MOVEMENT crew_move;
    public CREW_CLOTH crew_cloth;
    public EQUIPMENT_TRAIT[] equipment_trait;
    public EVENT_TRAIT[] event_trait;
    public CREW_TRAIT[] crew_trait;
    public string CREW_NAME;
    public float HPMAX, HP, hungerMAX, hunger, moraleMAX, morale, temperature;
}
     */
}
