using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rigid.position; //���� = Ÿ�� ��ġ - ����ġ
        Vector2 nextVec = dirVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        //���Ͱ� �÷��̾� �Ѿư���

    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
        //���Ͱ� �÷��̾� �ٶ󺸱�
    }
}
