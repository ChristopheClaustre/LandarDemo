using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpeditionManager : MonoBehaviour
{

    private static ExpeditionManager instance;
    public static ExpeditionManager Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ExpeditionManager>();
            }
            return instance;
        }
    }

    private static List<GameObject> personages;
    public static List<GameObject> Persos
    {
        get
        {
            if (personages == null)
            {
                personages = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            }
            return personages;
        }
    }

    public List<int>[] formations;
    public List<int> selected;
    public int formationSelected = -1;

    void Start()
    {
        formations = new List<int>[5];
    }

    private List<int> newSelection = new List<int>();

    public void nouvellesSelections(List<int> newSelec)
    {
        // remplacement de l'ancienne sélection
        newSelection.Clear();
        newSelection.AddRange(newSelec);

        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            for (int i = 0; i < Persos.Count; i++)
            {
                bool inSelected = selected.Contains(i);
                bool inNewSelect = newSelection.Contains(i);
                
                // inSelected xor inNewSelect
                Persos[i].GetComponent<Navigation>().Selected = inSelected ^ inNewSelect;
            }
        }
        // sélection simple
        else
        {
            for (int i = 0; i < Persos.Count; i++)
            {
                bool inNewSelect = newSelection.Contains(i);

                Persos[i].GetComponent<Navigation>().Selected = inNewSelect;
            }
        }
    }

    public void applyNewSelection()
    {
        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            foreach (int i in newSelection)
            {
                if (selected.Contains(i))
                {
                    selected.Remove(i);
                }
                else
                {
                    selected.Add(i);
                }
            }
        }
        // sélection simple
        else
        {
            selected.Clear();
            selected.AddRange(newSelection);
        }

        newSelection.Clear();
    }

    public void nouvelleSelection(int i)
    {
        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // dé/sélection du perso
            if (!selected.Contains(i))
            {
                selected.Add(i);
                Persos[i].GetComponent<Navigation>().Selected = true;
            }
            else
            {
                selected.Remove(i);
                Persos[i].GetComponent<Navigation>().Selected = false;
            }
        }
        else
        {
            // suppression de la sélection précedente des persos
            clearSelection();

            // sélection du perso
            selected.Add(i);
            Persos[i].GetComponent<Navigation>().Selected = true;
        }
    }

    /* Some private function */

    private void clearSelection ()
    {
        foreach (int i in selected)
        {
            Persos[i].GetComponent<Navigation>().Selected = false;
        }
        selected.Clear();
    }
}
