using UnityEngine;
using UnityEngine.EventSystems;
//- If camera gitters when fully zoomed out, check that max zoom limit isnt so far out that its past the maps bounds.

public class ZoomPanCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer mapSpriteRenderer = default;

    Vector3 touchStart = default;

    [SerializeField] private float minZoomLimit = 2;
    [SerializeField] private float maxZoomLimit = 3.8f;

    [Tooltip("This is set on Start, it uses the mapSpriteRenderer's bounds as the cameras bounds limits")]
    private Vector2 panLimit = default;

    private Camera cam = default;

    private void Start()
    {
        cam = Camera.main;
        CalculatePanLimit();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.touchCount == 2)
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

            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
    void ClampCamPosWithinMapBounds(Vector3 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, -panLimit.y, panLimit.y);
        this.transform.position = newPosition;
    }

    void CalculatePanLimit()
    {
        panLimit = (mapSpriteRenderer.bounds.extents) - new Vector3(cam.orthographicSize * cam.aspect, cam.orthographicSize, 0);
    }

    void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoomLimit, maxZoomLimit);
        CalculatePanLimit();

        ClampCamPosWithinMapBounds(this.transform.position);
    }
}