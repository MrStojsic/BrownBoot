using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeGroup : MonoBehaviour
{
    [SerializeField] public float minsSwipeLenght = 1;
    [SerializeField] public SwipeButton selectedSwipeButton;

    public ScrollRect mainScroll;
   
    // Start is called before the first frame update
    public void SetSelectedSwipeButton(SwipeButton _selectedSwipeButton)
    {
        selectedSwipeButton = _selectedSwipeButton;
    }

    public void ResetSelectedSwipeButton()
    {
        if (selectedSwipeButton != null)
        {
            selectedSwipeButton.OnReset();
        }
    }
}
