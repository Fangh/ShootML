using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			GetComponent<Animator>().SetTrigger("Shoot");
		}
		if (TargetManager.Instance.nbTarget <= 0)
		{
			Debug.Log("YOU WIN");
		}
		
	}

	void Hit()
	{
		Collider[] objTouched = Physics.OverlapSphere(transform.position, 0.3f);
		for (int i = 0; i < objTouched.Length; i++)
		{
			objTouched[i].GetComponent<TargetAgent>().Hit();
		}
	}
}
