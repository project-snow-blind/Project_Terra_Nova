using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Events : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�ؿ��� �ܾ�°�

    //�������ΰ�
    //�̺�Ʈ�� mtthó�� Ȯ������ ���Խ�Ŵ
    //�³��� ������ �̺�Ʈ ����Ʈ�� ����
    //�� ���ϰ� ���� ������ �� �ȿ��� ������ �ϳ� ����
    //��������ŭ �迭���� ������ ������ �� ���� �Ǵ� �迭�� ��÷
    float Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
    //private void 

}
