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

        //foreach (Item_data item in data.Items)
        //{
        //    if (item.icon == null)
        //    {
        //        switch (trait.categrory)
        //        {
        //            case trait_category.CR:
        //                trait.icon = Backup[0];
        //                trait.big_icon = Backup[0];
        //                break;

        //            case trait_category.EQ:
        //                trait.icon = Backup[1];
        //                trait.big_icon = Backup[1];
        //                break;

        //            case trait_category.EV:
        //                trait.icon = Backup[2];
        //                trait.big_icon = Backup[2];
        //                break;
        //        }
        //    }

        //}
        itemlist = data.Items;
    }
    //[SerializeField]
    //public List<Item_SO> items = new List<Item_SO>();

    //private void Awake()
    //{
    //    instance = this;
    //}

    //public Item_SO GetItem(string name)
    //{
    //    return items.Where(x => x.Name == name).FirstOrDefault();
    //}
}
