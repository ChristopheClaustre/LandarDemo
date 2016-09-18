using UnityEngine;
using System.Collections;

public class PosSelection : MonoBehaviour {
    [HideInInspector]
    public Movement mov;
    [HideInInspector]
    public Destination dest;

	void OnRightUpAsButton()
    {
        mov.bouclerTrajet(dest);
    }
}
