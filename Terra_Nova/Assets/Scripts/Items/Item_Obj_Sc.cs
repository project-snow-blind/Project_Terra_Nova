using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Item_Obj_Sc : MonoBehaviour
{
    //[SerializeField]
    public string Name;
    //[SerializeField]
    public Image image;
    public TextMeshProUGUI text;
    public void setinfo(Item_data data)
    {
        Name = data.name;
        text.text = data.name + "\n" + "x" + data.have;
        image.sprite = data.icon;
    }

    public void buttonfunction()
    {
        Expedition.instance.iteminfosetting(Name);
    }
}
