using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] //커스텀 메뉴를 생성하는 속성 //Project창의 +를 클릭하면 상단에 Scriptble Object항목이 추가된걸 볼 수 있을거임

public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal}
    [Header("# Main Info")]
    public ItemType itemType; //enum을 담아줄게 필요함.
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon; //스프라이트를 담아줘야 캔버스에 스프라이트 이미지로 버튼표시해줌

    [Header("#Level Per Data")] //성장할때마다 증가해줌.
    public float baseDamage; //기본 시작 데미지
    public int baseCount; //기본 시작 관통 남은 횟수
    public float[] damages; 
    public int[] counts;


    [Header("#Weapon")]
    public GameObject projectile; //프리펩을 담아줘야함.

}
