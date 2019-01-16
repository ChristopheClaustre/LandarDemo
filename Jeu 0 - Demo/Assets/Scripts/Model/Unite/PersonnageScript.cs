/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class PersonnageScript :
    UniteScript
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public virtual Personnage Perso
    {
        get { return (Personnage)m_unite; }
        set
        {
            if (!m_unite.Equals(value))
            {
                m_unite = value;
                gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
                gameObject.SendMessage("NewPerso", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    [System.Obsolete("Journey system is deprecated.")]
    protected override Journey Journey
    {
        get { return Perso.Journey; }
        set
        {
            if (!Perso.Journey.Equals(value))
            {
                Perso.Journey = value;
                gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
                gameObject.SendMessage("NewPerso", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public bool Selected
    {
        get { return m_selected; }
        set
        {
            if (m_selected != value)
            {
                m_selected = value;
                gameObject.SendMessage("NewSelected", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private bool m_selected = false;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    //new Personnage m_unite;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_unite = new Personnage();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
