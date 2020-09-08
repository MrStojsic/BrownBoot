﻿using System.Collections;
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


    private void Start()
    {
        if (selectedSelectorButton != null)
        {
            OnButtonSelected(selectedSelectorButton);
        }
        if (selectedSelectorButton == null)
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
            selectedSelectorButton.background.color = buttonDefaultColour;
            selectedSelectorButton.Deselect();
        }

        selectedSelectorButton = selectorButton;

        if (useButtonsSelectedColour)
        {
			selectorButton.background.color = selectorButton.buttonSelectedColour;
        }
        else
        {
            selectedSelectorButton.background.color = buttonSelectedColour;
        }

        selectedSelectorButton.Select();
        selectedIndex = selectedSelectorButton.transform.GetSiblingIndex();

    }
    public void SelectSelectorViaIndex(int childIndex)
    {
        if (selectorButtonsParent.childCount > 0 && childIndex <= selectorButtonsParent.childCount)
        {
            OnButtonSelected(selectorButtonsParent.GetChild(childIndex).GetComponent<SelectorButton>());
        }
    }
}
