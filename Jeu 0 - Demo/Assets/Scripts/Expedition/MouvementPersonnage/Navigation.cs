using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Navigation : MonoBehaviour {

    public interface IAbonneNavigation
    {
        void newTrajet();
        void newSelected();
    }

    // les variables de model
    public Unite unite;

    // information about rotation
    [SerializeField]
    private float rotatingDistance = 1;
    
    [Header("Need to click the button to be updated")]
    [SerializeField]
    private bool selected = false;

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            newSelected();
        }
    }

    // the destination for next call of the pathfinder
    private Destination dest;

    private NavMeshAgent agent;

    // booléen de traitement
    [Header("Just watch, don't modify")]
    [SerializeField]
    private bool moving = false;
    [SerializeField]
    private bool rotating = false;
    [SerializeField]
    private bool rectifying = false;

    void Awake()
    {
        abonnes = new List<IAbonneNavigation>();
    }

    // Use this for initialization
    void Start () {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        // si on a lancé l'ordre de bouger et qu'on a finit de calculer le path
        if (moving && !agent.pathPending)
        {
            // fin du déplacement
            if (agent.remainingDistance - agent.stoppingDistance <= float.Epsilon)
            {
                moving = false;

                // si il n'y a pas de rotation c'est fini -> on relance l'algo sur la prochaine destination
                if (float.IsNaN(dest.OrientationFinale))
                {
                    // Reinit
                    unite.Trajet.nextDestination();
                    newTrajet();
                }
            }

            // début de rotation
            if (!float.IsNaN(dest.OrientationFinale) && !rotating && agent.remainingDistance <= rotatingDistance)
            {
                rotating = true;
                // on s'assure que le syst de nav ne va pas pourir ma rotation à venir
                agent.updateRotation = false;
            }
        }
        // si on a recu l'ordre de roter
        if (rotating || rectifying)
        {
            // fin de rotation
            if (!moving && Mathf.Abs(transform.eulerAngles.y - dest.OrientationFinale) <= 1f && !rectifying)
            {
                rotating = false;
                // on reactive la rotation pour le syst de nav
                agent.updateRotation = true;

                // Reinit
                unite.Trajet.nextDestination();
                newTrajet();

                // si c'était la dernière destination
                if (!unite.Trajet.hasDestination())
                    rectifying = true; // on autorise la rectification
            }
            // on rotate
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.AngleAxis(dest.OrientationFinale, Vector3.up), Time.deltaTime * 2);
            }
        }

        // si on ne fais rien mais que l'on a un trajet à faire
        if (!moving && !rotating && unite.Trajet.hasDestination())
        {
            go();
        }
    }

    // some public function

    public void nouveauTrajet(Trajet trajet)
    {
        unite.Trajet = trajet;
        newTrajet();
        go();
    }

    // some private function ;)

    private void go()
    {
        if (unite.Trajet.hasDestination())
        {
            dest = unite.Trajet.currentDestination();
            // on s'assure que le joueur va se déplacer en regardant dans la direction de son déplacement
            agent.updateRotation = true;
            // Reinit
            rotating = false;
            rectifying = false;
            // on va se mettre à bouger
            moving = true;
            // on applique la destination
            agent.SetDestination(dest.Cible);
        }
    }

    // gestion des abonnés

    // les abonnés
    private List<IAbonneNavigation> abonnes;

    private void newTrajet()
    {
        foreach (IAbonneNavigation a in abonnes)
        {
            a.newTrajet();
        }
    }

    private void newSelected()
    {
        foreach (IAbonneNavigation a in abonnes)
        {
            a.newSelected();
        }
    }

    public void abonnement(IAbonneNavigation cible)
    {
        abonnes.Add(cible);
    }
}
