using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Option_Setting_script : MonoBehaviour
{
    public static Option_Setting_script settings;
    public int difficult = 0;
    public float volume_audio = 1f,volume_bgm = 1f;

    [SerializeField]
    private Slider audioslider, bgmslider;
    // Start is called before the first frame update
    private void Awake()
    {
        if(settings != null)
        {
            Destroy(this);
            
        }
        settings = this;
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void difficult_change(int i)
    {
        difficult = i;
    }

    public int difficult_output()
    {
        return difficult;
    }
    //public void volume_set(float var, bool b)
    //{
    //    if(b == true)
    //    {
    //        volume_bgm = var;
    //    }
    //    else
    //    {
    //        volume_audio = var;
    //    }
    //}
    public void volumereorg()
    {
        //GameObject Volumeslider = GameObject.Find("Volume_slider");
        //GameObject Bgmslider = GameObject.Find("Bgm_slider");

        //audioslider = Volumeslider.GetComponent<Slider>();
        //bgmslider = Bgmslider.GetComponent<Slider>();
        //audioslider.onValueChanged.AddListener(volume_setting);
        //bgmslider.onValueChanged.AddListener(bgm_setting);
    }
    //public void volume_setting(float value)
    //{
    //    volume_audio = value;
    //    Core.core.aud.volumechange();
    //}
    //public void bgm_setting(float value)
    //{
    //    volume_bgm = value;
    //    Core.core.BGM.volumechange();
    //}
}
