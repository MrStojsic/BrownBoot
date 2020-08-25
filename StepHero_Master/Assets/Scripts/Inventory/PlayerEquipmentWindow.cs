using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void OpenClose()
    {

        if(canvasGroup.alpha == 0)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            return;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

    }
}
