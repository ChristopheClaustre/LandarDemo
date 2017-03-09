using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkFowToCamera : MonoBehaviour {

    public RenderTexture grayFow;
    // Use this for initialization
    void Start () {
        //On resize la texture pour quelle correponde dynamiquement à la camera
        grayFow.width = Camera.main.pixelWidth;
        grayFow.height = Camera.main.pixelHeight;

        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float widthInWorld = (lowerLeft - lowerRight).magnitude;
        float heighInWorld = (lowerLeft - topLeft).magnitude;

        this.transform.localScale = new Vector3(widthInWorld * Camera.main.aspect, heighInWorld * Camera.main.aspect, 0);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float widthInWorld = (lowerLeft - lowerRight).magnitude;
        float heighInWorld = (lowerLeft - topLeft).magnitude;

        this.transform.localScale = new Vector3(widthInWorld, heighInWorld, 0);
    }
}