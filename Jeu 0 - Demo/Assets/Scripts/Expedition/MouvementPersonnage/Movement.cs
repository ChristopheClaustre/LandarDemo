using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    public interface IAbonneMovement {
        void newTrajet(Trajet t);
    }

    [SerializeField]
    private float rotatingDistance = 25;
    [Header("Just watch, don't modify !")]
    [SerializeField]
    private float rotationValue;
    [SerializeField]
    private bool rotationRequired;
    [SerializeField]
    private Vector3 rStartClick;
    [SerializeField]
    private Vector3 rEndClick;
    [SerializeField]
    private bool rPressed = false;

    public float RotationValue
    {
        get
        {
            return rotationValue;
        }
    }

    public bool RotationRequired
    {
        get
        {
            return rotationRequired;
        }
    }

    public Vector3 RightStartClick
    {
        get
        {
            return rStartClick;
        }
    }

    public Vector3 RightEndClick
    {
        get
        {
            return rEndClick;
        }
    }

    public bool RightPressed
    {
        get
        {
            return rPressed;
        }
    }

    // the script this script have to send to the infos
    private PersoController pc;

    private Trajet trajet;

    void Awake()
    {
        abonnes = new List<IAbonneMovement>();
    }

    void Start()
    {
        pc = GetComponent<PersoController>();
        trajet = new Trajet(Setting.Inst.MaxDestinationsPerTraject);
    }

    void Update()
    {
        if (rPressed)
        {
            // right is pressed actually
            if (Input.GetMouseButton(1))
            {
                // get the current end of click
                rEndClick = Input.mousePosition;

                // if needed
                if (Mathf.Abs(Vector3.Distance(rStartClick, rEndClick)) > rotatingDistance)
                {
                    rotationRequired = true;
                    // calculate the orientation
                    rotationValue = angleBetweenVectorNAxis(rStartClick, rEndClick);
                }
                else
                {
                    rotationRequired = false;
                }
            }

            // right click
            if (Input.GetMouseButtonUp(1))
            {
                // get the final end of click
                rEndClick = Input.mousePosition;

                // get the new wanted destination ;)
                Destination dest;
                Vector3 cible = Camera.main.ScreenToWorldPoint(rStartClick);
                cible.y = 0;

                // create the new destination
                if (rotationRequired)
                    dest = new Destination(cible, rotationValue);
                else
                    dest = new Destination(cible);

                // add the new destination
                trajet.addDestination(dest);
                newTrajet();

                // finished !
                reinit();
            }
        }

        // Is it the end ??
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && trajet.hasDestinations())
        {
            pc.Trajet = trajet;
            resetTrajet();

            // finished !
            reinit();
        }
    }

    // right down
    void OnRightDown()
    {
        if (ClickValidator.Inst == null || !ClickValidator.Inst.isOnBlackFoW(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (!Input.GetMouseButton(0) && !(trajet.WillLoop))
            {
                rStartClick = Input.mousePosition;
                rPressed = true;
            }
        }
    }

    public void bouclerTrajet(Destination candidat)
    {
        trajet.addDestination(candidat);
        if (trajet.WillLoop)
        {
            Debug.Log("(la bite à cricri == victoire) = vérité générale");
            pc.Trajet = trajet;
            resetTrajet();

            // finished !
            reinit();
        }
    }

    public bool canLoopOn(Destination d)
    {
        return trajet.Destinations.IndexOf(d) < trajet.Destinations.Count - 1;
    }

    // Some private function

    private void reinit()
    {
        rPressed = false;
        rStartClick = -Vector3.one;
        rEndClick = -Vector3.one;
        rotationRequired = false;
    }

    private void resetTrajet()
    {
        trajet = new Trajet(Setting.Inst.MaxDestinationsPerTraject);
        newTrajet();
    }

    private float angleBetweenVectorNAxis(Vector3 pivot, Vector3 point)
    {
        Vector2 diff = point - pivot;
        return Vector2.Angle(Vector2.up, diff) * ((point.x < pivot.x)?-1:+1);
    }

    // abonnement

    private List<IAbonneMovement> abonnes;
    
    public void abonnement(IAbonneMovement abonne)
    {
        abonnes.Add(abonne);
    }

    private void newTrajet()
    {
        foreach(IAbonneMovement abonne in abonnes)
        {
            abonne.newTrajet(trajet);
        }
    }
}
