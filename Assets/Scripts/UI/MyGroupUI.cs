using System;
using System.Collections;
using System.Collections.Generic;
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
	private long _requestAmount;

	public void Show(SugarUI.GroupData data)
	{
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
		var currentUserId = SUGARManager.CurrentUser.Id;

		//transfer the money
		//SUGARManager.Resource.Add("Money", _requestAmount, AddSuccess);
	}

	private void TransferSuccess(bool b)
	{
		MoneyInputField.text = "";
	}

	private void AddSuccess(bool b)
	{
		// TODO update ui
	}

	public void Btn_LeaveTip()
	{
		if (MoneyInputField.text == "")
			return;
		_requestAmount = Convert.ToInt64(MoneyInputField.text);
		SUGARManager.Resource.Transfer(_id, "Money", _requestAmount, TransferSuccess);
		SUGARManager.GameData.Send("TipReputation", _requestAmount * 2);
	}

}
