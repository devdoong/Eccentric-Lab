using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
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

        Debug.Log(pools.Length);


    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        return select;
    }

    //위 두개는 1대1 관계여야한다

}
