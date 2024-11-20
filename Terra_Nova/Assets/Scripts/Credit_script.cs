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
        desc.text = "전진한 거리<br><color=yellow>115%</color><br>소요 시간<br><color=blue>" + infos.currentday + "</color><br>현재 날짜<br><color=#EF8BC8>" + infos.month + "월" + infos.day + "일</color><br>생존자 수<br><color=red>없음</color>";
        string temp = "";
        switch(infos.difficult)
        {
            case 0:
                temp = "<color=#007D00>쉬움</color>";
                break;

            case 1:
                temp = "<color=#0000FF>보통</color>";
                break;

            case 2:
                temp = "<color=#FF0000>어려움</color>";
                break;

            case 3:
                temp = "<color=#5A0000>테라노바</color>";
                break;
        }
        int temp2 = infos.currentday / 15;
        temp2 *= -1;

        int temp3 = temp2 + infos.additional_score;
        desc2.text = "<color=yellow>결산</color><br>난이도 : " + temp + "<br>시간 소모에 따른 점수 : " + temp2 + "<br>아이템 점수 : " + infos.additional_score + "<br><br>총점 : " + temp3;
    }

    public void returntotitle()
    {
        Core.core.aud.AudioPlay(0);
        SceneManager.LoadScene("Title_Scene");
    }
}
