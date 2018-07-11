using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int Team { get; set; }
	public float MovementSpeed = 1;
	private Movement _movement;
	public GameObject Hands;

	public GameObject SabotagePrefab;
	public GameObject BonusPrefab;

	private GameObject _sabotageItem;
	private GameObject _bonusItem;

	public GameObject ProgressBarParent;
	public GameObject ProgressBar;

	public bool Busy = false;

	private bool _isHolding { get { return Hands.transform.childCount > 0; } }

	private void Awake()
	{
		_movement = GetComponent<Movement>();
		_movement.MovementSpeed = MovementSpeed;
	}

	void Update()
	{
		ProgressBarParent.SetActive(Busy);
		float x, z;
		
		x = Input.GetAxis("Horizontal");
		z = Input.GetAxis("Vertical");

		if (x != 0 || z != 0)
		{
			Move(x, z);
		}
		if (!Busy)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Interact();
			}

			// TODO move to command for server
			if (Input.GetKeyDown(KeyCode.Alpha1) && _sabotageItem == null)
			{
				// Spawn a sabotage Item
				_sabotageItem = Instantiate(SabotagePrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
			}

			if (Input.GetKeyDown(KeyCode.Alpha2) && _bonusItem == null)
			{
				// Spawn a bonus Item
				_bonusItem = Instantiate(BonusPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);

			}
		}


	}

	private void Move(float x, float z)
	{
		Busy = false;
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
				case Interactable.InteractionType.Spawn:
					if (!_isHolding)
					{
						Instantiate(nearest.GetObject(), Hands.transform);
					}
				break;
				case Interactable.InteractionType.Pickup:
					if (!_isHolding)
					{
						nearest.transform.SetParent(Hands.transform);
					}
					else
					{
						// HACK assuming that the object we are holding is the nearest, this may cause issues
						nearest.transform.SetParent(null);
						nearest.Drop(this);
					}
					break;
				case Interactable.InteractionType.Place:
					if (_isHolding && nearest.CanInteract(Team))
					{
						var holdingObj = Hands.transform.GetChild(0).gameObject;
						nearest.PlaceObject(holdingObj);
					}
					break;
				case Interactable.InteractionType.Use:
					if (!_isHolding && !Busy && nearest.CanInteract(Team))
					{
						Busy = true;
						StartCoroutine(WaitToUse(nearest));
					}
					break;
				case Interactable.InteractionType.Take:
					nearest.Take();
					break;
			}
		}
	}

	private IEnumerator WaitToUse(Interactable interactable)
	{
		var current = 0f;
		var requiredTime = interactable.GetUseTime();
		
		while (current < requiredTime && Busy) // players can interrupt busy by moving 
		{
			ProgressBar.transform.localScale = new Vector3(current/requiredTime, ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
			current += Time.deltaTime;
			yield return null;
		}
		if (current > requiredTime)
		{
			Busy = false;
			interactable.Use();
		}
	}
}
