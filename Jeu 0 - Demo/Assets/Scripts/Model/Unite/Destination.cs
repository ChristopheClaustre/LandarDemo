﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Destination {

    [SerializeField]
    private Vector2 cible;
    [SerializeField]
    private float orientationFinale;

    public Vector2 Cible
    {
        get
        {
            return cible;
        }
        set
        {
            cible = value;
        }
    }
    public float OrientationFinale
    {
        get
        {
            return orientationFinale;
        }
        set
        {
            orientationFinale = MyMathf.posModulo(value, 360);
        }
    }

    /* constructeur */

    // const avec orientation demande
    public Destination(Vector2 _c, float _o)
    {
        orientationFinale = (float.IsNaN(_o))? _o : MyMathf.posModulo(_o, 360);
        cible = _c;
    }

    // const sans orientation demande
    public Destination(Vector2 _c)
    {
        orientationFinale = float.NaN;
        cible = _c;
    }
}
