using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWalls : MonoBehaviour
{
	public float[] DetectWallsIn4Directions()
	{
		float[] directions = new float[4];

		Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
		RaycastHit2D hit;
		
		for(int i = 0; i < dirs.Length; i++)
		{
			hit = Physics2D.Raycast(transform.localPosition, dirs[i], 1f);
			directions[i] = System.Convert.ToInt32(hit.collider != null);
		}

		float sum = 0;
		foreach(float d in directions)
		{
			sum += d;
		}

		if (sum > 0)
		{
			GetComponent<Player>().Fitness -= 0.1f;
		}

		return directions;
	}
}
