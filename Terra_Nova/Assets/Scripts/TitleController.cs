using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    Button Start;
    [SerializeField]
    Button Load;
    [SerializeField]
    Button Exit;
    [SerializeField]
    CanvasGroup Curtain;
    [SerializeField]
    GameObject QuitAlert;
    [SerializeField]
    GameObject PopUp;

    private Touch touch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction;

    [SerializeField]
    AudioSc aud;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Touch()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치한 좌표 가져옴
            Ray2D ray = new Ray2D(wp, Vector2.zero); // 원점에서 터치한 좌표 방향으로 Ray를 쏨

            float distance = Mathf.Infinity; // Ray 내에서 감지할 최대 거리

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance); // 다 잡음 
            RaycastHit2D hitDrawer = Physics2D.Raycast(ray.origin, ray.direction, distance, 1 << LayerMask.NameToLayer("drawer")); // drawer 레이어만 잡음

            if (hit)
            { // 게임 배경화면 등이 눌렸을 때 에러가 발생하는 것을 방지하기 위한 부분
                if (hitDrawer)
                { // drawer 눌렀을 때
                    if (!drawer.GetComponent<drawercontroller>().IsOpen())
                    {
                        Debug.Log("열려라 참깨");
                        drawer.GetComponent<drawercontroller>().Open();
                    }
                    else drawer.GetComponent<drawercontroller>().Close();
                }
            }
        }*/
        //출처: https://holika.tistory.com/entry/유어테일-개발-01-Ray2D를-통한-2D-터치이벤트-구현 [Uing? Uing!!:티스토리]
        TouchFind();
        //Ray2D ray = new Ray2D(touchEndPosition,)
    }

    private void TouchFind()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                touchEndPosition = touch.position;
            }
        }
    }

    public void Startbutton()
    {
        aud.AudioPlay(0);
        SceneManager.LoadScene("ExpScene");
        
    }

    public void Loadbutton()
    {
        PopUp.SetActive(true);
        Curtain.alpha = 0.75f;
        aud.AudioPlay(0);

    }
    
    public void ExitButton()
    {
        QuitAlert.SetActive(true);
        Curtain.alpha = 0.75f;
        aud.AudioPlay(0);
    }

    public void ExitButton2(bool b)
    {
        if(b == true)
        {
            Application.Quit();
        }
        else if(b == false)
        {
            QuitAlert.SetActive(false);
            Curtain.alpha = 0f;
            aud.AudioPlay(0);
        }
    }
    public void OptionButton()
    {
        PopUp.SetActive(true);
        Curtain.alpha = 0.75f;
        aud.AudioPlay(0);
        //120이 적정값
    }

    public void PopUpExit()
    {
        PopUp.SetActive(false);
        Curtain.alpha = 0f;
        aud.AudioPlay(0);
    }
}
