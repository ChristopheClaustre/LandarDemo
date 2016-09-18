using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class UniteScript : MonoBehaviour {

    public interface IAbonneUnite
    {
        void newTrajet();
    }

    // les variables de model
    [SerializeField]
    protected Unite unite;

    protected abstract Trajet Trajet
    {
        get;
        set;
    }

    // Abonnement

    protected abstract void newTrajet();

    // gestion des trajets

    public void Trajet_nextDestination()
    {
        Trajet t = Trajet;
        t.nextDestination();
        Trajet = t;
        // appel à la fonction qui prévient du changement
        newTrajet();
    }
    
    public Destination Trajet_currentDestination()
    {
        return Trajet.currentDestination();
    }

    public IList<Destination> Trajet_Destinations()
    {
        return Trajet.Destinations;
    }

    public bool Trajet_hasDestinations()
    {
        return Trajet.hasDestinations();
    }

    public bool Trajet_WillLoop()
    {
        return Trajet.WillLoop;
    }

    public bool Trajet_Boucler()
    {
        return Trajet.Boucler;
    }

    public void setTrajet(Trajet t)
    {
        if (!Trajet.Equals(t))
        {
            Trajet = t;
            newTrajet();
        }
    }
}
