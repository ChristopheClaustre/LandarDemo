/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class GUINavigation :
    MonoBehaviour
{
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    // pour faciliter la lecture
    private readonly static Quaternion c_orientationNull = Quaternion.AngleAxis(0, Vector3.zero);

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private GameObject m_prefabPosition;
    [SerializeField] private GameObject m_prefabLine;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private List<GameObject> m_instances;

    private PersonnageScript m_personnageScript;
    private GameObject m_goSelec;
    private GameObject m_startingLine = null;
    private GameObject m_endingLine = null;
    private bool m_show = false;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_personnageScript = GetComponent<PersonnageScript>();
        //m_personnageScript.abonnement(this);
        m_goSelec = this.transform.Find("selector").gameObject;
        m_instances = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // gestion input
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_show = true;
            NewJourney();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            m_show = false;
            EraseStaticPrefab();
            EraseDynamicPrefab();
        }
        // affichage de la première ligne
        if (m_show && m_personnageScript.Selected && m_personnageScript.Journey_hasDestinations())
        {
            EraseDynamicPrefab();
            m_startingLine = CreateLine(m_personnageScript.Journey_currentDestination().Cible, transform.position, m_prefabLine);
            if (m_personnageScript.Journey_Boucler())
            {
                IList<Destination> dests = m_personnageScript.Journey_Destinations();
                m_endingLine = CreateLine(dests[dests.Count - 1].Cible, transform.position, m_prefabLine);
            }
        }
    }

    /********  OUR MESSAGES     ************************/

    public void NewJourney()
    {
        if (m_personnageScript.Selected & m_show)
        {
            // d'abord on supprime tout
            EraseStaticPrefab();
            EraseDynamicPrefab();

            if (m_personnageScript.Journey_hasDestinations())
            {
                // on récupére les destinations
                IList<Destination> dests = m_personnageScript.Journey_Destinations();

                // on créé les marqueurs
                for (int i = 0; i < dests.Count; i++)
                {
                    // affichage de la position
                    GameObject go =
                        Instantiate(
                            m_prefabPosition,
                            dests[i].Cible,
                            c_orientationNull) as GameObject;
                    // si il faut affichage de l'orientation
                    if (!float.IsNaN(dests[i].OrientationFinale))
                    {
                        Quaternion orientation = Quaternion.AngleAxis(dests[i].OrientationFinale, Vector3.back);
                        GameObject direction = go.transform.Find("position_apparence/direction_apparence").gameObject;
                        direction.SetActive(true);
                        direction.transform.localRotation = orientation;
                    }
                    m_instances.Add(go);

                    // affichage du trait
                    if (i != 0)
                    {
                        go = CreateLine(dests[i].Cible, dests[i - 1].Cible, m_prefabLine);
                        m_instances.Add(go);
                    }
                }

                // on créé la première ligne
                m_startingLine = CreateLine(m_personnageScript.Journey_currentDestination().Cible, transform.position, m_prefabLine);
                if (m_personnageScript.Journey_Boucler())
                {
                    m_endingLine = CreateLine(dests[dests.Count - 1].Cible, transform.position, m_prefabLine);
                }
            }
        }
    }

    public void NewSelected()
    {
        bool isSelected = m_personnageScript.Selected;
        // on affiche l'état selectionné
        m_goSelec.SetActive(isSelected);
        // si il est sélectionné
        if (isSelected)
        {
            NewJourney();
        }
        else
        {
            EraseStaticPrefab();
            EraseDynamicPrefab();
        }
    }

    public void NewPerso()
    {
        // Rien à faire
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void EraseStaticPrefab()
    {
        // les prefab "static"
        foreach (GameObject i in m_instances)
        {
            Destroy(i);
        }
        m_instances.Clear();
    }

    private GameObject CreateLine(Vector3 p_start, Vector3 p_end, GameObject p_prefab)
    {
        GameObject go = Instantiate(p_prefab, p_start, c_orientationNull) as GameObject;
        LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, p_start);
        lineRenderer.SetPosition(1, p_end);

        return go;
    }

    private void EraseDynamicPrefab()
    {
        Destroy(m_startingLine);
        m_startingLine = null;
        Destroy(m_endingLine);
        m_endingLine = null;
    }

    #endregion
}
