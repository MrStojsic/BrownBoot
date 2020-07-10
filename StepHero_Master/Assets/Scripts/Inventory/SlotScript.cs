using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Asstached to Image_Slot
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class SlotScript : MonoBehaviour
{
    /// <summary>
    /// Icon should be set in the inspector to reference the slots child image_Icon,
    /// so we can change the icon to display the item we have stored in that slot.
    /// </summary>
    [SerializeField] private Image icon;

    private Stack<Item> items = new Stack<Item>();

    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }


    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.Icon;
        icon.color = Color.white;
        
        return true;
    }


}
