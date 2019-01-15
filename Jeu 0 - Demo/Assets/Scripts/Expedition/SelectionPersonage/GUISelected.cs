/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GUISelected :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    private PersonnageScript m_personnageScript;
    private GameObject m_goSelec;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_personnageScript = GetComponent<PersonnageScript>();
        m_goSelec = transform.Find("selector").gameObject;
    }

    /********  OUR MESSAGES     ************************/

    private void NewSelected()
    {
        m_goSelec.SetActive(m_personnageScript.Selected);
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
