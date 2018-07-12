using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public enum InteractionType
	{
		Pickup,
		Spawn,
		Place,
		Use,
		Take,
		Trash
	}

	[SerializeField]
	protected InteractionType Interaction;

	[SerializeField]
	protected float InteractableRange = 0.5f;

	public bool InRange(Vector3 position)
	{
		return Vector3.Distance(transform.position, position) < InteractableRange;
	}

	public InteractionType GetInteractionType()
	{
		return Interaction;
	}

	public virtual GameObject GetObject()
	{
		return null;
	}

	public virtual Transform GetPlacePosition()
	{
		return transform;
	}

	public virtual float GetUseTime()
	{
		return 0f;
	}

	public virtual bool CanInteract(int team)
	{
		return true;
	}

	public virtual void Drop(Player player)
	{
		transform.SetParent(null);
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

	public virtual void Use()
	{
		
	}

	// used for trash cans
	public virtual void Use(Player player, GameObject gameObject)
	{
		Destroy(gameObject);
	}

	public virtual void Take(int team)
	{
		Destroy(gameObject);
	}

	public virtual void PlaceObject(GameObject obj)
	{
		var placePosition = transform.position;
		var placeParent = transform;

		// setting position separately to prevent funny scaling
		obj.transform.SetParent(placeParent, true);
		obj.transform.position = placePosition;
	}
}
