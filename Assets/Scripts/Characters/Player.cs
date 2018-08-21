using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
	public int ID;
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

	public List<Renderer> TeamDependentRenderers;

	public Material Team1Material;
	public Material Team2Material;


	public bool Busy = false;

	private bool _isHolding { get { return Hands.transform.childCount > 0; } }

	private void Awake()
	{
		_movement = GetComponent<Movement>();
		_movement.MovementSpeed = MovementSpeed;
	}

	public void SetColours()
	{
		var material = Team == 1 ? Team1Material : Team2Material;
		foreach (var teamDependentRenderer in TeamDependentRenderers)
		{
			teamDependentRenderer.material = material;
		}
	}

	void Update()
	{
		ProgressBarParent.SetActive(Busy);
		float x = 0f;
		float z = 0f;
		//	x = Input.GetAxis("Horizontal");
		//	z = Input.GetAxis("Vertical");

		//	if (x != 0 || z != 0)
		//	{
		//		Move(x, z);
		//	}
		//if (!Busy)
		//{
		//	if (Input.GetKeyDown(KeyCode.Space))
		//	{
		//		Interact();
		//	}

		//	 TODO move to command for server
		//	if (Input.GetKeyDown(KeyCode.Alpha1) && _sabotageItem == null)
		//	{
		//		 Spawn a sabotage Item
		//		_sabotageItem = Instantiate(SabotagePrefab, new Vector3(transform.position.x, 0, transform.position.z),
		//			Quaternion.identity);
		//	}

		//	if (Input.GetKeyDown(KeyCode.Alpha2) && _bonusItem == null)
		//	{
		//		 Spawn a bonus Item
		//		_bonusItem = Instantiate(BonusPrefab, new Vector3(transform.position.x, 0, transform.position.z),
		//			Quaternion.identity);

		//	}
		//}

		if (Team == 1)
		{
			if (Input.GetKey(KeyCode.A))
			{
				x = -1;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				x = 1;
			}
			if (Input.GetKey(KeyCode.W))
			{
				z = 1;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				z = -1;
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Interact();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1) && _sabotageItem == null)
			{
				// Spawn a sabotage Item
				_sabotageItem = Instantiate(SabotagePrefab, new Vector3(transform.position.x, 0, transform.position.z),
					Quaternion.identity);
			}

			if (Input.GetKeyDown(KeyCode.Alpha2) && _bonusItem == null)
			{
				// Spawn a bonus Item
				_bonusItem = Instantiate(BonusPrefab, new Vector3(transform.position.x, 0, transform.position.z),
					Quaternion.identity);

			}
		}
		if (Team == 2)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				x = -1;
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				x = 1;
			}
			if (Input.GetKey(KeyCode.UpArrow))
			{
				z = 1;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				z = -1;
			}
			if (Input.GetKeyDown(KeyCode.Keypad0))
			{
				Interact();
			}
			if (Input.GetKeyDown(KeyCode.Keypad1) && _sabotageItem == null)
			{
				// Spawn a sabotage Item
				_sabotageItem = Instantiate(SabotagePrefab, new Vector3(transform.position.x, 0, transform.position.z),
					Quaternion.identity);
			}

			if (Input.GetKeyDown(KeyCode.Keypad2) && _bonusItem == null)
			{
				// Spawn a bonus Item
				_bonusItem = Instantiate(BonusPrefab, new Vector3(transform.position.x, 0, transform.position.z),
					Quaternion.identity);

			}
		}
		
		if (x != 0 || z != 0)
		{
			Move(x, z);
		}
	}

	public void Move(float x, float z)
	{
		Busy = false;
		_movement.Move(x: x, z: z);
	}

	public void Interact()
	{
		// Get Nearest Interactable
		var nearest = InteractionManager.GetNearestInteractable(transform.position);
		if (nearest != null)
		{
			Debug.Log(nearest);
			GameObject holdingObj;

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
						
						nearest.Drop(this);
					}
					break;
				case Interactable.InteractionType.Place:
					if (_isHolding && nearest.CanInteract(Team))
					{
						holdingObj = Hands.transform.GetChild(0).gameObject;
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
					nearest.Take(Team);
					break;
				case Interactable.InteractionType.Trash:
					if (_isHolding)
					{
						holdingObj = Hands.transform.GetChild(0).gameObject;
						nearest.Use(this, holdingObj);
					}
					break;
			}
		}
	}

	public void Hit()
	{
		_movement.MovementSpeed = 0f;
		_movement.CanMove = false;
		StartCoroutine(PlayHitAnim());
	}

	private IEnumerator PlayHitAnim()
	{
		var anim = GetComponent<Animation>();
		anim.Play();
		while (anim.isPlaying)
		{
			yield return null;
		}
		_movement.CanMove = true;
		_movement.MovementSpeed = MovementSpeed;
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
