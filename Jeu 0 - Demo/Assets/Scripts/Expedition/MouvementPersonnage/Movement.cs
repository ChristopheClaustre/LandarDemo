using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

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

    void Start()
    {
        pc = GetComponent<PersoController>();
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
                Vector3 dest = Camera.main.ScreenToWorldPoint(rStartClick);
                dest.y = 0;

                // if rotation needed
                if (rotationRequired)
                {
                    // calculate the orientation
                    rotationValue = angleBetweenVectorNAxis(rStartClick, rEndClick);
                    // apply the destination and rotation
                    pc.go(new Destination(dest, rotationValue));
                }
                else
                {
                    // apply the destination
                    pc.go(new Destination(dest));
                }

                // finished !
                rPressed = false;
                rStartClick = -Vector3.one;
                rEndClick = -Vector3.one;
                rotationRequired = false;
            }
        }
    }

    // right down
    void OnRightDown()
    {
        if (!Input.GetMouseButton(0))
        {
            rStartClick = Input.mousePosition;
            rPressed = true;
        }
    }

    // Some private function

    private float angleBetweenVectorNAxis(Vector3 pivot, Vector3 point)
    {
        Vector2 diff = point - pivot;
        return Vector2.Angle(Vector2.up, diff) * ((point.x < pivot.x)?-1:+1);
    }
}
