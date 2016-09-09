using UnityEngine;
using System.Collections;

public class Navigation : CI_caller {

    [HideInInspector]
    public Unite unite;

    // information about rotation
    public float rotatingDistance = 1;
    
    [Header("Need to click the button to be updated")]
    [SerializeField]
    private bool selected = false;

    // Some code for the CustomInspector
#if (UNITY_EDITOR)
    public override void updateVarFromCI()
    {
        Selected = selected;
    }
#endif

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            this.transform.Find("selector").gameObject.SetActive(value);
        }
    }

    // the destination for next call of the pathfinder
    private Destination dest;

    private NavMeshAgent agent;
    private bool moving = false;
    private bool rotating = false;

    // Use this for initialization
    void Start () {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (!agent.pathPending)
        {
            if (moving)
            {
                // fin du déplacement
                if (agent.remainingDistance <= float.Epsilon)
                {
                    moving = false;

                    // si il n'y as pas de rotation c'est fini -> on relance l'algo sur la prochaine destination
                    if (float.IsNaN(dest.OrientationFinale))
                    {
                        // Reinit
                        unite.Trajet.nextDestination();

                        // on relance l'algo de déplacement pour la prochaine destination
                        go();
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
            if (rotating)
            {
                // fin de rotation
                if (!moving && Mathf.Abs(transform.eulerAngles.y - MyMathf.posModulo(dest.OrientationFinale, 360)) <= 0.5f)
                {
                    rotating = false;
                    // on reactive la rotation pour le syst de nav
                    agent.updateRotation = true;

                    // on relance l'algo de déplacement pour la prochaine destination
                    go();
                }
                // on rotate
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.AngleAxis(dest.OrientationFinale, Vector3.up), Time.deltaTime * 2);
                }
            }
        }

        if (!moving && !rotating && unite.Trajet.hasDestination())
        {
            go();
        }
    }

    // some public function

    public void nouveautrajet(Trajet trajet)
    {
        unite.Trajet = trajet;
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
            // on va se mettre à bouger
            moving = true;
            // on applique la destination
            agent.SetDestination(dest.Cible);
        }
    }

}
