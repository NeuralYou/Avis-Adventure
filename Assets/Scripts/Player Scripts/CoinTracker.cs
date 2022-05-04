using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTracker : MonoBehaviour
{

	private void Update()
	{
		LocateNearestCoin();	
	}
	public Vector2 LocateNearestCoin()
	{
		Transform container = null;
		GameObject closest = null;
		foreach(Transform t in transform.parent)
		{
			if (t.CompareTag("CoinContainer"))
			{
				container = t;
				break;
			}
		}

		closest = container.GetComponentsInChildren<Transform>()[1].gameObject;
		foreach(Transform t in container)
		{
			if ((transform.localPosition - t.localPosition).magnitude < (transform.localPosition - closest.transform.localPosition).magnitude)
			{
				closest = t.gameObject;
			}
		}

		return (transform.localPosition - closest.transform.position);
	}
}
