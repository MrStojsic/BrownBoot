using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteraction_LocationPoiPopulator : MonoBehaviour
{
    [SerializeField] private SelectorGroup poiDisplaySelectorGroup = null;

    [SerializeField] private PoiSlot poiSlotPrefab = null;
    private List<PoiSlot> _poiSlots = new List<PoiSlot>();

    [SerializeField] private Transform _pooledPoiSlotHolder = default;

    [SerializeField] LocationPoi locationPoi = null;

    // Start is called before the first frame update
    public void PopulateLocationPoiList(string locationName)
    {
        if (locationPoi?.name != locationName)
        {
            // NOTE This just shows how to load an asset at runtime, this can be used to load a towns shops at runtime.
            locationPoi = (LocationPoi)Resources.Load("LocationData/" + locationName);

        }
        if (locationPoi != null)
        {
            poiDisplaySelectorGroup.SelectSelectorViaIndex(0);
        }
        // HACK.
        // For now we just loop through _poiSlots and move all the poiSlots to hide them,
        // as not all locations have a LocationPoi in resources yet and hiding them makes it easier to troubleshoot.
        else
        {
            ClearOutDisplay();
        }
        // TOHERE
    }

    public void InitialisePoiSlots_Quests()
    {
        ClearOutDisplay();
    }


    public void InitialisePoiSlots_Merchants()
    {
        if (locationPoi != null)
        {
            int slotIndex = 0;

            for (; slotIndex < locationPoi.Merchants.Count; slotIndex++)
            {
                if (_poiSlots.Count > slotIndex)
                {
                    _poiSlots[slotIndex].SetPoiInfo(locationPoi.Merchants[slotIndex].name, locationPoi.Merchants[slotIndex].Icon);
                    _poiSlots[slotIndex].transform.SetParent(this.transform);
                }
                else
                {
                    AddMenuItem(slotIndex);
                }
            }

            if (locationPoi.Merchants.Count < _poiSlots.Count)
            {
                for (; slotIndex < _poiSlots.Count; slotIndex++)
                {
                    _poiSlots[slotIndex].transform.SetParent(_pooledPoiSlotHolder);

                    // TODO, thers an issues when we change types, the selector group calls the last buttons deslect reenabling the buttons we disabled  here. 
                }
            }
        }
    }

    void AddMenuItem(int slotIndex)
    {
        PoiSlot newMenuItem;
        newMenuItem = Instantiate(poiSlotPrefab, this.transform);
        newMenuItem.SetPoiInfo(locationPoi.Merchants[slotIndex].name, locationPoi.Merchants[slotIndex].Icon);
        newMenuItem.transform.name += _poiSlots.Count.ToString();
        _poiSlots.Add(newMenuItem);
    }

    private void ClearOutDisplay()
    {
        for (int i = 0; i < _poiSlots.Count; i++)
        {
            _poiSlots[i].transform.SetParent(_pooledPoiSlotHolder);
        }
    }

}
