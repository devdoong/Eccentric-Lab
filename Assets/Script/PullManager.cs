using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    public GameObject[] EnemyPrefeb;
    //�����յ��� ������ ����
    List<GameObject>[] pools;
    //Ǯ ����� �ϴ� ����Ʈ��


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

    //�� �ΰ��� 1��1 ���迩���Ѵ�

}
