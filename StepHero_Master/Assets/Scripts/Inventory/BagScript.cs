using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Canvas_Bag.
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class BagScript : MonoBehaviour
{

    [SerializeField] private GameObject slotPrefab;

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, transform);
        }
    }
}
