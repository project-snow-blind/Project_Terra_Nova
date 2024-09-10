using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Setting_script : MonoBehaviour
{
    public static Option_Setting_script settings;
    public float volume_audio = 1f,volume_bgm = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        settings = this;
    }
        void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
