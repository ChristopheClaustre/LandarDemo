using UnityEngine;
using System.Collections;

public class MeshLayerManager : MonoBehaviour {

    public string LayerName;
    public int Order;
    // Use this for initialization
    void Start () {
        foreach (Renderer r in this.GetComponents<Renderer>())
        {
            r.sortingLayerName = LayerName;
            r.sortingOrder = Order;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
