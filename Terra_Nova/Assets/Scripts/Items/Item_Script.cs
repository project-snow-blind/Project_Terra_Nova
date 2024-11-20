using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Item_Script : MonoBehaviour
{

    public ItemData itemdata;

    [ContextMenu("To Json Data")]
    void SaveTraitData()
    {
        string JsonData = JsonUtility.ToJson(itemdata, true);
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Items", "Itemdata.json");
        File.WriteAllText(path, JsonData);

        path = Path.Combine(Application.dataPath + "/Resources/JsonDat/backup", "Itemdata.json");
        File.WriteAllText(path, JsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadTraitData()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Items", "Itemdata.json");
        string jsondata = File.ReadAllText(path);

        itemdata = JsonUtility.FromJson<ItemData>(jsondata);


        foreach(Item_data item in itemdata.Items)
        {
            //item.icon = Resources.Load<Sprite>("Sprites/Item_Icons/" + item.path + ".png" );
            //item.big_icon = Resources.Load<Sprite>("Sprites/Item_Icons/" + item.path + ".png");
            item.icon = Resources.Load<Sprite>("Sprites/Item_Icons/" + item.path);
            item.big_icon = Resources.Load<Sprite>("Sprites/Item_Icons/" + item.path);
        }
    }
}


public enum item_category
{
    food,
    equipment,
    others
}
[System.Serializable]
public class Item_data
{
    public item_category category;
    public string path;
    public Sprite icon, big_icon;
    public string name, description;
    public int tier, capacity, have;
    public List<Item_effect> effect;
    public triggers trigger;

    public bool activeable;


    //빠진것 - 효과, 카테고리
}

[System.Serializable]
public class ItemData
{
    public List<Item_data> Items;
}

[System.Serializable]
public struct Item_effect
{
    public List<Stat_st> stateffect;
    public List<Trait_st> traiteffect;
    public List<Item_st> itemeffect;
}