using UnityEngine;
using System.Collections;

public class ShadowRayScan2 : MonoBehaviour
{
	private GameObject lightmeshholder;

	
	public int RaysToShoot = 256; //64; 128; 1024; 
	public float distanceMax = 16;
	public float distanceMin = 1;
	public float champDeVision = 60;
	public float champPeripherique= 30;
	private float distance;
	private Vector3[] vertices;
	private Vector2[] vertices2d;
	private int[] triangles;
	//private Vector3[] vertices2;
	private Mesh mesh;
	
	// texture grabber
	private Texture2D texture;
	private int screenwidth;
	private int screenheight;
	private int grab = 0;

	// Use this for initialization
	void Start ()
	{
		lightmeshholder = gameObject;
		screenwidth = Screen.width;
		screenheight = Screen.height;
		var texture = new Texture2D (screenwidth, screenheight, TextureFormat.RGB24, false);
		
		vertices = new Vector3[RaysToShoot+1];
		vertices2d = new Vector2[RaysToShoot+1];
		triangles = new int[RaysToShoot+1];
		//	vertices2 = new Vector3[4];
		mesh = Instantiate(lightmeshholder.GetComponent<MeshFilter>().mesh);

		//lightmeshholder.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		// dont cast if not moved?
		// build prelook-array of hit points/pixels/areas?
		// skip duplicate hit points (compare previous)
		// always same amount of vertices, no need create new mesh?..but need to triangulate or not??
		
		//float angle = 0;
		LayerMask maskLayer; 
		float deltaDistance = distanceMax - distanceMin;
		float angle = lightmeshholder.transform.eulerAngles.y * Mathf.Deg2Rad ;
		vertices2d[0] = new Vector2(0,0);
		for (int i = 1; i<RaysToShoot+1; i++) {
			var x = Mathf.Sin (angle);
			var y = Mathf.Cos (angle);
			angle += 2 * Mathf.PI / RaysToShoot;
			
			Vector3 dir = new Vector3 (x, 0, y);
			RaycastHit hit = new RaycastHit ();
			maskLayer = ~(1 << 13);//On ignore les colliders des lumieres

			if ((angle*Mathf.Rad2Deg-lightmeshholder.transform.eulerAngles.y > (360-(champDeVision/2))) || (angle*Mathf.Rad2Deg-lightmeshholder.transform.eulerAngles.y < (champDeVision/2))){
				distance = distanceMax;
			}else if(angle*Mathf.Rad2Deg-lightmeshholder.transform.eulerAngles.y <= (champDeVision/2)+champPeripherique){
				distance = distanceMax - (deltaDistance/champPeripherique)*(angle*Mathf.Rad2Deg-lightmeshholder.transform.eulerAngles.y - (champDeVision/2)) ;
			}else if(angle*Mathf.Rad2Deg-lightmeshholder.transform.eulerAngles.y >= (360-((champDeVision/2)+champPeripherique))){
				distance = distanceMax - (deltaDistance/champPeripherique)*((360-(angle*Mathf.Rad2Deg-lightmeshholder.transform.eulerAngles.y) - (champDeVision/2))) ;
			}else{
				distance = distanceMin;
				}
			//Transform transform = new Transform();

			if (Physics.Raycast(transform.position, dir,out hit, distance, maskLayer)) 
			{
							Debug.DrawLine (transform.position, hit.point,new Color(1,1,0,1));

				/*
			// bounce ray, ignore last hit object?
			// reflect more than 1 ray?
			Vector3 dir2 = Vector3.Reflect(dir, hit.normal);
			RaycastHit hit2 ;
			if (Physics.Raycast (hit.point, dir2, hit2, distance/4)) 
			{
				Debug.DrawLine (hit.point, hit2.point, Color(1,0,0,1));
			}else{ // we might not hit anything, because of short bounce distance
				Debug.DrawRay (hit.point, dir2*(distance/4), Color(1,0,0,1));
			}
			*/

				var tmp = lightmeshholder.transform.InverseTransformPoint(hit.point);
				vertices2d[i] = new Vector2(tmp.x*(float)1.1,tmp.z*(float)1.1);
			}else{ // no hit
							Debug.DrawRay (transform.position, dir*distance, new Color(1,1,0,1));
				var tmp2 = lightmeshholder.transform.InverseTransformPoint(lightmeshholder.transform.position+dir*distance);
				vertices2d[i] = new Vector2(tmp2.x,tmp2.z);
			}
		}
		
		// triangulate.cs
		Triangulator tr = new Triangulator(vertices2d);
		int[] indices = tr.Triangulate();
		
		// build mesh
		Vector2[] uvs = new Vector2[vertices2d.Length];
		Vector3[] newvertices = new Vector3[vertices2d.Length];
		for (int n = 0; n<newvertices.Length;n++) 
		{
			newvertices[n] = new Vector3(vertices2d[n].x, 0, vertices2d[n].y);
			
			// create some uv's for the mesh?
			 uvs[n] = vertices2d[n];
			
		}
		
		// Create the mesh
		//var msh : Mesh = new Mesh();
		mesh.vertices = newvertices;
		mesh.triangles = indices;
		mesh.uv = uvs;
		
		//    mesh.RecalculateNormals(); // need?
		//    mesh.RecalculateBounds(); // need ?
		
		// last triangles
		//	triangles[i+1] = 0;
		//	triangles[i+2] = 0;
		//	triangles[i+1] = 0;
		
		//triangles.Reverse();
		
		//	mesh.vertices = newvertices;
		//	mesh.triangles = triangles;
		
		// not every frame? clear texture before take new shot?
		//	if (grab>10) GrabToTexture();
		//	grab++;

	}

	public Mesh getMesh()
	{
		return mesh;//(MeshFilter)gameObject.GetComponent("MeshFilter");
	}
}
/*
function GrabToTexture()
{
	yield WaitForEndOfFrame();
  	texture.ReadPixels(new Rect(0, 0, screenwidth, screenheight), 0, 0, false);
	texture.Apply();
	renderer.material.mainTexture = texture;
	grab=0;
}
*/