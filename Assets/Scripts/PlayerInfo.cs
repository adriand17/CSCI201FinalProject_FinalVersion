using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfo
{

	public PlayerInfo(Vector3 pos, string Id, double state)
	{
		Position = new double[3];
		Position[0] = pos.x;
		Position[1] = pos.y;
		Position[2] = pos.z;
		PlayerId = Id;
		PlayerState = state;
	}

	public double[] Position;
	public string PlayerId;
	public double PlayerState;
	//public bool PlayerKilled {get;set;}
}
