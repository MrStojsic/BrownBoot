using UnityEngine;
using UnityEngine.EventSystems;
//- If camera gitters when fully zoomed out, check that max zoom limit isnt so far out that its past the maps bounds.

public class ZoomPanCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer mapSpriteRenderer = default;

    private Vector3 touchStart = default;

    // The possible UI elements that may obscure the top and bottom of the map.
    [SerializeField] RectTransform frameHeaderRect = default;
    [SerializeField] RectTransform frameFooterRect = default;


    [Tooltip("This is set on Start, it uses the mapSpriteRenderer's bounds as the cameras bounds limits")]
    [SerializeField] private float panXLimit = default;
    [SerializeField] private Vector2 panYLimits = default;

    [SerializeField] private bool zoomAllowed = true;
    [SerializeField] private float minZoomLimit = 2;
    [SerializeField] private float maxZoomLimit = 3.8f;

    private Camera cam = default;

    private void Start()
    {
        cam = Camera.main;
        CalculateMapLimits();
        ClampCamPosWithinMapBounds(this.transform.position);
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (zoomAllowed && Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                ClampCamPosWithinMapBounds(this.transform.position + direction);
            }

            if (zoomAllowed)
            { Zoom(Input.GetAxis("Mouse ScrollWheel")); }
        }
    }

    void CalculateMapLimits()
    {
        // If theres a header/footer UI calulate the offset needed to not obscure the map to keep it fully displayed, otherwise set the offset to 0.
        float topVerticalOffset = (frameFooterRect == null) ? 0 : frameFooterRect.anchorMax.y * cam.orthographicSize * 2;
        float bottomVerticalOffset = (frameHeaderRect == null) ? 0 : (1 - frameHeaderRect.anchorMin.y) * cam.orthographicSize * 2;

        panXLimit = mapSpriteRenderer.bounds.extents.x - (cam.orthographicSize * cam.aspect);
        panYLimits = new Vector2((mapSpriteRenderer.bounds.extents.y - cam.orthographicSize) + topVerticalOffset,
                                (mapSpriteRenderer.bounds.extents.y - cam.orthographicSize) + bottomVerticalOffset);

        maxZoomLimit = Mathf.Min(mapSpriteRenderer.bounds.extents.x / cam.aspect, mapSpriteRenderer.bounds.extents.y);
        minZoomLimit = Mathf.Min(mapSpriteRenderer.bounds.extents.x / cam.aspect, mapSpriteRenderer.bounds.extents.y) * .5f;
    }

    void ClampCamPosWithinMapBounds(Vector3 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, -panXLimit, panXLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, -panYLimits.x, panYLimits.y);
        this.transform.position = newPosition;
    }

    void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoomLimit, maxZoomLimit);
        CalculateMapLimits();

        ClampCamPosWithinMapBounds(this.transform.position);
    }
}