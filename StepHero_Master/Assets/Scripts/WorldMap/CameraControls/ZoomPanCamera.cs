using UnityEngine;

public class ZoomPanCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer mapSpriteRenderer;

    Vector3 touchStart;

    [SerializeField] private float minZoomLimit = 2;
    [SerializeField] private float maxZoomLimit = 3.8f;

    [SerializeField] private Vector2 panLimit;
    private void Start()
    {
        panLimit = (mapSpriteRenderer.bounds.extents) - new Vector3(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ClampCamPosWithinMapBounds(Camera.main.transform.position + direction);
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    void ClampCamPosWithinMapBounds(Vector3 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, -panLimit.y, panLimit.y);
        Camera.main.transform.position = newPosition;
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoomLimit, maxZoomLimit);
        panLimit = (mapSpriteRenderer.bounds.extents) - new Vector3(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize, 0);

        ClampCamPosWithinMapBounds(Camera.main.transform.position);
    }
}
