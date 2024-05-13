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
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ��ġ�� ��ǥ ������
            Ray2D ray = new Ray2D(wp, Vector2.zero); // �������� ��ġ�� ��ǥ �������� Ray�� ��

            float distance = Mathf.Infinity; // Ray ������ ������ �ִ� �Ÿ�

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance); // �� ���� 
            RaycastHit2D hitDrawer = Physics2D.Raycast(ray.origin, ray.direction, distance, 1 << LayerMask.NameToLayer("drawer")); // drawer ���̾ ����

            if (hit)
            { // ���� ���ȭ�� ���� ������ �� ������ �߻��ϴ� ���� �����ϱ� ���� �κ�
                if (hitDrawer)
                { // drawer ������ ��
                    if (!drawer.GetComponent<drawercontroller>().IsOpen())
                    {
                        Debug.Log("������ ����");
                        drawer.GetComponent<drawercontroller>().Open();
                    }
                    else drawer.GetComponent<drawercontroller>().Close();
                }
            }
        }*/
        //��ó: https://holika.tistory.com/entry/��������-����-01-Ray2D��-����-2D-��ġ�̺�Ʈ-���� [Uing? Uing!!:Ƽ���丮]
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
        //120�� ������
    }

    public void PopUpExit()
    {
        PopUp.SetActive(false);
        Curtain.alpha = 0f;
        aud.AudioPlay(0);
    }
}
