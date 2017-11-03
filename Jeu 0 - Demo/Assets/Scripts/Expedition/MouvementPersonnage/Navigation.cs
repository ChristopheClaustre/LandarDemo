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
        e_none,
        e_pending,
        e_moving,
        e_rotatingAndMoving,
        e_rotating,
        e_NavigationState
    }

    #endregion
    #region Unity's inspector attributes
    /***************************************************/
    /***  INSPECTOR ATTRIBUTES  ************************/
    /***************************************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    // information about rotation
    [Header("Rotation")]
    [SerializeField]
    private float u_rotatingDistance = 0.5f;
    [SerializeField, Range(1, 4)]
    private float u_coeffRotation = 1.5f;

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    // aide pour l'animation
    private const string c_etatName = "seDeplace";

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    [Header("Navigation State")]
    [SerializeField, ReadOnly]
    private EnumNavigationState m_state = EnumNavigationState.e_none;

    // les variables de model
    private PersonnageScript m_persoScript;

    // the destination for next call of the pathfinder
    private Destination m_dest;

    // l'agent
    private UnityEngine.AI.NavMeshAgent m_agent;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  MESSAGES         ************************/

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
            case EnumNavigationState.e_none:
                break;
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
                    m_state = EnumNavigationState.e_none;
                }
                else if (!float.IsNaN(m_dest.OrientationFinale) && m_agent.remainingDistance <= u_rotatingDistance)
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
                if (m_state == EnumNavigationState.e_rotating && Mathf.Abs(transform.eulerAngles.y - m_dest.OrientationFinale) <= 1f)
                {
                    RotationEnding();
                    m_state = EnumNavigationState.e_none;
                }
                else
                {
                    Rotate();
                }
                break;
            default: break;
        }
    }

    /********  PUBLIC           ************************/

    public void NewSelected()
    {
        // Rien à faire
    }

    public void NewPerso()
    {
        // Rien à faire
    }

    public void NewJourney()
    {
        Go();
    }

    /********  PRIVATE          ************************/

    private void MovementEnding()
    {
        // ceci n'a rien à faire ici
        GetComponentInChildren<Animator>().SetBool(c_etatName, false);

        // Reinit
        m_persoScript.Journey_nextDestination();
    }

    private void RotationBegining()
    {
        // ceci n'a rien à faire ici
        GetComponentInChildren<Animator>().SetBool(c_etatName, false);

        // on s'assure que le syst de nav ne va pas pourir ma rotation à venir
        m_agent.updateRotation = false;
    }

    private void RotationEnding()
    {
        // on reactive la rotation pour le syst de nav
        m_agent.updateRotation = true;

        // Reinit
        m_persoScript.Journey_nextDestination();
    }

    private void Rotate()
    {
        transform.rotation =
            Quaternion.Slerp(transform.rotation,
                Quaternion.AngleAxis(m_dest.OrientationFinale, Vector3.up),
                Time.deltaTime * u_coeffRotation);
    }

    private void Go()
    {
        if (m_persoScript.Journey_hasDestinations())
        {
            // ceci n'a rien à faire ici
            GetComponentInChildren<Animator>().SetBool(c_etatName, true);

            // récuperation de la destination
            m_dest = m_persoScript.Journey_currentDestination();
            // on s'assure que le joueur va se déplacer en regardant dans la direction de son déplacement
            m_agent.updateRotation = true;
            // on applique la destination
            m_agent.SetDestination(new Vector3(m_dest.Cible.x, 0, m_dest.Cible.y));

            // on va se mettre à bouger
            m_state = EnumNavigationState.e_pending;
        }
    }

    #endregion
}
