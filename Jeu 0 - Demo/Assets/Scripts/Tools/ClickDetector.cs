using UnityEngine;
using System.Collections;

public class ClickDetector : MonoBehaviour
{
    public bool HandleRightClick = true;
    public bool HandleMiddleClick = false;

    public string OnRightClickMethodName = "OnRightUpAsButton";
    public string OnRightDownMethodName = "OnRightDown";
    public string OnRightUpMethodName = "OnRightUp";
    public string OnMiddleClickMethodName = "OnMiddleUpAsButton";
    public string OnMiddleDownMethodName = "OnMiddleDown";
    public string OnMiddleUpMethodName = "OnMiddleUp";

    public LayerMask layerMask;

    private GameObject rClicked;
    private GameObject mClicked;
    
    void Update()
    {
        GameObject clickedGmObj = null;
        bool clickedGmObjAcquired = false;
        // Right down
        if (HandleRightClick && Input.GetMouseButtonDown(1))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(OnRightDownMethodName, null, SendMessageOptions.DontRequireReceiver);
                rClicked = clickedGmObj;
            }
        }
        // Middle down
        if (HandleMiddleClick && Input.GetMouseButtonDown(2))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(OnMiddleDownMethodName, null, SendMessageOptions.DontRequireReceiver);
                mClicked = clickedGmObj;
            }
        }
        // Right up and up as button
        if (HandleRightClick && Input.GetMouseButtonUp(1))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(OnRightUpMethodName, null, SendMessageOptions.DontRequireReceiver);
                if (clickedGmObj == rClicked)
                {
                    clickedGmObj.SendMessage(OnRightClickMethodName, null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        // Middle up and up as button
        if (HandleMiddleClick && Input.GetMouseButtonUp(2))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                clickedGmObj.SendMessage(OnMiddleUpMethodName, null, SendMessageOptions.DontRequireReceiver);
                if (clickedGmObj == mClicked)
                {
                    clickedGmObj.SendMessage(OnMiddleClickMethodName, null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

    private GameObject GetClickedGameObject()
    {
        // Builds a ray from camera point of view to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return hit.transform.gameObject;
        else
            return null;
    }
}