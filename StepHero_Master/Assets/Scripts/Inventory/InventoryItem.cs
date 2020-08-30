using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    /// <summary>

    /// </summary>
    [SerializeField] private Image icon;
    public Image Icon
    {
        get { return icon; }
    }

    [SerializeField] private Text stackSize;

    private Item item;
    public Item Item
    {
        get { return item; }
        set { item = value; }

    }


    public Text Stacktext
    {
        get
        {
            return stackSize;
        }
    }
}
