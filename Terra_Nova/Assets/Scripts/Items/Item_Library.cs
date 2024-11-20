using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Item_Library : MonoBehaviour
{
    public static Item_Library instance;
    [SerializeField]
    public List<Item_data> itemlist = new List<Item_data>();
    public ItemData data;

    private void Awake()
    {
        //if (instance != this)
        //{
        //    Destroy(this);
        //}
        instance = this;
        Read_Item_data();
        //DontDestroyOnLoad(transform.gameObject);

    }

    public Item_data GetItem(string name)
    {
        return itemlist.Where(x => x.name == name).FirstOrDefault();
    }

    public void Read_Item_data()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Items", "Itemdata.json");
        string jsonData = File.ReadAllText(path);

        data = JsonUtility.FromJson<ItemData>(jsonData);

        foreach (Item_data item in data.Items)
        {
            item.icon = Resources.Load<Sprite>("Sprites/Item_Icons/" + item.path);
            item.big_icon = Resources.Load<Sprite>("Sprites/Item_Icons/" + item.path);
        }
        itemlist = data.Items;
    }
}
