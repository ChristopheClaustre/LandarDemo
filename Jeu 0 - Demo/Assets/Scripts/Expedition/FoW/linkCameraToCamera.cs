using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkCameraToCamera : MonoBehaviour {

    public Camera fowCamera;
    // Use this for initialization
    void Start()
    {
        fowCamera.aspect = Camera.main.aspect;
        fowCamera.orthographicSize = Camera.main.orthographicSize;
        fowCamera.fieldOfView = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        fowCamera.aspect = Camera.main.aspect;
        fowCamera.orthographicSize = Camera.main.orthographicSize;
        fowCamera.fieldOfView = Camera.main.fieldOfView;
    }
}
