using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Credit_script : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title, desc, desc2;
    public creditinfo infos;

    public void getcreditinfo(creditinfo info)
    {
        infos = info;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetinfotoText()
    {
        desc.text = "������ �Ÿ�<br><color=yellow>115%</color><br>�ҿ� �ð�<br><color=blue>" + infos.currentday + "</color><br>���� ��¥<br><color=#EF8BC8>" + infos.month + "��" + infos.day + "��</color><br>������ ��<br><color=red>����</color>";
        string temp = "";
        switch(infos.difficult)
        {
            case 0:
                temp = "<color=#007D00>����</color>";
                break;

            case 1:
                temp = "<color=#0000FF>����</color>";
                break;

            case 2:
                temp = "<color=#FF0000>�����</color>";
                break;

            case 3:
                temp = "<color=#5A0000>�׶���</color>";
                break;
        }
        int temp2 = infos.currentday / 15;
        temp2 *= -1;

        int temp3 = temp2 + infos.additional_score;
        desc2.text = "<color=yellow>���</color><br>���̵� : " + temp + "<br>�ð� �Ҹ� ���� ���� : " + temp2 + "<br>������ ���� : " + infos.additional_score + "<br><br>���� : " + temp3;
    }

    public void returntotitle()
    {
        Core.core.aud.AudioPlay(0);
        SceneManager.LoadScene("Title_Scene");
    }
}
