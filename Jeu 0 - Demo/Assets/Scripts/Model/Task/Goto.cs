/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Goto : Activity
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    private enum State
    {
        eBegin,
        eMoving,
        eFinished
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    private Vector3 m_localisation;
    private float m_orientation;
    private State m_state;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public Goto(Vector3 p_localisation, float p_orientation = float.NaN)
    {
        m_localisation = p_localisation;
        m_orientation = p_orientation;
    }

    public override bool DoAction(PersonnageScript p_personnageScript)
    {
        bool lActionFinished = false;
        switch(m_state)
        {
            case State.eBegin:
                Goto(p_personnageScript);
                m_state = State.eMoving;
                lActionFinished = false;
                break;
            case State.eMoving:
                if (GotoFinished(p_personnageScript))
                {
                    m_state = State.eFinished;
                    lActionFinished = true;
                }
                else
                {
                    lActionFinished = false;
                }
                break;
            case State.eFinished:
                lActionFinished = true;
                break;
        }

        return lActionFinished;
    }

    public override Vector3 GetLocalisation()
    {
        return m_localisation;
    }

    public override float GetOrientation()
    {
        return m_orientation;
    }

    public override bool HasOrientation()
    {
        return float.IsNaN(m_orientation);
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
