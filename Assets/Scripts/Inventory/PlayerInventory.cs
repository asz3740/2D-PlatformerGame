using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myQuestInventory = new List<InventoryItem>();
    public List<InventoryItem> myCombatInventory = new List<InventoryItem>();
}