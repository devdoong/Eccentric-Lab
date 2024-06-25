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
    public void Init(float damage, int per, Vector3 shootTarget) 
    {
        this.damage = damage;
        this.per = per;//per ����

        if (per >=0 ) //������ ������ �ƴѰ�. (����ȸ���� -1�� �־���)
        {
            rigid.velocity = shootTarget * 10f; //velocity ���� �ӵ�. 
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision) //Ʈ���� �۵���.
    {
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
        per--;

        if (per < 0)
        {
            rigid.velocity = Vector3.zero;//��Ȱ���� ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false); //������Ʈ Ǯ������ ����ҰŶ� ��Ƽ�길 ��Ȱ��ȭ
 
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false); //�÷��̾� ���� ����� ��Ȱ��ȭ
    }

}
