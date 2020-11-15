using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoiSlot : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    public void SetPoiInfo(string name, Sprite icon)
    {
        text.text = name;
        image.sprite = icon;
    }
}
