using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSlot : MonoBehaviour
{
    [SerializeField]
    private Text _textDescription;
    [SerializeField]
    private Image _checkBoxImage;

    public void SetSlot(Objective o, Sprite tickBox = null)
    {
        if (tickBox != null)
        {
            _checkBoxImage.sprite = tickBox;
        }
        _textDescription.text = o.Description + " " + o.CurrentAmount + "/" + o.Amount;
    }
}
