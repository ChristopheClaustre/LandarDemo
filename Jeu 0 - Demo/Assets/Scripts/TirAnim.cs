using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirAnim : MonoBehaviour
{
	[SerializeField]
	private GameObject Projectile;
	
	void bang ()
	{
		//Instantiate(Projectile,transform.position,transform.rotation);
		Projectile.GetComponent<ParticleSystem>().Emit(1);
	}
}
