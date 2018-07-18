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
	private long _requestAmount;

	public void Show(SugarUI.GroupData data)
	{
		_id = data.Id;
		NameText.text = data.Name;
		DescriptionText.text = data.Description;
		MembersText.text = data.Members;
		MoneyText.text = "Loading";

		GameObject.Find("SUGARUI").GetComponent<SugarUI>().UpdateGroupMoney(Result);
	}

	public void Btn_Take()
	{
		if (MoneyInputField.text == "")
			return;
		_requestAmount = Convert.ToInt64(MoneyInputField.text);
		var currentUserId = SUGARManager.CurrentUser.Id;

		//SUGARManager.CurrentUser.

		//transfer the money
		//SUGARManager.Resource.Add("Money", _requestAmount, AddSuccess);
	}

	private void TransferSuccess(bool b)
	{
		MoneyInputField.text = "";
		GameObject.Find("SUGARUI").GetComponent<SugarUI>().UpdateGroupMoney(Result);
	}

	private void AddSuccess(bool b)
	{
		// TODO update ui
		GameObject.Find("SUGARUI").GetComponent<SugarUI>().UpdateGroupMoney(Result);
	}

	public void Btn_LeaveTip()
	{
		if (MoneyInputField.text == "")
			return;
		_requestAmount = Convert.ToInt64(MoneyInputField.text);
		SUGARManager.Resource.Transfer(_id, "Money", _requestAmount, TransferSuccess);
		SUGARManager.GameData.Send("TipReputation", _requestAmount * 2);
		SUGARManager.GameData.Send("TipMoney", _requestAmount);
	}

	private void Result(List<ResourceResponse> resourceResponses)
	{
		var groupResources = resourceResponses.Where(r => r.ActorId == _id);
		if (groupResources.Any())
		{
			MoneyText.text = "$" +groupResources.First(r => r.Key == "Money").Quantity;
		}
		else
		{
			MoneyText.text = "$" + 0;
		}

	}
}
