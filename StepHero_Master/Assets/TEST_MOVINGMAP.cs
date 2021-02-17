using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class TEST_MOVINGMAP : MonoBehaviour

//- If camera gitters when fully zoomed out, check that max zoom limit isnt so far out that its past the maps bounds.
{
    [SerializeField] SpriteRenderer mapSpriteRenderer = default;

    Vector3 touchStart = default;
    private Vector3 offset;

    [SerializeField] private float minZoomLimit = 2;
    [SerializeField] private float maxZoomLimit = 3.8f;

    [SerializeField] RectTransform frameRect;


    [Tooltip("This is set on Start, it uses the mapSpriteRenderer's bounds as the cameras bounds limits")]
    [SerializeField] private float panXLimit = default;
    [SerializeField] private Vector2 panYLimit = default;

    private Camera cam = default;

    float topVerticalOffsetPercentage = default;
    float bottomVerticalOffsetPercentage = default;

    private void  PlaceShopTest()
    {
        
    }
    private void Start()
    {
        cam = Camera.main;

        bottomVerticalOffsetPercentage = (.5f - frameRect.anchorMin.y) * 2;
        topVerticalOffsetPercentage = (frameRect.anchorMax.y - .5f) * 2;
        CalculatePanLimit();

        PlaceShopTest();

    }

    void CalculatePanLimit()
    {
        panXLimit = mapSpriteRenderer.bounds.extents.x - (cam.orthographicSize * cam.aspect);

        panYLimit = new Vector2((cam.orthographicSize * topVerticalOffsetPercentage) - mapSpriteRenderer.bounds.extents.y,
                                (cam.orthographicSize * bottomVerticalOffsetPercentage) - mapSpriteRenderer.bounds.extents.y);
    }








    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                offset = gameObject.transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
            }

            else if (Input.GetMouseButton(0))
            {
                ClampCamPosWithinMapBounds( Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset);
            }

            //Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
    void ClampCamPosWithinMapBounds(Vector3 newPosition)
    {



        newPosition.x = Mathf.Clamp(newPosition.x, -panXLimit, panXLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, panYLimit.x, -panYLimit.y);

        this.transform.position = newPosition;
    }

    void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoomLimit, maxZoomLimit);
        CalculatePanLimit();

        ClampCamPosWithinMapBounds(this.transform.position);
    }
}