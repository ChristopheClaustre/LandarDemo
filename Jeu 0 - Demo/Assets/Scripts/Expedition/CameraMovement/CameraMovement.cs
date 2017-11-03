/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CameraMovement :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private Camera m_camera;

    // Scripts
    private CameraMovementAPI m_API;
    private Setting m_setting;

    // Middle click management
    private Vector2 m_previousMiddleClick = Vector2.zero;
    private bool m_middleClickHandled = false;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_API = GetComponent<CameraMovementAPI>();
        m_setting = Setting.Instance;

        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            // MOUSE POSITION
            if (m_camera.pixelRect.Contains(Input.mousePosition))
            {
                // Input.mousePosition :
                // The bottom-left of the screen or window is at (0, 0).
                // The top-right of the screen or window is at (Screen.width, Screen.height).

                int marginHeight = Mathf.RoundToInt(Screen.height * m_setting.MouseMarginInPercent);
                int marginWidth = Mathf.RoundToInt(Screen.width * m_setting.MouseMarginInPercent);

                // (Top)
                if (Input.mousePosition.y >= Screen.height - marginHeight
                    && Input.mousePosition.y <= Screen.height)
                {
                    m_API.MoveToTop();
                }
                // (Bottom)
                if (Input.mousePosition.y >= 0
                    && Input.mousePosition.y <= marginHeight)
                {
                    m_API.MoveToBottom();
                }
                // (Right)
                if (Input.mousePosition.x >= Screen.width - marginWidth
                    && Input.mousePosition.x <= Screen.width)
                {
                    m_API.MoveToRight();
                }
                // (Left)
                if (Input.mousePosition.x >= 0
                    && Input.mousePosition.x <= marginWidth)
                {
                    m_API.MoveToLeft();
                }
            }

            // KEYBOARD
            // (Top)
            if (Input.GetAxis("Vertical") > 0)
            {
                m_API.MoveToTop();
            }
            // (Bottom)
            if (Input.GetAxis("Vertical") < 0)
            {
                m_API.MoveToBottom();
            }
            // (Right)
            if (Input.GetAxis("Horizontal") > 0)
            {
                m_API.MoveToRight();
            }
            // (Left)
            if (Input.GetAxis("Horizontal") < 0)
            {
                m_API.MoveToLeft();
            }
        }

        // Zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Zoom") > 0)
        {
            m_API.ZoomIn();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Zoom") < 0)
        {
            m_API.ZoomOut();
        }

        // Middle click movement
        if (Input.GetMouseButtonDown(2))
        {
            m_previousMiddleClick = Input.mousePosition;
            m_middleClickHandled = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            m_middleClickHandled = false;
        }
        else if (Input.GetMouseButton(2))
        {
            if (m_middleClickHandled)
            {
                m_API.MoveFromVector(m_previousMiddleClick - (Vector2)Input.mousePosition);
            }

            m_previousMiddleClick = Input.mousePosition;
            m_middleClickHandled = true;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
