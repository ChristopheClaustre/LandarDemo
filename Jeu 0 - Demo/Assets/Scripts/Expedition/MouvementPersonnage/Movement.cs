using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float rotatingDistance = 25;
    [SerializeField, ReadOnly] private float rotationValue;
    [SerializeField, ReadOnly] private bool rotationRequired;
    [SerializeField, ReadOnly] private Vector3 rStartClick;
    [SerializeField, ReadOnly] private Vector3 rEndClick;
    [SerializeField, ReadOnly] private bool rPressed = false;

    public float RotationValue
    {
        get
        {
            return rotationValue;
        }
    }

    public bool RotationRequired
    {
        get
        {
            return rotationRequired;
        }
    }

    public Vector3 RightStartClick
    {
        get
        {
            return rStartClick;
        }
    }

    public Vector3 RightEndClick
    {
        get
        {
            return rEndClick;
        }
    }

    public bool RightPressed
    {
        get
        {
            return rPressed;
        }
    }

    // the script this script have to send to the infos
    private PersoController m_PersoController;

    private Journey m_journey;

    void Start()
    {
        m_PersoController = GetComponent<PersoController>();
        m_journey = new Journey();
    }

    void Update()
    {
        if (rPressed)
        {
            // right is pressed actually
            if (Input.GetMouseButton(1))
            {
                // get the current end of click
                rEndClick = Input.mousePosition;

                // if needed
                if (Mathf.Abs(Vector3.Distance(rStartClick, rEndClick)) > rotatingDistance)
                {
                    rotationRequired = true;
                    // calculate the orientation
                    rotationValue = angleBetweenVectorNAxis(rStartClick, rEndClick);
                }
                else
                {
                    rotationRequired = false;
                }
            }

            // right click
            if (Input.GetMouseButtonUp(1))
            {
                // get the final end of click
                rEndClick = Input.mousePosition;

                // get the new wanted destination ;)
                Destination dest;

                // retrieve the cible
                Vector3 cible3 = Camera.main.ScreenToWorldPoint(rStartClick);
                Vector2 cible2 = new Vector2(cible3.x, cible3.z);

                // create the new destination
                if (rotationRequired)
                    dest = new Destination(cible2, rotationValue);
                else
                    dest = new Destination(cible2);

                // add the new destination
                m_journey.addDestination(dest);
                gameObject.SendMessage("NewJourney", m_journey, SendMessageOptions.DontRequireReceiver);

                // finished !
                reinit();
            }
        }

        // Is it the end ??
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && m_journey.hasDestinations())
        {
            m_PersoController.Journey = m_journey;
            resetJourney();

            // finished !
            reinit();
        }
    }

    // right down
    void OnRightDown()
    {
        if (ClickValidator.Inst == null || !ClickValidator.Inst.isOnBlackFoW(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (!Input.GetMouseButton(0) && !(m_journey.WillLoop))
            {
                rStartClick = Input.mousePosition;
                rPressed = true;
            }
        }
    }

    public void LoopJourney(Destination p_candidat)
    {
        m_journey.addDestination(p_candidat);
        if (m_journey.WillLoop)
        {
            m_PersoController.Journey = m_journey;
            resetJourney();

            // finished !
            reinit();
        }
    }

    public bool canLoopOn(Destination d)
    {
        return m_journey.Destinations.IndexOf(d) < m_journey.Destinations.Count - 1;
    }

    // Some private function

    private void reinit()
    {
        rPressed = false;
        rStartClick = -Vector3.one;
        rEndClick = -Vector3.one;
        rotationRequired = false;
    }

    private void resetJourney()
    {
        m_journey = new Journey();
        gameObject.SendMessage("NewJourney", m_journey, SendMessageOptions.DontRequireReceiver);
    }

    private float angleBetweenVectorNAxis(Vector3 pivot, Vector3 point)
    {
        Vector2 diff = point - pivot;
        return Vector2.Angle(Vector2.up, diff) * ((point.x < pivot.x)?-1:+1);
    }
}
