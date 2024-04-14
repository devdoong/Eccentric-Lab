using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class PoolManager : MonoBehaviour
{
    public GameObject[] EnemyPrefeb;
    //프리팹들을 보관할 변수
    List<GameObject>[] pools;
    //풀 담당을 하는 리스트들


    private void Awake()
    {
       pools = new List<GameObject>[EnemyPrefeb.Length];
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //1. 선택한 풀에 놀고 있는 (비활성화) 게임 오브젝트를 찾는다.
            //찾으면 select에 저장
        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //모두 활성화 되어 있다면? (못찾았다면)
        // 신규 오브젝트 생성후 select에 넣어줌.
        if (select == null)
        {
            select = Instantiate(EnemyPrefeb[index], transform);
            pools[index].Add(select);
        }
        return select;
    }

    //위 두개는 1대1 관계여야한다

}
