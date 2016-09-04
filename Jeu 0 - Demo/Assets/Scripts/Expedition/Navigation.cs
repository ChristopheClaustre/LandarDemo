using UnityEngine;
using System.Collections;

public class Navigation : CI_caller {

    // information about rotation
    public float rotatingDistance = 1;
    public bool rotationRequired = false;
    public float rotationValue = 0;
    
    [Header("Need to click the button to be updated")]

    // Some code for the CustomInspector
    // use the getter after an update of v2Destination (via button)
#if (UNITY_EDITOR)
    // destination for next call to pathfinder from Inspector
    [SerializeField]
    private Vector2 v2Destination;

    public override void updateVarFromCI()
    {
        v3Destination = new Vector3(v2Destination.x, 0, v2Destination.y);
        go();
        Selected = selected;
    }
#endif

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
            this.transform.Find("selector").gameObject.SetActive(value);
        }
    }

    // the destination for next call of the pathfinder
    private Vector3 v3Destination;

    public Vector3 V3Destination
    {
        get
        {
            return v3Destination;
        }
        set
        {
            v2Destination = new Vector2(value.x, value.z);
            v3Destination = value;
            go();
        }
    }

    private NavMeshAgent agent;
    private bool moving = false;
    private bool rotating = false;

    // Use this for initialization
    void Start () {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        // fin du déplacement
        if (moving && agent.remainingDistance <= float.Epsilon)
        {
            // fin de mouvement
            moving = false;
            // on s'assure que le joueur va se déplacer en regardant dans la direction de son déplacement
            agent.updateRotation = true;
        }

        // fin de la rotation (généralement après la fin du déplacement)
        if (rotationRequired && rotating
            && Mathf.Abs(transform.eulerAngles.y - rotationValue) <= 0.5)
        {
            // fin de rotation
            rotating = false;
            // on remet les variables public en config par défaut
            rotationRequired = false;
            rotationValue = 0;
        }

        // la rotation (à partir de rotatingDistance case de la destination)
        if (rotationRequired && (rotating || moving)
            && Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) < rotatingDistance)
        {
            if (!rotating) // si on était pas déjà en train de rotate
            {
                // on s'assure que le syst de nav va pas pourir ma rotation à venir
                agent.updateRotation = false;
            }
            // en train d'effectuer la rotation
            rotating = true;
            // on rotate
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.AngleAxis(rotationValue, Vector3.up), Time.deltaTime * 2);
        }
    }

    // some private function ;)

    private void go()
    {
        agent.SetDestination(v3Destination);
        moving = true;
    }

}
