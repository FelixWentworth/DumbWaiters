using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{

	public GameObject Team1Tables;
	public GameObject Team2Tables;

	private static List<Table> _team1Tables = new List<Table>();
	private static List<Table> _team2Tables = new List<Table>();


	void Start()
	{
		_team1Tables = Team1Tables.GetComponentsInChildren<Table>().ToList();
		_team2Tables = Team2Tables.GetComponentsInChildren<Table>().ToList();
	}

	public static Seat GetRandomAvailableSeatForTeam(int team)
	{
		var teamtables = team == 1 ? _team1Tables : _team2Tables;
		var tables = teamtables.Where(t => t.GetAvailableSeats() > 0);
		if (!tables.Any())
		{
			return null;
		}

		var rand = UnityEngine.Random.Range(0, tables.Count());
		return tables.ElementAt(rand).AvailableSeat();
	}

	public static Seat GetRandomAvailableSeat()
	{
		var tables = Object.FindObjectsOfType<Table>().Where(t => t.GetAvailableSeats() > 0);
		if (!tables.Any())
		{
			return null;
		}

		var rand = UnityEngine.Random.Range(0, tables.Count());
		return tables.ElementAt(rand).AvailableSeat();
	}
}
