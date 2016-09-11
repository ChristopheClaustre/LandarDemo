using UnityEngine;
using System.Collections;

public class GUIExpeditionManager : MonoBehaviour {

    private ExpeditionManager em;

	// Use this for initialization
	void Start () {
        em = ExpeditionManager.Inst;
	}
	
	// Update is called once per frame
	void Update () {
        // affichage
        for (int i = 0; i < ExpeditionManager.Persos.Count; i++)
        {
            ExpeditionManager.Persos[i].GetComponent<Navigation>().Selected = em.Selected.Contains(i);
        }
    }
}
