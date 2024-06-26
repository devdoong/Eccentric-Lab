using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    //Weapon과 구조는 비슷하게 가져감

    public ItemData.ItemType type;
    public float rate; //레벨별 데이터 받아옴

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear " + data.itemId; //이거 왜해주는거지? ★
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear(); //생성되자마자
    }

    public void LevelUp(float rate)
    {
        this .rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp(); break;
        }
    }

    void RateUp() //공속
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)//어떤 무기인지 근접무기인지 총인지 뭐인지
            {
                case 0: //회전 bullet0
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;

                default:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;

            }
        }
    }

    void SpeedUp()
    {
        float speed = 3 * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
