using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom_UI_Sc : MonoBehaviour
{
    private bool UI_pos = false;//true = above false = bellow
    [SerializeField]
    private GameObject UI;
    //private RectTransform UI;



    private void Start()
    {
        //anim = GetComponent<Animator>();
    }
    public void UI_Animation()
    {
        RectTransform pos = UI.GetComponent<RectTransform>();
        if(!UI_pos)
        {
            pos.anchoredPosition = new Vector2(0, 0);
            UI_pos = true;
            Core.core.aud.AudioPlay(0);
        }
        else
        {
            pos.anchoredPosition = new Vector2(0, -1250);
            UI_pos = false;
            Core.core.aud.AudioPlay(0);
        }
    }
}
