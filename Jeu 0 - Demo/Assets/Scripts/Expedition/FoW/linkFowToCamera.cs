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
This class is use to link the Fog of War to a camera.
The Texture use to define the FoW is automatically scale to the camera viewport

This class should be associed to a camera
*/
public class linkFowToCamera : MonoBehaviour {

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
    public RenderTexture grayFow;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY METHODES   ************************/

    // Use this for initialization
    void Start () {
        //On resize la texture pour quelle correponde dynamiquement à la camera
        grayFow.width = Camera.main.pixelWidth;
        grayFow.height = Camera.main.pixelHeight;

        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float widthInWorld = (lowerLeft - lowerRight).magnitude;
        float heighInWorld = (lowerLeft - topLeft).magnitude;

        this.transform.localScale = new Vector3(widthInWorld * Camera.main.aspect, heighInWorld * Camera.main.aspect, 0);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float widthInWorld = (lowerLeft - lowerRight).magnitude;
        float heighInWorld = (lowerLeft - topLeft).magnitude;

        this.transform.localScale = new Vector3(widthInWorld, heighInWorld, 0);
    }

    /********  OUR METHODES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}