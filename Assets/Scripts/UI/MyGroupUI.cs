using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayGen.SUGAR.Contracts;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.UI;

public class MyGroupUI : MonoBehaviour
{
	public Text NameText;
	public Text DescriptionText;
	public Text MembersText;
	public Text MoneyText;

	public InputField MoneyInputField;

	private int _id;
	private bool _sending;
	private long _requestAmount;

	public void Show()
	{
		var data = GameObject.Find("SUGARUI").GetComponent<SugarUI>().UpdateGroupMoney();

		_id = data.Id;
		NameText.text = data.Name;
		DescriptionText.text = data.Description;
		MembersText.text = data.Members;
		MoneyText.text = "$" + data.Money;
	}

	public void Btn_Take()
	{
		if (MoneyInputField.text == "")
			return;
		_requestAmount = Convert.ToInt64(MoneyInputField.text);
		_sending = false;
		//transfer the money
		SUGARManager.Resource.TryTake(_id, "Money", _requestAmount, TransferSuccess);
	}

	private void TransferSuccess(bool b)
	{
		MoneyInputField.text = "";
		if (b)
		{
			if (_sending)
			{
				SUGARManager.GameData.Send("TipReputation", _requestAmount * 2);
				SUGARManager.GameData.Send("TipMoney", _requestAmount);
				SUGARManager.GameData.Send("NetMoneyGiven", _requestAmount);
			}
			else
			{
				SUGARManager.GameData.Send("GroupMoneyTaken", _requestAmount);
				SUGARManager.GameData.Send("NetMoneyGiven", -_requestAmount);
			}
			Show();
		}
	}

	public void Btn_LeaveTip()
	{
		if (MoneyInputField.text == "")
			return;
		_sending = true;
		_requestAmount = Convert.ToInt64(MoneyInputField.text);
		SUGARManager.Resource.Transfer(_id, "Money", _requestAmount, TransferSuccess);
	}

}
