using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Navigation : MonoBehaviour, PersonnageScript.IAbonnePerso {

    // les variables de model
    private PersonnageScript persoScript;

    // information about rotation
    [SerializeField]
    private float rotatingDistance = 0.5f;
    [SerializeField]
    private string etatName = "seDeplace";
    
    // the destination for next call of the pathfinder
    private Destination dest;

    private NavMeshAgent agent;

    // état de la nav
    private enum EtatNav
    {
        None,
        Moving,
        RotaMoving,
        Rotating,
        Rectifying
    }

    [Header("Just watch, don't modify")]
    [SerializeField]
    private EtatNav state = EtatNav.None;

    // Use this for initialization
    void Start () {
        persoScript = GetComponent<PersonnageScript>();
        agent = this.GetComponent<NavMeshAgent>();
        persoScript.abonnement(this);
    }

    void Update ()
    {
        // si on a lancé l'ordre de bouger et qu'on a finit de calculer le path
        if ((state == EtatNav.Moving || state == EtatNav.RotaMoving) && !agent.pathPending)
        {
            // fin du déplacement
            if (agent.remainingDistance - agent.stoppingDistance <= float.Epsilon)
            {
                // si il n'y a pas de rotation c'est fini -> on relance l'algo sur la prochaine destination
                if (state == EtatNav.Moving)
                {
                    // ceci n'a rien à faire ici
                    GetComponentInChildren<Animator>().SetBool(etatName, false);

                    state = EtatNav.None;
                    // Reinit
                    persoScript.Trajet_nextDestination();
                }
                else
                {
                    state = EtatNav.Rotating;
                }
            }

            // début de rotation
            if (!float.IsNaN(dest.OrientationFinale) && state == EtatNav.Moving && agent.remainingDistance <= rotatingDistance)
            {
                // ceci n'a rien à faire ici
                GetComponentInChildren<Animator>().SetBool(etatName, false);

                state = EtatNav.RotaMoving;
                // on s'assure que le syst de nav ne va pas pourir ma rotation à venir
                agent.updateRotation = false;
            }
        }
        // si on a recu l'ordre de roter
        if (state != EtatNav.Moving && state != EtatNav.None)
        {
            // fin de rotation
            if (state == EtatNav.Rotating && Mathf.Abs(transform.eulerAngles.y - dest.OrientationFinale) <= 1f)
            {
                state = EtatNav.Rectifying;
                // on reactive la rotation pour le syst de nav
                agent.updateRotation = true;

                // Reinit
                persoScript.Trajet_nextDestination();
            }
            // on rotate
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.AngleAxis(dest.OrientationFinale, Vector3.up), Time.deltaTime * 2);
            }
        }
    }

    // some private function ;)

    private void go()
    {
        if (persoScript.Trajet_hasDestinations())
        {
            // ceci n'a rien à faire ici
            GetComponentInChildren<Animator>().SetBool(etatName, true);

            // récuperation de la destination
            dest = persoScript.Trajet_currentDestination();
            // on s'assure que le joueur va se déplacer en regardant dans la direction de son déplacement
            agent.updateRotation = true;
            // on applique la destination
            agent.SetDestination(dest.Cible);

            // on va se mettre à bouger
            state = EtatNav.Moving;
        }
    }

    public void newSelected(bool s)
    {
        // Ne rien faire
    }

    public void newPerso(Personnage p)
    {
        // Ne rien faire
    }

    public void newTrajet()
    {
        go();
    }
}
