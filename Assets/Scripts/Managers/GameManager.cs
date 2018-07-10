using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the creation of core game elements that set the game up,
/// Also handles transitions to UIs
/// </summary>
public class GameManager : MonoBehaviour
{
	public GameObject CustomerSpawnPoint;
	public GameObject CustomerGameObject;

	private float _current = 0f;
	private float _Interval = 3f;

	
	IEnumerator Start()
	{
		while (true)
		{
			if (_current <= 0f)
			{
				if (TableManager.GetRandomAvailableSeat(transform.position) != null)
				{
					// Check there is a seat available before instantiating
					var customer = Instantiate(CustomerGameObject, CustomerSpawnPoint.transform);
					customer.GetComponent<NPC>().SetRequests(GetRandomDrink(), GetRandomFood());
				}
				_current = _Interval;
			}

			_current -= Time.deltaTime;
			yield return null;
		}
	}

	public static FoodConfig.FoodType GetRandomDrink()
	{
		var rand = UnityEngine.Random.Range(3, 5);
		return (FoodConfig.FoodType)rand;
	}

	public static FoodConfig.FoodType GetRandomFood()
	{
		var rand = UnityEngine.Random.Range(1, 3);
		return (FoodConfig.FoodType)rand;
	}
}
