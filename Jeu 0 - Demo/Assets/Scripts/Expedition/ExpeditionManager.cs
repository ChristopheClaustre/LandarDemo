/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ExpeditionManager :
    MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public int FormationSelected
    {
        get
        {
            return m_formationSelected;
        }
    }
    public List<int> Selected
    {
        get
        {
            if (m_selecting)
            {
                List<int> selec = new List<int>();

                // complétion de sélection
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    selec.AddRange(m_selected);
                    foreach (int i in m_newSelection)
                    {
                        if (selec.Contains(i))
                            selec.Remove(i);
                        else
                            selec.Add(i);
                    }
                }
                else // sélection simple
                {
                    selec.AddRange(m_newSelection);
                }

                return selec;
            }
            else
            {
                return m_selected;
            }
        }
    }
    
    // Singleton management about a list of GameObject
    public static List<GameObject> Persos
    {
        get
        {
            if (m_personnages == null)
            {
                m_personnages = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            }
            return m_personnages;
        }
    }

    // Singleton management about an ExpeditionManager instance
    public static ExpeditionManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ExpeditionManager>();
            }
            return m_instance;
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    // the formations
    public List<int>[] m_formations;

    /********  INSPECTOR        ************************/

    [SerializeField] private List<KeyCode> m_keys = new List<KeyCode>();
    [Header("-1 : no formation")]
    [Range(-1, 9)] private int m_formationSelected = -1;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private bool m_selecting = false;
    private List<int> m_selected;
    private List<int> m_newSelection;

    // Singleton management about an ExpeditionManager instance
    private static ExpeditionManager m_instance;

    // Singleton management about a list of GameObject
    private static List<GameObject> m_personnages;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_formations = new List<int>[m_keys.Count];
        m_newSelection = new List<int>();
        m_selected = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_keys.Count; i++)
        {
            if (Input.GetKeyUp(m_keys[i]))
            {
                // si on demande la modification de la formation i
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    m_formations[i] = new List<int>();
                    m_formations[i].AddRange(m_selected);
                }
                // si la formation existe
                if (m_formations[i] != null)
                {
                    m_formationSelected = i;
                    m_selected.Clear();
                    m_selected.AddRange(m_formations[i]);
                    gameObject.SendMessage("NewSelected", null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    private void clearNewSelection()
    {
        m_newSelection.Clear();
        m_selecting = false;
        gameObject.SendMessage("NewSelected", null, SendMessageOptions.DontRequireReceiver);
    }

    public void addToNewSelection(int ind)
    {
        if (!m_newSelection.Contains(ind))
            m_newSelection.Add(ind);
        m_selecting = true;
        gameObject.SendMessage("NewSelected", null, SendMessageOptions.DontRequireReceiver);
    }

    public void removeToNewSelection(int ind)
    {
        if (m_newSelection.Contains(ind))
            m_newSelection.Remove(ind);
        m_selecting = true;
        gameObject.SendMessage("NewSelected", null, SendMessageOptions.DontRequireReceiver);
    }

    public void applyNewSelection()
    {
        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            foreach (int i in m_newSelection)
            {
                if (m_selected.Contains(i))
                    m_selected.Remove(i);
                else
                    m_selected.Add(i);
            }
        }
        else // sélection simple
        {
            m_selected.Clear();
            m_selected.AddRange(m_newSelection);
        }

        m_formationSelected = -1;
        clearNewSelection();
    }

    public void monoSelection(int i)
    {
        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (m_selected.Contains(i))
                m_selected.Remove(i);
            else
                m_selected.Add(i);
        }
        else // sélection simple
        {
            m_selected.Clear();
            m_newSelection.Add(i);
            m_selected.AddRange(m_newSelection);
        }

        m_formationSelected = -1;
        clearNewSelection();
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
