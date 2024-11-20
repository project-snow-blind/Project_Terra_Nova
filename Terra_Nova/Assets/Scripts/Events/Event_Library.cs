using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class Event_Library : MonoBehaviour
{
    public static Event_Library instance;

    [SerializeField]
    public List<Event_data> All_Events = new List<Event_data>();
    public Eventdata tempevent;
    private void Awake()
    {
        //if(instance != this)
        //{
        //    Destroy(this);
        //}
        instance = this;
        Read_Event_data();
        //DontDestroyOnLoad(transform.gameObject);
    }

    public Event_data GetEvent(string name)
    {
        return All_Events.Where(x => x.Event_Name == name).FirstOrDefault();
    }

    public void Read_Event_data()
    {
        string[] path = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/JsonDat/Events", "*.json");
        //Eventdata tempdata = new Eventdata();

        foreach (string path2 in path)
        {
            string JsonData = File.ReadAllText(path2);

            tempevent.eventdatas.Add(JsonUtility.FromJson<Event_data>(JsonData));
        }
        //string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Events", "Eventdata.json");
        //string jsonData = File.ReadAllText(path);

        //tempevent = tempdata;

        foreach(Event_data eventdata in tempevent.eventdatas)
        {
            eventdata.Event_Picture = Resources.Load<Sprite>("Sprites/Event_Images/" + eventdata.path);
        }

        All_Events = tempevent.eventdatas;
    }

    //void LoadEventData()
    //{
        

    //    //string jsondata = File.ReadAllText(path);

    //    //eventdata = JsonUtility.FromJson<Eventdata>(jsondata);
    //}
    //public void Read_trait_data()
    //{
    //    string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Traits", "Traitdata.json");
    //    string jsonData = File.ReadAllText(path);

    //    Traitdata = JsonUtility.FromJson<TraitData>(jsonData);

    //    foreach (Trait_data trait in Traitdata.Traits)
    //    {

    //        trait.icon = Resources.Load<Sprite>("Sprites/Trait_Images/" + trait.path);
    //        trait.big_icon = Resources.Load<Sprite>("Sprites/Trait_Images/" + trait.path);
    //    }
    //    Traits = Traitdata.Traits;
    //}
}
