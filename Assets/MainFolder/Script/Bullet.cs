using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 shootTarget) //per 관통
    {
        this.damage = damage;
        this.per = per;

        if (per > -1) //관통이 무한이 아닌것. (근접회전은 -1을 주었음)
        {
            rigid.velocity = shootTarget * 10f; //velocity 물리 속도. 
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision) //트리거 작동시.
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;
        per--;

        if (per == -1)
        {
            rigid.velocity = Vector3.zero;//비활전에 물리 속도 초기화
            gameObject.SetActive(false); //오브젝트 풀링으로 사용할거라 액티브만 비활성화
 
        }

    }

}
