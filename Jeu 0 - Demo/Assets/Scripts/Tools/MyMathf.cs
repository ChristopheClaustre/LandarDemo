using UnityEngine;
using System.Collections;

public class MyMathf : MonoBehaviour {

	public static float posModulo(float f, float m)
    {
        f = f % m;
        if (f < 0)
            f += m;
        return f;
    }

    public static int posModulo(int i, int m)
    {
        i = i % m;
        if (i < 0)
            i += m;
        return i;
    }
}
