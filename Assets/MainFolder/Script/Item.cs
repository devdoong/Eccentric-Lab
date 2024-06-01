using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� Canvas -> LevelUP_Select -> Item0~...

public class Item : MonoBehaviour
{
    public ItemData data; //������ �����Ϳ� �ִ°� ����� �߰����ַ����ϸ� ����
    public int level=0;
    public Weapon weapon; //�÷��̾ ���� ���⸦ ȹ���ߴ��� �˾ƾ���
    public Gear gear;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        //�θ� ������Ʈ(Item)���� ��ũ��Ʈ�� �������ٰŶ� �ڽ��� �����;���
        //�׷��� ���ε� �����̶� ������ ������ ���� �ڽĵ� �Ͼ�;��ؼ� 1 �־���
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>(); //�̰� ��¥�� �ؽ�Ʈ�� �ϳ��ۿ� ��� �׳� ������
        textLevel = texts[0];
    }
    void LateUpdate()
    {
        textLevel.text = "Lv." + (level);
    }
    public void OnClick()
    {
        switch (data.itemType) //Item Data Ŭ�������� enum ItemType { Melee, Range, Glove, Shoe, Heal}
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                //�ڵΰ��� �����ִ����� �ľ��ؾ��� -> itemData�� ��ư�� ���� �˾Ƽ� itemType�� �������ְ�
                
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject(); //���׷��̵� ��ư ������⶧�� ù ����
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data); //Data������ itemdata�� ����
                }
                else
                {
                    //�Ϻη� 42�нð���� �ٸ��� �Ѱ��� �̷��� �ϴ°� ����
                    weapon.LevelUp(data.damages[level], data.counts[level]); //������ ��ư ��������
                    //���������� LevelUp���� �˸°� ����ֵ��� ����
                }
                level++;
                break;


            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level==0)
                {
                    GameObject newGear = new GameObject(); //���׷��̵� ��ư ������⶧�� ù ����
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }
        

        if (level == data.damages.Length) //itemdata���� damages �迭���̸� �����ͼ� �� �迭���̿� ������ ������ �ִ��� �Ǵ�
        {
            GetComponent<Button>().interactable = false;
            foreach (Image img in GetComponentsInChildren<Image>()) img.color = new Color32(125, 125, 125, 132);
        }
    }
}
