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
        if (!GameManager.instance.isLive)
            return;
        switch (id)
        {
            case 0: //근접 회전 무기
                
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
    }

    public void LevelUp(float damage, int count)
    {
        this.damage *= damage * Character.Damage;
        this.count += count;

        if (id == 0) Batch();

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);

    }

    public void Init(ItemData data)
     {
         //Item Data 만져줄거임 지금부터
         //Basic Set 기본 세팅
         name = "Weapon " + data.itemId;
         transform.parent = player.transform; //플레이어의 자식으로 등록됨
         transform.localPosition = Vector3.zero; //플레이어의 내에서 0으로 좌표를 맞춤

         //Property Set id,damage,count  등등
         id = data.itemId;
         damage = data.baseDamage * Character.Damage;
         count = data.baseCount + Character.Count;

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
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;

             default:
                speed = 0.5f * Character.WeaponRate;
                break;
         }

        //Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

         //어플라이기어라는 함수를 가지고 있는 모두에게 이걸 실행해줘
         player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

     }

    void Batch()
    {
        Debug.Log("배치실행");
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

            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); //-100 관통 무한

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
        }
    }

    void Fire()
    {
        Debug.Log("총실행");
        if (!player.EnemyScanner.nearestTarget)
            return;

        Vector3 targetPos = player.EnemyScanner.nearestTarget.position; //타겟 포지션
        Vector3 dir = targetPos - transform.position; //방향 구하기 타겟포지션 - 내 포지션
        dir = dir.normalized; //벡터 크기 정규화 (방향은 유지)




        Transform bullet = GameManager.instance.pool.Get(prefabId/*2*/).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//목표 방향으로 로테이션 //축을 전달해줘야함
        bullet.GetComponent<Bullet>().Init(damage, count/*남은관통수*/, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
