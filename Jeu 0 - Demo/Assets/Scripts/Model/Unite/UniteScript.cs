/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

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
    protected virtual void Update()
    {
        if (m_unite.CountTask() == 0) return;

        Task task = m_unite.GetTask(0);
        bool taskFinished = task.DoTask(this);

        if (taskFinished)
            m_unite.RemoveTask(0);
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    // Tasks management

    public void SetTask(Task p_task, bool p_append = false)
    {
        if (! p_append)
            m_unite.ClearTasks();

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

    // navigation

    public void Goto(Vector3 p_vector3)
    {
        GetComponent<Navigation>().Goto(p_vector3);
    }

    public bool GotoFinished(Vector3 p_vector3)
    {
        Navigation nav = GetComponent<Navigation>();
        return nav.GotoFinished() && nav.CurrentDestination() == p_vector3;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}

