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
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    public delegate void NewSelected();

    #endregion
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
                if (m_newSelectedEvent != null)
                    m_newSelectedEvent();
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

    public event NewSelected m_newSelectedEvent;

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
    protected override void Update()
    {
        base.Update();
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
