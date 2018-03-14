using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseGameAgent : Agent 
{
	[Header("References")]
	public Transform mouseObject;
	public TextMesh myText;
	public Text nbTry;

	[Header("Private")]
	private float bestCumulativeReward = 0;
	private float originalZ = 0f;

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
		float action_horizontal = act[0];
		float action_vertical = act[1];

		// Debug.Log("action_horizontal = " + action_horizontal);
		// Debug.Log("action_vertical = " + action_vertical);

		Vector3 nextPos = transform.position + new Vector3( action_horizontal * Time.deltaTime,  action_vertical * Time.deltaTime, originalZ);

		// nextPos = Camera.main.WorldToScreenPoint(nextPos);

		// Debug.Log("nextPos = " + nextPos);
		// Debug.Log("Screen.width = " + Screen.width);
		// Debug.Log("Screen.height = " + Screen.height);

		if ( nextPos.x > -10 && nextPos.x < 10 )
		{
			transform.Translate( Vector3.right * action_horizontal * Time.deltaTime );
		}

		if ( nextPos.y > -5 && nextPos.y < 5 )
		{
			transform.Translate( Vector3.up * action_vertical * Time.deltaTime );
		}

		if ( Vector3.Distance(mouseObject.position, transform.position) < 1f)
		{
			reward -= 1f;
			done = true;
		}
		else
		{
			reward += 0.01f;
		}

		// Debug.Log("stepCounter = " + stepCounter);
		// Debug.Log("maxStep = " + maxStep);

		if (stepCounter >= maxStep)
		{
			reward += 5f;
			done = true;
		}

		myText.text = CumulativeReward.ToString("F2");
		// Debug.Log("academy.episodeCount = " + academy.episodeCount);

		// Debug.Log("mousePos = " + mouseObject.position );
	}

	public override void AgentReset()
	{
		if ( null != mouseObject.GetComponent<DeterministMouse>() )
			mouseObject.GetComponent<DeterministMouse>().Init();

		transform.position = new Vector3( Random.Range(-10f, 10f), Random.Range(-5f,5f), originalZ);
		nbTry.text = (int.Parse(nbTry.text) + 1).ToString();
	}

	public override void AgentOnDone()
	{

	}
}
