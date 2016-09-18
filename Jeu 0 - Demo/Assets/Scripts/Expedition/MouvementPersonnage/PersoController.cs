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
    private Trajet trajet;

    public Trajet Trajet
    {
        get
        {
            return trajet;
        }
        set
        {
            trajet = value;
            go();
        }
    }
    
    void Start ()
    {

    }

    private static List<Trajet> createFormation(Trajet pivots, int nbPerso)
    {
        // on calcule le nombre d'unité par ligne
        int nbPerRow = 2;
        if (nbPerso > 6)
            nbPerRow = 3;

        // init du nombre de ligne
        int nbRow = (nbPerso / nbPerRow) + 1;
        // init des destinations
        List<Trajet> trajets = new List<Trajet>();
        for (int i = 0; i < nbPerso; i++)
        {
            trajets.Add(new Trajet(Setting.Inst.MaxDestinationsPerTraject));
        }

        // calcul the destination(s)
        float baseZ = ((nbRow - 1.0f) / 2.0f) * Setting.Inst.FormationIncr.y;
        foreach (Destination pivot in pivots.Destinations) {
            for (int i = 0; i < nbRow; i++)
            {
                int _i = i * nbPerRow;
                // the incrementation from the original position
                float incrX = ((Mathf.Min(nbPerRow, nbPerso - _i) - 1.0f) / -2.0f) * Setting.Inst.FormationIncr.x;
                float incrZ = baseZ - (i * Setting.Inst.FormationIncr.y);
                // we calculate each row of the formation
                for (int j = 0; (_i + j) < nbPerso && j < nbPerRow; j++)
                {
                    // on calcul la destination
                    Vector3 _v = new Vector3(pivot.Cible.x + incrX + (j * Setting.Inst.FormationIncr.x), pivot.Cible.y, pivot.Cible.z + incrZ);
                    // on la rotate si il faut
                    if (!float.IsNaN(pivot.OrientationFinale))
                        _v = RotatePointAroundPivot(_v, pivot.Cible, new Vector3(0, pivot.OrientationFinale));
                    // on l'ajoute au trajet !
                    trajets[_i + j].addDestination(new Destination(_v, pivot.OrientationFinale));
                }
            }
        }

        return trajets;
    }

    // function to calculate and send the destinations to all the selected personages
    private void go()
    {
        // on récupère les sélectionnés
        List<int> selec = ExpeditionManager.Inst.Selected;

        // on créé les destinations
        List<Trajet> trajets = createFormation(trajet, selec.Count);
        
        // assign the destination
        for (int i = 0; i < selec.Count; i++)
        {
            PersonnageScript perso = ExpeditionManager.Persos[selec[i]].GetComponent<PersonnageScript>();
            perso.setTrajet(trajets[i]);
        }
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
