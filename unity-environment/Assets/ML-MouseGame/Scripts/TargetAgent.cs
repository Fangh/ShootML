using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetAgent : Agent 
{
	[Header("References")]
	public Transform mouseObject;
	public GameObject hole;

	[Header("Private")]
	private float bestCumulativeReward = 0;
	private float originalZ = 0f;
	private bool isTouched = false;

	void Start()
	{
		originalZ = transform.position.z;
	}

	public override List<float> CollectState()
	{
		List<float> state = new List<float>();
		state.Add(transform.position.x);
		state.Add(transform.position.y);
		state.Add(transform.position.x - mouseObject.transform.position.x);
		state.Add(transform.position.y - mouseObject.transform.position.y);

		return state;
	}

	public override void AgentStep(float[] act)
	{
		if ( isTouched )
			return;
		
		float action_horizontal = act[0];
		float action_vertical = act[1];
		Vector3 nextPos = transform.position + new Vector3( action_horizontal * Time.deltaTime,  action_vertical * Time.deltaTime, originalZ);

		if ( nextPos.x > -10 && nextPos.x < 10 )
		{
			transform.Translate( Vector3.right * action_horizontal * Time.deltaTime );
		}

		if ( nextPos.y > -5 && nextPos.y < 5 )
		{
			transform.Translate( Vector3.up * action_vertical * Time.deltaTime );
		}
	}

	public override void AgentReset()
	{

	}

	public override void AgentOnDone()
	{

	}

	void OnMouseDown()
	{
		isTouched = true;
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;
		hole.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
		hole.SetActive(true);

	}
}
