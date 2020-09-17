using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Canvas_Bag.
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class BagScript : MonoBehaviour
{

    [SerializeField] private GameObject slotPrefab = default;

    [SerializeField] private CanvasGroup canvasGroup = default;

    private List<SlotScript> slots = new List<SlotScript>();

    public List<SlotScript> Slots
    {
        get { return slots; }
    }

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            Slots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach (SlotScript slot in Slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }
        return false;
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }
}
