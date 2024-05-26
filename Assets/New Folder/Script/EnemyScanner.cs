using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    //플레이어에 컴포넌트를 적용하였습니다

    public float scanRange = 5.0f; //스캔범위
    public LayerMask targetLayer; //타겟 대상
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void Awake()
    {
        targetLayer = LayerMask.GetMask("Enemy");
    }

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll
             (transform.position /*플레이어 포지션*/,
             scanRange /*스캔범위*/,
             Vector2.zero /*캐스팅방향*/,
             0 /*캐스팅길이*/,
             targetLayer/*타겟대상*/);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()//가장 가까운 타겟 위치 반환
    {
        Transform result = null;

        float closestDistance = Mathf.Infinity; // 초기 거리를 무한대로 설정

        foreach (RaycastHit2D target in targets) 
        {
            Vector3 myPos = transform.position; //플레이어 위치 가져와서
            Vector3 targetPos = target.transform.position; //타겟거리도 가져와서
            float curDistance = Vector3.Distance( myPos, targetPos ); //거리를 잼

            if ( curDistance < closestDistance) //거리를 잰것이 지금 가장 가까운애보다 더 가까우면
            {
                closestDistance = curDistance; //교체
                result = target.transform; //지금 그 가장 가까운 애를 반환할것임
            }
        }

        return result; //가장 최소의 거리에 있는 타겟을 리턴.
    }
}
