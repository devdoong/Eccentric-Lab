using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //개선사항 메모
    // Spawn함수를 건드려서
    // Update를 통해 시간을 측정하고
    // 특정 시간에 도달하면 spawnPoint 배열을 쭉 돌며 몹을 주르륵 소환
    
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
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0,spawnPoint.Length)].position; //GetComponentsInChildren 이게 자기 자신도 포함이라서 0은 건너뛰고 1부터
    }
}
