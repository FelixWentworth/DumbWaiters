using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// base class for shared logic between networking and local gameplay
/// </summary>
public class GameManagerBase : MonoBehaviour {

	protected float TimeAvailable = 100;

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

	protected bool PlayGame;
	protected bool ServerCheck = true;

	public int PlayerCount;
	public Dictionary<int, Player> ConnectedPlayers = new Dictionary<int, Player>();

	public GameObject ServerScene;

	private int _winningRep = 100;

	protected virtual void Start()
	{
		SetupGame();
	}

	protected virtual void SetupGame()
	{
		_timeAvailable = TimeAvailable;
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		_scoreManager.Setup(2);

		_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
	}

	public void StartGame()
	{
		PlayGame = true;
		Time.timeScale = 1f;
	}

	protected virtual void Update()
	{
		ServerScene.SetActive(ServerCheck);
		if (PlayGame)
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
				_timeAvailable = 1;
				Time.timeScale = 0f;
				_uiManager.SetState(UIManager.UIState.GameOver).GetComponent<GameOverUI>().Show(_scoreManager.GetTeamMoney(1), _scoreManager.GetTeamMoney(2));
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(0);
			}
		}
	}

	private void DistributeMoney()
	{
		var players = Object.FindObjectsOfType<CommandHandler>();
		foreach (var commandHandler in players)
		{
			commandHandler.RpcGameFinished(_scoreManager.GetTeamTotalMoney(1), _scoreManager.GetTeamTotalMoney(2),
				_scoreManager.GetWinningTeam(), _winningRep);
		}
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

	public virtual void GoToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
