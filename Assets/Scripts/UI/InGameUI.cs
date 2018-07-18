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

	[SerializeField]
	private GameManagerBase _gameManager;

	public GameObject StartGameButton;

	void Start()
	{
		StartGameButton.SetActive(true);
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}

	void LateUpdate()
	{
		Team1ScoreText.text = _scoreManager.TextPrefix + _scoreManager.GetTeamTotalMoney(1);
		Team2ScoreText.text = _scoreManager.TextPrefix + _scoreManager.GetTeamTotalMoney(2);
		
		if (_gameManager != null)
		{
			TimeRemainingText.text = _gameManager.TimeRemaining + "s";
		}
	}

	public void Btn_StartGame()
	{
		if (_scoreManager != null)
		{
			_gameManager.StartGame();
			StartGameButton.SetActive(false);
		}
	}
}
