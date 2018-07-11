using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	public Action<int, int, Vector3> LeaveAction;
	private Movement _movement;
	public float MovementSpeed = 3f;
	public GameObject RequestContainer;
	public GameObject TipGameObject;

	private Seat _seat;
	private Vector3 _startPos;

	private List<FoodConfig.FoodType> _requests;
	public int Team { get; set; }

	private Satisfaction _satisfaction;

	private bool busy;

	public Renderer MoodGameRenderer;
	private MaterialPropertyBlock _propBlock;

	public void MoveToSeat()
	{
		_propBlock = new MaterialPropertyBlock();

		_satisfaction = GetComponent<Satisfaction>();
		_startPos = transform.position;
		_movement = GetComponent<Movement>();
		_movement.MovementSpeed = MovementSpeed;

		_seat = TableManager.GetRandomAvailableSeatForTeam(Team);
		if (_seat != null)
		{
			_seat.IsTaken = true;
			_movement.SetDestination(_seat.transform.position);
		}
	}

	public void SetRequests(List<FoodConfig.FoodType> foodRequests)
	{
		_requests = foodRequests;
	}

	void Update()
	{
		// update color based on satisfaction from http://thomasmountainborn.com/2016/05/25/materialpropertyblocks/
		MoodGameRenderer.GetPropertyBlock(_propBlock);
		_propBlock.SetColor("_Color", _satisfaction.GetSatisfactionColor());
		MoodGameRenderer.SetPropertyBlock(_propBlock);

		if (transform.position == _seat.transform.position && RequestContainer != null && RequestContainer.transform.childCount == 0)
		{
			// Todo move call to a more managed place
			// request the first item
			var requestSprite = GameObject.Find("Foods").GetComponent<FoodConfig>().GetFoodSprite(_requests[0]);
			Instantiate(requestSprite, RequestContainer.transform);
		}

		if (!busy && (int) _seat.PlacedFoodType != 0)
		{
			// NPC has to handle the food given to them
			busy = true;
			if (_seat.PlacedFoodType == _requests[0])
			{
				_satisfaction.FoodSatisfaction += 0.1f;
				StartCoroutine(WaitToLeave());
			}
			else
			{
				_satisfaction.FoodSatisfaction /= 2;
				StartCoroutine(WaitToRemoveFood());
			}
		}

		if (transform.position == _startPos && _requests[0] != FoodConfig.FoodType.None)
		{
			Destroy(this.gameObject);
		}
	}

	IEnumerator WaitToLeave()
	{
		yield return new WaitForSeconds(2.5f);
		_seat.PlacedFoodType = FoodConfig.FoodType.None;
		
		Leave();
	}

	IEnumerator WaitToRemoveFood()
	{
		yield return new WaitForSeconds(1.0f);
		Destroy(_seat.PlacedFood);
		_seat.PlacedFoodType = FoodConfig.FoodType.None;
		busy = false;
	}

	public void Leave()
	{
		Destroy(RequestContainer.transform.gameObject);
		Destroy(_seat.PlacedFood);

		if (LeaveAction != null)
		{
			LeaveAction(Team, _requests.Count, transform.position);
		}
		LeaveTip();
		_movement.SetDestination(_startPos);
		_seat.IsTaken = false;
	}

	private void LeaveTip()
	{
		var go = Instantiate(TipGameObject, _seat.PlacementGameObject.transform).GetComponent<Money>();
		go.Set(_satisfaction.CharacterSatisfaction);
	}

}
