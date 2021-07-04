using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class TownMapMovement : MonoBehaviour

//- If camera gitters when fully zoomed out, check that max zoom limit isnt so far out that its past the maps bounds.
{
    [SerializeField] SpriteRenderer mapSpriteRenderer = default;

    private Vector3 touchStart = default;

    [SerializeField] RectTransform frameHeaderRect;
    [SerializeField] RectTransform frameFooterRect;


    [Tooltip("This is set on Start, it uses the mapSpriteRenderer's bounds as the cameras bounds limits")]
    [SerializeField] private float panXLimit = default;
    [SerializeField] private Vector2 panYLimits = default;

    private Camera cam = default;

    private void Start()
    {
        cam = Camera.main;
        CalculatePanLimit();
    }

    void CalculatePanLimit()
    {
        float topVerticalOffsetPercentage = frameFooterRect.anchorMax.y;
        float bottomVerticalOffsetPercentage = 1 - frameHeaderRect.anchorMin.y;

        panXLimit = mapSpriteRenderer.bounds.extents.x - (cam.orthographicSize * cam.aspect);
        panYLimits = new Vector2((mapSpriteRenderer.bounds.extents.y - cam.orthographicSize) + (bottomVerticalOffsetPercentage * cam.orthographicSize * 2),
                                (mapSpriteRenderer.bounds.extents.y - cam.orthographicSize) + (topVerticalOffsetPercentage * cam.orthographicSize * 2));
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = gameObject.transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
            }

            else if (Input.GetMouseButton(0))
            {
                ClampCamPosWithinMapBounds( Camera.main.ScreenToWorldPoint(Input.mousePosition) + touchStart);
            }

            // Currently No Zooming In Towns.
            //Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
    void ClampCamPosWithinMapBounds(Vector3 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, -panXLimit, panXLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, -panYLimits.x, panYLimits.y);

        this.transform.position = newPosition;
    }


    // Currently No Zooming In Towns.
    /*
    [SerializeField] private float minZoomLimit = 2;
    [SerializeField] private float maxZoomLimit = 3.8f;
    void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoomLimit, maxZoomLimit);
        CalculatePanLimit();

        ClampCamPosWithinMapBounds(this.transform.position);
    }*/
}