using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleOnScroll : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private RectTransform mapRectTransform = null;
    [SerializeField] private RectTransform scrollviewRectTransform = null;

    [SerializeField] float minScale = default;
    [SerializeField] float maxScale = default;

    bool isHovered;

    [SerializeField] Scrollbar scrollbar;

    void Start()
    {
        minScale = Mathf.Max(((scrollviewRectTransform.rect.height / mapRectTransform.rect.height)-.01f),
                             ((scrollviewRectTransform.rect.width / mapRectTransform.rect.width)-.01f));
    }

    void Zoom(float increment)
    {
        float newScale = Mathf.Clamp(mapRectTransform.localScale.x - increment, minScale, maxScale);
        mapRectTransform.localScale = Vector2.one * newScale;
    }
    void Update()
    {
        if (isHovered)
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }
}
