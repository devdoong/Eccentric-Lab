using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;
    //개선사항 메모
    // Spawn함수를 건드려서
    // Update를 통해 시간을 측정하고
    // 특정 시간에 도달하면 spawnPoint 배열을 쭉 돌며 몹을 주르륵 소환
    
    float timer;
    int level;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length-1); //floorto 소수 버림.

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0); //그냥 Enemy라는 프리팹을 계속 들고오고 Spawn()함수에서
        /*어디에*/enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1,spawnPoint.Length)].position; //GetComponentsInChildren 이게 자기 자신도 포함이라서 0은 건너뛰고 1부터
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


[System.Serializable] // 직렬화. 인스펙터에 아래처럼 복잡한 구조는 친절히 안보여 주기 때매 직접 연동해줘야함.
public class SpawnData
{
    
    public float spawnTime;//스폰 간격

    public int spriteType; //몬스터의 타입 1.좀비 2.해골 같이 ㅇㅇ
    public int health;
    public float speed;
}
