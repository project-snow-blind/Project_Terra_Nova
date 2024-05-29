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
        
        //text.text = Core.core.Crew_Read(0).CREW_NAME;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Core.core.Ttembang3(0).CREW_NAME);
    }
    
    public void TtemBang3()
    {
        PopUp.SetActive(true);
        text2.text = Core.core.Crew_Read(0).CREW_NAME + "\n" + "HP : " + Core.core.Crew_Read(0).HP + "/" + Core.core.Crew_Read(0).HPMAX;
        Core.core.aud.AudioPlay(0);
    }

    
}
