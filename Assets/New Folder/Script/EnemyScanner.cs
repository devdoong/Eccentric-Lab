using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    //�÷��̾ ������Ʈ�� �����Ͽ����ϴ�

    public float scanRange = 5.0f; //��ĵ����
    public LayerMask targetLayer; //Ÿ�� ���
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void Awake()
    {
        targetLayer = LayerMask.GetMask("Enemy");
    }

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll
             (transform.position /*�÷��̾� ������*/,
             scanRange /*��ĵ����*/,
             Vector2.zero /*ĳ���ù���*/,
             0 /*ĳ���ñ���*/,
             targetLayer/*Ÿ�ٴ��*/);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()//���� ����� Ÿ�� ��ġ ��ȯ
    {
        Transform result = null;

        float closestDistance = Mathf.Infinity; // �ʱ� �Ÿ��� ���Ѵ�� ����

        foreach (RaycastHit2D target in targets) 
        {
            Vector3 myPos = transform.position; //�÷��̾� ��ġ �����ͼ�
            Vector3 targetPos = target.transform.position; //Ÿ�ٰŸ��� �����ͼ�
            float curDistance = Vector3.Distance( myPos, targetPos ); //�Ÿ��� ��

            if ( curDistance < closestDistance) //�Ÿ��� ����� ���� ���� �����ֺ��� �� ������
            {
                closestDistance = curDistance; //��ü
                result = target.transform; //���� �� ���� ����� �ָ� ��ȯ�Ұ���
            }
        }

        return result; //���� �ּ��� �Ÿ��� �ִ� Ÿ���� ����.
    }
}
