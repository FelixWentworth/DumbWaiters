using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the creation of core game elements that set the game up,
/// Also handles transitions to UIs
/// </summary>
public class GameManager : MonoBehaviour
{
	/// <summary>
	/// In Seconds
	/// </summary>
	public float TimeAvailable = 100;

	public Material Team1Material;
	public Material Team2Material;

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

	void Start()
	{
		SetupGame();
	}

	void Update()
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
			Time.timeScale = 0f;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}
		
	}

	private void SetupGame()
	{
		_timeAvailable = TimeAvailable;
		_scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		_scoreManager.Setup(2);

		_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		_uiManager.SetState(UIManager.UIState.Game);
		// Create our player
		Instantiate(PlayerGameObject, Vector3.up, Quaternion.identity).GetComponent<Player>().Set(1, Team1Material);
		Instantiate(PlayerGameObject, Vector3.up, Quaternion.identity).GetComponent<Player>().Set(2, Team2Material);
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
