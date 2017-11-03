using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Unite {

    public enum EnumIAState {
        ETDRF,
        BALLADE,
        CHASSE,
        ATTAQUE,
        POURCHASSE,
        FUITE
    }

    private int m_vie = 0;
    private int m_vieTemporaire = 0;
    private Unite m_cible = null;
    private Vector3 m_position = Vector3.one;
    private Description m_description = null;
    private EnumIAState m_etatIA = EnumIAState.ETDRF;
    [SerializeField]
    private Journey m_journey = new Journey();
    private CaracteristicUnite m_caracteristic = null;
    private float m_luminosite = 0.0f;

    public int Vie
    {
        get
        {
            return m_vie;
        }
    }
    public int VieTemporaire
    {
        get
        {
            return m_vieTemporaire;
        }
    }
    public Unite Cible
    {
        get
        {
            return m_cible;
        }
    }
    public Vector3 Position
    {
        get
        {
            return m_position;
        }
    }
    public Description Desc
    {
        get
        {
            return m_description;
        }
    }
    public EnumIAState EtatIA
    {
        get
        {
            return m_etatIA;
        }
    }
    public Journey Journey
    {
        get
        {
            return m_journey;
        }
        set
        {
            m_journey = value;
        }
    }
    public CaracteristicUnite Carac
    {
        get
        {
            return m_caracteristic;
        }
    }

    public float Luminosite
    {
        get
        {
            return m_luminosite;
        }
        set
        {
            m_luminosite = value;
        }
    }

    public Unite ()
    {
        
    }

}
