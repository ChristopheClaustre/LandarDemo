using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpeditionManager : MonoBehaviour {

    public interface IAbonneEM
    {
        void newSelected();
    }

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
    private static List<GameObject> personnages;
    public static List<GameObject> Persos
    {
        get
        {
            if (personnages == null)
            {
                personnages = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            }
            return personnages;
        }
    }

    // the formations 
    public List<int>[] formations;

    [SerializeField]
    private List<KeyCode> keys = new List<KeyCode>();
    [Header("-1 : no formation")]
    [Range(-1, 9)]
    private int formationSelected = -1;
    [Header("Just watch, don't modify ;)")]
    [SerializeField]
    private bool selecting = false;
    [SerializeField]
    private List<int> selected;
    [SerializeField]
    private List<int> newSelection;

    public int FormationSelected
    {
        get
        {
            return formationSelected;
        }
    }
    public List<int> Selected
    {
        get
        {
            if (selecting)
            {
                List<int> selec = new List<int>();
                
                // complétion de sélection
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    selec.AddRange(selected);
                    foreach (int i in newSelection)
                    {
                        if (selec.Contains(i))
                            selec.Remove(i);
                        else
                            selec.Add(i);
                    }
                }
                else // sélection simple
                {
                    selec.AddRange(newSelection);
                }

                return selec;
            }
            else
                return selected;
        }
    }

    void Start()
    {
        formations = new List<int>[keys.Count];
        newSelection = new List<int>();
        selected = new List<int>();
    }

    void Update()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyUp(keys[i]))
            {
                // si on demande la modification de la formation i
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    formations[i] = new List<int>();
                    formations[i].AddRange(selected);
                }
                // si la formation existe
                if (formations[i] != null)
                {
                    formationSelected = i;
                    selected.Clear();
                    selected.AddRange(formations[i]);
                    newSelected();
                }
            }
        }
    }

    // some public function

    private void clearNewSelection()
    {
        newSelection.Clear();
        selecting = false;
        newSelected();
    }

    public void addToNewSelection(int ind)
    {
        if (!newSelection.Contains(ind))
            newSelection.Add(ind);
        selecting = true;
        newSelected();
    }

    public void removeToNewSelection(int ind)
    {
        if (newSelection.Contains(ind))
            newSelection.Remove(ind);
        selecting = true;
        newSelected();
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

        formationSelected = -1;
        clearNewSelection();
    }

    public void monoSelection(int i)
    {
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

        formationSelected = -1;
        clearNewSelection();
    }

    // abonnement
    private List<IAbonneEM> abonnes;

    void Awake()
    {
        abonnes = new List<IAbonneEM>();
    }
    
    public void abonnement(IAbonneEM abonne)
    {
        abonnes.Add(abonne);
    }

    public void newSelected()
    {
        foreach(IAbonneEM abonne in abonnes)
        {
            abonne.newSelected();
        }
    }
}
