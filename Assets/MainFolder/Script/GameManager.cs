using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# GameControl")]
    public float gameTime;//게임시간
    public float maxGameTime = 2*10f;//게임끝시간 
    public bool isLive;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; //임의 설정

    [Header("# Gmae Object")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;/// <summary>
    /// 
    /// </summary>

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
        ///*문제*/uiLevelUp.Select(0);//@@@@@ Ui레벨업은 레벨업 클래스의 -> Select 함수를 지니고 있음 (기본아이템 주기)
        GetExp();
    }

    void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp >= nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    } 
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}


