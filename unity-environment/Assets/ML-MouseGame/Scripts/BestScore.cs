using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour 
{
	MouseGameAgent[] agents;
	float currentBestScore = 0f;

	// Use this for initialization
	void Start () 
	{
		agents = GameObject.FindObjectsOfType<MouseGameAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < agents.Length; i++)
		{
			float r = agents[i].CumulativeReward;
			if ( r > currentBestScore )
			{
				GetComponent<Text>().text = r.ToString("F2");
				currentBestScore = r;
			}
		}		
	}
}
