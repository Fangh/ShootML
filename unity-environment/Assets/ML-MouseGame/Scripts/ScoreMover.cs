using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreMover : MonoBehaviour 
{
	[Header("References")]
	public TextMesh text3D;
	[Header("Private")]
	public TargetAgent myTarget;

	// Use this for initialization
	void Start () 
	{
		transform.DOMoveY(transform.position.y + 1, 1f).OnComplete( () => 
		{
			GetComponent<Renderer>().material.DOFade(0,0.7f).SetEase(Ease.Flash, 9, 1).OnComplete( () => 
			{
				myTarget.GetOut();
			});
		});
	}

	public void SetText(string s)
	{
		text3D.text = s;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
