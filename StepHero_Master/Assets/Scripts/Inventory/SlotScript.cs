using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Asstached to Image_Slot
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class SlotScript : MonoBehaviour, IPointerClickHandler,IClickable
{
    /// <summary>
    /// Icon should be set in the inspector to reference the slots child image_Icon,
    /// so we can change the icon to display the item we have stored in that slot.
    /// </summary>
    [SerializeField] private Image icon;
    public Image Icon
    {
        get { return icon;  }
        set { icon = value; }
    }

    public int Count
    {
        get { return items.Count; }
    }

    private ObservableStack<Item> items = new ObservableStack<Item>();

    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }

    [SerializeField]  private Text stackSize;

    public Item Item
    {
        get
        {
            if (!IsEmpty)
            {
                return items.Peek();
            }
            return null;
        }

    }

    public Text Stacktext
    {
        get
        {
            return stackSize;
        }
    }
    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }


    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.Icon;
        icon.color = Color.white;
        item.Slot = this;

        return true;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            items.Pop();
        }
    }

    public void UpdateSlot()
    {
        UpdateStackSize(this);
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.Count > 1)
        {
            clickable.Stacktext.text = clickable.Count.ToString();
            clickable.Stacktext.color = Color.white;
            clickable.Icon.color = Color.white;
        }
        else
        {
            clickable.Stacktext.color = Color.clear;
        }


        if (clickable.Count == 0)
        {
            icon.color = Color.clear;
            clickable.Stacktext.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (Item is IUseable)
        {
            (Item as IUseable).Use();
            print("USED");
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == Item.name && items.Count < Item.StackSize)
        {
            items.Push(item);
            item.Slot = this;
            return true;
        }
        return false;
    }
}
