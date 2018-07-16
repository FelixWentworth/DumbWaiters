using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public int BasePayPerItem;
	public int BaseTipValue;

	public GameObject MoneyPickupGameObject;
	public string TextPrefix = "$";

	public Color Team1Color;
	public Color Team2Color;

	class TeamMoney
	{
		public int Team;
		public int BaseMoney { get; set; }
		public int Tips { get; set; }

		public TeamMoney(int team)
		{
			Team = team;
		}
	}

	private readonly List<TeamMoney> _teamMoney = new List<TeamMoney>();

	public void Setup(int teamCount)
	{
		for (var i = 0; i < teamCount; i++)
		{
			_teamMoney.Add(new TeamMoney(i +1));
		}
	}

	private TeamMoney GetTeamMoney(int team)
	{
		return _teamMoney.First(t => t.Team == team);
	}

	public int GetTeamTotalMoney(int team)
	{
		if (!_teamMoney.Any())
			return 0;
		var teamMoney = _teamMoney.First(t => t.Team == team);
		return teamMoney.BaseMoney + teamMoney.Tips;
	}

	public void CustomerLeft(int team, int requestsCount, Vector3 location)
	{
		var teamMoney = _teamMoney.First(t => t.Team == team);
		if (teamMoney != null)
		{
			var increment = requestsCount * BasePayPerItem;
			teamMoney.BaseMoney += increment;
			ShowMoney(increment, location, GetTeamColor(team));
		}
	}

	public void TipPickedUp(int team, float satisfaction, Vector3 location)
	{
		var teamMoney = GetTeamMoney(team);
		var increment = Mathf.RoundToInt(satisfaction * BaseTipValue);
		teamMoney.Tips += increment;
		ShowMoney(increment, location, GetTeamColor(team));
	}

	private void ShowMoney(int amount, Vector3 location, Color teamColor)
	{
		var go = Instantiate(MoneyPickupGameObject, location + Vector3.up, Quaternion.identity);
		go.GetComponentInChildren<TextMesh>().text = TextPrefix + amount;
		go.GetComponentInChildren<TextMesh>().color = teamColor;
		StartCoroutine(DestroyAfterAnimation(go));
	}

	IEnumerator DestroyAfterAnimation(GameObject go)
	{
		var anim = go.GetComponentInChildren<Animation>();
		while (anim.isPlaying)
		{
			yield return null;
		}
		Destroy(go);
	}

	private Color GetTeamColor(int team)
	{
		return team == 1 ? Team1Color : Team2Color;
	}
}
