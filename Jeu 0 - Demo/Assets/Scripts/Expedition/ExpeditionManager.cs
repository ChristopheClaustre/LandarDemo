using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpeditionManager : MonoBehaviour
{

    // Singleton management about an ExpeditionManager instance
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

    // Singleton management about a list of GameObject
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

    // the formations 
    public List<int>[] formations;

    [Header("Just watch, don't modify ;)")]
    public List<int> selected;
    [SerializeField]
    private List<int> newSelection;
    [Header("Can be modified (-1 : no formation)")]
    [Range(-1,4)]
    public int formationSelected = -1;

    void Start()
    {
        formations = new List<int>[5];
        newSelection = new List<int>();
    }

    public void clearNewSelection()
    {
        newSelection.Clear();
    }

    public void addNewSelection(int i)
    {
        if (!newSelection.Contains(i))
            newSelection.Add(i);
    }

    public void previewNewSelection ()
    {
        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            for (int i = 0; i < Persos.Count; i++)
            {
                // dse booléens pour éclaircir le code
                bool inSelected = selected.Contains(i);
                bool inNewSelect = newSelection.Contains(i);
                
                // inSelected xor inNewSelect
                Persos[i].GetComponent<Navigation>().Selected = inSelected ^ inNewSelect;
            }
        }
        else // sélection simple
        {
            for (int i = 0; i < Persos.Count; i++)
            {
                Persos[i].GetComponent<Navigation>().Selected = newSelection.Contains(i);
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
                    selected.Remove(i);
                else
                    selected.Add(i);
            }
        }
        else // sélection simple
        {
            selected.Clear();
            selected.AddRange(newSelection);
        }

        // affichage
        for(int i = 0; i < Persos.Count; i++)
        {
            Persos[i].GetComponent<Navigation>().Selected = selected.Contains(i);
        }
    }

    public void monoSelection(int i)
    {
        clearNewSelection();

        // complétion de sélection
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (selected.Contains(i))
                selected.Remove(i);
            else
                selected.Add(i);
        }
        else // sélection simple
        {
            selected.Clear();
            newSelection.Add(i);
            selected.AddRange(newSelection);
        }

        // affichage
        for (int j = 0; j < Persos.Count; j++)
        {
            Persos[j].GetComponent<Navigation>().Selected = selected.Contains(j);
        }
    }
}
