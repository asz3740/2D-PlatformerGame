using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    //[SerializeField] private GameObject useButton;
    public InventoryItem currentItem;

    private bool quest = true;
    private bool combat = false;
    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
    }

    void MakeInventorySlots()
    {
        if (playerInventory && quest)
        {
            for (int i = 0; i < playerInventory.myQuestInventory.Count; i++)
            {


                    GameObject temp =
                        Instantiate(blankInventorySlot,
                        inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    temp.transform.localScale = new Vector3(1,1,1);
                    InventorySlot1 newSlot = temp.GetComponent<InventorySlot1>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myQuestInventory[i], this);
                    }
                
            }
        }
        else if (playerInventory && combat)
        {
            for (int i = 0; i < playerInventory.myCombatInventory.Count; i++)
            {


                GameObject temp =
                    Instantiate(blankInventorySlot,
                        inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);
                temp.transform.localScale = new Vector3(1, 1, 1);
                InventorySlot1 newSlot = temp.GetComponent<InventorySlot1>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.myCombatInventory[i], this);
                }

            }
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        Debug.Log("made inventory");
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString, string newNameString, InventoryItem newItem)
    {
        currentItem = newItem;
        nameText.text = newNameString;
        descriptionText.text = newDescriptionString;
    }

    void ClearInventorySlots()
    {
        for(int i = 0; i < inventoryPanel.transform.childCount; i ++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    
     public void ButtonPressed(bool _quest, bool _combat)
        {
           
            quest = _quest;
            combat = _combat;
            print(quest);
            print(combat);
            ClearInventorySlots();
            MakeInventorySlots();
        }
    
}