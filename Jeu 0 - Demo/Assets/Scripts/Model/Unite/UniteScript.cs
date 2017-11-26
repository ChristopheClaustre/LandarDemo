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

    // gestion des trajets

    public void Journey_nextDestination()
    {
        Journey t = Journey;
        t.NextDestination();
        Journey = t;

        // appel à la fonction qui prévient du changement
        gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
    }

    public Destination Journey_currentDestination()
    {
        return Journey.CurrentDestination();
    }

    public IList<Destination> Journey_Destinations()
    {
        return Journey.Destinations;
    }

    public bool Journey_hasDestinations()
    {
        return Journey.HasDestinations();
    }

    public bool Journey_WillLoop()
    {
        return Journey.WillLoop;
    }

    public bool Journey_Boucler()
    {
        return Journey.Loop;
    }

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

