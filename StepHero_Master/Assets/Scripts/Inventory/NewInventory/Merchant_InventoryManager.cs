using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Merchant_InventoryManager : MonoBehaviour
{ 
    [SerializeField]
    private InventoryInteractionManager IIM = null;

    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[15];

    public int gold;

    [SerializeField] private List<MerchantInventoryItem> commonPossibleStockItems = default;
    [SerializeField] private int minCommonItemsExpected = default;
    [SerializeField] private List<MerchantInventoryItem> uncommonPossibleStockItems = default;
    [SerializeField] private int minUncommonItemsExpected = default;
    [SerializeField] private List<MerchantInventoryItem> rarePossibleStockItems = default;
    [SerializeField] private int minRareItemsExpected = 0;


    /// <summary>
    /// This returns the number week of the current date, this may be moved to somewhere else later.
    /// </summary>
    /// <returns> </returns>
    public int GetWeekOfYear()
    {
        System.Globalization.DateTimeFormatInfo dfi = System.Globalization.DateTimeFormatInfo.CurrentInfo;

        return dfi.Calendar.GetWeekOfYear(System.DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
    }

    public void AddStock_DRAFT()
    {
        for (int i = 0; i < inventoryTypePockets.Length - 1; i++)
        {
            inventoryTypePockets[i].Initialise((ItemType)i);
        }

        Random.InitState(System.DateTime.Now.DayOfYear);

        // COMMON STOCK.
        if(commonPossibleStockItems.Count > 0)
        {
            int commonStockToSkip = commonPossibleStockItems.Count - Random.Range(minCommonItemsExpected, commonPossibleStockItems.Count - 1);
            print("commonStockToSkip" + commonStockToSkip);
            for (int i = 0; i < commonStockToSkip; i++)
            {
                commonPossibleStockItems[Random.Range(0, commonPossibleStockItems.Count - 1)] = null;
                print("Removed an item");
            }
            for (int i = 0; i < commonPossibleStockItems.Count; i++)
            {
                if (commonPossibleStockItems[i] != null)
                {
                    inventoryTypePockets[(int)commonPossibleStockItems[i].Item.ItemType].SafeForceAddItem(commonPossibleStockItems[i]);
                }
            }
        }
        // UNCOMMON STOCK.
        if (uncommonPossibleStockItems.Count > 0)
        {
            int uncommonStockToSkip = uncommonPossibleStockItems.Count - Random.Range(minUncommonItemsExpected, uncommonPossibleStockItems.Count - 1);
            print("uncommonStockToSkip" + uncommonStockToSkip);
            for (int i = 0; i < uncommonStockToSkip; i++)
            {
                uncommonPossibleStockItems[Random.Range(0, uncommonPossibleStockItems.Count - 1)] = null;
                print("Removed an item");
            }
            for (int i = 0; i < uncommonPossibleStockItems.Count; i++)
            {
                if (uncommonPossibleStockItems[i] != null)
                {
                    inventoryTypePockets[(int)uncommonPossibleStockItems[i].Item.ItemType].SafeForceAddItem(uncommonPossibleStockItems[i]);
                }
            }
        }
        // RARE STOCK.
        // 50% chance of guareteed no items.
        if (rarePossibleStockItems.Count > 0)
        {
            int rareStockToSkip = rarePossibleStockItems.Count - Random.Range(minRareItemsExpected, 2 * rarePossibleStockItems.Count - 1);
            print("rareStockToSkip" + rareStockToSkip);
            if (rarePossibleStockItems.Count > rareStockToSkip)
            {
                for (int i = 0; i < rareStockToSkip; i++)
                {
                    rarePossibleStockItems[Random.Range(0, rarePossibleStockItems.Count - 1)] = null;
                    print("Removed an item");
                }
                for (int i = 0; i < rarePossibleStockItems.Count; i++)
                {
                    if (rarePossibleStockItems[i] != null)
                    {
                        inventoryTypePockets[(int)rarePossibleStockItems[i].Item.ItemType].SafeForceAddItem(rarePossibleStockItems[i]);
                    }
                }
            }
        }
    }




    public void AddStock()
    {
        // TODO.
        // Add randomness to stock.
        // Seed random with the current week of the year.
        //Random.InitState(GetWeekOfYear());
        //Random.InitState(System.DateTime.Now.DayOfYear);
        Random.InitState((int)System.DateTime.Now.Ticks);
        // TOHERE


        for (int i = 0; i < inventoryTypePockets.Length - 1; i++)
        {
            inventoryTypePockets[i].Initialise((ItemType)i);
        }

        /*for (int i = 0; i < possibleStockItems.Count; i++)
        {
            inventoryTypePockets[(int)possibleStockItems[i].Item.ItemType].SafeForceAddItem(possibleStockItems[i]);
        }*/
    }

    // Start is called before the first frame update
    void Awake()
    {
        // HACK
        //AddStock();
       // AddStock_DRAFT();
        // TOHERE
       // IIM.SetOtherInventoryTypePockets(inventoryTypePockets, InventoryInteractionManager.InventoryType.SHOP_BUY);
       // IIM.SetFocustedInventoryTypePockets(false);
    }
}
