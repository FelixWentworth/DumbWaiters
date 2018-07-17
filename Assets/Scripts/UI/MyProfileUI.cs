using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyProfileUI : MonoBehaviour
{
	public Text HomeUsername;
	public Text HomeMoney;

	public Text Username;
	public Text Money;

	public Text Reputation;
	public Text GamesWon;

	void Update()
	{
		Username.text = HomeUsername.text;
		Money.text = HomeMoney.text;
	}
}
