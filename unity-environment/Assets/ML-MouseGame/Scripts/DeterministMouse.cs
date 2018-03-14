using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeterministMouse : MonoBehaviour 
{
	[Header("References")]
	public Transform target;

	[Header("Balancing")]
	public float speed = 3;


	[Header("Private")]
	Vector3 targetPos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		if (null == target)
			target = GameObject.FindObjectOfType<MouseGameAgent>().transform;

		Init();
		NextTarget();		
	}

	public void Init()
	{
		transform.localPosition = new Vector3( Random.Range(-10f, 10f), Random.Range(-5f,5f), 0);
		speed = Random.Range(5f,20f);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if ( Vector3.Distance(transform.position, targetPos) < 0.5f)
			NextTarget();
		else
		{
			Vector3 dir = targetPos - transform.position;
			transform.Translate( dir.normalized * speed * Time.deltaTime );
		}
		
	}

	void NextTarget()
	{
		targetPos = target.position + new Vector3 ( Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
	}
}
