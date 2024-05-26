using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    Rigidbody2D rigid;
    public float speed;
    SpriteRenderer spriteRenderer;
    Animator Animator;
    public EnemyScanner EnemyScanner;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        EnemyScanner = GetComponent<EnemyScanner>();
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        Animator.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
            spriteRenderer.flipX = inputVec.x < 0; 
    }
}
