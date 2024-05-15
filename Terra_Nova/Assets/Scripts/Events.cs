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

    //밑에는 긁어온거

    //생각중인것
    //이벤트에 mtth처럼 확률값을 포함시킴
    //걔내를 가능한 이벤트 리스트에 넣음
    //싹 더하고 총합 낸다음 그 안에서 랜덤값 하나 뽑음
    //랜덤값만큼 배열에서 빼내고 랜덤값 다 쓰게 되는 배열이 당첨
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
