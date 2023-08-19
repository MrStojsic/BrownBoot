using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabDefaultColour;
    public Color tabDownColour;
 
    public TabButton selectedTab;

    public List<GameObject> objectsToSwap;

    void Start()
    {
        for (int i = 1; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(false);
        }
        if (selectedTab != null)
        {
            OnTabSelected(selectedTab);
        }
    }
    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(tabButton);
    }

    public void OnTabDown(TabButton tabButton)
    {
        ResetTabs();
        if (selectedTab == null || tabButton != selectedTab)
        {
            tabButton.background.color = tabDownColour;
        }
    }

    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButton tabButton)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }
       
        selectedTab = tabButton;
        selectedTab.background.color = selectedTab.tabSelectedColour;
        selectedTab.Select();

        ResetTabs();

        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton tabButton in tabButtons)
        {
            if (selectedTab != null && tabButton == selectedTab)
            {
                continue;
            }
            tabButton.background.color = tabDefaultColour;
        }
    }

    public void SelectSelectorViaIndex(int childIndex)
    {
        if (transform.childCount > 0)
        {
            OnTabSelected(transform.GetChild(childIndex).GetComponent<TabButton>());
        }
    }
}
