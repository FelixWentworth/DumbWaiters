using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Interactable
{
	private float _satisfaction;
	private int _team;
	private ScoreManager _scoreManager;

	public void Set(int team, float customerSatisfaction)
	{
		_team = team;
		_satisfaction = customerSatisfaction;
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}

	public override void Take()
	{
		base.Take();
		_scoreManager.TipPickedUp(_team, _satisfaction, transform.position);
	}
}
