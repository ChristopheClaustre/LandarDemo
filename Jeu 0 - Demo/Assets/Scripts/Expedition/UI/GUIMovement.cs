using UnityEngine;
using System.Collections;

public class GUIMovement : MonoBehaviour {

    // Draggable inspector reference to the Image GameObject's RectTransform.
    private RectTransform directionFormation;

    private Movement mov;

    private bool working = false;

    // Use this for initialization
    void Start () {
        directionFormation = (RectTransform)GameObject.Find("directionFormation").transform;
        mov = GetComponent<Movement>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (mov.RotationRequired)
        {
            directionFormation.anchoredPosition = mov.RightStartClick;
            directionFormation.localEulerAngles = new Vector3(0, 0, -mov.RotationValue);
            working = true;
        }
        else if (working)
        {
            // Reset
            directionFormation.anchoredPosition = new Vector2(Screen.width, Screen.height);
            directionFormation.localEulerAngles = Vector3.zero;
            working = false;
        }
	}
}
