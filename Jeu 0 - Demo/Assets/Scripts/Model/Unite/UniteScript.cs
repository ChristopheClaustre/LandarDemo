using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class UniteScript :
    MonoBehaviour
{
    // les variables de model
    [SerializeField]
    protected Unite unite;

    public List<Light> lampes;
    public LightStateGenerator Lsg;

    void Update()
    {
    }
    
    protected abstract Journey Journey
    {
        get;
        set;
    }

    // gestion des trajets

    public void Journey_nextDestination()
    {
        Journey t = Journey;
        t.nextDestination();
        Journey = t;

        // appel à la fonction qui prévient du changement
        gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
    }
    
    public Destination Journey_currentDestination()
    {
        return Journey.currentDestination();
    }

    public IList<Destination> Journey_Destinations()
    {
        return Journey.Destinations;
    }

    public bool Journey_hasDestinations()
    {
        return Journey.hasDestinations();
    }

    public bool Journey_WillLoop()
    {
        return Journey.WillLoop;
    }

    public bool Journey_Boucler()
    {
        return Journey.Loop;
    }

    public void setJourney(Journey t)
    {
        if (!Journey.Equals(t))
        {
            Journey = t;

            gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
