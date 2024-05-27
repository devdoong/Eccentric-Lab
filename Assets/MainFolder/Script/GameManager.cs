using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# GameControl")]
    public float gameTime;//���ӽð�
    public float maxGameTime = 2*10f;//���ӳ��ð� 

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; //���� ����

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


