using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
public class Trait_Library : MonoBehaviour
{
    public static Trait_Library instance;

    [SerializeField]
    public List<Trait_data> Traits = new List<Trait_data>();
    public Sprite[] Backup;
    public TraitData Traitdata;
    private void Awake()
    {
        instance = this;
        Read_trait_data();;
    }

    public Trait_data GetTrait(string name)
    {
        Trait_data output = Traits.Where(x => x.name == name).FirstOrDefault();


        if(output.icon == null)
        {
            switch(output.categrory)
            {
                case trait_category.CR:
                    output.icon = Backup[0];
                    output.big_icon = Backup[0];
                    break;

                case trait_category.EQ:
                    output.icon = Backup[1];
                    output.big_icon = Backup[1];
                    break;

                case trait_category.EV:
                    output.icon = Backup[2];
                    output.big_icon = Backup[2];
                    break;
            }
        }

        //return Traits.Where(x => x.name == name).FirstOrDefault();
        return output;
    }


    public void Read_trait_data()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/JsonDat/Traits", "Traitdata.json");
        string jsonData = File.ReadAllText(path);

        Traitdata = JsonUtility.FromJson<TraitData>(jsonData);

        //foreach(Trait_data trait in Traitdata.Traits)
        //{
        //    if (trait.icon == null)
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

        foreach (Trait_data trait in Traitdata.Traits)
        {
            
            trait.icon = Resources.Load<Sprite>("Sprites/Trait_Images/" + trait.path);
            trait.big_icon = Resources.Load<Sprite>("Sprites/Trait_Images/" + trait.path);
        }
        Traits = Traitdata.Traits;
    }


}
