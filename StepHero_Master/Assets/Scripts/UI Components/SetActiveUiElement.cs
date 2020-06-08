using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveUiElement : MonoBehaviour
{
    [SerializeField] Transform uiBlockerPanel;
    // Start is called before the first frame update
    public void SetUiActive()
    {
        if (this.transform.parent.gameObject.activeSelf == false)
        {
            this.transform.parent.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);
        //lastActiveUi = uiBlockerPanel.transform.parent.GetChild(uiBlockerPanel.GetSiblingIndex() + 1);
        if (uiBlockerPanel != null)
        {
            uiBlockerPanel.SetSiblingIndex(this.transform.GetSiblingIndex() - 1);
        }

    }

    // Update is called once per frame
    public void SetUiInactive(bool disableParent)
    {
        if (disableParent == true)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            if (uiBlockerPanel != null)
            {
                uiBlockerPanel.SetSiblingIndex(this.transform.GetSiblingIndex() - 2);
            }
        }
        this.gameObject.SetActive(false);
    }
}
