using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item1> items = new List<Item1>();
    void Start()
    {
        items.Add(new Item1("1", "Iron Sword", 1001, "This sword is normal style sword", 10, 1, 0, 0, 1, 1,1,1,  Item1.ItemType.Combat));
        items.Add(new Item1("2","Iron Spear", 1011, "This spear is normal style spear", 12, 2, 0, 0,1,1,1,1, Item1.ItemType.Combat));

        items.Add(new Item1("3","Boxing Gloves", 2001, "This Gloves is fast gloves", 10, 1, 0, 1,1,1,1,1, Item1.ItemType.Combat));
        items.Add(new Item1("4","Drill Gloves", 2002, "This Gloves is Drill gloves", 13, 2, 0, 1, 1,1,1,1, Item1.ItemType.Combat));

        items.Add(new Item1("5","Red Potion", 4001, "This potion is restores hp(+50)", 0, 0, 0, 0,1,1,1,1, Item1.ItemType.Combat));
        items.Add(new Item1("6","Orange Potion", 4011, "This potion is increase sight (+5)", 0, 0, 0,1,1,1,1,1, Item1.ItemType.Combat));

    }

}