using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Asstached to Image_Slot
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Icon should be set in the inspector to reference the slots child image_Icon,
    /// so we can change the icon to display the item we have stored in that slot. 
    /// </summary>
    [SerializeField] private Image icon = default;
    public Image Icon
    {
        get { return icon; }
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
    public bool IsFull
    {
        get
        {
            if (IsEmpty || Count < Item.StackSize)
            {
                return false;
            }
            return true;
        } 
    }

    [SerializeField] private Text stackSize = default;

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

        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == Item.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            items.Pop();
        }
    }

    public void Clear()
    {
        if (items.Count > 0)
        {
            items.Clear();
        }
    }


    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.Item.GetType() != Item.GetType() || from.Count + Count > Item.StackSize)
        {
            ObservableStack<Item> tempFrom = new ObservableStack<Item>(from.items);
            from.items.Clear();

            from.AddItems(items);
            items.Clear();

            AddItems(tempFrom);

            return true;
        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.Item.GetType() == Item.GetType() && !IsFull)
        {
            int freeSpace = Item.StackSize - Count;
            for (int i = 0; i < freeSpace; i++)
            {
                AddItem(from.items.Pop());
            }
            return true;
        }
        return false;
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
            clickable.Icon.color = Color.white;
        }


        if (clickable.Count == 0)
        {
            icon.color = Color.clear;
            clickable.Stacktext.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // If hand is empty and hand is empty.
            if (InventoryScript.Instance.FromSlot == null && !IsEmpty)
            {
                HandScript.Instance.TakeMoveable(Item as IMoveable);
                InventoryScript.Instance.FromSlot = this;
            }
            // IF hand if full.
            else if (InventoryScript.Instance.FromSlot != null)
            {
                if(PutItemBack() || MergeItems(InventoryScript.Instance.FromSlot) || SwapItems(InventoryScript.Instance.FromSlot) || AddItems(InventoryScript.Instance.FromSlot.items))
                {
                    HandScript.Instance.DropMoveable();
                    InventoryScript.Instance.FromSlot = null;
                }
            }
        }

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
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if (InventoryScript.Instance.FromSlot == this)
        {
            InventoryScript.Instance.FromSlot.Icon.color = Color.white;
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            UiManager_Inventory.Instance.ShowToolTip(transform.position, Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UiManager_Inventory.Instance.HideToolTip();
    }

    public string GetDescription()
    {
        return "This is an item";
    }
}
