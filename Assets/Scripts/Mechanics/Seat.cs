using UnityEngine;

public class Seat : MonoBehaviour
{
	public bool IsTaken { get; set; }

	public GameObject PlacedFood { get; set; }

	public FoodConfig.FoodType PlacedFoodType { get; set; }

	// TODO move this cos its some sort of circle jerk at the moment
	public GameObject PlacementGameObject;

	void Update()
	{
		if (!IsTaken)
		{
			if (transform.childCount > 0)
			{
				var child = transform.GetChild(0);
				Destroy(child);
			}
		}
	}
}
