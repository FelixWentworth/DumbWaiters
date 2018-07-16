using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	public float SpriteSliceOverlap = 33;
	public Text WinnerText;

	public Text BlueMoneyText;
	public Text RedMoenyText;

	public RectTransform BlueMoneyBar;
	public RectTransform RedMoneyBar;

	public RectTransform TeamMoneyPanel;

	public Text BlueCustomerValueText;
	public Text RedCustomerValueText;

	public Text BlueTipsValueText;
	public Text RedTipsValueText;

	// Testing
	//void Start()
	//{
	//	var blue = new ScoreManager.TeamMoney(1) {BaseMoney = 1, Tips = 12};
	//	var red = new ScoreManager.TeamMoney(2) {BaseMoney = 38, Tips = 25};

	//	Show(blue, red);
	//}

	public void Show(ScoreManager.TeamMoney blueMoney, ScoreManager.TeamMoney redMoney)
	{
		var blueTotal = blueMoney.BaseMoney + blueMoney.Tips;
		var redTotal = redMoney.BaseMoney + redMoney.Tips;

		var winner = "It's a tie";
		if (redTotal > blueTotal)
			winner = "Red Team Wins";
		if (blueTotal > redTotal)
			winner = "Blue Team Wins";

		WinnerText.text = winner;
		BlueMoneyText.text = "$" + blueTotal;
		RedMoenyText.text = "$" + redTotal;

		BlueCustomerValueText.text = "Bills: $" + blueMoney.BaseMoney;
		RedCustomerValueText.text = "Bills: $" + redMoney.BaseMoney;

		BlueTipsValueText.text = "Tips: $" + blueMoney.Tips;
		RedTipsValueText.text = "Tips: $" + redMoney.Tips;

		// Set the score bars based on a ratio between the teams
		var minBarWidth = TeamMoneyPanel.rect.width * 0.2f;
		var totalMoney = redTotal + blueTotal;
		var flexBarWidth = TeamMoneyPanel.rect.width - (2 * minBarWidth);
		var flexblueWidth = ((float)blueTotal/(float)totalMoney * flexBarWidth);

		var blueWidth = minBarWidth + flexblueWidth;
		var redWidth = minBarWidth + (flexBarWidth - flexblueWidth);

		// blue = offset right
		// red = offset left
		
		BlueMoneyBar.offsetMax = new Vector2(-redWidth + SpriteSliceOverlap, 0);
		RedMoneyBar.offsetMin = new Vector2(blueWidth - SpriteSliceOverlap, 0);
	}

	public void Btn_Menu()
	{
		SceneManager.LoadScene(0);
	}
}
