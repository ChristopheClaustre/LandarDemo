using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersoController : CI_caller {

    // Some code for the CustomInspector
    // use the getter after an update of v2Destination (via button)
#if (UNITY_EDITOR)
    public override void updateVarFromCI()
    {
        go();
    }
#endif

    [SerializeField]
    private Destination destination;

    void Start ()
    {

    }

    // destination + rotation
    public void go(Destination dest)
    {
        destination = dest;
        go();
    }

    // function to calculate and send the destinations to all the selected personages
    private void go()
    {
        // on récupère les sélectionnés
        List<int> selec = ExpeditionManager.Inst.selected;

        // on créé les destinations
        List<Destination> dests = createFormation(destination, selec.Count);
        
        // assign the destination
        for (int i = 0; i < selec.Count; i++)
        {
            Navigation nav = ExpeditionManager.Persos[selec[i]].GetComponent<Navigation>();
            Trajet t = new Trajet();
            t.addDestination(dests[i]);
            nav.nouveautrajet(t);
        }
    }

    public static List<Destination> createFormation(Destination pivot, int nbPerso)
    {
        // on calcule le nombre d'unité par ligne
        int nbPerRow = 2;
        if (nbPerso > 6)
            nbPerRow = 3;

        // init du nombre de ligne
        int nbRow = (nbPerso / nbPerRow) + 1;
        // init des destinations
        List<Destination> destinations = new List<Destination>();

        // calcul the destination(s)
        float baseZ = ((nbRow - 1.0f) / 2.0f) * Setting.Inst.FormationIncr.y;
        for (int i = 0; i < nbRow; i++)
        {
            int _i = i * nbPerRow;
            // the incrementation from the original position
            float incrX = ((Mathf.Min(nbPerRow, nbPerso - _i) - 1.0f) / -2.0f) * Setting.Inst.FormationIncr.x;
            float incrZ = baseZ - (i * Setting.Inst.FormationIncr.y);
            // we calculate each row of the formation
            for (int j = 0; (_i + j) < nbPerso && j < nbPerRow; j++)
            {
                destinations.Add(new Destination(
                    new Vector3(pivot.Cible.x + incrX + (j * Setting.Inst.FormationIncr.x), pivot.Cible.y, pivot.Cible.z + incrZ),
                    pivot.OrientationFinale)
                    );
            }
        }

        // rotate them if needed
        if (!float.IsNaN(pivot.OrientationFinale))
        {
            for (int i = 0; i < destinations.Count; i++)
            {
                destinations[i].Cible =
                    RotatePointAroundPivot(destinations[i].Cible, pivot.Cible, new Vector3(0, pivot.OrientationFinale));
            }
        }

        return destinations;
    }

    // function to rotate a point around another one (the pivot)
    private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
    
}
