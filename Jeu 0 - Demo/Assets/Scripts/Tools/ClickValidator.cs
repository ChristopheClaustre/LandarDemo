/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.IO;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ClickValidator : 
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    private static ClickValidator s_clickValidatorInstance;
    // the black FoW's cam
    private Camera m_fowCamera;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_fowCamera = GetComponent<Camera>();
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/
    // Singleton management about an ClickValidator instance
    public static ClickValidator Inst
    {
        get
        {
            if (s_clickValidatorInstance == null)
            {
                s_clickValidatorInstance = FindObjectOfType<ClickValidator>();
            }
            return s_clickValidatorInstance;
        }
    }

    //Warning, this function works only if the FoW's camera have the exact same dimension than the FoW's mesh
    public bool isOnBlackFoW(Vector3 p_positionOnFoW)
    {
        Vector3 screenPos = m_fowCamera.WorldToScreenPoint(p_positionOnFoW);
        screenPos = new Vector3(screenPos.x, m_fowCamera.pixelHeight - screenPos.y, screenPos.z);

        RenderTexture renderTexture = m_fowCamera.targetTexture;
        RenderTexture.active = renderTexture;

        // Read pixels
        Texture2D texture2D = new Texture2D(1, 1);
        Rect rect = new Rect(Mathf.FloorToInt(screenPos.x), Mathf.FloorToInt(screenPos.y), 1, 1);
        texture2D.ReadPixels(rect, 0, 0);
        texture2D.Apply();
        Color32 pixelSample = texture2D.GetPixel(0, 0);

        return (pixelSample.r > 0 || pixelSample.g > 0 || pixelSample.b > 0);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    
}