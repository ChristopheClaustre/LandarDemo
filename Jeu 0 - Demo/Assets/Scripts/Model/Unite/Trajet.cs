using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Trajet {

    [SerializeField]
    private List<Destination> destinations;
    [SerializeField]
    private bool boucler;

    public List<Destination> Destinations
    {
        get
        {
            return destinations;
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


    // const prenant aucun paramètre (pas de destinations donc !)
    public Trajet()
    {
        destinations = new List<Destination>();
        boucler = false;
    }

    // passe à la destination suivante
    public void nextDestination()
    {
        if (hasDestination()) {
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
        return (hasDestination())? destinations[0] : null;
    }

    // est ce qu'il y a une ou plusieurs destinations dans la liste
    public bool hasDestination()
    {
        return destinations.Count > 0;
    }

    // ajout d'une destination
    public void addDestination(Destination destination)
    {
        destinations.Add(destination);
    }

}
