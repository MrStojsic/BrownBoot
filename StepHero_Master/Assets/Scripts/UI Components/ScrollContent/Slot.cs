using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // UI.
    [SerializeField] protected Text _title = default;
    public Text Title
    {
        get { return _title; }
    }

    [SerializeField] protected Image _icon = default;
    public Image Icon
    {
        get { return _icon; }
    }

    [SerializeField] protected SelectorButton _selectorButton = default;
    public SelectorButton SelectorButton
    {
        get { return _selectorButton; }
    }

    private int _index;
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }
}
