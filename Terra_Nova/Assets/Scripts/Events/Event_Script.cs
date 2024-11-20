using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Event_Script : MonoBehaviour
{
    public Eventdata eventdata,temp;

    //[ContextMenu("To Json Data")]
    //void SaveTraitData()
    //{
    //    string JsonData = JsonUtility.ToJson(eventdata, true);
    //    string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Events", "Eventdata.json");
    //    File.WriteAllText(path, JsonData);

    //    path = Path.Combine(Application.dataPath + "/Resources/JsonDat/backup", "Eventdata.json");
    //    File.WriteAllText(path, JsonData);
    //}

    //[ContextMenu("From Json Data")]
    //void LoadTraitData()
    //{
    //    string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Events", "Eventdata.json");
    //    string jsondata = File.ReadAllText(path);

    //    eventdata = JsonUtility.FromJson<Eventdata>(jsondata);
    //}


    [ContextMenu("To Json Data")]
    void Save_Event_data()
    {
        
        foreach(Event_data data in eventdata.eventdatas)
        {
            string Jsondata = JsonUtility.ToJson(data, true);
            string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Events", data.Event_Id + ".json");
            File.WriteAllText(path, Jsondata);
        }
    }
    //void SaveTraitData()
    //{
    //    string JsonData = JsonUtility.ToJson(eventdata, true);
    //    string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Events", "Eventdata.json");
    //    File.WriteAllText(path, JsonData);

    //    path = Path.Combine(Application.dataPath + "/Resources/JsonDat/backup", "Eventdata.json");
    //    File.WriteAllText(path, JsonData);
    //}

    [ContextMenu("From Json Data")]
    void LoadEventData()
    {
        string[] path = System.IO.Directory.GetFiles(Application.dataPath +  "/Resources/JsonDat/Events", "*.json");
        temp = new Eventdata();

        foreach (string path2 in path)
        {
            string JsonData = File.ReadAllText(path2);

            temp.eventdatas.Add(JsonUtility.FromJson<Event_data>(JsonData));
        }

        eventdata = temp;
        //string jsondata = File.ReadAllText(path);

        //eventdata = JsonUtility.FromJson<Eventdata>(jsondata);
    }
}
[System.Serializable]
public class Event_data
{
    [Header("Id")]
    public string Event_Id;
    [Header("event tag")]
    public List<EVENT_TAG> tags;
    public Sprite Event_Picture;
    public string path;
    [Header("event name and description")]
    public string Event_Name, Event_Desc;
    [Header("Effects")]
    public List<Effect> effects = new List<Effect>();
    public triggers event_trigger;
    //public List<triggers> triggers = new List<triggers>();
    public float eventchance;
    public bool fire_only_once;
    public List<change_variable> event_chance_var = new List<change_variable>();
    public List<EventOption> eventoptions = new List<EventOption>();
    //public trait_category categrory;

    //public List<Modifier> modifiers = new List<Modifier>();

    //public Sprite icon, big_icon;
    //public string name, description;
    //public bool unique;
}
[System.Serializable]
public struct change_variable
{
    public triggers trigger;
    public float value;
}

[System.Serializable]
public class Eventdata
{
    public List<Event_data> eventdatas;
}
[System.Serializable]
public class EventOption
{
    public string name, desc;
    public List<Effect> effects = new List<Effect>();
    public triggers trigger;
    public bool Randomized;
}
[System.Serializable]
public struct Effect
{
    public triggers target;
    public bool randomtarget;
    public List<Stat_st> stateffect;
    public List<Trait_st> traiteffect;
    public List<Item_st> itemeffect;
    public List<string> event_effect;
}
[System.Serializable]
public struct Stat_st
{
    public string stat;
    public float value;
    
    public Stat_st(string name, float var)
    {
        stat = name;
        value = var;
    }
    
}
[System.Serializable]
public struct Item_st
{
    public string name;
    public int stack;

    public Item_st(string name, int stck)
    {
        this.name = name;
        stack = stck;
    }
}
[System.Serializable]
public struct stat_trigger
{
    public Stat_st stat;
    public bool over_or_below;//true == over;
}
[System.Serializable]
public struct Trait_st
{
    public string name;
    public bool b;

    public Trait_st(string name, bool b = true)
    {
        this.name = name;
        this.b = b;
    }
}
[System.Serializable]
public struct Item_trigger
{
    public string name;
    public int stack;
    public bool b;
}
[System.Serializable]
public struct triggers
{
    public List<int> targetID;
    public List<stat_trigger> stat_trigger;
    public List<Trait_st> Trait_trigger;
    public List<geograph> geograph_trigger;
    [Range(0f, 1f)]
    public List<float> progress_trigger;
    public bool trigger_only,rest_event;
    public List<bool> is_evening;
    public List<int> daycheck;
    public List<Item_trigger> item_limit;
    public triggers(List<int> id = default,  List<stat_trigger> st = default, List<Trait_st> trait = default, List<Item_trigger> item = default)
    {
        if (id != null)
        {
            targetID = id;
        }
        else
            targetID = null;
        if (st != null)
        {
            stat_trigger = st;
        }
        else
            stat_trigger = null;
        if(trait != null)
        {
            Trait_trigger = trait;
        }
        else
        {
            Trait_trigger = null;
        }

        if (item != null)
        {
            item_limit = item;
        }
        else
            item_limit = null;
        geograph_trigger = null;
        progress_trigger = null;
        trigger_only = false;
        rest_event = false;
        is_evening = null;
        daycheck = null;
    }
}

public enum EVENT_TAG
{
    HISTORICAL,
    GOOD,
    BAD,

    //지형 태그
    GLACIER,//빙붕
    MOUNTAIN,//산
    PLATEAU,//고원
    GOAL,//남극점 부근

    //행동 태그
    REST,
    SCOUT,
    MAINTANANCE
}