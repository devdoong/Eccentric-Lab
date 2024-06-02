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

        Vector2 dirVec = target.position - rigid.position; //���� = Ÿ�� ��ġ - ����ġ
        Vector2 nextVec = dirVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        //���Ͱ� �÷��̾� �Ѿư���

    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
        //���Ͱ� �÷��̾� �ٶ󺸱�
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;//��Ȱ��ȭ
        rigid.simulated = true; //������ �ùķ��̼� �����ڳ� �ȵ����ڳ�.
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType]; //�ִϸ��̼� ������ ����.
        speed = data.speed;
        maxHealth = data.health;
        health = data.health; 
    }

    void OnTriggerEnter2D(Collider2D collision) //�ι� Ʈ���Ź߻��ؼ� ������� ���޾� ����Ǵ°� �����ؾ���
    {
        if (!collision.CompareTag("Bullet") || !isLive /*��� ������ ���޾� ����Ǵ°��� ����*/)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            //.. Live ����, Hit Action
            anim.SetTrigger("Hit");
        }
        else
        {
            //..Die
            isLive = false;
            coll.enabled = false;//��Ȱ��ȭ
            rigid.simulated = false; //������ �ùķ��̼� �����ڳ� �ȵ����ڳ�.
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }

        IEnumerator KnockBack()
        {
            yield return wait; //���� �ϳ��� ���� �����ӱ��� ������

            //�÷��̾��� ��ġ �ҷ����� ����(����)�� ��ġ ���ؿ�
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse); //�˹�

        }

        void Dead()
        {
            gameObject.SetActive(false); //�ϴ� �ı��� �ƴ� ��Ȱ��ȭ�� ������
        }
    }
}
 