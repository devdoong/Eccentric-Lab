using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;//���ӽð�
    public float maxGameTime = 2*10f;//���ӳ��ð� 
     
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
}


