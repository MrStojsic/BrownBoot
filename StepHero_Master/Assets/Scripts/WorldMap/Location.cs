using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer),typeof(SpriteButton))]
public class Location : MonoBehaviour
{
    [SerializeField] private MapUiManager mapUiManager;

    public Treasure treasure;

    public IInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {


        interactable = treasure;
    }

    // Update is called once per frame
    public void DisplayLocationInfo()
    {
        mapUiManager.DisplayMapInfoWindow(this);
    }
}
