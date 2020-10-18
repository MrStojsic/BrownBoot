using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer),typeof(SpriteButton))]
public class Location : MonoBehaviour
{
    public Treasure treasure;

    public IInteractable interactable;


    // Start is called before the first frame update
    void Start()
    {
        interactable = treasure;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
