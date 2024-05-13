using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom_UI_Sc : MonoBehaviour
{
    private bool UI_pos;//true = above false = bellow
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void UI_Animation()
    {
        if(!UI_pos)
        {
            anim.SetTrigger("bottom_UI");
            UI_pos = false;
        }
        else
        {
            anim.SetTrigger("bottom_UI");
            UI_pos = true;
        }
    }
}
