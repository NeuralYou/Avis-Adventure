using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	Population population;
	Timer timer;
	int currentIndex;
	bool canRun;
	[SerializeField] int populationSize;
	[SerializeField] GameObject player;
	List<GameObject> rooms;
	GameObject displayRoom;
	List<Vector2> positions;
	int generationCounter = 1;



	private void Start()
	{
		Application.runInBackground = true;
		adjustFrameRate();
		canRun = true;
		rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
		timer = GetComponent<Timer>();
		currentIndex = 0;
		population = new Population(0.2f, rooms.Count, player);
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

		population.altInitPopulation(positions.ToArray(), rooms.ToArray());

		RunAll();
		timer.Fire(7f, ApplyGeneticOperators);

	}

	private void RunAll()
	{
		for (int i = 0; i < population.Size(); i++)
		{
			population[i].Run();
		}
	}

	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.U))
		{
			Time.timeScale += 0.4f;
			print(Time.timeScale);
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			Time.timeScale -= 1;
		}

		if (canRun)
		{
			for (int i = 0; i < population.Size(); i++)
			{
				if(!population[i].HasStopped())
				{
					return;
				}
			}

			canRun = false;
			timer.Stop();
			ApplyGeneticOperators();
		}
	}


	public void ApplyGeneticOperators()
	{
		playbackBestElement();
		population.CrossOver();
		population.RegeneratePopulation();
		population.Mutate();
		ResetRooms();

		canRun = true;
		RunAll();
		timer.Fire(5f, ApplyGeneticOperators);
	}

	private void playbackBestElement()
	{
		Player p = population.GetFittest();
		print($"Best in generation {generationCounter++} is: {p.Fitness}");
		float time = Time.realtimeSinceStartup;
		float current = time;
		Player i = Instantiate(player).GetComponent<Player>();
		displayRoom.GetComponent<Level>().RegenerateContainer();
		i.InitSelf();
		i.LoadProperties(p.GetInfo(), displayRoom);
		i.Run();
		StartCoroutine(co());
		IEnumerator co()
		{
			while (current < time + 5)
			{
				current = Time.realtimeSinceStartup;
				yield return null;
			}
			Destroy(i.gameObject);

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
//public bool isGenerationTested()
//{
//	if (currentIndex < population.Size())
//	{
//		return false;
//	}

//	else
//	{
//		return true;
//	}
//}


//public void RunSingleGeneration()
//{
//	Player temp = population[currentIndex++];
//	temp.Stop();
//	print(population[currentIndex - 1].GetInstanceID() + " Stopped");
//	if (!isGenerationTested())
//	{
//		population[currentIndex].Run();
//		timer.Fire(3, RunSingleGeneration);
//	}

//	else
//	{
//		ApplyGeneticOperators();
//	}

//}