using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public InventoryManager inventoryManager;
            [SerializeField] private GameObject inventorySlotColor; 
            private bool quest = true;
            private bool combat = false;
            public void QuestButtonPressed()
            {
                quest = true;
                combat = false;
                inventoryManager.ButtonPressed(quest,combat);
            }
            
            public void CombatButtonPressed()
            {
                quest = false;
                combat = true;
                inventoryManager.ButtonPressed(quest,combat);
            }
            
            
            
            public void Click()
            {
                print("1");
                Color color = inventorySlotColor.GetComponent<Image>().color;
                color.a = 1f;
                inventorySlotColor.GetComponent<Image>().color = color;
              
                
            
            }
    
}
