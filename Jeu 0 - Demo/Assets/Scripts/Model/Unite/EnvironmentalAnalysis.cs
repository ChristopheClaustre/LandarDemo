using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalAnalysis : MonoBehaviour
{

    public ShadowRayScan2 radar;
    private  List<GameObject> ennemies;



    void Awake()
    {
        ennemies = new List<GameObject>();
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Tri des objets détectés
        ennemies.Clear();
        foreach (GameObject objetDetected in radar.objectsDetected)
        {
            if(objetDetected.CompareTag("model_ennemy"))
            {
                ennemies.Add(objetDetected);
            }
        }
        Debug.Log(ennemies.Count);
    }
}
