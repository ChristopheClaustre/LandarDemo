using UnityEngine;
using System.Collections;

public class GUISelectionBox : MonoBehaviour {

    // Draggable inspector reference to the Image GameObject's RectTransform.
    private RectTransform selectionBox;

    private SelectionBox box;

    private bool working = false;

    void Start ()
    {
        selectionBox = (RectTransform)GameObject.Find("selectionBox").transform;
        box = GetComponent<SelectionBox>();
    }

    void Update ()
    {
        if (box.LeftPressed)
        {
            working = true;

            // différence entre deux coins opposés du rectangle à calculer
            Vector2 difference = box.LeftEndClick - box.LeftStartClick;

            // copie du startclick pour calculer le nouveau rectangle
            Vector2 startPoint = box.LeftStartClick;

            // gestion de la possibilité de draggé depuis n'importe quel coin
            if (difference.x < 0)
            {
                startPoint.x = box.LeftEndClick.x;
                difference.x = -difference.x;
            }
            if (difference.y < 0)
            {
                startPoint.y = box.LeftEndClick.y;
                difference.y = -difference.y;
            }

            // on applique
            selectionBox.anchoredPosition = startPoint;
            selectionBox.sizeDelta = difference;
        }
        else if (working)
        {
            // Reset
            selectionBox.anchoredPosition = Vector2.zero;
            selectionBox.sizeDelta = Vector2.zero;
            working = false;
        }

        /*
        // While we are dragging.
        if (Input.GetMouseButton(0) && pressed)
        {
            // Store the current mouse position in screen space.
            Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // How far have we moved the mouse?
            Vector2 difference = currentMousePosition - initialClickPosition;

            // Copy the initial click position to a new variable. Using the original variable will cause
            // the anchor to move around to wherever the current mouse position is,
            // which isn't desirable.
            Vector2 startPoint = initialClickPosition;

            // The following code accounts for dragging in various directions.
            if (difference.x < 0)
            {
                startPoint.x = currentMousePosition.x;
                difference.x = -difference.x;
            }
            if (difference.y < 0)
            {
                startPoint.y = currentMousePosition.y;
                difference.y = -difference.y;
            }

            // Set the anchor, width and height every frame.
            selectionBox.anchoredPosition = startPoint;
            selectionBox.sizeDelta = difference;
        }

        // After we release the mouse button.
        if (Input.GetMouseButtonUp(0))
        {
            // Reset
            initialClickPosition = Vector2.zero;
            selectionBox.anchoredPosition = Vector2.zero;
            selectionBox.sizeDelta = Vector2.zero;
            pressed = false;
        }*/
    }
    /*
    // Click somewhere in the Game View.
    void OnMouseDown ()
    {
        // Get the initial click position of the mouse. No need to convert to GUI space
        // since we are using the lower left as anchor and pivot.
        initialClickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // The anchor is set to the same place.
        selectionBox.anchoredPosition = initialClickPosition;

        // the user pressed on this go
        pressed = true;
    }*/
}

