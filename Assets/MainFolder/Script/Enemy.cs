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
    WaitForFixedUpdate wait;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position; //방향 = 타겟 위치 - 몹위치
        Vector2 nextVec = dirVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        //몬스터가 플레이어 쫓아가기

    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
        //몬스터가 플레이어 바라보기
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;//비활성화
        rigid.simulated = true; //물리를 시뮬레이션 돌리겠냐 안돌리겠냐.
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType]; //애니메이션 가지고 오기.
        speed = data.speed;
        maxHealth = data.health;
        health = data.health; 
    }

    void OnTriggerEnter2D(Collider2D collision) //두번 트리거발생해서 사망로직 연달아 실행되는것 방지해야함
    {
        if (!collision.CompareTag("Bullet") || !isLive /*사망 로직이 연달아 실행되는것을 방지*/)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            //.. Live 유무, Hit Action
            anim.SetTrigger("Hit");
        }
        else
        {
            //..Die
            isLive = false;
            coll.enabled = false;//비활성화
            rigid.simulated = false; //물리를 시뮬레이션 돌리겠냐 안돌리겠냐.
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }

        IEnumerator KnockBack()
        {
            yield return wait; //다음 하나의 물리 프레임까지 딜레이

            //플레이어의 위치 불러오고 본인(몬스터)의 위치 구해옴
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse); //넉백

        }

        void Dead()
        {
            gameObject.SetActive(false); //일단 파괴가 아닌 비활성화로 돌려줌
        }
    }
}
 