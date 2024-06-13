using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Events : MonoBehaviour
{
    [SerializeField]
    Image img;
    [SerializeField]
    TextMeshProUGUI title, desc;
    [SerializeField]
    List<TextMeshProUGUI> option;
    [SerializeField]
    Sprite[] EventImageset;
    [SerializeField]
    GameObject Contents;
    [SerializeField]
    GameObject OptionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventSetting(Event info)
    {
        title.text = info.event_title;
        desc.text = info.event_desc;
        img.sprite = EventImageset[1];
        GameObject controller = GameObject.Find("GameController");
       // Expedition ctrler = controller.GetComponent<Expedition>();
        foreach(EventOptions option in info.option)
        {
            GameObject opt = Instantiate(OptionPrefab, transform.position, Quaternion.identity);
            opt.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = option.option_title;
            opt.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = option.option_desc;

            opt.GetComponent<Button>().onClick.AddListener( () => controller.GetComponent<Expedition>().OptionBt(option.eventtarget ,option.eventoption));
        }
    }

    private void MScreateevent()
    {
        int i = Random.Range(1, 2);


    }
    //Event Bonobono(1, true,)
    //�ؿ��� �ܾ�°�

    //�������ΰ�
    //�̺�Ʈ�� mtthó�� Ȯ������ ���Խ�Ŵ
    //�³��� ������ �̺�Ʈ ����Ʈ�� ����
    //�� ���ϰ� ���� ������ �� �ȿ��� ������ �ϳ� ����
    //��������ŭ �迭���� ������ ������ �� ���� �Ǵ� �迭�� ��÷
    //float Choose(float[] probs)
    //{

    //    float total = 0;

    //    foreach (float elem in probs)
    //    {
    //        total += elem;
    //    }

    //    float randomPoint = Random.value * total;

    //    for (int i = 0; i < probs.Length; i++)
    //    {
    //        if (randomPoint < probs[i])
    //        {
    //            return i;
    //        }
    //        else
    //        {
    //            randomPoint -= probs[i];
    //        }
    //    }
    //    return probs.Length - 1;
    //}
    //private void 




    
}
