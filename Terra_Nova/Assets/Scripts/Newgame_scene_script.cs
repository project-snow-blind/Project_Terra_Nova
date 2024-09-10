using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Newgame_scene_script : MonoBehaviour
{
    [SerializeField]
    private int difficult = 0;
    [SerializeField]
    private GameObject[] diffdesc;
    [SerializeField]
    private Button button1, button2;
    private AudioSc aud;
    [SerializeField]
    private GameObject Alert, wipalert;
    //private AudioSc BGM;
    // [SerializeField]
    //private Button Start;
    // Start is called before the first frame update
    void Start()
    {
        GameObject audioobj = GameObject.Find("Audio Source");
        aud = audioobj.GetComponent<AudioSc>();
        //GameObject audioobj2 = GameObject.Find("BGM Source");
        //BGM = audioobj2.GetComponent<AudioSc>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void return_to_title()
    {
        aud.AudioPlay(0);
        SceneManager.LoadScene("Title_Scene");

    }

    public void Game_Start()
    {
        aud.AudioPlay(0);
        SceneManager.LoadScene("ExpScene");

    }

    public void difficult_change(bool b)
    {
        if (b == true)
        {
            aud.AudioPlay(0);
            if (difficult > 0)
                difficult--;

            if(difficult == 0)
            {
                button1.gameObject.SetActive(false);
            }
        }
        else if (b == false)
        {
            aud.AudioPlay(0);
            if (difficult < 3)
                difficult++;
            if (difficult == 3)
            {
                button2.gameObject.SetActive(false);
            }
        }
        difficult_desc_change();

        if(difficult != 0)
        {
            button1.gameObject.SetActive(true);
        }
        if(difficult != 3)
        {
            button2.gameObject.SetActive(true);
        }
    }
    private void difficult_desc_change()
    {
        foreach(GameObject obj in diffdesc)
        {
            if(obj.activeSelf == true)
                obj.SetActive(false);
        }

        diffdesc[difficult].SetActive(true);
        diffdesc[difficult].SetActive(true);
    }

    public void diffselection()
    {
        if(difficult == 3)
        {
            Alert.SetActive(true);
        }
        else
        {
            wipalert.SetActive(true);
        }
        aud.AudioPlay(0);
    }
    public void alertpopup(bool b)
    {
        if(b == true)
        {
            Alert.SetActive(false);
        }
        else
        {
            Game_Start();
        }
        aud.AudioPlay(0);
    }

    public void wipalert_disable()
    {
        aud.AudioPlay(0);
        wipalert.SetActive(false);
    }
}
