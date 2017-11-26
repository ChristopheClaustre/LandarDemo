/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class SelectionBox :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    public bool LeftPressed
    {
        get { return m_pressed; }
    }

    public Vector3 LeftStartClick
    {
        get { return m_begin; }
    }

    public Vector3 LeftEndClick
    {
        get { return m_end; }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private bool m_pressed = false;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private Vector3 m_begin;
    private Vector3 m_worldBegin;
    private Vector3 m_end;
    private Vector3 m_worldEnd;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Update is called once per frame
    void Update()
    {
        if (m_pressed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ExpeditionManager.Instance.applyNewSelection();

                // Reset
                m_pressed = false;
                m_begin = Vector3.zero;
                m_end = Vector3.zero;
                m_worldBegin = Vector3.zero;
                m_worldEnd = Vector3.zero;
            }
            else
            {
                // on récupère la position de fin courante
                m_end = Input.mousePosition;
                m_worldEnd = Camera.main.ScreenToWorldPoint(m_end);

                // quels sont les persos dans la selectionBox ?
                for (int i = 0; i < ExpeditionManager.Persos.Count; i++)
                {
                    GameObject p = ExpeditionManager.Persos[i];
                    Vector3 pPos = p.transform.position;
                    bool xCheck = false;
                    bool zCheck = false;

                    // on check en X (en vérifiant la forme de la box)
                    if (m_worldBegin.x < m_worldEnd.x)
                        xCheck = (m_worldBegin.x <= pPos.x && pPos.x <= m_worldEnd.x);
                    else
                        xCheck = (m_worldEnd.x <= pPos.x && pPos.x <= m_worldBegin.x);

                    // on check en Z (en vérifiant la forme de la box)
                    if (m_worldBegin.z < m_worldEnd.z)
                        zCheck = (m_worldBegin.z <= pPos.z && pPos.z <= m_worldEnd.z);
                    else
                        zCheck = (m_worldEnd.z <= pPos.z && pPos.z <= m_worldBegin.z);

                    // si c'est bon, c'est bon !
                    if (xCheck && zCheck)
                        ExpeditionManager.Instance.addToNewSelection(i);
                    else
                        ExpeditionManager.Instance.removeToNewSelection(i);
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (!Input.GetMouseButton(1))
        {
            // on récupère la position de début
            m_begin = Input.mousePosition;
            m_worldBegin = Camera.main.ScreenToWorldPoint(m_begin);
            m_pressed = true;
            // on init la fin
            m_end = m_begin;
            m_worldEnd = m_worldBegin;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
