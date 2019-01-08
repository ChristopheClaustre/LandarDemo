/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CreationDestinationAPI :
    MonoBehaviour
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



    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/



    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    public bool TestMePlease = false;

    [Header("ray")]
    public Vector3 origin_;
    public Vector3 target;
    public LayerMask m_layerMaskForPossiblePosition;

    [Header("formation")]
    public Vector3 origin;
    public List<int> elementNumbers = new List<int> { 1, 3, 5, 4 };
    public Vector2 shifts = new Vector2(0.5f, 0.5f);
    public float orientation = 0.0f;

    [Header("expedition")]
    public int element = 0;
    public float shiftExpedition = 0.5f;
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (TestMePlease)
        {
            // test rayon
            //Vector3 obstacle = GetObstaclePosition(origin_, target, m_layerMaskForPossiblePosition);
            //ShowARay(origin_, obstacle, target, Color.green, Time.deltaTime);

            // test formation
            //List<List<Vector3>> formationPosition;
            //GetFormationPositions(origin, shifts, elementNumbers, orientation, out formationPosition);

            //ShowFormation(formationPosition, Quaternion.AngleAxis(orientation, Vector3.up), shifts.x / 2.0f, Color.blue, Time.deltaTime);

            // test expedition
            List<Vector3> expeditionPosition;
            float radius = GetExpeditionRadius(element, shiftExpedition);
            GetExpeditionPositions(origin, shiftExpedition, radius, element, out expeditionPosition);

            ShowExpedition(expeditionPosition, shiftExpedition, Color.magenta, Time.deltaTime);

            //TestMePlease = false;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void GetExpeditionPositions(Vector3 p_target, float p_shiftRadius, float p_radius, float p_numberOfElement, out List<Vector3> p_positions)
    {
        p_positions = new List<Vector3>();

        for(int i = 0; i < p_numberOfElement; i++)
        {
            int j = 0;
            Vector3 candidate = p_target;
            do {
                Vector3 tempCandidate = CreateAnExpeditionPosition(p_target, p_radius);
                candidate = GetObstaclePosition(p_target, tempCandidate, m_layerMaskForPossiblePosition);
                // Debug
                ShowARay(p_target, candidate, tempCandidate, Color.green, Time.deltaTime);
            } while (! CheckAPosition(candidate, p_shiftRadius, p_positions) && ++j < 30);

            p_positions.Add(candidate);
        }
    }

    public void GetFormationPositions(Vector3 p_target, Vector2 p_shifts, List<int> p_numberOfElementOnEachRow, float p_angleAroundUp, out List<List<Vector3>> p_formationPositions)
    {
        p_formationPositions = new List<List<Vector3>>();

        foreach (int number in p_numberOfElementOnEachRow)
        {
            List<Vector3> line;
            GetLinePosition(p_target, p_shifts.y, number, p_angleAroundUp, out line);
            p_formationPositions.Add(line);

            p_target -= Quaternion.AngleAxis(p_angleAroundUp, Vector3.up) * new Vector3(p_shifts.x, 0, 0);
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    //** Expedition position

    private Vector3 CreateAnExpeditionPosition(Vector3 p_origin, float p_radius)
    {
        float r = Random.Range(0, p_radius);
        float angle = Random.Range(0, 2 * Mathf.PI);

        Vector3 candidate = p_origin + new Vector3(r * Mathf.Cos(angle), 0, r * Mathf.Sin(angle));

        return candidate;
    }

    private bool CheckAPosition(Vector3 p_candidate, float p_shiftRadius, List<Vector3> p_previousPositions)
    {
        return p_previousPositions.TrueForAll(x => Mathf.Abs(Vector3.Distance(x, p_candidate)) > 2 * p_shiftRadius);
    }

    private float GetExpeditionRadius(int p_nbElement, float p_shiftBetweenElement)
    {
        // a : area taken by one element
        float a = p_shiftBetweenElement * Mathf.PI * Mathf.PI;
        // A : area of the circle where elements will be created
        float computedA = a * ((p_nbElement-1) * 0.75f);

        return Mathf.Sqrt(computedA / Mathf.PI);
    }

    //** Formation position

    private void GetLinePosition(Vector3 p_origin, float p_columnShift, int p_numberOfColumn, float p_angleAroundUp, out List<Vector3> p_linePositions)
    {
        p_linePositions = new List<Vector3>();

        float endZ = p_columnShift * (p_numberOfColumn-1) / 2.0f;
        float startZ = -endZ;

        for (float i = startZ; i <= endZ+float.Epsilon; i += p_columnShift)
        {
            p_linePositions.Add(GetNewPosition(p_origin, i, p_angleAroundUp));
        }
    }

    private Vector3 GetNewPosition(Vector3 p_origin, float p_actualShift, float p_angleAroundUp = 0.0f)
    {
        Vector3 position = new Vector3(0, 0, p_actualShift);
        Quaternion rotation = Quaternion.AngleAxis(p_angleAroundUp, Vector3.up);

        return p_origin + rotation * position;
    }

    //** Get obstacle and show debug info

    private Vector3 GetObstaclePosition(Vector3 p_original, Vector3 p_candidate, LayerMask p_layerMask)
    {
        RaycastHit raycastHit;
        float distance = Vector3.Distance(p_original, p_candidate);
        bool hit = Physics.Raycast(p_original, p_candidate - p_original, out raycastHit, distance, p_layerMask);

        if (hit)
        {
            return raycastHit.point;
        }
        else
        {
            return p_candidate;
        }
    }

    private void ShowARay(Vector3 p_origin, Vector3 p_obstacle, Vector3 p_end, Color p_color, float p_duration)
    {
        ShowARay(p_origin, p_obstacle, p_color, p_duration);

        if (p_obstacle != p_end)
        {
            ShowARay(p_obstacle, p_end, Color.gray, p_duration, true);
        }
    }

    private void ShowARay(Vector3 p_origin, Vector3 p_end, Color p_color, float p_duration, bool p_noOriginMarker = false)
    {
        Debug.DrawRay(p_origin, p_end - p_origin, p_color, p_duration, false);

        Color markerColor = p_color / 1.5f;

        if (! p_noOriginMarker)
        {
            ShowMarker(p_origin, 0.1f, markerColor, p_duration);
        }

        Quaternion direction = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.right, p_end - p_origin, Vector3.up), Vector3.up);
        ShowDirectionMarker(p_end, direction, 0.1f, markerColor, p_duration);
    }

    private void ShowMarker(Vector3 p_point, float p_size, Color p_color, float p_duration)
    {
        // horizontal line ---
        Debug.DrawLine(p_point - new Vector3(p_size, 0, 0), p_point + new Vector3(p_size, 0, 0), p_color, p_duration);
        // vertical line |
        Debug.DrawLine(p_point - new Vector3(0, 0, p_size), p_point + new Vector3(0, 0, p_size), p_color, p_duration);
    }

    private void ShowDirectionMarker(Vector3 p_point, Quaternion p_direction, float p_size, Color p_color, float p_duration)
    {
        // upper part \
        Debug.DrawLine(p_point + p_direction * new Vector3(-p_size, 0, p_size), p_point + p_direction * new Vector3(0, 0, 0), p_color, p_duration);
        // lower part /
        Debug.DrawLine(p_point + p_direction * new Vector3(-p_size, 0, -p_size), p_point + p_direction * new Vector3(0, 0, 0), p_color, p_duration);
    }

    private void ShowExpedition(List<Vector3> p_points, float p_size, Color p_color, float p_duration)
    {
        foreach (var pos in p_points) ShowMarker(pos, p_size, p_color, p_duration);
    }

    private void ShowFormation(List<List<Vector3>> p_formation, Quaternion p_direction, float p_size, Color p_color, float p_duration)
    {
        foreach(var line in p_formation)
            foreach (var pos in line) ShowDirectionMarker(pos, p_direction, p_size, p_color, p_duration);
    }

    #endregion
}
