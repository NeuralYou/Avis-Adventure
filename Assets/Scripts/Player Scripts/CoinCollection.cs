using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Coin"))
		{
			GetComponent<Player>().Fitness += 1;
			//GetComponent<CoinTracker>().LocateNearestCoin();
			Destroy(collision.gameObject);
		}
	}

}
