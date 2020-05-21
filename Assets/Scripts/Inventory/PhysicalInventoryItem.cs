using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }

    void AddItemToInventory()
    {
        if(playerInventory && thisItem && thisItem.itemType == InventoryItem.ItemType.Quest)
        {
            playerInventory.myQuestInventory.Add(thisItem);
        }
        else if(playerInventory && thisItem && thisItem.itemType == InventoryItem.ItemType.Combat)
        {
            playerInventory.myCombatInventory.Add(thisItem);
        }
    }
}