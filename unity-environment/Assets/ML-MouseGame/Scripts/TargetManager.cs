using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour 
{
	public static TargetManager Instance;

	TargetManager()
	{
		Instance = this;
	}
	
	public int nbTarget = 4;
}
