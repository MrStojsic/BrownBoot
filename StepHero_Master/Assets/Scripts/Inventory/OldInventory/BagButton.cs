using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    private Bag _bag;

    [SerializeField] private Sprite sprite;

    public Bag Bag
    {
        get{ print("Got a bag"); return _bag; }
        set { print("Set a bag"); _bag = value; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_bag != null)
        {
            _bag.BagScript.OpenClose();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
