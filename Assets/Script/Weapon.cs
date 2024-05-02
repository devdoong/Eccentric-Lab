using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId; //풀메니저에있는 몇번째 프리팹
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);


                break;


            default:
                break;
        }

        //테스트용
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 2);
        }
        //
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) Batch();



    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();

                break;


                default:
                break;
        }

        
    }
    void Batch()
    {
        for (int index = 0; index < count; index++)
        {//레벨업 하니까 추가된 bullet의 갯수가 이상함. 가지고 있던 bullet 재사용해주는 코드임.
            Transform bullet;
            if(index < transform.childCount) 
            {
                bullet = transform.GetChild(index); 
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; //자식 bullet 생성
            }
               


            //레벨업하니까 추가된 bullet의 위치가 안좋음. 개선코드
            bullet.localPosition = Vector3.zero; //플레이어의 위치 
            bullet.localRotation = Quaternion.identity; //회전값 초기화

            //bullet count만큼 이쁘게 생성.
            Vector3 rotateVec = Vector3.forward * 360 * index / count; //bulelt 갯수 만큼 나눠줌
            bullet.Rotate(rotateVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); //-1 관통 무한
        }
    }
}
