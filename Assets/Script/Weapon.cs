using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId; //Ǯ�޴������ִ� ���° ������
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

        //�׽�Ʈ��
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
        {//������ �ϴϱ� �߰��� bullet�� ������ �̻���. ������ �ִ� bullet �������ִ� �ڵ���.
            Transform bullet;
            if(index < transform.childCount) 
            {
                bullet = transform.GetChild(index); 
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; //�ڽ� bullet ����
            }
               


            //�������ϴϱ� �߰��� bullet�� ��ġ�� ������. �����ڵ�
            bullet.localPosition = Vector3.zero; //�÷��̾��� ��ġ 
            bullet.localRotation = Quaternion.identity; //ȸ���� �ʱ�ȭ

            //bullet count��ŭ �̻ڰ� ����.
            Vector3 rotateVec = Vector3.forward * 360 * index / count; //bulelt ���� ��ŭ ������
            bullet.Rotate(rotateVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); //-1 ���� ����
        }
    }
}
