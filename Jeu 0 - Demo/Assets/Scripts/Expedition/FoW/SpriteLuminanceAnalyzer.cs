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
public class SpriteLuminanceAnalyzer : MonoBehaviour {

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
    public SpriteRenderer spriteRenderer;
    public Camera renderCamera;

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
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(getSpriteRenderingLuminance(spriteRenderer.sprite, renderCamera));
    }

    /********  OUR METHODES     ************************/

    /********  PUBLIC           ************************/
    public float getSpriteRenderingLuminance(Sprite _sprite, Camera renderCamera)
    {
        renderCamera.orthographicSize = _sprite.rect.size.x / (2 * _sprite.pixelsPerUnit);
        renderCamera.aspect = _sprite.rect.size.y / _sprite.rect.size.x;

        //Creation d'une rendertexture
        RenderTexture _RTex = new RenderTexture(renderCamera.pixelWidth, renderCamera.pixelHeight, 0);
        renderCamera.targetTexture = _RTex;
        RenderTexture.active = _RTex;
        renderCamera.enabled = false;
        renderCamera.Render();
        float luminance = 0;
        float H, S, V;
        Texture2D mainTexture = new Texture2D(_RTex.width, _RTex.height);
        mainTexture.ReadPixels(new Rect(0, 0, mainTexture.width, mainTexture.height), 0, 0);
        mainTexture.Apply();
        RenderTexture.active = null;
        Color[] allColors = mainTexture.GetPixels();

        foreach (Color color in allColors)
        {
            Color.RGBToHSV(color, out H, out S, out V);
            if (V > luminance)
            {
                luminance = V;
            }
        }
        Destroy(_RTex);
        Destroy(mainTexture);

        return luminance;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
