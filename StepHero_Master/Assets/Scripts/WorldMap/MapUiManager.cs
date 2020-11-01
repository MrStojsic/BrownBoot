using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUiManager : MonoBehaviour
{
    [SerializeField] private LocationInfoDisplay locationInfoDisplay = null;

    private void Start()
    {
        // NOTE This just shows how to load an asset at runtime, this can be used to load a towns shops at runtime.
        print(Resources.Load("Apple"));
    }

    public void DisplayMapInfoWindow(Location location)
    {
        locationInfoDisplay.SetLocationDetails(location);
        locationInfoDisplay.ToggleButton(false);
        locationInfoDisplay.gameObject.SetActive(true);
    }
    public void DisplayMapEnterWindow(Location location)
    {
        locationInfoDisplay.SetLocationDetails(location);
        locationInfoDisplay.ToggleButton(true);
        locationInfoDisplay.gameObject.SetActive(true);
    }
}
