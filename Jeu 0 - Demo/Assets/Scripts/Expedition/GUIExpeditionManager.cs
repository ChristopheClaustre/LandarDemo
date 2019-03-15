/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GUIExpeditionManager :
    MonoBehaviour
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private ExpeditionManager m_expeditionManager;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_expeditionManager = ExpeditionManager.Instance;
    }

    /********  OUR MESSAGES     ************************/

    public void NewSelected()
    {
        // affichage
        for (int i = 0; i < ExpeditionManager.Persos.Count; i++)
        {
            ExpeditionManager.Persos[i].GetComponent<CharacterScript>().Selected = m_expeditionManager.Selected.Contains(i);
        }
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
