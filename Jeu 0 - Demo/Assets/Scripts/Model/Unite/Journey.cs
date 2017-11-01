using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[System.Serializable]
public class Journey {

    [SerializeField]
    private List<Destination> m_destinations;
    [SerializeField]
    private bool m_loop = false;
    private bool m_willLoop = false;

    public IList<Destination> Destinations
    {
        get
        {
            return new ReadOnlyCollection<Destination>(m_destinations);
        }
    }
    public bool Loop
    {
        get
        {
            return m_loop;
        }

        set
        {
            m_loop = value;
        }
    }
    public bool WillLoop
    {
        get
        {
            return m_willLoop;
        }
    }


    // const prenant aucun paramètre (pas de destinations donc !)
    public Journey()
    {
        m_destinations = new List<Destination>();
    }

    // passe à la destination suivante
    public void nextDestination()
    {
        if (hasDestinations()) {
            if (m_willLoop && m_destinations[0].Cible.Equals(m_destinations[m_destinations.Count-1].Cible) && m_destinations.Count > 2)
            {
                m_loop = true;
                m_destinations.RemoveAt(m_destinations.Count-1);
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
    public Destination currentDestination()
    {
        return (hasDestinations())? m_destinations[0] : null;
    }

    // est ce qu'il y a une ou plusieurs destinations dans la liste
    public bool hasDestinations()
    {
        return m_destinations.Count > 0;
    }

    // ajout d'une destination
    public bool addDestination(Destination destination)
    {
        if (m_destinations.Count <= Setting.Instance.MaxDestinationsPerTraject && !m_willLoop)
        {
            if (willThisJourneyLoopWithThisDestination(this, destination))
            {
                m_willLoop = true;
            }
            m_destinations.Add(destination);
            return true;
        }
        return false;
    }

    public static bool willThisJourneyLoopWithThisDestination(Journey p_journey, Destination m_dest)
    {
        if (p_journey.Destinations.Count > 2)
        {
            int i = 0;
            while(i < p_journey.Destinations.Count - 1)
            {
                if (p_journey.Destinations[i].Cible == m_dest.Cible)
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
}
