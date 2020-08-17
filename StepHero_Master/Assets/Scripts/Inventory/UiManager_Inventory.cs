using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager_Inventory : MonoBehaviour
{
    private static UiManager_Inventory _instance;
    public static UiManager_Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UiManager_Inventory>();
            }
            return _instance;
        }
    }

    [SerializeField] private RectTransform toolTip;
    [SerializeField] private Text toolTipText;

    private void Awake()
    {
        toolTipText = toolTip.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Shows the tooltip in ideal position on screen relative to tool.
    /// </summary>
    public void ShowToolTip(Vector3 position, IDescribable describable)
    {
        toolTip.transform.position = new Vector3(Screen.width*0.5f,position.y+150,0);
        toolTipText.text = describable.GetDescription();
        toolTip.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the tooltip.
    /// </summary>
    public void HideToolTip()
    {
        toolTip.gameObject.SetActive(false);
    }
}
