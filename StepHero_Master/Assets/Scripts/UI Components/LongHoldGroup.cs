using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHoldGroup : MonoBehaviour
{
    public LongHoldButton heldButton;

    float holdTriggerTime = 1f;
    float buttonPressedTime = 0;
    bool isHeld = false;

    public void OnButtonDown(LongHoldButton heldButton)
    {
        if (heldButton == null)
        {
            isHeld = true;
            buttonPressedTime = Time.time;
        }
    }

    public void OnButtonUp(LongHoldButton holdButton)
    {
        if (heldButton == null)
        {
            isHeld = true;
            buttonPressedTime = Time.time;
        }
    }


    public void OnButtonExit(LongHoldButton heldButton)
    {
        ResetButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld && Time.time - buttonPressedTime >= holdTriggerTime)
        {
            heldButton.OnLongHold();
        }
    }

    private void ResetButton()
    {
        if (heldButton != null)
        {
            isHeld = false;
            buttonPressedTime = float.MaxValue;
            heldButton.OnReset();

        }
    }
}
