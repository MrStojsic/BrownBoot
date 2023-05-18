using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=cAiYebAR8Hk&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=100")]
public class ISS_QuestScript : MonoBehaviour
{
    public ISS_Quest Quest { get; set; }

    [SerializeField] private SelectorButton _selectorButton = default;
    public SelectorButton SelectorButton
    {
        get { return _selectorButton; }
    }
    //TODO -Impliment the rest of the InventoryScript to InventoryPageManager like functionality
    //      to QuestScript and QuestLog so they act like the inventory slots, InventoryPageManager
    //      so we can scroll through like we can items selecting items and having them replace with it details slot.
    /*
    [SerializeField] protected Image _icon = default;
    public Image Icon
    {
        get { return _icon; }
    }*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ONLY TO MATCH TUTORIAL, will replace later.
    public void Select()
    {
        Debug.Log("Selected");

    }
    // ONLY TO MATCH TUTORIAL, will replace later.
    public void Deselect()
    {
        Debug.Log("Deselected");
    }

}
