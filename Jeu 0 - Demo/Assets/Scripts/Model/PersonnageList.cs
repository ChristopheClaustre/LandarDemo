/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
[System.Serializable]
public class PersonnageList :
    ListWrapper<Character>
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public List<Character> Personnages
    {
        get
        {
            return new List<Character>(m_list);
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PROTECTED        ************************/
    
    /********  PRIVATE          ************************/

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public PersonnageList() : base( new List<Character>() )
    {
    }

    public int GetConsumptionFood()
    {
        return 0; // TODO
    }

    // TODO : d'autres fonctions ...

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
