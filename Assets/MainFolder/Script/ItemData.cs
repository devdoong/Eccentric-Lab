using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] //Ŀ���� �޴��� �����ϴ� �Ӽ� //Projectâ�� +�� Ŭ���ϸ� ��ܿ� Scriptble Object�׸��� �߰��Ȱ� �� �� ��������

public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal}
    [Header("# Main Info")]
    public ItemType itemType; //enum�� ����ٰ� �ʿ���.
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon; //��������Ʈ�� ������ ĵ������ ��������Ʈ �̹����� ��ưǥ������

    [Header("#Level Per Data")] //�����Ҷ����� ��������.
    public float baseDamage; //�⺻ ���� ������
    public int baseCount; //�⺻ ���� ���� ���� Ƚ��
    public float[] damages; 
    public int[] counts;


    [Header("#Weapon")]
    public GameObject projectile; //�������� ��������.

}
