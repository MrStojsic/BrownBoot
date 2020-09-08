using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the handscript
    /// </summary>
    private static HandScript instance;

    public static HandScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    /// <summary>
    /// The current moveable
    /// </summary>
    public IMoveable Moveable { get; set; }

    /// <summary>
    /// The icon of the item, that we acre moving around atm.
    /// </summary>
    private Image icon = default;

    /// <summary>
    /// An offset to move the icon away from the mouse
    /// </summary>
    [SerializeField]
    private Vector3 offset = default;

    // Use this for initialization
    void Start ()
    {
        //Creates a reference to the image on the hand
        icon = GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Makes sure that the icon follows the hand
        icon.transform.position = Input.mousePosition+offset;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && Instance.Moveable != null)
        {
            DeleteItem();
        }

      
	}

    /// <summary>
    /// Take a moveable in the hand, so that we can move it around
    /// </summary>
    /// <param name="moveable">The moveable to pick up</param>
    public void TakeMoveable(IMoveable moveable)
    {
        this.Moveable = moveable;
        icon.sprite = moveable.Icon;
        icon.enabled = true;
    }

    public IMoveable PutMoveable()
    {
        IMoveable tmp = Moveable;
        Moveable = null;
        icon.enabled = false;
        return tmp;
    }
  
    public void DropMoveable()
    {
        Moveable = null;
        icon.enabled = false;
        InventoryScript.Instance.FromSlot = null;
    }
    
    
    /// <summary>
    /// Deletes an item from the inventory
    /// </summary>
    public void DeleteItem()
    {
        if (Moveable is Item && InventoryScript.Instance.FromSlot != null)
        {
            (Moveable as Item).Slot.Clear();
        }
        DropMoveable();
        InventoryScript.Instance.FromSlot = null;
    }
}
