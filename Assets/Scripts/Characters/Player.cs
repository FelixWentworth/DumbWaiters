using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float MovementSpeed = 1;
	private Movement _movement;
	public GameObject Hands;

	private bool _isHolding { get { return Hands.transform.childCount > 0; } }

	private void Awake()
	{
		_movement = GetComponent<Movement>();
		_movement.MovementSpeed = MovementSpeed;
	}

	void Update()
	{
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");
		if (x != 0 || z != 0)
		{
			Move(x, z);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Interact();
		}
	}

	private void Move(float x, float z)
	{
		_movement.Move(x: x, z: z);
	}

	private void Interact()
	{
		// Get Nearest Interactable
		var nearest = InteractionManager.GetNearestInteractable(transform.position);
		if (nearest != null)
		{
			switch (nearest.GetInteractionType())
			{
				case Interactable.InteractionType.Pickup:
					if (!_isHolding)
					{
						Instantiate(nearest.GetObject(), Hands.transform);
					}
					break;
				case Interactable.InteractionType.Place:
					if (_isHolding && nearest.CanInteract())
					{
						var holdingObj = Hands.transform.GetChild(0).gameObject;
						nearest.PlaceObject(holdingObj);
					}
					break;
				case Interactable.InteractionType.Use:
					break;
			}
		}
	}
}
