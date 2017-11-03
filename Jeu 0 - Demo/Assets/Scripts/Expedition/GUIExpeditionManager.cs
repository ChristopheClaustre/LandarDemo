using UnityEngine;
using System.Collections;

public class GUIExpeditionManager : MonoBehaviour, ExpeditionManager.IAbonneEM {

    private ExpeditionManager em;

	// Use this for initialization
	void Start () {
        em = ExpeditionManager.Instance;
        em.abonnement(this);
	}

    public void newSelected()
    {
        // affichage
        for (int i = 0; i < ExpeditionManager.Persos.Count; i++)
        {
            ExpeditionManager.Persos[i].GetComponent<PersonnageScript>().Selected = em.Selected.Contains(i);
        }
    }
}
