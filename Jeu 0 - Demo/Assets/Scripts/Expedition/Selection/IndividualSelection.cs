using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndividualSelection : MonoBehaviour {
    void OnMouseUpAsButton ()
    {
        ExpeditionManager.Inst.monoSelection(ExpeditionManager.Persos.IndexOf(this.gameObject));
    }
}
