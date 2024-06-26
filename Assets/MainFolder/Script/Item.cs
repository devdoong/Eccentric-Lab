using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//적용 Canvas -> LevelUP_Select -> Item0~...

public class Item : MonoBehaviour
{
    public ItemData data; //아이템 데이터에 있는게 뜰거임 추가해주려고하면 ㅇㅇ
    public int level=0;
    public Weapon weapon; //플레이어가 무슨 무기를 획득했는지 알아야함
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        //부모 오브젝트(Item)현재 스크립트를 적용해줄거라 자식을 가져와야함
        //그런데 본인도 포함이라 본인을 제외한 다음 자식들 일어와야해서 1 넣어줌
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>(); //이건 어짜피 텍스트가 하나밖에 없어서 그냥 가져옴
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDescription, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDescription, data.damages[level]*100);
                break ;
            default:
                textDesc.text = string.Format(data.itemDescription);
                break;



        }
    }

    public void OnClick()
    {
        switch (data.itemType) //Item Data 클래스에서 enum ItemType { Melee, Range, Glove, Shoe, Heal}
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                //★두개를 붙혀주는이유 파악해야함 -> itemData는 버튼에 따라 알아서 itemType을 전달해주고

                if (level == 0)
                {
                    GameObject newWeapon = new GameObject(); //업그레이드 버튼 눌러줬기때매 첫 생성
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data); //Data폴더의 itemdata를 전달
                }
                else
                {
                    //일부러 42분시간대와 다르게 한것임 이렇게 하는게 맞음
                    weapon.LevelUp(data.damages[level], data.counts[level]); //레벨업 버튼 적용해줌
                    //★증가값은 LevelUp에서 알맞게 잡아주도록 설계
                }
                level++;
                break;


            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject(); //업그레이드 버튼 눌러줬기때매 첫 생성
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



        if (level == data.damages.Length) //itemdata에서 damages 배열길이를 가져와서 그 배열길이와 같으면 레벨이 최대라고 판단
        {
            GetComponent<Button>().interactable = false;
            foreach (Image img in GetComponentsInChildren<Image>()) img.color = new Color32(125, 125, 125, 132);
        }
    }
}
