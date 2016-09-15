using UnityEngine;
using System.Collections;


public class randomBoolGenerator : MonoBehaviour
{
    private Animator anim;

    public void Update()
    {
        anim = this.GetComponent<Animator>();
        if (Random.Range(0,2) == 1)
        {
            anim.SetBool("randomBool", true);
        }
        else
        {
            anim.SetBool("randomBool", false);
        }
    }
}
