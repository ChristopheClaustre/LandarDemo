/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class Setting :
    MonoBehaviour
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /* gestion des settings du curseur */
    public Texture2D CursorTex { get { return m_cursorTex; } }
    public Vector2 Hotspot { get { return m_hotspot; } }

    /* gestion des settings de déplacement de la cam */
    public float MouseMarginInPercent { get { return m_mouseMarginInPercent; } }
    public float CameraVelocity { get { return m_cameraVelocity; } }
    public float ZoomVelocity { get { return m_zoomVelocity; } }
    public float MinimalZoom { get { return m_minimalZoom; } }

    /* padding de formation */
    public Vector2 FormationPadding { get { return m_formationPadding; } }
    public int MaxDestinationsPerTraject { get { return m_maxDestinationsPerTraject; } }

    /* gestion du singleton */
    public static Setting Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /* gestion des settings du curseur */
    [Header("Curseur")]
    [SerializeField] private Texture2D m_cursorTex;
    [SerializeField] private Vector2 m_hotspot;

    /* gestion des settings de déplacement de la cam */
    [Header("Déplacements de la caméra")]
    [SerializeField] private float m_mouseMarginInPercent = 0.015f;
    [SerializeField] private float m_cameraVelocity = 5;
    [SerializeField] private float m_zoomVelocity = 2;
    [SerializeField] private float m_minimalZoom = 1;

//    private Transform mapTransform;

    /* increment de formation */
    [Header("Formation de personage")]
    [SerializeField] private Vector2 m_formationPadding = new Vector2(0.75f, 0.75f);
    [SerializeField] private int m_maxDestinationsPerTraject = 15;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    /* gestion du singleton */
    private static Setting instance;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    void Awake()
    {
//        instance = Camera.main.GetComponent<Setting>();
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(m_cursorTex, m_hotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    public void Update()
    {

    }

    #endregion
}
