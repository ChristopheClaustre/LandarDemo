using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUINavigation : MonoBehaviour, PersonnageScript.IAbonnePerso {

    [SerializeField]
    private GameObject prefab_position;
    [SerializeField]
    private GameObject prefab_line;

    private List<GameObject> instances;
    
    private PersonnageScript ps;
    private GameObject go_selec;
    private GameObject startingLine = null;
    private GameObject endingLine = null;
    private bool show = false;

    // pour faciliter la lecture
    private Quaternion orientationNull = Quaternion.AngleAxis(0, Vector3.zero);

    // Use this for initialization
    void Start () {
        ps = GetComponent<PersonnageScript>();
        ps.abonnement(this);
        go_selec = this.transform.Find("selector").gameObject;
        instances = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        // gestion input
        if (Input.GetKeyDown(KeyCode.T))
        {
            show = true;
            newTrajet();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            show = false;
            eraseStaticPrefab();
            eraseDynamicPrefab();
        }
        // affichage de la première ligne
        if (show && ps.Selected && ps.Trajet_hasDestinations())
        {
            eraseDynamicPrefab();
            startingLine = createLine(ps.Trajet_currentDestination().Cible, transform.position);
            if (ps.Trajet_Boucler())
            {
                IList <Destination> dests = ps.Trajet_Destinations();
                endingLine = createLine(dests[dests.Count - 1].Cible, transform.position);
            }
        }
    }

    // abonnement

    public void newTrajet()
    {
        if (ps.Selected & show)
        {
            // d'abord on supprime tout
            eraseStaticPrefab();
            eraseDynamicPrefab();

            if (ps.Trajet_hasDestinations())
            {
                // on récupére les destinations
                IList<Destination> dests = ps.Trajet_Destinations();

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
                        GameObject direction = go.transform.Find("position_apparence/direction_apparence").gameObject;
                        direction.SetActive(true);
                        direction.transform.localRotation = orientation;
                    }
                    instances.Add(go);

                    // affichage du trait
                    if (i != 0)
                    {
                        go = createLine(dests[i].Cible, dests[i - 1].Cible);
                        instances.Add(go);
                    }
                }

                // on créé la première ligne
                startingLine = createLine(ps.Trajet_currentDestination().Cible, transform.position);
                if (ps.Trajet_Boucler())
                {
                    endingLine = createLine(dests[dests.Count - 1].Cible, transform.position);
                }
            }
        }
    }

    public void newSelected(bool s)
    {
        // on affiche l'état selectionné
        go_selec.SetActive(s);
        // si il est sélectionné
        if (s)
        {
            newTrajet();
        }
        else
        {
            eraseStaticPrefab();
            eraseDynamicPrefab();
        }
    }
    
    public void newPerso(Personnage p)
    {
        // Rien à faire
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

    private void eraseDynamicPrefab()
    {
        Destroy(startingLine);
        startingLine = null;
        Destroy(endingLine);
        endingLine = null;
    }
}
