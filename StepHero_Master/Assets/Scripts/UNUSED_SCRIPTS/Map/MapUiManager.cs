using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUiManager : MonoBehaviour
{
    private static MapUiManager _instance;
    public static MapUiManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapUiManager>();
            }
            return _instance;
        }
    }


    [SerializeField] private LocationInfoDisplay locationInfoDisplay = null;


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

    public void HideMapInfoWindow()
    {
        locationInfoDisplay.gameObject.SetActive(false);
    }

    public void JourneyOn()
    {
        locationInfoDisplay.gameObject.SetActive(false);
        Player_FollowBezierPath.instance.ContinueJourney();
    }
}
