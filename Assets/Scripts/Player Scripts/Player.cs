using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerInfo
{
	public NetworkInfo networkInfo;
	public Vector3 localPosition;
	public Color color;

	public PlayerInfo(NetworkInfo i_Info, Vector3 i_LocalPosition, Color i_Color)
	{
		this.networkInfo = i_Info;
		localPosition = i_LocalPosition;
		color = i_Color;
	}
}

public class Player : MonoBehaviour
{
	Movement movementM;
	CoinCollection collectibleM;
	ClassicNeuralNetwork brainM;
	DetectWalls detectionM;
	Timer timer;

	public float Fitness { get; set; }
	public void InitSelf()
	{

		timer = GetComponent<Timer>();
		movementM = GetComponent<Movement>();
		movementM.InitSelf();

		collectibleM = GetComponent<CoinCollection>();
		
		brainM = GetComponent<ClassicNeuralNetwork>();
		brainM.InitSelf(6,5,4);
		detectionM = GetComponent<DetectWalls>();
	}

    public void Win()
	{
		print("Avi Won!!!");
	}

	public void Mutate(float i_MutationRate)
	{
		brainM.MutateNetwork(i_MutationRate);
	}


	public void SetMovementBasedOnGuess()
	{

		float[] positions = { transform.localPosition.x, transform.localPosition.y };
		float[] walls = detectionM.DetectWallsIn4Directions();
		List<float> list = new List<float>();
		list.AddRange(positions);
		list.AddRange(walls);
		float[] outputs = brainM.FeedForward(list.ToArray());
		Vector2 guess = new Vector2();
		//Order: UP, DOWN, LEFT, RIGHT
		int highestIndex = 0;
		for(int i = 0; i < outputs.Length; i++)
		{
			if (outputs[i] > outputs[highestIndex])
			{
				highestIndex = i;
			}
		}


		if (highestIndex == 0)
		{
			guess += Vector2.up;
		}

		else if(highestIndex == 1)
		{
			guess += Vector2.down;
		}

		else if (highestIndex == 2)
		{
			guess += Vector2.left;
		}

		else if (highestIndex == 3)
		{
			guess += Vector2.right;
		}

		movementM.SetMovementVector(guess);
		brainM.ResetNetwork();
		timer.Fire((1f/60), SetMovementBasedOnGuess);
	}

	public void Run()
	{
		movementM.canRun = true;

		SetMovementBasedOnGuess();
	}

	public void Stop()
	{
		movementM.canRun = false;
	}

	public void LoadProperties(PlayerInfo i_Info, GameObject i_Room)
	{
		brainM.InitFromNetworkInfo(i_Info.networkInfo);
		GetComponent<SpriteRenderer>().color = i_Info.color;
		transform.parent = i_Room.transform;
		transform.localPosition = new Vector3(-24.74f, 14f, 0);
	}

	public PlayerInfo GetInfo()
	{
		Color color = GetComponent<SpriteRenderer>().color;
		PlayerInfo p = new PlayerInfo(brainM.GetNetworkInfo(), transform.localPosition, color);
		return p;
	}

	public bool HasStopped()
	{
		return movementM.hasStopped;
	}
}
