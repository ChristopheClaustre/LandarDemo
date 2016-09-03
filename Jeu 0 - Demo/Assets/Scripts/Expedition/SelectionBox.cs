using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionBox : MonoBehaviour {

    [SerializeField]
    private bool pressed = false;
    private Vector3 begin;
    private Vector3 end;

    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && pressed)
        {
            // on récupère la position de fin courante
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // la liste des persos dans la selectionBox
            List<int> newSelection = new List<int>();

            // quels sont les persos dans la selectionBox ?
            for (int i = 0; i < ExpeditionManager.Persos.Count; i++)
            {
                GameObject p = ExpeditionManager.Persos[i];
                Vector3 pPos = p.transform.position;
                bool xCheck = false;
                bool zCheck = false;

                // on check en X (en vérifiant la forme de la box)
                if (begin.x < end.x)
                    xCheck = (begin.x <= pPos.x && pPos.x <= end.x);
                else
                    xCheck = (end.x <= pPos.x && pPos.x <= begin.x);

                // on check en Z (en vérifiant la forme de la box)
                if (begin.z < end.z)
                    zCheck = (begin.z <= pPos.z && pPos.z <= end.z);
                else
                    zCheck = (end.z <= pPos.z && pPos.z <= begin.z);

                // si c'est bon, c'est bon !
                if (xCheck && zCheck)
                {
                    newSelection.Add(i);
                }
            }

            // on applique ;)
            ExpeditionManager.Inst.nouvellesSelections(newSelection);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ExpeditionManager.Inst.applyNewSelection();

            // on reinit
            pressed = false;
            begin = Vector3.zero;
            end = Vector3.zero;
        }
	}

    void OnMouseDown ()
    {
        // on récupère la position de début
        begin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pressed = true;
        // on init la fin
        end = begin;
    }
}
