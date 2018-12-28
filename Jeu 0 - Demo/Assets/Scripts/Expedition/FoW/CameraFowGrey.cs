/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/

/*
This class is use to compute a luminance value on a sprite look by a camera
This class should be associed the Game Object using the sprite renderer to analyse
*/
public class CameraFowGrey : MonoBehaviour
{

    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Unity GUI property
    /***************************************************/
    /***  UNITY GUI PROPERTY    ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Assessor
    /***************************************************/
    /***  ASSESSOR              ************************/
    /***************************************************/


    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    [SerializeField]
    public GameObject m_GreyPlane;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    private Camera m_FowCamera;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY METHODES   ************************/

    // Use this for initialization
    void Start()
    {
        //Align FoW camera to main camera
        m_FowCamera = GetComponent<Camera>();
        m_FowCamera.aspect = Camera.main.aspect;
        m_FowCamera.orthographicSize = Camera.main.orthographicSize;
        m_FowCamera.fieldOfView = Camera.main.fieldOfView;

        //Fit the plane to camera frustum
        float height = m_FowCamera.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        m_GreyPlane.transform.localScale = new Vector3(width / 10.0f, 1, height / 10.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //Follow to main camera
        m_FowCamera.aspect = Camera.main.aspect;
        m_FowCamera.orthographicSize = Camera.main.orthographicSize;
        m_FowCamera.fieldOfView = Camera.main.fieldOfView;

        //Fit the plane to camera frustum
        float height = m_FowCamera.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        m_GreyPlane.transform.localScale = new Vector3(width / 10.0f, 1, height / 10.0f);
    }

    /********  OUR METHODES     ************************/

    /********  PUBLIC           ************************/


    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
