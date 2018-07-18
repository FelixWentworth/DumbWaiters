using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayGen.SUGAR.Common;
using PlayGen.SUGAR.Contracts;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.UI;

public class MyProfileUI : MonoBehaviour
{
	public Text HomeUsername;
	public Text HomeMoney;

	public Text Username;
	public Text Money;

	public Text Reputation;
	public Text Level;
	public Text GamesWon;
	public Text GamesCompleted;
	public Text TipsDonated;

	void OnEnable()
	{
		GetGameData();
	}
	void Update()
	{
		Username.text = HomeUsername.text;
		Money.text = HomeMoney.text;


	}

	private void GetGameData()
	{
		SUGARManager.GameData.Get(Success, new []{"TotalReputation", "GameWin", "GameFinished", "TipMoney"});
	}

	private void Success(IEnumerable<EvaluationDataResponse> evaluationDataResponses)
	{
		var dataResponses = evaluationDataResponses as EvaluationDataResponse[] ?? evaluationDataResponses.ToArray();

		Reputation.text = "" +  dataResponses.Where(e => e.Key == "TotalReputation").Select(r => r.Value).Sum(s => Convert.ToInt64(s));
		GamesWon.text = "" + dataResponses.Where(e => e.Key == "GameWin").Select(r => r.Value).Sum(s => Convert.ToInt64(s));
		GamesCompleted.text = "" + dataResponses.Where(e => e.Key == "GameFinished").Select(r => r.Value).Sum(s => Convert.ToInt64(s));
		TipsDonated.text = "$" + dataResponses.Where(e => e.Key == "TipMoney").Select(r => r.Value).Sum(s => Convert.ToInt64(s));

		// TODO get level
		Level.text = "1";
	}
}
