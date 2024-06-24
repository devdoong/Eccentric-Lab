using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    public float speed;
    public EnemyScanner EnemyScanner; //scanner
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    


    void Awake()//
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        EnemyScanner = GetComponent<EnemyScanner>();
        hands = GetComponentsInChildren<Hand>(true);
        Debug.Log(anim);
    }

    void OnEnable()
    {
        gameObject.SetActive(true);
        speed *= Character.Speed; 
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];


    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        anim.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
            spriteRenderer.flipX = inputVec.x < 0; 
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
