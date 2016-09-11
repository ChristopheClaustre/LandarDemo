using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionBox : MonoBehaviour {

    [SerializeField]
    private bool pressed = false;
    private Vector3 begin;
    private Vector3 worldBegin;
    private Vector3 end;
    private Vector3 worldEnd;

    public bool LeftPressed
    {
        get
        {
            return pressed;
        }
    }

    public Vector3 LeftStartClick
    {
        get
        {
            return begin;
        }
    }

    public Vector3 LeftEndClick
    {
        get
        {
            return end;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (pressed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ExpeditionManager.Inst.applyNewSelection();

                // Reset
                pressed = false;
                begin = Vector3.zero;
                end = Vector3.zero;
                worldBegin = Vector3.zero;
                worldEnd = Vector3.zero;
            }
            else
            {
                ExpeditionManager.Inst.clearNewSelection();

                // on récupère la position de fin courante
                end = Input.mousePosition;
                worldEnd = Camera.main.ScreenToWorldPoint(end);

                // quels sont les persos dans la selectionBox ?
                for (int i = 0; i < ExpeditionManager.Persos.Count; i++)
                {
                    GameObject p = ExpeditionManager.Persos[i];
                    Vector3 pPos = p.transform.position;
                    bool xCheck = false;
                    bool zCheck = false;

                    // on check en X (en vérifiant la forme de la box)
                    if (worldBegin.x < worldEnd.x)
                        xCheck = (worldBegin.x <= pPos.x && pPos.x <= worldEnd.x);
                    else
                        xCheck = (worldEnd.x <= pPos.x && pPos.x <= worldBegin.x);

                    // on check en Z (en vérifiant la forme de la box)
                    if (worldBegin.z < worldEnd.z)
                        zCheck = (worldBegin.z <= pPos.z && pPos.z <= worldEnd.z);
                    else
                        zCheck = (worldEnd.z <= pPos.z && pPos.z <= worldBegin.z);

                    // si c'est bon, c'est bon !
                    if (xCheck && zCheck)
                        ExpeditionManager.Inst.addNewSelection(i);
                }
            }
        }
	}

    void OnMouseDown ()
    {
        if (!Input.GetMouseButton(1))
        {
            // on récupère la position de début
            begin = Input.mousePosition;
            worldBegin = Camera.main.ScreenToWorldPoint(begin);
            pressed = true;
            // on init la fin
            end = begin;
            worldEnd = worldBegin;
        }
    }
}
