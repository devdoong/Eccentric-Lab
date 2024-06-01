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

    Player player;
    void Awake()
    {
       player = GameManager.instance.player;
    }

    public float getValue()
    {
        return speed;
    }

    float timer;

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0: //���� ȸ�� ����
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;


            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        /*//�׽�Ʈ��
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10*//*Damage*//*, 1*//*count*//*);
        }
        //*/
    }

    public void LevelUp(float damage, int count)
    {
        this.damage *= damage;
        this.count += count;

        if (id == 0) Batch();

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);

    }

    public void Init(ItemData data)
    {
        //Item Data �����ٰ��� ���ݺ���
        //Basic Set �⺻ ����
        name = "Weapon " + data.itemId;
        transform.parent = player.transform; //�÷��̾��� �ڽ����� ��ϵ�
        transform.localPosition = Vector3.zero; //�÷��̾��� ������ 0���� ��ǥ�� ����

        //Property Set id,damage,count  ���
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.instance.pool.EnemyPrefeb.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.EnemyPrefeb[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;


            default:
                speed = 0.85f;
                break;
        }
        //���ö��̱���� �Լ��� ������ �ִ� ��ο��� �̰� ��������
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);

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

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); //-1 ���� ����
        }
    }

    void Fire()
    {
        if (!player.EnemyScanner.nearestTarget)
            return;

        Vector3 targetPos = player.EnemyScanner.nearestTarget.position; //Ÿ�� ������
        Vector3 dir = targetPos - transform.position; //���� ���ϱ� Ÿ�������� - �� ������
        dir = dir.normalized; //���� ũ�� ����ȭ (������ ����)




        Transform bullet = GameManager.instance.pool.Get(prefabId/*2*/).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//��ǥ �������� �����̼� //���� �����������
        bullet.GetComponent<Bullet>().Init(damage, count/*���������*/, dir);
    }
}
