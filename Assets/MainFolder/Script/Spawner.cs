using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    float timer;
    int phase; // 현재 페이즈
    int lastPhase = -1; // 마지막 페이즈, 초기값은 -1로 설정

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); 
        GameManager.instance.maxGameTime = 180f; // 전체 시간을 3분으로 설정
        levelTime = GameManager.instance.maxGameTime / (180f / 15f); // 30초마다 페이즈를 나누기 위해 계산
    }

    void Start()
    {
        InvokeRepeating("RandomCount", 0.4f, 1.0f);
        InvokeRepeating("RepeatMethod", 1.0f, GetEnemiesToSpawnCount(phase)); // phase에 따른 생성할 적의 수를 결정하는 함수
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        phase = Mathf.FloorToInt(GameManager.instance.gameTime / 15f); // 30초 간격으로 페이즈 계산
        
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
        int randomIndex = Random.Range(1, spawnPoint.Length); // 0번째는 Spawner 자신이므로 제외
        return spawnPoint[randomIndex];
    }

    void Spawning(Transform point, int currentPhase)
    {
        float setspeed = 1f;
        float sethealth = 1;
        int setspriteType = 0;

        // 페이즈별로 설정 변경
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
                Debug.Log("디폴트");
                setspeed = 1.3f;
                sethealth = 1.0f;
                setspriteType = 3;
                break;
        }

        GameObject enemy = GameManager.instance.pool.Get(0); // Enemy 프리팹을 계속 들고오고 Spawn()함수에서
        enemy.transform.position = point.position; //생성

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
        // phase에 따른 생성할 적의 수를 반환
        switch (phase)
        {
            case 0:
                return 3; // phase 0에서는 1마리
            case 1:
                return 2; // phase 1에서는 2마리
            case 2:
                return 1; // phase 2에서는 3마리
            case 3:
                return 2; // phase 0에서는 4마리
            case 4:
                return 1; // phase 1에서는 5마리
            case 5:
                return 0.5f; // phase 2에서는 6마리
            case 6:
                return 0.3f; // phase 0에서는 7마리
            case 7:
                return 0.3f; // phase 1에서는 8마리
            case 8:
                return 0.1f; // phase 2에서는 3마리
            // 필요한 만큼 추가
            default:
                return 0.3f;
        }
    }
}

[System.Serializable] // 직렬화. 인스펙터에 아래처럼 복잡한 구조는 친절히 안보여 주기 때문에 직접 연동해줘야 함.
public class SpawnData
{
    public float spawnTime; // 스폰 간격
    public int spriteType;  // 몬스터의 타입 1.좀비 2.해골 같이
    public float health;
    public float speed;
}
