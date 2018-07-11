using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Interactable
{
	private float _satisfaction;
	private ScoreManager _scoreManager;

	public void Set(float customerSatisfaction)
	{
		_satisfaction = customerSatisfaction;
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}

	public override void Take(int team)
	{
		base.Take(team);
		_scoreManager.TipPickedUp(team, _satisfaction, transform.position);
	}
}
