using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item1 {
    public string itemName;         // 아이템의 이름
    public int itemID;              // 아이템의 고유번호
    public string itemDes;          // 아이템의 설명
    public Texture2D itemIcon;      // 아이템의 아이콘(2D)
    public int itemPower;           // 아이템의 공격력
    public int itemSkillPower;      // 아이템의 스킬 공격력
    public int itemSpeed;           // 아이템의 속도(공속?)
    public int itemHealth;          // 아이템의 체력
    public int itemCoolTime;        // 아이템의 쿨타임
    public int itemDefense;         // 아이템의 방어력
    public int itemEvasion;         // 아이템의 회피력
    public int itemJump;            // 아이템의 점프력

    public ItemType itemType;       // 아이템의 속성 설정
    
    public enum ItemType            // 아이템의 속성 설정에 대한 갯수
    {
        Quest,                      // 퀘스트 아이템류
        Combat                      // 전투 아이템류    
    }

    public Item1()
    {
        
    }
    public Item1(string img, string name, int id, string des, int power, int skillpower, int speed, int health, int cooltime, int defense, int evasion, int jump, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDes = des;
        itemPower = power;
        itemSkillPower = skillpower;
        itemIcon = Resources.Load<Texture2D>("ItemIcon/" + img);

        itemSpeed = speed;
        itemHealth = health;
        itemCoolTime = cooltime;
        itemDefense = defense;
        itemEvasion = evasion;
        itemJump = jump;
        itemType = type;
    }
}