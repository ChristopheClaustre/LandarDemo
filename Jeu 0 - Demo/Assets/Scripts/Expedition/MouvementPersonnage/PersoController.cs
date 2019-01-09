/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class PersoController :
    MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    [System.Obsolete("Journey system is deprecated.")]
    public Journey Journey
    {
        get { return m_journey; }
        set
        {
            m_journey = value;
            go();
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] private Journey m_journey;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

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
    private void Update()
    {

    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    [System.Obsolete("Journey system is deprecated.")]
    private static List<Journey> createDestinations(Journey p_pivots, int p_nbPerso)
    {
        // init des destinations
        List<Journey> l0_journey = new List<Journey>();
        for (int i = 0; i < p_nbPerso; i++)
        {
            l0_journey.Add(new Journey());
        }

        // define radius
        float l0_sRadius = Setting.Instance.FormationPadding.x * Mathf.Sqrt(p_nbPerso) / 2.0f;
        float l0_gRadius = Setting.Instance.FormationPadding.y * Mathf.Sqrt(p_nbPerso) / 2.0f;

        // compute the destination(s)
        foreach (Destination l1_pivot in p_pivots.Destinations)
        {
            for (int i = 0; i < p_nbPerso; ++i)
            {
                float l2_incrX = Random.Range(-l0_sRadius, l0_sRadius);
                float l2_incrY = Random.Range(-l0_gRadius, l0_gRadius);
                Vector2 l2_v = new Vector3(l1_pivot.Cible.x + l2_incrX, l1_pivot.Cible.y + l2_incrY);
                l0_journey[i].AddDestination(new Destination(l2_v, l1_pivot.OrientationFinale));
            }
        }

        return l0_journey;
    }

    [System.Obsolete("Journey system is deprecated.")]
    private static List<Journey> createFormation(Journey p_pivots, int p_nbPerso)
    {
        // init du nombre de ligne
        int l0_nbRow = numberOfRows(p_nbPerso);

        // on calcule le nombre d'unité par ligne
        int l0_nbPerRow = Mathf.CeilToInt(p_nbPerso / l0_nbRow);

        // init des destinations
        List<Journey> l0_journey = new List<Journey>();
        for (int i = 0; i < p_nbPerso; i++)
        {
            l0_journey.Add(new Journey());
        }

        // récupérer le delta de chaque perso sur la formation
        List<Vector2> l0_deltas;
        getFormationDelta(p_nbPerso, l0_nbRow, l0_nbPerRow, out l0_deltas);


        foreach (Destination l1_pivot in p_pivots.Destinations)
        {
            int i = 0;
            foreach (Vector2 l2_delta in l0_deltas)
            {
                // compute aboslute destination
                Vector2 l2_v = l1_pivot.Cible + l2_delta;
                // rotation
                if (!float.IsNaN(l1_pivot.OrientationFinale))
                    l2_v = RotatePointAroundPivot(l2_v, l1_pivot.Cible, new Vector3(0, 0, l1_pivot.OrientationFinale));

                // store
                l0_journey[i].AddDestination(new Destination(l2_v, l1_pivot.OrientationFinale));

                i++;
            }
        }

        return l0_journey;
    }

    [System.Obsolete("Journey system is deprecated.")]
    private static int numberOfRows(int p_count)
    {
        if (p_count <= 2)
            return 1; // 1x1, 1x2
        else if (p_count <= 8)
            return 2; // 2x2, 2x2, 2x3, 2x3, 2x4, 2x4
        else
            return 3; // 3x3, 3x4, 3x4, 3x4, 3x5, 3x5, 3x5
    }

    [System.Obsolete("Journey system is deprecated.")]
    private static void getFormationDelta(int p_nbPerso, int p_nbRows, int p_nbCols, out List<Vector2> o_deltas)
    {
        // init
        o_deltas = new List<Vector2>();

        // init base y for each lines
        float l0_baseY = ((p_nbRows - 1.0f) / -2.0f);

        int l0_remainingPerso = p_nbPerso;
        for (int i = 0; i < p_nbRows; i++)
        {
            // actual number of cols for this line
            int l1_nbCols = 0;
            if (l0_remainingPerso > p_nbPerso)
                l1_nbCols = p_nbCols;
            else
                l1_nbCols = l0_remainingPerso;

            // the incrementation from the original position
            float l1_incrX = ((l1_nbCols - 1.0f) / -2.0f) * Setting.Instance.FormationPadding.x;
            float l1_incrY = l0_baseY - (i * Setting.Instance.FormationPadding.y);
            // we calculate each row of the formation
            for (int j = 0; j < l1_nbCols; j++)
            {
                // on calcul la destination
                o_deltas.Add(new Vector2(l1_incrX + (j * Setting.Instance.FormationPadding.x), l1_incrY));
            }

            // update remaining perso
            l0_remainingPerso -= p_nbPerso;
        }
    }

    // function to calculate and send the destinations to all the selected personages
    [System.Obsolete("Journey system is deprecated.")]
    private void go()
    {
        // on récupère les sélectionnés
        List<int> selec = ExpeditionManager.Instance.Selected;

        // on créé les destinations
        List<Journey> l0_journey;
        if (ExpeditionManager.Instance.FormationSelected == -1)
            l0_journey = createDestinations(m_journey, selec.Count);
        else
            l0_journey = createFormation(m_journey, selec.Count);

        // assign the destination
        for (int i = 0; i < selec.Count; i++)
        {
            PersonnageScript perso = ExpeditionManager.Persos[selec[i]].GetComponent<PersonnageScript>();
            perso.SetJourney(l0_journey[i]);
        }
    }

    // function to rotate a point around another one (the pivot)
    private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    #endregion
}
