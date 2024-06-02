using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    public float speed;
    public EnemyScanner EnemyScanner; //scanner
    public Hand[] hands;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator Animator;
    


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        EnemyScanner = GetComponent<EnemyScanner>();
        hands = GetComponentsInChildren<Hand>(true);
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
        Animator.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
            spriteRenderer.flipX = inputVec.x < 0; 
    }
}
