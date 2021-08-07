using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoiSlot : MonoBehaviour
{
    [SerializeField] private Text text = default;
    [SerializeField] private Image image = default;

    public void SetPoiInfo(string name, Sprite icon)
    {
        text.text = name;
        image.sprite = icon;
    }

    internal void SetPoiInfo(object name, Sprite icon)
    {
        throw new NotImplementedException();
    }
}
