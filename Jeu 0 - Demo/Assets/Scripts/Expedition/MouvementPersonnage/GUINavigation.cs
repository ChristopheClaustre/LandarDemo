using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUINavigation : MonoBehaviour, Navigation.IAbonneNavigation {

    [SerializeField]
    private GameObject prefab_position;
    [SerializeField]
    private GameObject prefab_line;

    private List<GameObject> instances;
    
    private Navigation nav;
    private GameObject go_selec;
    private GameObject startingLine = null;

    // pour faciliter la lecture
    private Quaternion orientationNull = Quaternion.AngleAxis(0, Vector3.zero);

    // Use this for initialization
    void Start () {
        nav = GetComponent<Navigation>();
        nav.abonnement(this);
        go_selec = this.transform.Find("selector").gameObject;
        instances = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if (nav.Selected && nav.unite.Trajet.hasDestination())
        {
            eraseStartingLine();
            startingLine = createLine(nav.unite.Trajet.currentDestination().Cible, transform.position);
        }
    }

    public void newTrajet()
    {
        // d'abord on supprime tout
        eraseStaticPrefab();
        eraseStartingLine();

        if (nav.unite.Trajet.hasDestination())
        {
            // on récupére les destinations
            List<Destination> dests = nav.unite.Trajet.Destinations;

            // on créé les marqueurs
            for (int i = 0; i < dests.Count; i++)
            {
                // affichage de la position
                GameObject go =
                    Instantiate(
                        prefab_position,
                        dests[i].Cible,
                        orientationNull) as GameObject;
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
            startingLine = createLine(nav.unite.Trajet.currentDestination().Cible, transform.position);
        }
    }

    public void newSelected()
    {
        // on affiche l'état selectionné
        go_selec.SetActive(nav.Selected);
        // si il est sélectionné
        if (nav.Selected)
        {
            newTrajet();
        }
        else
        {
            eraseStaticPrefab();
            eraseStartingLine();
        }
    }

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

    private void eraseStartingLine()
    {
        Destroy(startingLine);
        startingLine = null;
    }
}
