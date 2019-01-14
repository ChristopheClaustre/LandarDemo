/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public abstract class UniteScript :
    MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    [System.Obsolete("Journey system is deprecated.")]
    protected abstract Journey Journey
    {
        get;
        set;
    }

    public List<Light> Lampes
    {
        get { return m_lampes; }
        set { m_lampes = value; }
    }

    public LightStateGenerator Lsg
    {
        get { return m_lsg; }
        set { m_lsg = value; }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    // les variables de model
    [SerializeField] protected Unite m_unite;

    /********  PRIVATE          ************************/

    private List<Light> m_lampes;
    private LightStateGenerator m_lsg;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    // Tasks management

    public void AddTask(Task p_task)
    {
        m_unite.AddTask(p_task);

        // warn all the scripts
        gameObject.SendMessage("TaskUpdated", null, SendMessageOptions.DontRequireReceiver);
    }

    public void ClearTasks()
    {
        m_unite.ClearTasks();

        // warn all the scripts
        gameObject.SendMessage("TaskUpdated", null, SendMessageOptions.DontRequireReceiver);
    }

    public void RemoveTask(int i)
    {
        m_unite.RemoveTask(i);

        // warn all the scripts
        gameObject.SendMessage("TaskUpdated", null, SendMessageOptions.DontRequireReceiver);
    }

    // gestion des trajets

    [System.Obsolete("Journey system is deprecated.")]
    public void Journey_nextDestination()
    {
        Journey t = Journey;
        t.NextDestination();
        Journey = t;

        // appel à la fonction qui prévient du changement
        gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
    }

    [System.Obsolete("Journey system is deprecated.")]
    public Destination Journey_currentDestination()
    {
        return Journey.CurrentDestination();
    }

    [System.Obsolete("Journey system is deprecated.")]
    public IList<Destination> Journey_Destinations()
    {
        return Journey.Destinations;
    }

    [System.Obsolete("Journey system is deprecated.")]
    public bool Journey_hasDestinations()
    {
        return Journey.HasDestinations();
    }

    [System.Obsolete("Journey system is deprecated.")]
    public bool Journey_WillLoop()
    {
        return Journey.WillLoop;
    }

    [System.Obsolete("Journey system is deprecated.")]
    public bool Journey_Boucler()
    {
        return Journey.Loop;
    }

    [System.Obsolete("Journey system is deprecated.")]
    public void SetJourney(Journey p_journey)
    {
        if (!Journey.Equals(p_journey))
        {
            Journey = p_journey;

            gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}

