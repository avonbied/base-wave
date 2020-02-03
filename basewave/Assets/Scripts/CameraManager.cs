using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public int CameraMinDistance = 5;
    public int CameraMaxDistance = 15;

    private Camera cam;
    Vector2 LastMousePos;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y, CameraMinDistance, CameraMaxDistance);

        if (Input.GetMouseButtonDown(2))
        {

            LastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            if (LastMousePos != (Vector2)Input.mousePosition)
            {
                Vector2 oldpos = cam.ScreenToWorldPoint(LastMousePos);
                Vector2 newpos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = (Vector2)cam.transform.position - (newpos - oldpos);
                pos.z = -10;
                cam.transform.position = pos;
            }
            LastMousePos = Input.mousePosition;
        }
    }
}