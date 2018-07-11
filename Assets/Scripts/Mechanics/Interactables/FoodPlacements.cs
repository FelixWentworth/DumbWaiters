using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlacements : Interactable
{
	public Seat Seat;

	private bool _objectPlaced { get { return transform.childCount > 0; } }

	public override bool CanInteract(int team)
	{
		return !_objectPlaced;
	}

	public override void PlaceObject(GameObject obj)
	{
		base.PlaceObject(obj);

		if (obj.GetComponent<HACK_FOOD_CONFIG>() != null)
		{
			Seat.PlacedFoodType = obj.GetComponent<HACK_FOOD_CONFIG>().FoodType;
		}
		Seat.PlacedFood = obj;
	}
}
