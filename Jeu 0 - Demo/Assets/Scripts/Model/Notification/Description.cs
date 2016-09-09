using UnityEngine;
using System.Collections;

public class Description {

    private string nom;
    private string resume;

    public string Nom
    {
        get
        {
            return nom;
        }
    }
    public string Resume
    {
        get
        {
            return resume;
        }
    }

    // const
    public Description(string _n, string _r)
    {
        nom = _n;
        resume = _r;
    }
    
}
