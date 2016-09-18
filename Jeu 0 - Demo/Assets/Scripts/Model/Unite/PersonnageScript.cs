using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonnageScript : UniteScript {

    public interface IAbonnePerso : IAbonneUnite
    {
        void newSelected(bool s);
        void newPerso(Personnage p);
    }

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
                newTrajet();
                newPerso();
            }
        }
    }

    protected override Trajet Trajet
    {
        get
        {
            return Perso.Trajet;
        }
        set
        {
            if (!Perso.Trajet.Equals(value))
            {
                Perso.Trajet = value;
                newTrajet();
                newPerso();
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
                newSelected(selected);
            }
        }
    }

    void Start()
    {
        unite = new Personnage();
    }

    // Abonement

    private List<IAbonnePerso> abonnes;

    void Awake()
    {
        abonnes = new List<IAbonnePerso>();
    }

    public void abonnement(IAbonnePerso abonne)
    {
        abonnes.Add(abonne);
    }

    protected override void newTrajet()
    {
        // changement sur le perso en général donc
        newPerso();
        // je prévient les abonnés
        foreach (IAbonnePerso a in abonnes)
        {
            a.newTrajet();
        }
    }

    protected void newPerso()
    {
        foreach (IAbonnePerso a in abonnes)
        {
            a.newPerso(unite);
        }
    }

    private void newSelected(bool s)
    {
        // changement sur le perso en général donc
        newPerso();
        // je prévient les abonnés
        foreach(IAbonnePerso a in abonnes)
        {
            a.newSelected(s);
        }
    }
}
