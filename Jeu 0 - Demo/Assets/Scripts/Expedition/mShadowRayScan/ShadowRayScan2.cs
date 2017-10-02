/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/

/*
This class is use to define a mesh by raytracing from a point

This class can be associed with any kind of GameObject
*/
public class ShadowRayScan2 : MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Unity GUI property
    /***************************************************/
    /***  UNITY GUI PROPERTY    ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Assessor
    /***************************************************/
    /***  ASSESSOR              ************************/
    /***************************************************/


    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  PUBLIC           ************************/
    public int RaysToShoot = 256; //64; 128; 1024; 
    public float distanceMax = 16;
    public float distanceMin = 1;
    public float champDeVision = 60;
    public float champPeripherique = 30;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public List<GameObject> objectsDetected;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private GameObject lightmeshholder;
    private float distance;
    private Vector2[] vertices2d;
    private int[] triangles;
    private Mesh mesh;

    //Texture grabber
    //Utilis� si l'allocation de texture se fait dans ce script
    //private Texture2D texture; 
    //private int screenwidth;
    //private int screenheight;
    //private int grab = 0;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY METHODES   ************************/

    // Use this for initialization
    void Start()
    {
        lightmeshholder = gameObject;
        //screenwidth = Screen.width;
        //screenheight = Screen.height;
        //var texture = new Texture2D (screenwidth, screenheight, TextureFormat.RGB24, false);

        vertices2d = new Vector2[RaysToShoot + 1];
        mesh = Instantiate(lightmeshholder.GetComponent<MeshFilter>().mesh);
        //lightmeshholder.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //float angle = 0;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        LayerMask maskLayer;
        float deltaDistance = distanceMax - distanceMin;
        float angle = lightmeshholder.transform.eulerAngles.y * Mathf.Deg2Rad;
        vertices2d[0] = new Vector2(0, 0);

        //Update flags and lists
        objectsDetected.Clear();


        for (int i = 1; i < RaysToShoot + 1; i++)
        {
            var x = Mathf.Sin(angle);
            var y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / RaysToShoot;

            Vector3 dir = new Vector3(x, 0, y);
            RaycastHit hit = new RaycastHit();
            maskLayer = ~(1 << 13);//On ignore les colliders des lumieres

            if ((angle * Mathf.Rad2Deg - lightmeshholder.transform.eulerAngles.y > (360 - (champDeVision / 2))) || (angle * Mathf.Rad2Deg - lightmeshholder.transform.eulerAngles.y < (champDeVision / 2)))
            {
                distance = distanceMax;
            }
            else if (angle * Mathf.Rad2Deg - lightmeshholder.transform.eulerAngles.y <= (champDeVision / 2) + champPeripherique)
            {
                distance = distanceMax - (deltaDistance / champPeripherique) * (angle * Mathf.Rad2Deg - lightmeshholder.transform.eulerAngles.y - (champDeVision / 2));
            }
            else if (angle * Mathf.Rad2Deg - lightmeshholder.transform.eulerAngles.y >= (360 - ((champDeVision / 2) + champPeripherique)))
            {
                distance = distanceMax - (deltaDistance / champPeripherique) * ((360 - (angle * Mathf.Rad2Deg - lightmeshholder.transform.eulerAngles.y) - (champDeVision / 2)));
            }
            else {
                distance = distanceMin;
            }

            if (Physics.Raycast(transform.position, dir, out hit, distance, maskLayer))
            {
                Debug.DrawLine(transform.position, hit.point, new Color(1, 1, 0, 1));
                var tmp = lightmeshholder.transform.InverseTransformPoint(hit.point);
                vertices2d[i] = new Vector2(tmp.x, tmp.z);

                analyserRayHit(hit);

            }
            else { // no hit
                Debug.DrawRay(transform.position, dir * distance, new Color(1, 1, 0, 1));
                var tmp2 = lightmeshholder.transform.InverseTransformPoint(lightmeshholder.transform.position + dir * distance);
                vertices2d[i] = new Vector2(tmp2.x, tmp2.z);
            }
        }

        // triangulate.cs
        Triangulator tr = new Triangulator(vertices2d);
        int[] indices = tr.Triangulate();

        // build mesh
        Vector2[] uvs = new Vector2[vertices2d.Length];
        Vector3[] newvertices = new Vector3[vertices2d.Length];
        for (int n = 0; n < newvertices.Length; n++)
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
        
    }

    /********  OUR METHODES     ************************/

    /********  PUBLIC           ************************/
    public Mesh getMesh()
    {
        return mesh;//(MeshFilter)gameObject.GetComponent("MeshFilter");
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    private void analyserRayHit(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            if (!objectsDetected.Contains(hit.collider.gameObject))
                objectsDetected.Add(hit.collider.gameObject);
        }
    }

    #endregion

}

