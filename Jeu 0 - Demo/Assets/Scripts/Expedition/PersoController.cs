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

    private void go()
    {
        // on récupère les sélectionnés
        List<int> selec = ExpeditionManager.Inst.selected;

        int nbPerRow = 3;

        // calcul the destination(s)
        int nbRow = (selec.Count / nbPerRow) + 1;
        Vector3[] destinations = new Vector3[selec.Count];
        for (int i = 0; i < selec.Count; i+=nbPerRow)
        {
            // the incrementation from the original position
            float incrX = (Mathf.Min(nbPerRow, selec.Count - i) - 1.0f) / -2.0f;
            float incrZ = (nbRow - 1.0f) / -2.0f;
            // we calculate each row of the formation
            for (int j = 0; (i + j) < selec.Count && j < nbPerRow; j++)
            {
                destinations[i+j] =
                    new Vector3(v3Destination.x + incrX + j, v3Destination.y, v3Destination.z + incrZ);
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
            {
                nav.rotationRequired = rotationRequired;
                nav.rotationValue = rotationValue;
            }
            nav.V3Destination = destinations[i];
        }
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
    
}
