using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomPanCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer mapSpriteRenderer;

    Vector3 touchStart;

    [SerializeField] private float minZoomLimit = 2;
    [SerializeField] private float maxZoomLimit = 3.8f;

    [SerializeField] private Vector2 panLimit;

    private Camera camera;

    [SerializeField] private Transform focusTransform;

    private void Start()
    {
        camera = Camera.main;
        panLimit = (mapSpriteRenderer.bounds.extents) - new Vector3(camera.orthographicSize * camera.aspect, camera.orthographicSize, 0);
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = camera.ScreenToWorldPoint(Input.mousePosition);
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
                Vector3 direction = touchStart - camera.ScreenToWorldPoint(Input.mousePosition);
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

    void Zoom(float increment)
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - increment, minZoomLimit, maxZoomLimit);
        panLimit = (mapSpriteRenderer.bounds.extents) - new Vector3(camera.orthographicSize * camera.aspect, camera.orthographicSize, 0);

        ClampCamPosWithinMapBounds(this.transform.position);
    }
}