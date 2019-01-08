/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Expedition :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public enum EnumFormationName
    {
        e_expedition = -1,
        e_one = 0,
        e_two,
        e_three,
        e_four,
        e_five,
        e_six,
        e_seven,
        e_eight,
        e_nine,
        e_ten,
        e_FormationName
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public PersonnageList SelectedPersonnages
    {
        get
        {
            PersonnageList personnages;
            if ( ! IsAFormationSelected() )
            {
                personnages = m_selected;
            }
            else
            {
                personnages = GetFormation(m_formationSelected);
            }

            return personnages;
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /// <summary>
    /// Name of the selected formation. If the name is EnumFormationName.e_expedition, it means that no formation is selected at this time and so the player is in free selection mode
    /// </summary>
    [SerializeField] private EnumFormationName m_formationSelected = EnumFormationName.e_expedition;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    /// <summary> List of personnage </summary>
    private PersonnageList m_personnages;
    /// <summary> List of selected personnage </summary>
    private PersonnageList m_selected;
    /// <summary> List of formation </summary>
    private Formation[] m_formations = new Formation[(int) EnumFormationName.e_FormationName];

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_formations = new Formation[(int) EnumFormationName.e_FormationName];
        for (int i = 0; i < (int) EnumFormationName.e_FormationName; ++i)
        {
            m_formations[i] = new Formation();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/
    
    /********  PUBLIC           ************************/

    // SELECTION
    public Expedition SetSelectedPersonnages( PersonnageList p_personnages )
    {
        m_selected.Clear();
        m_selected.AddRange(p_personnages);
        SelectFormation(EnumFormationName.e_expedition);

        return this;
    }

    public Expedition AddPersonnagesToSelection( PersonnageList p_personnagesToAdd )
    {
        m_selected.AddRange(p_personnagesToAdd);
        SelectFormation(EnumFormationName.e_expedition);

        return this;
    }

    public Expedition RemovePersonnagesFromSelection( PersonnageList p_personnagesToAdd )
    {
        m_selected.RemoveRange(p_personnagesToAdd);
        SelectFormation(EnumFormationName.e_expedition);

        return this;
    }

    // FORMATION
    public bool IsAFormationSelected()
    {
        return IsAFormation(m_formationSelected);
    }

    public Formation GetFormation(EnumFormationName p_name)
    {
        if (!IsAFormation(p_name))
            return null;

        return m_formations[(int)p_name];
    }

    public void SelectFormation(EnumFormationName p_name)
    {
        if (IsAFormationSelected())
        {
            GetFormation(m_formationSelected).IsSelected = false;
        }
        else
        {
            m_selected.Clear();
        }
        m_formationSelected = p_name;
        if (IsAFormationSelected())
        {
            GetFormation(m_formationSelected).IsSelected = true;
        }
    }

    public Expedition SetCurrentSelectionAsFormation( EnumFormationName p_name )
    {
        GetFormation(p_name).Clear();
        GetFormation(p_name).AddRange(m_selected);
        m_selected.Clear();
        SelectFormation(p_name);

        return this;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private bool IsAFormation(EnumFormationName p_name)
    {
        return p_name != EnumFormationName.e_expedition;
    }

    #endregion
}
