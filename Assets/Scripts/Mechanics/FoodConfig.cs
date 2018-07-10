using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodConfig : MonoBehaviour {

	public enum FoodType
	{
		None = 0,
		SugarBrown,
		SugarWhite,
		WaterBrown,
		WaterWhite
	}

	[Serializable]
	public struct FoodObjectMapping
	{
		public FoodType FoodType;
		public GameObject GameObject;
		public GameObject SpriteGameObject;
	}

	[Serializable]
	public class FoodObjects
	{
		public List<FoodObjectMapping> Foods = new List<FoodObjectMapping>
		{
			new FoodObjectMapping{FoodType = FoodType.SugarBrown},
			new FoodObjectMapping{FoodType = FoodType.SugarWhite},
			new FoodObjectMapping{FoodType = FoodType.WaterBrown},
			new FoodObjectMapping{FoodType = FoodType.WaterWhite}
		};
	}

	[SerializeField] private FoodObjects _foodObjects;

	public GameObject GetFoodObject(FoodType type)
	{
		if (type == FoodType.None)
			return null;
		return _foodObjects.Foods.First(f => f.FoodType == type).GameObject;
	}

	public GameObject GetFoodSprite(FoodType type)
	{
		if (type == FoodType.None)
			return null;
		return _foodObjects.Foods.First(f => f.FoodType == type).SpriteGameObject;
	}
}
