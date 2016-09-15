using UnityEngine;
using System.Collections;


public class randomBoolGenerator : MonoBehaviour
{
    private Animator anim;

    public void Update()
    {
        anim = this.GetComponent<Animator>();
        Debug.Log("fqsd");
        if (Random.Range(0,2) == 1)
        {
            anim.SetBool("randomBool", true);
            Debug.Log("1");
        }
        else
        {
            anim.SetBool("randomBool", false);
            Debug.Log("0");
        }
    }
}
