using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //�������� �޸�
    // Spawn�Լ��� �ǵ����
    // Update�� ���� �ð��� �����ϰ�
    // Ư�� �ð��� �����ϸ� spawnPoint �迭�� �� ���� ���� �ָ��� ��ȯ
    
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(UnityEngine.Random.Range(0,2));
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0,spawnPoint.Length)].position; //GetComponentsInChildren �̰� �ڱ� �ڽŵ� �����̶� 0�� �ǳʶٰ� 1����
    }
}
