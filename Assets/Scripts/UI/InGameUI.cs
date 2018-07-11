using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	public Text Team1ScoreText;
	public Text Team2ScoreText;

	public Text TimeRemainingText;

	private ScoreManager _scoreManager;
	private GameManager _gameManager;

	void Start()
	{
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void LateUpdate()
	{
		Team1ScoreText.text = _scoreManager.TextPrefix + _scoreManager.GetTeamTotalMoney(1);
		Team2ScoreText.text = _scoreManager.TextPrefix + _scoreManager.GetTeamTotalMoney(2);
		TimeRemainingText.text = _gameManager.TimeRemaining + "s";
	}
}
