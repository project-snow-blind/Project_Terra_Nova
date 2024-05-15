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
    CanvasGroup Curtain;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        ExpeditionCrew Oates = new ExpeditionCrew(0, CREW_MOVEMENT.WALK, CREW_CLOTH.WOOL, "L. Oates", 100, 100, 100, 36f);
        Core.core.Crew_update(0, Oates);
        text.text = Core.core.Crew_Read(0).CREW_NAME;
        Core.core.BGM.AudioPlay(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Core.core.Ttembang3(0).CREW_NAME);
    }
    public void PopUpExit()
    {
        PopUp.SetActive(false);
        Curtain.alpha = 0f;
        Core.core.aud.AudioPlay(0);
    }
    public void TtemBang3()
    {
        PopUp.SetActive(true);
        text2.text = Core.core.Crew_Read(0).CREW_NAME + "\n" + "HP : " + Core.core.Crew_Read(0).HP + "/" + Core.core.Crew_Read(0).HPMAX;
        Core.core.aud.AudioPlay(0);
    }
}
