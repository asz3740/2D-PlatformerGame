using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
[System.Serializable]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;

    public UnityEvent thisEvent;

    public ItemType itemType;
    public enum ItemType
    {
        Quest,
        Combat
    }
    public void Use()
    {
        thisEvent.Invoke();
    }

}