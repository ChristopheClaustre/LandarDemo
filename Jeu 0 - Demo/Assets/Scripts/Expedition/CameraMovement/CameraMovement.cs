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

    [Header("Initialisation only")][SerializeField]
    private GameObject m_limiterGO = null;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private Camera m_camera;

    // Scripts
    private CameraMovementAPI m_API;
    private Setting m_setting;

    // The movement limiter
    private Rect m_limiter;

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

        if (!m_limiterGO)
        {
            m_limiter = new Rect();
        }
        else
        {
            Vector2 size = new Vector2(m_limiterGO.transform.localScale.x, m_limiterGO.transform.localScale.y);
            Vector2 position = new Vector2(m_limiterGO.transform.position.x, m_limiterGO.transform.position.z) - (size / 2);

            m_limiter = new Rect(position, size);
        }
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
                    MoveToTop();
                }
                // (Bottom)
                if (Input.mousePosition.y >= 0
                    && Input.mousePosition.y <= marginHeight)
                {
                    MoveToBottom();
                }
                // (Right)
                if (Input.mousePosition.x >= Screen.width - marginWidth
                    && Input.mousePosition.x <= Screen.width)
                {
                    MoveToRight();
                }
                // (Left)
                if (Input.mousePosition.x >= 0
                    && Input.mousePosition.x <= marginWidth)
                {
                    MoveToLeft();
                }
                
                // Zoom
                if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Zoom") > 0)
                {
                    ZoomIn();
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Zoom") < 0)
                {
                    ZoomOut();
                }
            }
        }

        // KEYBOARD
        // (Top)
        if (Input.GetAxis("Vertical") > 0)
        {
            MoveToTop();
        }
        // (Bottom)
        if (Input.GetAxis("Vertical") < 0)
        {
            MoveToBottom();
        }
        // (Right)
        if (Input.GetAxis("Horizontal") > 0)
        {
            MoveToRight();
        }
        // (Left)
        if (Input.GetAxis("Horizontal") < 0)
        {
            MoveToLeft();
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
                MoveFromVector(m_previousMiddleClick - (Vector2)Input.mousePosition);
            }

            m_previousMiddleClick = Input.mousePosition;
            m_middleClickHandled = true;
        }

        m_API.CheckPosition(m_limiter);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void ZoomIn()
    {
        m_API.Zoom(-m_setting.ZoomVelocity);
    }
    public void ZoomOut()
    {
        m_API.Zoom(m_setting.ZoomVelocity);
    }

    public void MoveToLeft()
    {
        m_API.HorizontalMove(-m_setting.CameraVelocity, Time.unscaledDeltaTime);
    }
    public void MoveToRight()
    {
        m_API.HorizontalMove(m_setting.CameraVelocity, Time.unscaledDeltaTime);
    }
    public void MoveToTop()
    {
        m_API.VerticalMove(m_setting.CameraVelocity, Time.unscaledDeltaTime);
    }
    public void MoveToBottom()
    {
        m_API.VerticalMove(-m_setting.CameraVelocity, Time.unscaledDeltaTime);
    }

    public void MoveFromVector(Vector2 p_vector)
    {
        m_API.HorizontalMove(p_vector.x, Time.unscaledDeltaTime);
        m_API.VerticalMove(p_vector.y, Time.unscaledDeltaTime);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
