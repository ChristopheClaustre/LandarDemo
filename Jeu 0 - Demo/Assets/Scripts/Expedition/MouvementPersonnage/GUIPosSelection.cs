using UnityEngine;
using System.Collections;

public class GUIPosSelection : MonoBehaviour {

    private Vector3 scale;

	void OnMouseEnter()
    {
        // on retient la taille de marqueur actuel
        scale = transform.localScale;
        PosSelection pos = GetComponent<PosSelection>();
        if (pos.mov.canLoopOn(pos.dest))
            // modification de la taille du marqueur
            transform.localScale = scale * 2f;
    }

    void OnMouseExit()
    {
        transform.localScale = scale;
    }
}
