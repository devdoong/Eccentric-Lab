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
        textLevel.text = "Lv." + (level + 1);
    }
    public void OnClick()
    {
        switch (data.itemType) //Item Data Ŭ�������� enum ItemType { Melee, Range, Glove, Shoe, Heal}
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range: //�ڵΰ��� �����ִ����� �ľ��ؾ���
                break;


            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;

        if (level == data.damages.Length) //itemdata���� damages �迭���̸� �����ͼ� �� �迭���̿� ������ ������ �ִ��� �Ǵ�
        {
            GetComponent<Button>().interactable = false;
            foreach (Image img in GetComponentsInChildren<Image>()) img.color = new Color32(125, 125, 125, 132);
        }
    }
}
