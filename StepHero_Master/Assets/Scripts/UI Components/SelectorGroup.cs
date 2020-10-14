using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorGroup : MonoBehaviour
{
    public Color buttonDefaultColour;
    public Color buttonDownColour;
    public Color buttonSelectedColour;

    public SelectorButton selectedSelectorButton;
    public Transform selectorButtonsParent;

    public bool useButtonsSelectedColour = false;

    public int selectedIndex;

    [SerializeField] private bool autoSelectIndexOnStart = true;

    private void Start()
    {
        if (selectedSelectorButton != null)
        {
            OnButtonSelected(selectedSelectorButton);
        }
        if (selectedSelectorButton == null && autoSelectIndexOnStart)
        {
            SelectSelectorViaIndex(selectedIndex);
        }
    }

    public void OnButtonDown(SelectorButton selectorButton)
    {
        if (selectedSelectorButton == null || selectorButton != selectedSelectorButton)
        {
			selectorButton.background.color = buttonDownColour;
        }
    }
    public void OnButtonExit(SelectorButton selectorButton)
    {
        if (selectorButton != selectedSelectorButton)
        {
			selectorButton.background.color = buttonDefaultColour;
        }
    }
    public void OnButtonSelected(SelectorButton selectorButton)
    {
        if (selectedSelectorButton != null)
        {
            selectedSelectorButton.Deselect();
        }

        selectedSelectorButton = selectorButton;
        selectedIndex = selectedSelectorButton.transform.GetSiblingIndex();

        selectedSelectorButton.Select();
    }
    public void SelectSelectorViaIndex(int childIndex)
    {
        if (selectorButtonsParent.childCount > 0 && childIndex <= selectorButtonsParent.childCount)
        {
            OnButtonSelected(selectorButtonsParent.GetChild(childIndex).GetComponent<SelectorButton>());
        }
    }

    public Color ReturnSelectedColour()
    {
        if (useButtonsSelectedColour)
        {
            return selectedSelectorButton.buttonSelectedColour;
        }
        else
        {
            return buttonSelectedColour;
        }
    }
}