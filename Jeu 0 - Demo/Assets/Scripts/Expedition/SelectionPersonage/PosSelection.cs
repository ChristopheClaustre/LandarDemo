/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class PosSelection :
    MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    public Movement Movement
    {
        get
        {
            return m_movement;
        }

        set
        {
            m_movement = value;
        }
    }

    public Destination Destination
    {
        get
        {
            return m_destination;
        }

        set
        {
            m_destination = value;
        }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private Movement m_movement;
    private Destination m_destination;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/


    /********  OUR MESSAGES     ************************/

    void OnRightUpAsButton()
    {
        m_movement.LoopJourney(m_destination);
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
