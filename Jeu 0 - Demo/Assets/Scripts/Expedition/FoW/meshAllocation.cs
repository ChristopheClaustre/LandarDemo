using UnityEngine;
using System.Collections;

public class meshAllocation : MonoBehaviour {

	public GameObject initialObject;
	public GameObject player;
	public Material material;

    //Mesh initialMesh;
    Mesh swapMesh;
	
	GameObject theTarget;
	
	// Use this for initialization
	void Start () {
		theTarget = initialObject;
        //Remise à zero de la roatation du mesh
        theTarget.transform.eulerAngles = new Vector3(0, initialObject.transform.eulerAngles.y, 0);
        //initialMesh = initialObject.GetComponent<MeshFilter>().mesh;
        swapMesh = player.GetComponent<ShadowRayScan2>().getMesh();
		
		theTarget.GetComponent<MeshFilter>().mesh = swapMesh;
		theTarget.GetComponent<MeshRenderer>().material = material;
	}
	
	// Update is called once per frame
	void Update () {
		theTarget = initialObject;
        //initialMesh = initialObject.GetComponent<MeshFilter>().mesh;
        swapMesh = player.GetComponent<ShadowRayScan2>().getMesh();
		
		theTarget.GetComponent<MeshFilter>().mesh = swapMesh;
		//theTarget.GetComponent<Renderer>().material = material;
	}
}
