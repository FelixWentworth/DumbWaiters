using UnityEngine;

public class FoodContainer : Interactable
{
	public FoodConfig.FoodType Type;
	private FoodConfig _foodConfig;

	[SerializeField]
	private GameObject _spriteParent;

	void Start()
	{
		_foodConfig = GetComponentInParent<FoodConfig>();

		var sprite = _foodConfig.GetFoodSprite(Type);
		Instantiate(sprite, _spriteParent.transform);
	}

	private GameObject RetrieveFood()
	{
		var go = _foodConfig.GetFoodObject(Type);
		go.GetComponent<HACK_FOOD_CONFIG>().FoodType = Type;
		return go;
	}

	public override GameObject GetObject()
	{
		return RetrieveFood();
	}
}
