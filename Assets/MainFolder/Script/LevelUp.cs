using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
        Debug.Log(items.Length);
        foreach (Item item in items)
        {
            Debug.Log(item.name);
        }
    }

    public void Show()
    {
        Next(); 
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }


    public void hide() { 
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        /*문제*/items[index].OnClick();//@@@@@
    }

    void Next()
    {
        //1. 모든 아이템 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        //2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true)
        {
            
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            {
                break;
            }
        }
        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            //3. 만랩이된 무기는 안뜨도록 -> 소비아이템으로 대체 소비아이템이 여러개 있으면 
            if ( ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true); //으면 items[Random.Range(4,7)].gameObject.SetActive(true); //12- 3936
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
