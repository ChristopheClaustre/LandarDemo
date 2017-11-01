/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/

public class GUICreateJourney :
    MonoBehaviour
{
    #region Unity GUI property
    /***************************************************/
    /***  UNITY GUI PROPERTY    ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    [SerializeField, ReadOnly]
    private GameObject m_prefabPosition;
    [SerializeField, ReadOnly]
    private GameObject m_prefabLine;

    #endregion
    #region Assessor
    /***************************************************/
    /***  ASSESSOR              ************************/
    /***************************************************/


    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private List<GameObject> m_instances;
    
    private Journey m_journey;

    // pour faciliter la lecture
    private static readonly Quaternion c_orientationNull = Quaternion.AngleAxis(0, Vector3.zero);

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    void Start()
    {
        m_instances = new List<GameObject>();
        m_journey = new Journey();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    public void NewJourney(Journey p_journey)
    {
        m_journey = p_journey;

        // d'abord on supprime tout
        EraseStaticPrefab();

        if (m_journey.hasDestinations())
        {
            // on récupére les destinations
            IList<Destination> l1_dests = m_journey.Destinations;

            // on créé les marqueurs
            for (int i = 0; i < l1_dests.Count; i++)
            {
                // affichage de la position
                GameObject l2_go =
                    Instantiate(
                        m_prefabPosition,
                        l1_dests[i].Cible,
                        c_orientationNull) as GameObject;
                // si il faut affichage de l'orientation
                if (!float.IsNaN(l1_dests[i].OrientationFinale))
                {
                    Quaternion l3_orientation = Quaternion.AngleAxis(l1_dests[i].OrientationFinale, Vector3.back);
                    GameObject l3_direction = l2_go.transform.Find("posSelection_apparence/direction_apparence").gameObject;
                    l3_direction.SetActive(true);
                    l3_direction.transform.localRotation = l3_orientation;
                }
                // on s'assure qu'il a bien tout les éléments dont il a besoin
                PosSelection l2_pos = l2_go.GetComponent<PosSelection>();
                l2_pos.Movement = GetComponent<Movement>();
                l2_pos.Destination = l1_dests[i];
                m_instances.Add(l2_go);

                // affichage du trait
                if (i != 0)
                {
                    l2_go = CreateLine(l1_dests[i].Cible, l1_dests[i - 1].Cible);
                    m_instances.Add(l2_go);
                }
            }
        }
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void EraseStaticPrefab()
    {
        // les prefab "static"
        foreach (GameObject e_go in m_instances)
        {
            Destroy(e_go);
        }
        m_instances.Clear();
    }

    private GameObject CreateLine(Vector3 p_start, Vector3 p_end)
    {
        GameObject l1_go = Instantiate(m_prefabLine, p_start, c_orientationNull) as GameObject;
        LineRenderer l1_lineRenderer = l1_go.GetComponent<LineRenderer>();
        l1_lineRenderer.SetPosition(0, p_start);
        l1_lineRenderer.SetPosition(1, p_end);

        return l1_go;
    }

    #endregion
}
