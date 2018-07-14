/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CameraMovementAPI :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /*private Rect MovementAllowedArea {
        get {
            if (m_movementAllowedAreaGO)
                return new Rect();

            Vector2 size = new Vector2(m_movementAllowedAreaGO.transform.localScale.x, m_movementAllowedAreaGO.transform.localScale.y);
            Vector2 position = new Vector2(m_movementAllowedAreaGO.transform.position.x, m_movementAllowedAreaGO.transform.position.y) - (size / 2);

            Rect rect = new Rect(position, size);
            return rect;
        }
    }*/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    [Header("Initialisation only")]
    [SerializeField]
    private GameObject m_movementAllowedAreaGO = null;

    private Rect m_movementAllowedArea = new Rect();

    private Camera m_camera;
    private Setting m_setting;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_setting = Setting.Instance;

        m_camera = GetComponent<Camera>();

        if (! m_movementAllowedAreaGO)
        {
            m_movementAllowedArea = new Rect();
        }
        else
        {
            Vector2 size = new Vector2(m_movementAllowedAreaGO.transform.localScale.x, m_movementAllowedAreaGO.transform.localScale.y);
            Vector2 position = new Vector2(m_movementAllowedAreaGO.transform.position.x, m_movementAllowedAreaGO.transform.position.z) - (size / 2);

            m_movementAllowedArea = new Rect(position, size);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckActualCameraPosition();
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void ZoomIn()
    {
        Zoom(-m_setting.ZoomVelocity);
        Vector3 newPosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        QueryNewCameraPosition(new Vector2(newPosition.x, newPosition.z));
    }
    public void ZoomOut()
    {
        Zoom(m_setting.ZoomVelocity);
        Vector3 newPosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        QueryNewCameraPosition(new Vector2(newPosition.x, newPosition.z));
    }

    public void MoveToLeft()
    {
        HorizontalMove(-m_setting.CameraVelocity, Time.deltaTime);
    }
    public void MoveToRight()
    {
        HorizontalMove(m_setting.CameraVelocity, Time.deltaTime);
    }
    public void MoveToTop()
    {
        VerticalMove(m_setting.CameraVelocity, Time.deltaTime);
    }
    public void MoveToBottom()
    {
        VerticalMove(-m_setting.CameraVelocity, Time.deltaTime);
    }

    public void MoveFromVector(Vector2 p_vector)
    {
        HorizontalMove(m_setting.CameraVelocity * p_vector.x, Time.deltaTime);
        VerticalMove  (m_setting.CameraVelocity * p_vector.y, Time.deltaTime);
    }

    public void QueryNewCameraPosition(Vector2 p_newPosition)
    {
        Vector3 actualPosition = m_camera.transform.position;

        HorizontalMove(p_newPosition.x - actualPosition.x, Time.deltaTime);
        VerticalMove(p_newPosition.y - actualPosition.y, Time.deltaTime);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    public void Zoom(float p_increment)
    {
        float zoomMaxVertical   = m_movementAllowedArea.height /  2.0f;
        float zoomMaxHorizontal = m_movementAllowedArea.width  / (2.0f * m_camera.aspect);

        m_camera.orthographicSize =
            Mathf.Clamp(m_camera.orthographicSize + p_increment, m_setting.MinimalZoom, Mathf.Max(zoomMaxVertical, zoomMaxHorizontal));
    }

    public void VerticalMove(float p_increment, float p_vitesse)
    {
        float verifiedMove = ComputeVerifiedMove(
            m_camera.transform.position.z + p_increment * p_vitesse,
            m_camera.orthographicSize,
            m_movementAllowedArea.yMin,
            m_movementAllowedArea.yMax);

        Vector3 cameraPosition = m_camera.transform.position;
        m_camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, verifiedMove);
    }

    private void HorizontalMove(float p_increment, float p_vitesse)
    {
        float verifiedMove = ComputeVerifiedMove(
            m_camera.transform.position.x + p_increment * p_vitesse,
            m_camera.orthographicSize * m_camera.aspect,
            m_movementAllowedArea.xMin,
            m_movementAllowedArea.xMax);

        Vector3 cameraPosition = m_camera.transform.position;
        m_camera.transform.position = new Vector3(verifiedMove, cameraPosition.y, cameraPosition.z);
    }

    private float ComputeVerifiedMove(float p_nextValue, float p_cameraSize, float p_min, float p_max)
    {
        if (p_min + p_cameraSize >= p_max - p_cameraSize)
            return p_min + (p_max - p_min) / 2.0f;
        else
            return Mathf.Clamp(p_nextValue, p_min + p_cameraSize, p_max - p_cameraSize);
    }

    private void CheckActualCameraPosition()
    {
        HorizontalMove(0, 1);
        VerticalMove(0, 1);
    }

    #endregion
}
