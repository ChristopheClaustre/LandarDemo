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
This class is use to define a mesh base on the surface form by raytracing from a point

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
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public List<GameObject> ObjectsDetected
    {
        get
        {
            return m_objectsDetected;   // Don't do this
        }
    }
    /********  PROTECTED        ************************/

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /******** INSPECTOR ************************/
    [Header("Rays properties")]
    [SerializeField]
    private int m_raysToShoot = 256; //64; 128; 1024; 
    [SerializeField]
    private float m_distanceMax = 16;
    [SerializeField]
    private float m_distanceMin = 1;
    [Header("View properties")]
    [SerializeField]
    private float m_champDeVision = 60;
    [SerializeField]
    private float m_champPeripherique = 30;
    [Header("Rotation")]
    [SerializeField]
    private float m_rotationX;
    [SerializeField]
    private float m_rotationY;
    [SerializeField]
    private float m_rotationZ;
    private List<GameObject> m_objectsDetected;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private GameObject m_lightmeshholder;
    private float m_distance;
    private Vector2[] m_vertices2d;
    private int[] m_triangles;
    private Mesh m_mesh;

    //Texture grabber
    //Utilisé si l'allocation de texture se fait dans ce script
    //private Texture2D texture; 
    //private int screenwidth;
    //private int screenheight;
    //private int grab = 0;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /******** UNITY MESSAGES ************************/

    // Use this for initialization
    void Start()
    {
        m_lightmeshholder = gameObject;
        //screenwidth = Screen.width;
        //screenheight = Screen.height;
        //var texture = new Texture2D (screenwidth, screenheight, TextureFormat.RGB24, false);

        m_vertices2d = new Vector2[m_raysToShoot + 1];
        m_mesh = Instantiate(m_lightmeshholder.GetComponent<MeshFilter>().mesh);
        m_objectsDetected = new List<GameObject>();
        //lightmeshholder.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //float angle = 0;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        LayerMask maskLayer;
        float deltaDistance = m_distanceMax - m_distanceMin;
        float angle = m_lightmeshholder.transform.eulerAngles.y * Mathf.Deg2Rad;
        m_vertices2d[0] = new Vector2(0, 0);

        //Update flags and lists
        m_objectsDetected.Clear();


        for (int i = 1; i < m_raysToShoot + 1; i++)
        {
            var x = Mathf.Sin(angle);
            var y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / m_raysToShoot;

            Vector3 dir = new Vector3(x, 0, y);
            RaycastHit hit = new RaycastHit();
            maskLayer = ~(1 << 13);//On ignore les colliders des lumieres

            if ((angle * Mathf.Rad2Deg - m_lightmeshholder.transform.eulerAngles.y > (360 - (m_champDeVision / 2))) || (angle * Mathf.Rad2Deg - m_lightmeshholder.transform.eulerAngles.y < (m_champDeVision / 2)))
            {
                m_distance = m_distanceMax;
            }
            else if (angle * Mathf.Rad2Deg - m_lightmeshholder.transform.eulerAngles.y <= (m_champDeVision / 2) + m_champPeripherique)
            {
                m_distance = m_distanceMax - (deltaDistance / m_champPeripherique) * (angle * Mathf.Rad2Deg - m_lightmeshholder.transform.eulerAngles.y - (m_champDeVision / 2));
            }
            else if (angle * Mathf.Rad2Deg - m_lightmeshholder.transform.eulerAngles.y >= (360 - ((m_champDeVision / 2) + m_champPeripherique)))
            {
                m_distance = m_distanceMax - (deltaDistance / m_champPeripherique) * ((360 - (angle * Mathf.Rad2Deg - m_lightmeshholder.transform.eulerAngles.y) - (m_champDeVision / 2)));
            }
            else {
                m_distance = m_distanceMin;
            }

            if (Physics.Raycast(transform.position, dir, out hit, m_distance, maskLayer))
            {
                Debug.DrawLine(transform.position, hit.point, new Color(1, 1, 0, 1));
                var tmp = m_lightmeshholder.transform.InverseTransformPoint(hit.point);
                m_vertices2d[i] = new Vector2(tmp.x, tmp.z);

                analyserRayHit(hit);

            }
            else { // no hit
                Debug.DrawRay(transform.position, dir * m_distance, new Color(1, 1, 0, 1));
                var tmp2 = m_lightmeshholder.transform.InverseTransformPoint(m_lightmeshholder.transform.position + dir * m_distance);
                m_vertices2d[i] = new Vector2(tmp2.x, tmp2.z);
            }
        }

        // triangulate.cs
        Triangulator tr = new Triangulator(m_vertices2d);
        int[] indices = tr.Triangulate();

        // build mesh
        Vector2[] uvs = new Vector2[m_vertices2d.Length];
        Vector3[] newvertices = new Vector3[m_vertices2d.Length];
        for (int n = 0; n < newvertices.Length; n++)
        {
            newvertices[n] = new Vector3(m_vertices2d[n].x, 0, m_vertices2d[n].y);

            // create some uv's for the mesh?
            uvs[n] = m_vertices2d[n];

        }

        // Create the mesh
        //var msh : Mesh = new Mesh();
        m_mesh.vertices = newvertices;
        m_mesh.triangles = indices;
        m_mesh.uv = uvs;
        
    }

    /******** OUR MESSAGES ************************/

    /********  PUBLIC           ************************/
    public Mesh getMesh()
    {
        return m_mesh;//(MeshFilter)gameObject.GetComponent("MeshFilter");
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    private void analyserRayHit(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            if (!m_objectsDetected.Contains(hit.collider.gameObject))
                m_objectsDetected.Add(hit.collider.gameObject);
        }
    }

    #endregion

}

