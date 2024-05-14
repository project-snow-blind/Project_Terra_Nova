using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugSetting : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    [SerializeField]
    GameObject PopUp;
    [SerializeField]
    AudioSc aud;
    [SerializeField]
    CanvasGroup Curtain;
    private void creation()
    {
        ExpeditionCrew edward_evans;
        edward_evans.CREW_ID = 0;
        
    }
    private void Awake()
    {
        GameObject audioobj = GameObject.Find("Audio Source");
        aud = audioobj.GetComponent<AudioSc>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Core.core.Crew_update(0, TtemBang2());
        //Core.core.GetComponent<Core>().Crew_update();
        text.text = Core.core.Ttembang3(0).CREW_NAME;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Core.core.Ttembang3(0).CREW_NAME);
    }

    ExpeditionCrew TtemBang2()
    {
        ExpeditionCrew Oates;
        Oates.CREW_ID = 0;
        Oates.crew_move = CREW_MOVEMENT.WALK;
        Oates.crew_cloth = CREW_CLOTH.WOOL;
        Oates.equipment_trait = null;
        Oates.event_trait = null;
        Oates.crew_trait = null;
        Oates.CREW_NAME = "L. Oates";
        Oates.HPMAX = 100;
        Oates.HP = 100;
        Oates.hungerMAX = 100;
        Oates.hunger = 100;
        Oates.moraleMAX = 100;
        Oates.morale = 100;
        Oates.temperature = 36f;
        return Oates;
    }

    public void PopUpExit()
    {
        PopUp.SetActive(false);
        Curtain.alpha = 0f;
        aud.AudioPlay(0);
    }
    public void TtemBang3()
    {
        PopUp.SetActive(true);
        text2.text = Core.core.Ttembang3(0).CREW_NAME + "\n" + "HP : " + Core.core.Ttembang3(0).HP + "/" + Core.core.Ttembang3(0).HPMAX;
        aud.AudioPlay(0);
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
