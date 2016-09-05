using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float rotatingDistance = 25;
    [Header("Just watch, don't modify !")]
    [SerializeField]
    private float rotationValue;
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

    // the script this script have to send to the infos
    private PersoController pc;

    // right down
    void OnRightDown()
    {
        rStartClick = Input.mousePosition;
        rPressed = true;
    }

    void Update()
    {
        // right is pressed actually
        if (Input.GetMouseButton(1) && rPressed)
        {
            // get the current end of click
            rEndClick = Input.mousePosition;

            // if needed
            if (Mathf.Abs(Vector3.Distance(rStartClick, rEndClick)) > rotatingDistance)
            {
                // calculate the orientation
                rotationValue = angleBetweenVectorNAxis(rStartClick, rEndClick);
            }
        }

        // right click
        if (Input.GetMouseButtonUp(1) && rPressed)
        {
            // get the final end of click
            rEndClick = Input.mousePosition;

            // get the new wanted destination ;)
            Vector3 dest = Camera.main.ScreenToWorldPoint(rStartClick);
            dest.y = 0;

            // if rotation needed
            if (Mathf.Abs(Vector3.Distance(rStartClick, rEndClick)) > rotatingDistance)
            {
                // calculate the orientation
                rotationValue = angleBetweenVectorNAxis(rStartClick, rEndClick);
                // apply the destination and rotation
                pc.go(dest, rotationValue);
            }
            else
            {
                // apply the destination
                pc.go(dest);
            }

            // finished !
            rPressed = false;
            rStartClick = -Vector3.one;
            rEndClick = -Vector3.one;
        }
    }

    void Start()
    {
        pc = GetComponent<PersoController>();
    }

    private float angleBetweenVectorNAxis(Vector3 pivot, Vector3 point)
    {
        Vector2 diff = point - pivot;
        return Vector2.Angle(Vector2.up, diff) * ((point.x < pivot.x)?-1:+1);
    }
}
