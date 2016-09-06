using UnityEngine;
using System.Collections;

public class MeshLayerManager : MonoBehaviour {

    public string LayerName;
    public int Order;
    // Use this for initialization
    void Start () {
	    this.GetComponent<MeshRenderer>().sortingLayerName = LayerName;
        this.GetComponent<MeshRenderer>().sortingOrder = Order;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
