using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	private Movement _movement;
	public float MovementSpeed = 3f;
	public GameObject RequestContainer;

	private Seat _seat;
	private Vector3 _startPos;

	private FoodConfig.FoodType _requestType1;
	private FoodConfig.FoodType _requestType2;

	void Start()
	{
		_startPos = transform.position;
		_movement = GetComponent<Movement>();
		_movement.MovementSpeed = MovementSpeed;

		_seat = TableManager.GetRandomAvailableSeat(transform.position);
		if (_seat != null)
		{
			_seat.IsTaken = true;
			_movement.SetDestination(_seat.transform.position);
		}
	}

	public void SetRequests(FoodConfig.FoodType drink, FoodConfig.FoodType sugar)
	{
		_requestType1 = drink;
		_requestType2 = sugar;
	}

	void Update()
	{
		if (transform.position == _seat.transform.position)
		{
			// Todo move call to a more managed place
			// request the first item
			var requestSprite = GameObject.Find("Foods").GetComponent<FoodConfig>().GetFoodSprite(_requestType1);
			Instantiate(requestSprite, RequestContainer.transform);
		}

		if ((int) _seat.PlacedFoodType != 0)
		{
			if (_seat.PlacedFoodType == _requestType1)
			{
				StartCoroutine(WaitToLeave());
			}
			else
			{
				StartCoroutine(WaitToRemoveFood());
			}
		}

		if (transform.position == _startPos && _requestType1 != FoodConfig.FoodType.None)
		{
			Destroy(this.gameObject);
		}
	}

	IEnumerator WaitToLeave()
	{
		yield return new WaitForSeconds(2.5f);
		Destroy(_seat.PlacedFood);
		Destroy(RequestContainer.transform.GetChild(0).gameObject);
		Leave();
	}

	IEnumerator WaitToRemoveFood()
	{
		yield return new WaitForSeconds(1.0f);
		Destroy(_seat.PlacedFood);
	}

	public void Leave()
	{
		_movement.SetDestination(_startPos);
		_seat.IsTaken = false;
	}
}
