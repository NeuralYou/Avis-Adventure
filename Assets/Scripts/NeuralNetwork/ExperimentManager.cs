using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	Timer timer;
	bool canRun;
	[SerializeField] GameObject player;
	List<GameObject> rooms;
	GameObject displayRoom;
	List<Vector2> positions;

	private void Start()
	{
		Application.runInBackground = true;
		adjustFrameRate();
		canRun = true;
		rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
		timer = GetComponent<Timer>();
		positions = new List<Vector2>();
		foreach (GameObject g in rooms)
		{
			GameObject pos;
			foreach (Transform t in g.transform)
			{
				if (t.CompareTag("Respawn"))
				{
					pos = t.gameObject;
					positions.Add(pos.transform.position);
				}
			}

			//Setting the display room.
			displayRoom = GameObject.FindGameObjectWithTag("DisplayRoom");

		}

		RunAll();
		//timer.Fire(7f, ApplyGeneticOperators);
	}

	private void RunAll()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			Time.timeScale += 0.4f;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			Time.timeScale -= 1;
		}
	}

	public void ResetRooms()
	{
		foreach(GameObject room in rooms)
		{
			room.GetComponent<Level>().RegenerateContainer();
		}
	}

	private void adjustFrameRate()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}