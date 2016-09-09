using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersoController : CI_caller {

    // Some code for the CustomInspector
    // use the getter after an update of v2Destination (via button)
#if (UNITY_EDITOR)
    public Vector2 v2Destination;

    public override void updateVarFromCI()
    {
        v3Destination = new Vector3(v2Destination.x, 0, v2Destination.y);
        go();
    }
#endif

    public bool rotationRequired = false;
    public float rotationValue = 0;

    private Vector3 v3Destination;
    public Vector3 V3Destination
    {
        get
        {
            return v3Destination;
        }
        set
        {
            v3Destination = value;
            go();
        }
    }

    private Setting setting;

    void Start ()
    {
        setting = Camera.main.GetComponent<Setting>();
    }

    // destination + rotation
    public void go(Vector3 dest, float rValue)
    {
        v3Destination = dest;
        rotationRequired = true;
        rotationValue = rValue;
        go();
    }

    // just the destination
    public void go(Vector3 dest)
    {
        v3Destination = dest;
        rotationRequired = false;
        go();
    }

    // function to calculate and send the destinations to all the selected personages
    private void go()
    {
        // on récupère les sélectionnés
        List<int> selec = ExpeditionManager.Inst.selected;

        // on calcule le nombre d'unité par ligne
        int nbPerRow = 2;
        if (selec.Count > 6)
            nbPerRow = 3;

        // init du nombre de ligne
        int nbRow = (selec.Count / nbPerRow) + 1;
        // init des destinations
        Vector3[] destinations = new Vector3[selec.Count];

        // calcul the destination(s)
        float baseZ = ((nbRow - 1.0f) / 2.0f) * setting.FormationIncr.y;
        for (int i = 0; i < nbRow; i++)
        {
            int _i = i * nbPerRow;
            // the incrementation from the original position
            float incrX = ((Mathf.Min(nbPerRow, selec.Count - _i) - 1.0f) / -2.0f) * setting.FormationIncr.x;
            float incrZ = baseZ - (i * setting.FormationIncr.y);
            // we calculate each row of the formation
            for (int j = 0; (_i + j) < selec.Count && j < nbPerRow; j++)
            {
                destinations[_i+j] =
                    new Vector3(v3Destination.x + incrX + (j * setting.FormationIncr.x), v3Destination.y, v3Destination.z + incrZ);
            }
        }

        // rotate them if needed
        if (rotationRequired)
        {
            for (int i = 0; i < destinations.Length; i++)
            {
                destinations[i] =
                    RotatePointAroundPivot(destinations[i], v3Destination, new Vector3(0, rotationValue));
            }
        }
        
        // assign the destination
        for (int i = 0; i < selec.Count; i++)
        {
            Navigation nav = ExpeditionManager.Persos[selec[i]].GetComponent<Navigation>();
            if (rotationRequired)
                nav.newDestination(new Destination(destinations[i], rotationValue));
            else
                nav.newDestination(new Destination(destinations[i]));
        }
    }

    // function to rotate a point around another one (the pivot)
    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
    
}
