using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour 
{
	[Header("Balancing")]
	float z = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = z;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		transform.position = mousePos;		
	}
}
