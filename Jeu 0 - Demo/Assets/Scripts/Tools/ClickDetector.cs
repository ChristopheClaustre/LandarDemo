/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ClickDetector :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private bool m_handleRightClick = true;
    [SerializeField] private bool m_handleMiddleClick = false;

    [SerializeField] private string m_onRightClickMethodName  = "OnRightUpAsButton";
    [SerializeField] private string m_onRightDownMethodName   = "OnRightDown";
    [SerializeField] private string m_onRightUpMethodName     = "OnRightUp";
    [SerializeField] private string m_onMiddleClickMethodName = "OnMiddleUpAsButton";
    [SerializeField] private string m_onMiddleDownMethodName  = "OnMiddleDown";
    [SerializeField] private string m_onMiddleUpMethodName    = "OnMiddleUp";

    public LayerMask m_layerMask;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private GameObject m_rightClickedGO;
    private GameObject m_middleClickedGO;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Update is called once per frame
    void Update()
    {
        GameObject clickedGmObj = null;
        bool clickedGmObjAcquired = false;
        // Right down
        if (m_handleRightClick && Input.GetMouseButtonDown(1))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(m_onRightDownMethodName, null, SendMessageOptions.DontRequireReceiver);
                m_rightClickedGO = clickedGmObj;
            }
        }
        // Middle down
        if (m_handleMiddleClick && Input.GetMouseButtonDown(2))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(m_onMiddleDownMethodName, null, SendMessageOptions.DontRequireReceiver);
                m_middleClickedGO = clickedGmObj;
            }
        }
        // Right up and up as button
        if (m_handleRightClick && Input.GetMouseButtonUp(1))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(m_onRightUpMethodName, null, SendMessageOptions.DontRequireReceiver);
                if (clickedGmObj == m_rightClickedGO)
                {
                    clickedGmObj.SendMessage(m_onRightClickMethodName, null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        // Middle up and up as button
        if (m_handleMiddleClick && Input.GetMouseButtonUp(2))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(m_onMiddleUpMethodName, null, SendMessageOptions.DontRequireReceiver);
                if (clickedGmObj == m_middleClickedGO)
                {
                    clickedGmObj.SendMessage(m_onMiddleClickMethodName, null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private GameObject GetClickedGameObject()
    {
        // Builds a ray from camera point of view to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_layerMask))
            return hit.transform.gameObject;
        else
            return null;
    }

    #endregion
}
