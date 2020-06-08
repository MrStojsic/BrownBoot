using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBetweenTwoSimpleButtonComponents : MonoBehaviour
{
    public void toggleComponent(bool toggle)
    {
        SimpleButton[] simpleButtons = GetComponents<SimpleButton>();

        
        if (simpleButtons.Length > 1)
        {
            simpleButtons[0].enabled = !toggle;
            simpleButtons[1].enabled = toggle;
            GetComponent<Image>().sprite = simpleButtons[System.Convert.ToInt32(toggle)].sprite;
        }
        else
        {
            GetComponent<Image>().enabled = toggle;
        }


    }
}
