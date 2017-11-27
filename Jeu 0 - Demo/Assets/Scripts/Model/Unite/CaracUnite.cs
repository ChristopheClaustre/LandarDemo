/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
[System.Serializable]
public class CaracteristicUnite
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public enum EnumMalus
    {
        e_empoisonne,
        e_endormi,
        e_sonne,
        e_aveugle,
        e_perdu,
        e_Malus
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public int VieMax
    {
        get { return m_vieMax; }
    }
    public int Agilite
    {
        get { return m_agilite; }
    }
    public int Force
    {
        get { return m_force; }
    }
    public int Vision
    {
        get { return m_vision; }
    }
    public int Precision
    {
        get { return m_precision; }
    }
    public EnumMalus Malus
    {
        get { return m_malus; }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private int m_vieMax;
    [SerializeField] private int m_agilite;
    [SerializeField] private int m_force;
    [SerializeField] private int m_vision;
    [SerializeField] private int m_precision;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private EnumMalus m_malus;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public CaracteristicUnite(int p_vieMax, int p_agilite, int p_force, int p_vision, int p_precision, EnumMalus p_malus)
    {
        this.m_vieMax = p_vieMax;
        this.m_agilite = p_agilite;
        this.m_force = p_force;
        this.m_vision = p_vision;
        this.m_precision = p_precision;
        this.m_malus = p_malus;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
