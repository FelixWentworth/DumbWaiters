using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

	public static Interactable GetNearestInteractable(Vector3 position)
	{
		var interactables = Object.FindObjectsOfType<Interactable>().Where(i => i.InRange(position));
		if (!interactables.Any())
		{
			return null;
		}
		var closest = interactables.First();
		var dist = Vector3.Distance(position, closest.transform.position);

		foreach (var interactable in interactables)
		{
			if (Vector3.Distance(position, interactable.gameObject.transform.position) < dist)
			{
				closest = interactable;
			}
		}
		return closest;
	}
}
