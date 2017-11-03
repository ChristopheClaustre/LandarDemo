using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonnageScript :
    UniteScript
{
    new Personnage unite;

    public virtual Personnage Perso
    {
        get
        {
            return unite;
        }
        set
        {
            if (!unite.Equals(value))
            {
                unite = value;
                gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
                gameObject.SendMessage("NewPerso", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    protected override Journey Journey
    {
        get
        {
            return Perso.Journey;
        }
        set
        {
            if (!Perso.Journey.Equals(value))
            {
                Perso.Journey = value;
                gameObject.SendMessage("NewJourney", null, SendMessageOptions.DontRequireReceiver);
                gameObject.SendMessage("NewPerso", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    [SerializeField]
    private bool selected = false;

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            if (selected != value)
            {
                selected = value;
                gameObject.SendMessage("NewSelected", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void Start()
    {
        unite = new Personnage();
    }
}
