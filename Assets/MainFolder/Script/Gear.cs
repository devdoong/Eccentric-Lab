using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    //Weapon�� ������ ����ϰ� ������

    public ItemData.ItemType type;
    public float rate; //������ ������ �޾ƿ�

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear " + data.itemId; //�̰� �����ִ°���? ��
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear(); //�������ڸ���
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

    void RateUp() //����
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)//� �������� ������������ ������ ������
            {
                case 0: //ȸ�� bullet0
                    //weapon.speed = 150 + (150 * rate);
                    weapon.speed += (rate*1000) - 100;
                    break;

                case 1: //�� bullet3
                    //weapon.speed = 0.5f * (1f - rate); 
                    weapon.speed -= rate;
                    break;

            }
        }
    }

    void SpeedUp()
    {
        GameManager.instance.player.speed *= rate;
    }
}
