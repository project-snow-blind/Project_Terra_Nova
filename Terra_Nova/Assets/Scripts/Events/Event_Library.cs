using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using System.IO;
public class Event_Library : MonoBehaviour
{
    public static Event_Library instance;

    [SerializeField]
    public List<Event_data> All_Events = new List<Event_data>();

    private void Awake()
    {
        //if(instance != this)
        //{
        //    Destroy(this);
        //}
        instance = this;
        //DontDestroyOnLoad(transform.gameObject);
    }

    public Event_data GetEvent(string name)
    {
        return All_Events.Where(x => x.Event_Name == name).FirstOrDefault();
    }

    
}
