/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public abstract class Task
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

    public Task() {}

    public virtual bool CheckTask() { return true; }
    public abstract bool DoTask(UniteScript p_script);
    public abstract Vector3 GetLocalisation();

    public virtual bool HasOrientation() { return false; }
    public virtual float GetOrientation() { return float.NaN; }

    public void Goto(UniteScript p_script)
    {
        p_script.Goto(GetLocalisation());
    }

    public bool GotoFinished(UniteScript p_script)
    {
        return p_script.GotoFinished(GetLocalisation());
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
