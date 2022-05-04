using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Player : MonoBehaviour
{
	Movement movementM;
	CoinCollection collectibleM;
	DetectWalls detectionM;
	Timer timer;

	public float Fitness { get; set; }
	public void InitSelf()
	{

		timer = GetComponent<Timer>();
		movementM = GetComponent<Movement>();
		movementM.InitSelf();

		collectibleM = GetComponent<CoinCollection>();
		
		detectionM = GetComponent<DetectWalls>();
	}

    public void Win()
	{
		print("Avi Won!!!");
	}


	public void LoadProperties(GameObject i_Room)
	{
		transform.parent = i_Room.transform;
		transform.localPosition = new Vector3(-24.74f, 14f, 0);
	}

	public bool HasStopped()
	{
		return movementM.hasStopped;
	}
}
