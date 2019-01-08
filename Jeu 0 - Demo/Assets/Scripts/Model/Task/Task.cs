/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public abstract class Activity
{
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/



    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public Activity() {}

    public virtual bool CheckAction() { return true; }
    public abstract bool DoAction(PersonnageScript p_personnageScript);
    public abstract Vector3 GetLocalisation();

    public virtual bool HasOrientation() { return false; }
    public virtual float GetOrientation() { return float.NaN; }

    public void Goto(PersonnageScript p_personnageScript)
    {
        // @todo
    }

    public bool GotoFinished(PersonnageScript p_personnageScript)
    {
        // @todo
        return true;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
