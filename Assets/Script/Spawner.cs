using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    //�������� �޸�
    // Spawn�Լ��� �ǵ����
    // Update�� ���� �ð��� �����ϰ�
    // Ư�� �ð��� �����ϸ� spawnPoint �迭�� �� ���� ���� �ָ��� ��ȯ
    
    float timer;
    int level;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f); //floorto �Ҽ� ����.

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0); //�׳� Enemy��� �������� ��� ������� Spawn()�Լ�����
        /*���*/enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0,spawnPoint.Length)].position; //GetComponentsInChildren �̰� �ڱ� �ڽŵ� �����̶� 0�� �ǳʶٰ� 1����
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


[System.Serializable] // ����ȭ. �ν����Ϳ� �Ʒ�ó�� ������ ������ ģ���� �Ⱥ��� �ֱ� ���� ���� �����������.
public class SpawnData
{
    
    public float spawnTime;//���� ����

    public int spriteType; //������ Ÿ�� 1.���� 2.�ذ� ���� ����
    public int health;
    public float speed;
}