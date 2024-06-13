using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPopUp_Sc : MonoBehaviour
{
    [SerializeField]
    private Image bkg;
    [SerializeField]
    private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bkgswap(int i)
    {
        bkg.sprite = sprites[i];
    }
}
