using UnityEngine;
using System.Collections;

public class CamMovement : MonoBehaviour {
    
    private bool movementRight = false;
    private bool movementLeft = false;
    private bool movementUp = false;
    private bool movementDown = false;

    public bool MovementRight
    {
        get
        {
            return movementRight;
        }
    }

    public bool MovementLeft
    {
        get
        {
            return movementLeft;
        }
    }

    public bool MovementUp
    {
        get
        {
            return movementUp;
        }
    }

    public bool MovementDown
    {
        get
        {
            return movementDown;
        }
    }

    private Camera cam;
    private Setting setting;

    public Vector2 min;
    public Vector2 max;

    private bool _isAxisZoomInUse = false;

    // Use this for initialization
    void Start () {
        setting = Setting.Inst;
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (cam.pixelRect.Contains(Input.mousePosition) && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            // pour faciliter la lecture/ecriture
            Vector3 pos = cam.transform.position;
            Vector3 mPos = Input.mousePosition;
            Vector3 worldMPos = cam.ScreenToWorldPoint(mPos);
            // pour éviter des calculs redondant
            float margin = Screen.height * setting.PercentMargin;
            float coeffZoom = cam.orthographicSize / setting.ZoomMin;

            // UP
            movementUp = mPos.y > Screen.height - margin && pos.z < (max.y - cam.orthographicSize);
            if (movementUp)
            {
                float coeffDist = (margin - (Screen.height - mPos.y)) / margin;
                pos.z += coeffDist * setting.MoveMaxSpeed * coeffZoom;
            }
            // DOWN
            movementDown = mPos.y < margin && pos.z > (min.y + cam.orthographicSize);
            if (movementDown)
            {
                float coeffDist = (margin - mPos.y) / margin;
                pos.z -= coeffDist * setting.MoveMaxSpeed * coeffZoom;
            }
            // RIGHT
            movementRight = mPos.x > Screen.width - margin && pos.x < (max.x - (cam.orthographicSize * cam.aspect));
            if (movementRight)
            {
                float coeffDist = (margin - (Screen.width - mPos.x)) / margin;
                pos.x += coeffDist * setting.MoveMaxSpeed * coeffZoom;
            }
            // LEFT
            movementLeft = mPos.x < margin && pos.x > (min.x + (cam.orthographicSize * cam.aspect));
            if (movementLeft)
            {
                float coeffDist = (margin - mPos.x) / margin;
                pos.x -= coeffDist * setting.MoveMaxSpeed * coeffZoom;
            }

            // zoom+
            if ((Input.GetAxis("Zoom") > 0 || Input.GetAxis("Mouse ScrollWheel") > 0)
                && cam.orthographicSize > setting.ZoomMax && !_isAxisZoomInUse)
            {
                cam.orthographicSize -= setting.ZoomSpeed;
                pos.x = worldMPos.x;
                pos.y = worldMPos.y;
                _isAxisZoomInUse = true;
            }
            // zoom-
            if ((Input.GetAxis("Zoom") < 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
                && cam.orthographicSize < setting.ZoomMin && !_isAxisZoomInUse)
            {
                cam.orthographicSize += setting.ZoomSpeed;
                pos.x = worldMPos.x;
                pos.y = worldMPos.y;
                _isAxisZoomInUse = true;
            }
            // zoom reset
            if (Input.GetAxis("Zoom") == 0 && Input.GetAxis("Mouse ScrollWheel") == 0 && _isAxisZoomInUse)
            {
                _isAxisZoomInUse = false;
            }

            // APPLY
            if (movementUp || movementDown || movementRight || movementLeft)
            {
                cam.transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
            }
        }

        // correction up and down
        Vector2 min_real = new Vector2(min.x + (cam.orthographicSize * cam.aspect), min.y + cam.orthographicSize);
        Vector2 max_real = new Vector2(max.x - (cam.orthographicSize * cam.aspect), max.y - cam.orthographicSize);
        Vector3 camPos = cam.transform.position;
        // rectif en x
        if (min_real.x > max_real.x)
            camPos.x = (min_real.x + max_real.x) / 2.0f;
        else
            camPos.x = Mathf.Clamp(camPos.x, min_real.x, max_real.x);
        // rectif en y
        if (min_real.y > max_real.y)
            camPos.z = (min_real.y + max_real.y) / 2.0f;
        else
            camPos.z = Mathf.Clamp(camPos.z, min_real.y, max_real.y);
        // application
        cam.transform.position = camPos;
    }
}
