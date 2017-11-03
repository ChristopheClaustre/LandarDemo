using UnityEngine;
using System.Collections;

[System.Serializable]
public class CaracteristicUnite {

    public enum EnumMalus
    {
        EMPOISONNE,
        ENDORMI,
        SONNE,
        AVEUGLE,
        PERDU
    }

    [SerializeField]
    private int vieMax;
    [SerializeField]
    private int agilite;
    [SerializeField]
    private int force;
    [SerializeField]
    private int vision;
    [SerializeField]
    private int precision;
    private EnumMalus malus;

    public int VieMax
    {
        get
        {
            return vieMax;
        }
    }
    public int Agilite
    {
        get
        {
            return agilite;
        }
    }
    public int Force
    {
        get
        {
            return force;
        }
    }
    public int Vision
    {
        get
        {
            return vision;
        }
    }
    public int Precision
    {
        get
        {
            return precision;
        }
    }
    public EnumMalus Malus
    {
        get
        {
            return malus;
        }
    }

    public CaracteristicUnite(int vieMax, int agilite, int force, int vision, int precision, EnumMalus malus)
    {
        this.vieMax = vieMax;
        this.agilite = agilite;
        this.force = force;
        this.vision = vision;
        this.precision = precision;
        this.malus = malus;
    }
}
