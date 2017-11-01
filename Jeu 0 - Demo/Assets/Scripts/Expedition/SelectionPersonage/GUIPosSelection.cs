/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GUIPosSelection :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private Vector3 m_originalScale;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    void OnMouseEnter()
    {
        // on retient la taille de marqueur actuel
        m_originalScale = transform.localScale;
        PosSelection l1_pos = GetComponent<PosSelection>();
        if (l1_pos.Movement.canLoopOn(l1_pos.Destination))
            // modification de la taille du marqueur
            transform.localScale = m_originalScale * 2f;
    }

    void OnMouseExit()
    {
        transform.localScale = m_originalScale;
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
