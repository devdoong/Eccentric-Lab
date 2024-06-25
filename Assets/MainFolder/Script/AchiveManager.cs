using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNoitce;

    enum Achive { UnlockPotato, UnlockBean }

    Achive[] achives;
    WaitForSecondsRealtime wait;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData")) //최초실행 확인
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData",1);

        foreach ( Achive achive in achives )
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            /*lockCharacter[index].SetActive(!isUnlock);*/
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
            foreach (Achive achive in achives)
        {
            CheckAchieve(achive);
        }
    }

    void CheckAchieve(Achive achive)
    {
        bool isAchieve = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchieve = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;

        }

        if (isAchieve && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for (int index = 0; index < uiNoitce.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNoitce.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNoitce.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;

        uiNoitce.SetActive(false);
    }
}
