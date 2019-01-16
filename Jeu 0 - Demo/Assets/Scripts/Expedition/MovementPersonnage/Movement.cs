/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Movement :
    MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public float RotationValue
    {
        get { return m_rotationValue; }
    }

    public bool RotationRequired
    {
        get { return m_rotationRequired; }
    }

    public Vector3 RightStartClick
    {
        get { return m_rightClickStart; }
    }

    public Vector3 RightEndClick
    {
        get { return m_rightClickEnd; }
    }

    public bool RightPressed
    {
        get { return m_rightButtonPressed; }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private float m_rotatingDistance = 25;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private float m_rotationValue;
    private bool m_rotationRequired;
    private Vector3 m_rightClickStart;
    private Vector3 m_rightClickEnd;
    private bool m_rightButtonPressed = false;

    // the script this script has to send the infos to
    private PersoController m_PersoController;

    private Journey m_journey;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_PersoController = GetComponent<PersoController>();
        m_journey = new Journey();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rightButtonPressed)
        {
            // right is pressed actually
            if (Input.GetMouseButton(1))
            {
                // get the current end of click
                m_rightClickEnd = Input.mousePosition;

                // if needed
                if (Mathf.Abs(Vector3.Distance(m_rightClickStart, m_rightClickEnd)) > m_rotatingDistance)
                {
                    m_rotationRequired = true;
                    // calculate the orientation
                    m_rotationValue = AngleBetweenVectorNAxis(m_rightClickStart, m_rightClickEnd);
                }
                else
                {
                    m_rotationRequired = false;
                }
            }

            // right click
            if (Input.GetMouseButtonUp(1))
            {
                // get the final end of click
                m_rightClickEnd = Input.mousePosition;

                // get the new wanted destination ;)
                Destination dest;

                // retrieve the cible
                Vector3 cible3 = Camera.main.ScreenToWorldPoint(m_rightClickStart);
                Vector2 cible2 = new Vector2(cible3.x, cible3.z);

                // create the new destination
                if (m_rotationRequired)
                    dest = new Destination(cible2, m_rotationValue);
                else
                    dest = new Destination(cible2);

                // add the new destination
                m_journey.AddDestination(dest);
                gameObject.SendMessage("NewJourney", m_journey, SendMessageOptions.DontRequireReceiver);

                // finished !
                Reinit();
            }
        }

        // Is it the end ??
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && m_journey.HasDestinations())
        {
            m_PersoController.Journey = m_journey;
            ResetJourney();

            // finished !
            Reinit();
        }
    }

    /********  OUR MESSAGES     ************************/

    // right down
    void OnRightDown()
    {
        if (ClickValidator.Inst == null || !ClickValidator.Inst.isOnBlackFoW(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (!Input.GetMouseButton(0) && !(m_journey.WillLoop))
            {
                m_rightClickStart = Input.mousePosition;
                m_rightButtonPressed = true;
            }
        }
    }

    /********  PUBLIC           ************************/

    [System.Obsolete("Journey system is deprecated.")]
    public void LoopJourney(Destination p_candidat)
    {
        m_journey.AddDestination(p_candidat);
        if (m_journey.WillLoop)
        {
            m_PersoController.Journey = m_journey;
            ResetJourney();

            // finished !
            Reinit();
        }
    }

    public bool canLoopOn(Destination p_destination)
    {
        return m_journey.Destinations.IndexOf(p_destination) < m_journey.Destinations.Count - 1;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void Reinit()
    {
        m_rightButtonPressed = false;
        m_rightClickStart = -Vector3.one;
        m_rightClickEnd = -Vector3.one;
        m_rotationRequired = false;
    }

    [System.Obsolete("Journey system is deprecated.")]
    private void ResetJourney()
    {
        m_journey = new Journey();
        gameObject.SendMessage("NewJourney", m_journey, SendMessageOptions.DontRequireReceiver);
    }

    private float AngleBetweenVectorNAxis(Vector3 p_pivot, Vector3 p_point)
    {
        Vector2 diff = p_point - p_pivot;
        return Vector2.Angle(Vector2.up, diff) * ((p_point.x < p_pivot.x)?-1:+1);
    }

    #endregion
}
