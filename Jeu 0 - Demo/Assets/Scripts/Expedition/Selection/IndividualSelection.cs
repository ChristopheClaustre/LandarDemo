using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndividualSelection : MonoBehaviour {
    void OnMouseUpAsButton ()
    {
        if (!Input.GetMouseButton(1))
            ExpeditionManager.Inst.monoSelection(ExpeditionManager.Persos.IndexOf(this.gameObject));
    }
}
