using UnityEngine;
using System.Collections;

[System.Serializable]
public class Unite {

    public enum EnumIAState {
        ETDRF,
        BALLADE,
        CHASSE,
        ATTAQUE,
        POURCHASSE,
        FUITE
    }

    private int vie = 0;
    private int vieTemporaire = 0;
    private Unite cible = null;
    private Vector3 position = Vector3.one;
    private Description desc = null;
    private EnumIAState etatIA = EnumIAState.ETDRF;
    private Patrouille patrouille = new Patrouille();
    private CaracUnite carac = null;

    public int Vie
    {
        get
        {
            return vie;
        }
    }
    public int VieTemporaire
    {
        get
        {
            return vieTemporaire;
        }
    }
    public Unite Cible
    {
        get
        {
            return cible;
        }
    }
    public Vector3 Position
    {
        get
        {
            return position;
        }
    }
    public Description Desc
    {
        get
        {
            return desc;
        }
    }
    public EnumIAState EtatIA
    {
        get
        {
            return etatIA;
        }
    }
    public Patrouille Patrouille
    {
        get
        {
            return patrouille;
        }
        set
        {
            patrouille = value;
        }
    }
    public CaracUnite Carac
    {
        get
        {
            return carac;
        }
    }

    public Unite ()
    {
        
    }

}
