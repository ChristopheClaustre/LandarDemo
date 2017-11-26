/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        e_EXPEDITION = -1,
        e_ONE = 0,
        e_TWO,
        e_THREE,
        e_FOUR,
        e_FIVE,
        e_SIX,
        e_SEVEN,
        e_EIGHT,
        e_NINE,
        e_TEN
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
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public static readonly int c_numberOfFormation = System.Enum.GetNames(typeof(EnumFormationName)).Length - 1; // -1 for the e_expedition formation

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /// <summary>
    /// Name of the selected formation. If the name is EnumFormationName.e_expedition, it means that no formation is selected at this time and so the player is in free selection mode
    /// </summary>
    [SerializeField] private EnumFormationName m_formationSelected = EnumFormationName.e_EXPEDITION;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    /// <summary> List of personnage </summary>
    private PersonnageList m_personnages;
    /// <summary> List of selected personnage </summary>
    private PersonnageList m_selected;
    /// <summary> List of formation </summary>
    private Formation[] m_formations = new Formation[c_numberOfFormation];

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_formations = new Formation[c_numberOfFormation];
        for (int i = 0; i < c_numberOfFormation; ++i)
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
        SelectFormation(EnumFormationName.e_EXPEDITION);

        return this;
    }

    public Expedition AddPersonnagesToSelection( PersonnageList p_personnagesToAdd )
    {
        m_selected.AddRange(p_personnagesToAdd);
        SelectFormation(EnumFormationName.e_EXPEDITION);

        return this;
    }

    public Expedition RemovePersonnagesFromSelection( PersonnageList p_personnagesToAdd )
    {
        m_selected.RemoveRange(p_personnagesToAdd);
        SelectFormation(EnumFormationName.e_EXPEDITION);

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

    public bool IsAFormation(EnumFormationName p_name)
    {
        return p_name != EnumFormationName.e_EXPEDITION;
    }

    #endregion
}
