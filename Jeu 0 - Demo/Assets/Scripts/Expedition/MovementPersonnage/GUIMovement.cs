/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GUIMovement :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private RectTransform m_directionFormation;
    private Movement m_movement;
    private bool m_working = false;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_directionFormation = (RectTransform)GameObject.Find("directionFormation").transform;
        m_movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_movement.RotationRequired)
        {
            m_directionFormation.anchoredPosition = m_movement.RightStartClick;
            m_directionFormation.localEulerAngles = new Vector3(0, 0, -m_movement.RotationValue);
            m_working = true;
        }
        else if (m_working)
        {
            // Reset
            m_directionFormation.anchoredPosition = new Vector2(Screen.width, Screen.height);
            m_directionFormation.localEulerAngles = Vector3.zero;
            m_working = false;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
