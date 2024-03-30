using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    Rigidbody2D rigid;
    public float speed;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        /*//1.힘을준다
        rigid.AddForce(inputVec);

        //2. 속도제어
        rigid.velocity = inputVec;*/

        //3. 위치이동

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

    }
}
