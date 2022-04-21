using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedInventory : MonoBehaviour
{
    // TODO
    // This is kinda up in the air for now as i have no idea how to set equipment for both playes and enemies up,
    // as both will make use of their equipemtns effects etc.

    IEquippable[] equippedItems = new IEquippable[11];
    // TWO_HAND  3  index 0
    // ONE_HAND  4  index 1 or 2
    // SHIELD    5  index 1 or 2
    // THROWABLE 6  index 1 or 2
    // HELMET    7  index 3
    // SHOULDER  8  index 4
    // CHEST     9  index 5
    // LEGGING   10 index 6
    // BOOTS     11 index 7
    // GLOVES    12 index 8
    // NECKLACE  13 index 9
    // RING      14 index 10

    // Start is called before the first frame update
    void EquipItem(IEquippable itemToEquip)
    {
        Item item = itemToEquip as Item;
        int itemTypeAsInt = (int)item.ItemType;

        if (itemTypeAsInt > 6)
        {
            equippedItems[itemTypeAsInt - 4] = itemToEquip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
