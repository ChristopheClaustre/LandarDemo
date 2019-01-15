/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Navigation :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PRIVATE          ************************/

    // État de la nav
    private enum EnumNavigationState
    {
        e_pending,
        e_moving,
        e_rotatingAndMoving,
        e_rotating,
        e_finished,
        e_stopped,
        e_NavigationState
    }

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    // aide pour l'animation
    private const string STATE_NAME = "seDeplace";

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    // information about rotation
    [Header("Rotation")]
    [SerializeField] private float m_rotatingDistance = 0.5f;
    [SerializeField, Range(1, 4)] private float m_coeffRotation = 1.5f;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private EnumNavigationState m_state = EnumNavigationState.e_finished;

    // les variables de model
    private PersonnageScript m_persoScript;

    // the destination for next call of the pathfinder
    private Vector3 m_destination;
    private float m_orientation;

    // l'agent
    private UnityEngine.AI.NavMeshAgent m_agent;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_persoScript = GetComponent<PersonnageScript>();
        m_agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case EnumNavigationState.e_pending:
                if (!m_agent.pathPending)
                {
                    m_state = EnumNavigationState.e_moving;
                }
                break;
            case EnumNavigationState.e_moving:
                // fin déplacement
                if (m_agent.remainingDistance - m_agent.stoppingDistance <= float.Epsilon)
                {
                    MovementEnding();
                    m_state = EnumNavigationState.e_finished;
                }
                else if (!float.IsNaN(m_orientation) && m_agent.remainingDistance <= m_rotatingDistance)
                {
                    RotationBegining();
                    m_state = EnumNavigationState.e_rotatingAndMoving;
                }
                break;
            case EnumNavigationState.e_rotatingAndMoving:
                // fin déplacement
                if (m_agent.remainingDistance - m_agent.stoppingDistance <= float.Epsilon)
                {
                    m_state = EnumNavigationState.e_rotating;
                }
                break;
            case EnumNavigationState.e_rotating:
                // fin de rotation
                if (m_state == EnumNavigationState.e_rotating && Mathf.Abs(transform.eulerAngles.y - m_orientation) <= 1f)
                {
                    RotationEnding();
                    m_state = EnumNavigationState.e_finished;
                }
                else
                {
                    Rotate();
                }
                break;
            case EnumNavigationState.e_finished:
                break;
            case EnumNavigationState.e_stopped:
                break;
            default: break;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Goto(Vector3 p_destination)
    {
        // store/init data
        m_destination = p_destination;
        m_orientation = float.NaN;

        // agent will try to face the movement direction while moving
        m_agent.updateRotation = true;

        m_agent.SetDestination(p_destination);

        // currently computing path
        m_state = EnumNavigationState.e_pending;
    }

    public bool GotoFinished()
    {
        return m_state == EnumNavigationState.e_finished;
    }

    public Vector3 CurrentDestination()
    {
        return m_destination;
    }

    public void Stop()
    {
        if (m_state == EnumNavigationState.e_finished) return;

        m_agent.isStopped = true;
        m_state = EnumNavigationState.e_stopped;
    }

    public bool IsStopped()
    {
        return m_state == EnumNavigationState.e_stopped;
    }

    /********  PRIVATE          ************************/

    private void MovementBeginning()
    {
        // this shouldn't be here
        GetComponentInChildren<Animator>().SetBool(STATE_NAME, true);
    }

    private void MovementEnding()
    {
        // this shouldn't be here
        GetComponentInChildren<Animator>().SetBool(STATE_NAME, false);
    }

    private void RotationBegining()
    {
        // this shouldn't be here
        GetComponentInChildren<Animator>().SetBool(STATE_NAME, false);

        // we don't need anymore to face movement direction
        m_agent.updateRotation = false;
    }

    private void RotationEnding()
    {
        // agent must face movement direction now
        m_agent.updateRotation = true;
    }

    private void Rotate()
    {
        transform.rotation =
            Quaternion.Slerp(transform.rotation,
                Quaternion.AngleAxis(m_orientation, Vector3.up),
                Time.deltaTime * m_coeffRotation);
    }

    #endregion
}
