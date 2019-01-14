/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections.Generic;
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
[System.Serializable]
public abstract class Unite
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public enum EnumIAState
    {
        e_ETDRF,
        e_ballade,
        e_chasse,
        e_attaque,
        e_pourchasse,
        e_fuite,
        e_IAState
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public int Vie
    {
        get { return m_vie; }
    }

    public int VieTemporaire
    {
        get { return m_vieTemporaire; }
    }

    public Unite Cible
    {
        get { return m_cible; }
    }

    public Vector3 Position
    {
        get { return m_position; }
    }

    public Description Desc
    {
        get { return m_description; }
    }

    public EnumIAState EtatIA
    {
        get { return m_etatIA; }
    }

    [System.Obsolete("Journey system is deprecated.")]
    public Journey Journey
    {
        get { return m_journey; }
        set { m_journey = value; }
    }

    public CaracteristicUnite Carac
    {
        get { return m_caracteristic; }
    }

    public float Luminosite
    {
        get { return m_luminosite; }
        set { m_luminosite = value; }
    }

/********  PROTECTED        ************************/

#endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/
    
    /********  INSPECTOR        ************************/
    
    /********  PROTECTED        ************************/
    
    /********  PRIVATE          ************************/

    private int m_vie = 0;
    private int m_vieTemporaire = 0;
    private Unite m_cible = null;
    private Vector3 m_position = Vector3.one;
    private Description m_description = null;
    private EnumIAState m_etatIA = EnumIAState.e_ETDRF;

    [System.Obsolete("Journey system is deprecated.")]
    [SerializeField] private Journey m_journey = new Journey();
    [SerializeField] private List<Task> m_tasks;

    private CaracteristicUnite m_caracteristic = null;
    private float m_luminosite = 0.0f;
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/
    
    /********  PUBLIC           ************************/

    public Unite()
    {

    }

    public Task GetTask(int i)
    {
        if (i < 0 || i >= m_tasks.Count) return null;
        return m_tasks[i];
    }

    public void AddTask(Task p_task)
    {
        m_tasks.Add(p_task);
    }

    public void ClearTasks()
    {
        m_tasks.Clear();
    }

    public void RemoveTask(int i)
    {
        m_tasks.RemoveAt(i);
    }

    public int CountTask()
    {
        return m_tasks.Count;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
