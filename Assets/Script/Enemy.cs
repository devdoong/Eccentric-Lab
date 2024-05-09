using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;
    //123123
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rigid.position; //방향 = 타겟 위치 - 몹위치
        Vector2 nextVec = dirVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        //몬스터가 플레이어 쫓아가기

    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
        //몬스터가 플레이어 바라보기
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType]; //애니메이션 가지고 오기.
        speed = data.speed;
        maxHealth = data.health;
        health = data.health; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;
        Debug.Log(this.health);

        if (health > 0)
        {
            //.. Live 유무, Hit Action

        }
        else
        {
            //..Die
            Dead();
        }

        void Dead()
        {
            gameObject.SetActive(false); //일단 파괴가 아닌 비활성화로 돌려줌
            
        }
    }
}
 