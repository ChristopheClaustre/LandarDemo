using UnityEngine;
using System.Collections;

public abstract class CI_caller : MonoBehaviour {

#if (UNITY_EDITOR)
    public abstract void updateVarFromCI();
#endif

}
