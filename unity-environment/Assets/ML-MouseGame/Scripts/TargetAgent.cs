using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TargetAgent : Agent 
{
	[Header("References")]
	public Transform mouseObject;
	public GameObject hole;
	public GameObject bloodPrefab;
	public GameObject targetPrefab;

	[Header("Balancing")]
	public float speed = 1f;

	[Header("Private")]
	private float originalZ = 0f;
	private bool isTouched = false;
	private int score = 0;
	private bool isBreeding = false;
	private bool canBreed = false;

	void Start()
	{
		originalZ = transform.position.z;
		Invoke("CanBreed", 2f);
	}

	void CanBreed()
	{
		canBreed = true;
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
		Vector3 nextPos = transform.position + new Vector3( action_horizontal * Time.deltaTime * speed,  action_vertical * Time.deltaTime * speed, originalZ);

		if ( nextPos.x > -9.8f && nextPos.x < 9.8f )
		{
			transform.Translate( Vector3.right * action_horizontal * Time.deltaTime * speed );
		}
		if ( nextPos.y > -4.8f && nextPos.y < 4.8f )
		{
			transform.Translate( Vector3.up * action_vertical * Time.deltaTime * speed );
		}
	}

	public override void AgentReset()
	{

	}

	public override void AgentOnDone()
	{

	}

	void StopBreeding()
	{
		isBreeding = false;	
		canBreed = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag  == "agent")
		{
			TargetAgent t = other.GetComponent<TargetAgent>();
			if ( !t.isBreeding && canBreed && t.canBreed && TargetManager.Instance.nbTarget < 50 )
			{
				TargetManager.Instance.nbTarget++;
				isBreeding = true;
				canBreed = false;
				Invoke("StopBreeding", 2f);
				GameObject o = Instantiate(targetPrefab, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0), Quaternion.identity);
				o.transform.localScale = Vector3.zero;
				o.GetComponent<TargetAgent>().transform.DOScale(1, 2f);
				o.GetComponent<TargetAgent>().mouseObject = mouseObject;
				o.GetComponent<TargetAgent>().GiveBrain(brain);
				if ( !(o.transform.position.x > -9.8f
					&& o.transform.position.x < -9.8f
					&& o.transform.position.y > -4.8f
					&& o.transform.position.y < 4.8f)  )
					{
						o.transform.position = new Vector3(0,0,originalZ);
					}
			}
		}
	}

	public void Hit()
	{
		isTouched = true;
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		GameObject o = Instantiate(bloodPrefab, transform.position, Quaternion.Euler(0,0,Random.Range(0f,360f)));	
		GetComponent<Collider>().enabled = false;
		TargetManager.Instance.nbTarget--;
	}

	public void GetOut()
	{
		hole.SetActive(false);
		transform.DOMoveY(-10, 0.7f).SetEase(Ease.InBack);
	}
}
