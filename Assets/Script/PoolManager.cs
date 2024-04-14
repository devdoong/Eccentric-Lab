using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class PoolManager : MonoBehaviour
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

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //1. ������ Ǯ�� ��� �ִ� (��Ȱ��ȭ) ���� ������Ʈ�� ã�´�.
            //ã���� select�� ����
        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //��� Ȱ��ȭ �Ǿ� �ִٸ�? (��ã�Ҵٸ�)
        // �ű� ������Ʈ ������ select�� �־���.
        if (select == null)
        {
            select = Instantiate(EnemyPrefeb[index], transform);
            pools[index].Add(select);
        }
        return select;
    }

    //�� �ΰ��� 1��1 ���迩���Ѵ�

}
