using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    float timer;
    int phase; // ���� ������
    int lastPhase = -1; // ������ ������, �ʱⰪ�� -1�� ����

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); 
        GameManager.instance.maxGameTime = 180f; // ��ü �ð��� 3������ ����
        levelTime = GameManager.instance.maxGameTime / (180f / 15f); // 30�ʸ��� ����� ������ ���� ���
    }

    void Start()
    {
        InvokeRepeating("RandomCount", 0.4f, 1.0f);
        InvokeRepeating("RepeatMethod", 1.0f, GetEnemiesToSpawnCount(phase)); // phase�� ���� ������ ���� ���� �����ϴ� �Լ�
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        phase = Mathf.FloorToInt(GameManager.instance.gameTime / 15f); // 30�� �������� ������ ���
        
    }

    void RepeatMethod()
    {
        Spawn(phase);
        Spawn(phase);
        Spawn(phase);

        if (phase >= 3 && phase < 6)
        {
            Spawn(phase);
            Spawn(phase);
            Spawn(phase);
            Spawn(9);
            Spawn(phase);
            Spawn(9);
            Spawn(phase);
            Spawn(phase);
            Spawn(9);
        }
        if (phase > 6)
        {
            foreach (Transform transform in spawnPoint)
            {
                Spawn(9);
                RandomCount();
                Spawn(3);
            }

            for (int i = 0;i < 10; i++)
            {
                Spawn(Random.Range(1, 10));
                Spawn(Random.Range(5, 10));
                Spawn(Random.Range(1, 10));
            }
        }
    }

    void Spawn(int currentPhase)
    {
        Transform spawningPoint;
        spawningPoint = GetRandomSpawnPoint();
        Spawning(spawningPoint, currentPhase);
    }

    int RandomCount()
    {
        return Random.Range(1,10);
    }

    Transform GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(1, spawnPoint.Length); // 0��°�� Spawner �ڽ��̹Ƿ� ����
        return spawnPoint[randomIndex];
    }

    void Spawning(Transform point, int currentPhase)
    {
        float setspeed = 1f;
        float sethealth = 1;
        int setspriteType = 0;

        // ������� ���� ����
        switch (currentPhase)
        {
            case 0:
            case 1:
                Debug.Log("0");
                setspeed = 2.0f;
                sethealth = 1;
                setspriteType = 0;
                break;
            case 2:
                Debug.Log("2");
                setspeed = 2.0f;
                sethealth = 1;
                setspriteType = 1;
                break;
            case 3:
                Debug.Log("3");
                setspeed = 4.0f;
                sethealth = 0.1f;
                setspriteType = 2;
                break;
            case 4:

                Debug.Log("4");
                setspeed = 1.0f;
                sethealth = 1.0f;

                setspriteType = 3;
                break;
            case 5:
                Debug.Log("5");
                setspeed = 1.0f;
                sethealth = 1.0f;

                setspriteType = 3;
                break;
            case 6:
                Debug.Log("3");
                setspeed = 3.0f;
                sethealth = 0.1f;
                setspriteType = 2;
                break;
            case 7:
                Debug.Log("7");
                setspeed = 1.0f;
                sethealth = 1.0f;

                setspriteType = 3;
                break;
            case 8:
                Debug.Log("8");
                setspeed = 3.0f;
                sethealth = 0.1f;
                setspriteType = 2;
                break;
            case 9:
                Debug.Log("9");
                setspeed = 1.0f;
                sethealth = 1.0f;

                setspriteType = 3;
                break;
            case 10:
                Debug.Log("10");
                setspeed = 1.3f;
                sethealth = 1.0f;

                setspriteType = 3;
                break;
            default:
                Debug.Log("����Ʈ");
                setspeed = 1.3f;
                sethealth = 1.0f;
                setspriteType = 3;
                break;
        }

        GameObject enemy = GameManager.instance.pool.Get(0); // Enemy �������� ��� ������ Spawn()�Լ�����
        enemy.transform.position = point.position; //����

        enemy.GetComponent<Enemy>().Init(new SpawnData { 
            spawnTime = spawnData[currentPhase % spawnData.Length].spawnTime, 
            spriteType = setspriteType, 
            health = (spawnData[currentPhase % spawnData.Length].health) * sethealth,
            speed = (spawnData[currentPhase % spawnData.Length].speed) * setspeed 
        }
        );
    }

    float GetEnemiesToSpawnCount(int phase)
    {
        // phase�� ���� ������ ���� ���� ��ȯ
        switch (phase)
        {
            case 0:
                return 3; // phase 0������ 1����
            case 1:
                return 2; // phase 1������ 2����
            case 2:
                return 1; // phase 2������ 3����
            case 3:
                return 2; // phase 0������ 4����
            case 4:
                return 1; // phase 1������ 5����
            case 5:
                return 0.5f; // phase 2������ 6����
            case 6:
                return 0.3f; // phase 0������ 7����
            case 7:
                return 0.3f; // phase 1������ 8����
            case 8:
                return 0.1f; // phase 2������ 3����
            // �ʿ��� ��ŭ �߰�
            default:
                return 0.3f;
        }
    }
}

[System.Serializable] // ����ȭ. �ν����Ϳ� �Ʒ�ó�� ������ ������ ģ���� �Ⱥ��� �ֱ� ������ ���� ��������� ��.
public class SpawnData
{
    public float spawnTime; // ���� ����
    public int spriteType;  // ������ Ÿ�� 1.���� 2.�ذ� ����
    public float health;
    public float speed;
}
