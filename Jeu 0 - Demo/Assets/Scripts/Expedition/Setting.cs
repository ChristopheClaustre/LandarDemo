using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

    /* gestion des settings du curseur */

    [SerializeField]
    private Texture2D cursorTex;
    [SerializeField]
    private Vector2 hotspot;

    public Texture2D CursorTex
    {
        get
        {
            return cursorTex;
        }
    }
    public Vector2 Hotspot
    {
        get
        {
            return hotspot;
        }
    }

    /* gestion des settings de déplacement de la cam */

    [SerializeField]
    private string mapName = "map";
    [SerializeField]
    public float percentMargin = 0.1f;
    [SerializeField]
    private float moveMaxSpeed = 5;
    [SerializeField]
    private float zoomSpeed = 1;
    [SerializeField]
    private float zoomMin = 6;
    [SerializeField]
    private float zoomMax = 1;

    private Transform mapTransform;

    public Transform Map
    {
        get
        {
            if (mapTransform == null)
                mapTransform = GameObject.Find(mapName).transform;

            return mapTransform;
        }
    }
    public float PercentMargin
    {
        get
        {
            return percentMargin;
        }
    }
    public float MoveMaxSpeed
    {
        get
        {
            return moveMaxSpeed;
        }
    }
    public float ZoomSpeed
    {
        get
        {
            return zoomSpeed;
        }
    }
    public float ZoomMin
    {
        get
        {
            return zoomMin;
        }
    }
    public float ZoomMax
    {
        get
        {
            return zoomMax;
        }
    }

    /* increment de formation */

    [SerializeField]
    private Vector2 formationIncr = new Vector2(0.75f, 0.75f);

    public Vector2 FormationIncr
    {
        get
        {
            return formationIncr;
        }
    }

    /* gestion du singleton */

    private static Setting instance;
    public static Setting Inst
    {
        get
        {
            if (instance == null)
            {
                instance = Camera.main.GetComponent<Setting>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
        Cursor.SetCursor(cursorTex, hotspot, CursorMode.Auto);
	}

}
