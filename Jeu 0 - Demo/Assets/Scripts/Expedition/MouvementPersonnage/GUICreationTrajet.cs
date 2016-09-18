using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUICreationTrajet : MonoBehaviour, Movement.IAbonneMovement {

    [SerializeField]
    private GameObject prefab_position;
    [SerializeField]
    private GameObject prefab_line;

    private List<GameObject> instances;

    private Movement mov;
    private Trajet trajet; 
    
    // pour faciliter la lecture
    private Quaternion orientationNull = Quaternion.AngleAxis(0, Vector3.zero);

    // Use this for initialization
    void Start()
    {
        mov = GetComponent<Movement>();
        mov.abonnement(this);
        instances = new List<GameObject>();
        trajet = new Trajet(Setting.Inst.MaxDestinationsPerTraject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // abonnement

    public void newTrajet(Trajet t)
    {
        trajet = t;

        // d'abord on supprime tout
        eraseStaticPrefab();

        if (trajet.hasDestinations())
        {
            // on récupére les destinations
            IList<Destination> dests = t.Destinations;

            // on créé les marqueurs
            for (int i = 0; i < dests.Count; i++)
            {
                // affichage de la position
                GameObject go =
                    Instantiate(
                        prefab_position,
                        dests[i].Cible,
                        orientationNull) as GameObject;
                // si il faut affichage de l'orientation
                if (!float.IsNaN(dests[i].OrientationFinale))
                {
                    Quaternion orientation = Quaternion.AngleAxis(dests[i].OrientationFinale, Vector3.back);
                    GameObject direction = go.transform.Find("posSelection_apparence/direction_apparence").gameObject;
                    direction.SetActive(true);
                    direction.transform.localRotation = orientation;
                }
                // on s'assure qu'il a bien tout les éléments dont il a besoin
                PosSelection pos = go.GetComponent<PosSelection>();
                pos.mov = this.GetComponent<Movement>();
                pos.dest = dests[i];
                instances.Add(go);

                // affichage du trait
                if (i != 0)
                {
                    go = createLine(dests[i].Cible, dests[i - 1].Cible);
                    instances.Add(go);
                }
            }
        }
    }

    // some private function

    private void eraseStaticPrefab()
    {
        // les prefab "static"
        foreach (GameObject i in instances)
        {
            Destroy(i);
        }
        instances.Clear();
    }

    private GameObject createLine(Vector3 start, Vector3 end)
    {
        GameObject go = Instantiate(prefab_line, start, orientationNull) as GameObject;
        LineRenderer lr = go.GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        return go;
    }
}
