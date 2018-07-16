﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the creation of core game elements that set the game up,
/// Also handles transitions to UIs
/// </summary>
public class GameManagerLocal : MonoBehaviour
{
	/// <summary>
	/// In Seconds
	/// </summary>
	public float TimeAvailable = 100;

	public GameObject PlayerGameObject;

	public GameObject CustomerSpawnPoint;
	public GameObject CustomerGameObject;

	private float _current = 0f;
	private float _Interval = 3f;

	private int currentTeam = 1;

	private ScoreManager _scoreManager;
	private UIManager _uiManager;

	private float _timeAvailable;
	public int TimeRemaining { get { return Mathf.CeilToInt(_timeAvailable); } }

	private bool _startGame;

	public int PlayerCount;
	public Dictionary<int, Player> ConnectedPlayers = new Dictionary<int, Player>();

	void Start()
	{
		SetupGame();
	}

	void Update()
	{
		if (_startGame)
		{
			if (_current <= 0f)
			{
				if (TableManager.GetRandomAvailableSeatForTeam(currentTeam) != null)
				{
					// Check there is a seat available before instantiating
					var customer = Instantiate(CustomerGameObject, CustomerSpawnPoint.transform).GetComponent<NPC>();
					customer.SetRequests(GetRandomFoodList());
					customer.Team = currentTeam;
					customer.LeaveAction = _scoreManager.CustomerLeft;

					customer.MoveToSeat();

				}
				currentTeam = currentTeam != 1 ? 1 : 2;

				_current = _Interval;
			}

			_current -= Time.deltaTime;
			_timeAvailable -= Time.deltaTime;

			if (_timeAvailable <= 0f)
			{
				// HACK stop gameplay
				_uiManager.SetState(UIManager.UIState.GameOver).GetComponent<GameOverUI>().Show(_scoreManager.GetTeamMoney(1), _scoreManager.GetTeamMoney(2));
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(0);
			}
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			_startGame = true;
			Time.timeScale = 1f;
		}
	}

	private void SetupGame()
	{
		_timeAvailable = TimeAvailable;
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		_scoreManager.Setup(2);

		_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		_uiManager.SetState(UIManager.UIState.Game);	
		// Create our players
		var p1 = Instantiate(PlayerGameObject, Vector3.up, Quaternion.identity).GetComponent<Player>();
		p1.Team = 1;
		p1.SetColours();

		var p2 = Instantiate(PlayerGameObject, Vector3.up, Quaternion.identity).GetComponent<Player>();
		p2.Team = 2;
		p2.SetColours();
	}

	public static FoodConfig.FoodType GetRandomFood()
	{
		var rand = UnityEngine.Random.Range(1, 5);
		return (FoodConfig.FoodType)rand;
	}

	public static List<FoodConfig.FoodType> GetRandomFoodList()
	{
		var list = new List<FoodConfig.FoodType>();
		list.Add(GetRandomFood());

		return list;
	}
}
