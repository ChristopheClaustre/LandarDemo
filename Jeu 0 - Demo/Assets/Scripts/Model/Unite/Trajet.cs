using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[System.Serializable]
public class Trajet {

    [SerializeField]
    private List<Destination> destinations;
    [SerializeField]
    private bool boucler = false;
    private bool willLoop = false;
    private int maxDest;

    public IList<Destination> Destinations
    {
        get
        {
            return new ReadOnlyCollection<Destination>(destinations);
        }
    }
    public bool Boucler
    {
        get
        {
            return boucler;
        }

        set
        {
            boucler = value;
        }
    }
    public bool WillLoop
    {
        get
        {
            return willLoop;
        }
    }


    // const prenant aucun paramètre (pas de destinations donc !)
    public Trajet(int maxDest)
    {
        destinations = new List<Destination>();
        this.maxDest = maxDest;
    }

    // passe à la destination suivante
    public void nextDestination()
    {
        if (hasDestinations()) {
            if (willLoop && destinations[0].Cible.Equals(destinations[destinations.Count-1].Cible) && destinations.Count > 2)
            {
                boucler = true;
                destinations.RemoveAt(destinations.Count-1);
            }
            if (boucler)
            {
                Destination _d = destinations[0];
                destinations.Add(_d);
            }
            destinations.RemoveAt(0);
        }
    }

    // retourne la destination courante
    public Destination currentDestination()
    {
        return (hasDestinations())? destinations[0] : null;
    }

    // est ce qu'il y a une ou plusieurs destinations dans la liste
    public bool hasDestinations()
    {
        return destinations.Count > 0;
    }

    // ajout d'une destination
    public void addDestination(Destination destination)
    {
        if (destinations.Count <= maxDest && !willLoop)
        {
            if (WillLoopWith(this, destination))
            {
                willLoop = true;
            }
            destinations.Add(destination);
        }
    }

    public static bool WillLoopWith(Trajet t, Destination d)
    {
        if (t.Destinations.Count > 2) {
            for(int i = 0; i < t.Destinations.Count - 1; i++)
            {
                if (t.Destinations[i].Cible == d.Cible)
                    return true;
            }
            // pas de raison de boucler
            return false;
        }
        else
            return false;
    }
}
