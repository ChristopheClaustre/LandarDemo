/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using System.Collections.Generic;
using System.Collections.ObjectModel;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
[System.Obsolete("Journey system is deprecated.")]
[System.Serializable]
public class Journey
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

    public IList<Destination> Destinations
    {
        get { return new ReadOnlyCollection<Destination>(m_destinations); }
    }
    public bool Loop
    {
        get { return m_loop; }
        set { m_loop = value; }
    }
    public bool WillLoop
    {
        get { return m_willLoop; }
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

    private List<Destination> m_destinations;
    private bool m_loop = false;
    private bool m_willLoop = false;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    // const prenant aucun paramètre (pas de destinations donc !)
    public Journey()
    {
        m_destinations = new List<Destination>();
    }

    // passe à la destination suivante
    public void NextDestination()
    {
        if (HasDestinations())
        {
            if (m_willLoop && m_destinations[0].Cible.Equals(m_destinations[m_destinations.Count - 1].Cible) && m_destinations.Count > 2)
            {
                m_loop = true;
                m_destinations.RemoveAt(m_destinations.Count - 1);
            }
            if (m_loop)
            {
                Destination _d = m_destinations[0];
                m_destinations.Add(_d);
            }
            m_destinations.RemoveAt(0);
        }
    }

    // retourne la destination courante
    public Destination CurrentDestination()
    {
        return (HasDestinations()) ? m_destinations[0] : null;
    }

    // est ce qu'il y a une ou plusieurs destinations dans la liste
    public bool HasDestinations()
    {
        return m_destinations.Count > 0;
    }

    // ajout d'une destination
    public bool AddDestination(Destination p_destination)
    {
        if (m_destinations.Count <= Setting.Instance.MaxDestinationsPerTraject && !m_willLoop)
        {
            if (WillThisJourneyLoopWithThisDestination(this, p_destination))
            {
                m_willLoop = true;
            }
            m_destinations.Add(p_destination);
            return true;
        }
        return false;
    }
    
    public static bool WillThisJourneyLoopWithThisDestination(Journey p_journey, Destination p_dest)
    {
        if (p_journey.Destinations.Count > 2)
        {
            int i = 0;
            while (i < p_journey.Destinations.Count - 1)
            {
                if (p_journey.Destinations[i].Cible == p_dest.Cible)
                    return true;

                ++i;
            }
            // pas de raison de boucler
            return false;
        }
        else
        {
            return false;
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
