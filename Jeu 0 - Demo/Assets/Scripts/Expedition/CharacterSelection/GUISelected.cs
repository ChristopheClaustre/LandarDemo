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

    private CharacterScript m_personnageScript;
    private GameObject m_goSelec;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Awake()
    {
        m_personnageScript = GetComponent<CharacterScript>();
        m_goSelec = transform.Find("selector").gameObject;
    }

    private void OnEnable()
    {
        m_personnageScript.m_newSelectedEvent += NewSelected;
    }

    private void OnDisable()
    {
        m_personnageScript.m_newSelectedEvent -= NewSelected;
    }

    /********  OUR EVENTS       ************************/

    private void NewSelected()
    {
        m_goSelec.SetActive(m_personnageScript.Selected);
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
