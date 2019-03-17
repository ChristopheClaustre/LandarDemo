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

    private Camera m_camera;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Zoom(float p_increment)
    {
        m_camera.transform.position = m_camera.ScreenToWorldPoint(Input.mousePosition);
        m_camera.orthographicSize += p_increment;
    }

    public void VerticalMove(float p_increment, float p_vitesse)
    {
        m_camera.transform.Translate(0, p_increment * p_vitesse, 0);
    }

    public void HorizontalMove(float p_increment, float p_vitesse)
    {
        m_camera.transform.Translate(p_increment * p_vitesse, 0, 0);
    }

    public void CheckPosition(Rect p_limiter)
    {
        // Check Zoom
        float zoomMaxVertical = p_limiter.height / 2.0f;
        float zoomMaxHorizontal = p_limiter.width / (2.0f * m_camera.aspect);

        m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize, Setting.Instance.MinimalZoom, Mathf.Max(zoomMaxVertical, zoomMaxHorizontal));

        // Check Position
        Vector3 cameraPosition = m_camera.transform.position;

        cameraPosition.z = ComputeVerifiedMove(
            m_camera.transform.position.z,
            m_camera.orthographicSize,
            p_limiter.yMin,
            p_limiter.yMax);

        cameraPosition.x = ComputeVerifiedMove(
            m_camera.transform.position.x,
            m_camera.orthographicSize * m_camera.aspect,
            p_limiter.xMin,
            p_limiter.xMax);

        m_camera.transform.position = cameraPosition;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private float ComputeVerifiedMove(float p_nextValue, float p_cameraSize, float p_min, float p_max)
    {
        if (p_min + p_cameraSize >= p_max - p_cameraSize)
            return p_min + (p_max - p_min) / 2.0f;
        else
            return Mathf.Clamp(p_nextValue, p_min + p_cameraSize, p_max - p_cameraSize);
    }

    #endregion
}
