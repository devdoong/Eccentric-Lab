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

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; //임의 설정

    [Header("# Gmae Object")]
    public Player player;
    public PoolManager pool;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
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

        if(exp >= nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}


