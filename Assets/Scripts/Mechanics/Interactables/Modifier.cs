using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Modifier : Interactable {

	[SerializeField] private float _timeToDestroy;

	[SerializeField] private float _bonusAmount;
	[Range(1, 7)] [SerializeField] private int _range;


	public int Team { get; set; }
	public GameObject RangeGameObject;

	private bool _isPlaced;

	public void Place(int team)
	{
		Team = team;
		_isPlaced = true;
		Interaction = Interactable.InteractionType.Use;
		RangeGameObject.SetActive(true);
		RangeGameObject.transform.localScale = new Vector3(_range, RangeGameObject.transform.localScale.y, _range);
	}

	void Start()
	{
		if (!_isPlaced)
		{
			RangeGameObject.SetActive(false);
		}
	}

	void Update()
	{
		if (_isPlaced)
		{
			ApplyBonusToRange();
		}
	}

	private void ApplyBonusToRange()
	{
		var colliders = Physics.OverlapSphere(transform.position, _range);
		var customers = colliders.Where(c => c.GetComponent<Satisfaction>() != null);

		foreach (var customer in customers)
		{
			customer.GetComponent<Satisfaction>().ApplyEnvironemtChange(this.gameObject, _bonusAmount);
		}
	}

	private void RemoveBonusFromRange()
	{
		var colliders = Physics.OverlapSphere(transform.position, _range);
		var customers = colliders.Where(c => c.GetComponent<Satisfaction>() != null);

		foreach (var customer in customers)
		{
			customer.GetComponent<Satisfaction>().RemoveEnvironmentChange(this.gameObject, -_bonusAmount);
		}
	}

	public override void Drop(Player player)
	{
		base.Drop(player);
		Place(player.Team);
	}

	public override float GetUseTime()
	{
		return _timeToDestroy;
	}

	public override void Use()
	{
		RemoveBonusFromRange();
		Destroy(this.gameObject);
	}

	public override bool CanInteract(int team)
	{
		return Team != team;
	}
}
