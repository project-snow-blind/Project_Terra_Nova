using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Trait_Script : MonoBehaviour
{


    public TraitData traitdata;

    [ContextMenu("To Json Data")]
    void SaveTraitData()
    {
        string JsonData = JsonUtility.ToJson(traitdata, true);
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Traits", "Traitdata.json");
        File.WriteAllText(path, JsonData);

        path = Path.Combine(Application.dataPath + "/Resources/JsonDat/backup", "Traitdata.json");
        File.WriteAllText(path, JsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadTraitData()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Traits", "Traitdata.json");
        string jsondata = File.ReadAllText(path);

        traitdata = JsonUtility.FromJson<TraitData>(jsondata);
    }
}
[System.Serializable]
public class Trait_data
{
    public trait_category categrory;

    public List<Modifier> modifiers = new List<Modifier>();
    public string path;
    public Sprite icon, big_icon;
    public string name, description;
    public bool unique;
}

[System.Serializable]
public class TraitData
{
    public List<Trait_data> Traits;
}

public enum trait_category
{
    EV,
    CR,
    EQ
}

[System.Serializable]
public struct Modifier
{
    public string statname;
    public float value;
    public bool mult;
}