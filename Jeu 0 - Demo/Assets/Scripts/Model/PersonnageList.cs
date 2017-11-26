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
    ListWrapper<Personnage>
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public List<Personnage> Personnages
    {
        get
        {
            return new List<Personnage>(m_list);
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

    public PersonnageList() : base( new List<Personnage>() )
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
