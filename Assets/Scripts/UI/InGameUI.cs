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

	public GameObject GameManager;
	public GameObject GameManagerLocal;

	private GameManager _gameManager;
	private GameManagerLocal _gameManagerLocal;

	void Start()
	{
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		_gameManagerLocal = GameManagerLocal.GetComponent<GameManagerLocal>();
	}

	void LateUpdate()
	{
		Team1ScoreText.text = _scoreManager.TextPrefix + _scoreManager.GetTeamTotalMoney(1);
		Team2ScoreText.text = _scoreManager.TextPrefix + _scoreManager.GetTeamTotalMoney(2);
		
		if (_gameManager != null)
		{
			TimeRemainingText.text = _gameManager.TimeRemaining + "s";
		}
		else
		{
			// do this in the update as it may be activated later
			_gameManager = GameManager.GetComponent<GameManager>();
		}
		if (_gameManagerLocal != null)
		{
			TimeRemainingText.text = _gameManagerLocal.TimeRemaining + "s";
		}
	}
}
