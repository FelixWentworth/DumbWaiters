﻿using System;
using System.Collections.Generic;
using System.Linq;
using PlayGen.SUGAR.Contracts;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.UI;

public class SugarUI : MonoBehaviour
{

	/// <summary>
	/// Objects which are disabled until the player logs in
	/// </summary>
	public Text Username;

	public Text UserMoney;

	public Image GroupIcon;

	private Groups _groups;

	public GroupData MyGroupData;

	void Start()
	{
		_groups = GameObject.Find("GroupConfig").GetComponent<Groups>();
		//var isDevice = Application.platform != RuntimePlatform.WindowsEditor;
		var isDevice = Application.platform == RuntimePlatform.Android ||
		               Application.platform == RuntimePlatform.IPhonePlayer;

		if (SUGARManager.CurrentUser == null)
		{
			SUGARManager.Account.DisplayPanel(OnSuccess);
		}
	}

	private void OnSuccess(bool success)
	{
		if (success)
		{
			Username.text = SUGARManager.CurrentUser.Name;
			SUGARManager.UserGroup.GetGroupsList(OnGetGroupList);
		}
	}

	private void OnGetGroupList(bool success)
	{
		if (success)
		{
			var inGroup = SUGARManager.UserGroup.Groups.Any(g => _groups.GroupIds().Contains(g.Actor.Id));
			if (!inGroup)
			{
				GameObject.Find("MenuManager").GetComponent<MenuManager>().ShowSelectGroupScreen();
			}
		}
	}

	void LateUpdate()
	{
		if (SUGARManager.CurrentUser != null)
		{
			Username.text = SUGARManager.CurrentUser.Name;
			var myGroups = SUGARManager.UserGroup.Groups.Where(g => _groups.GroupIds().Contains(g.Actor.Id)).ToList();
			if (myGroups.Any() && _groups != null)
			{
				var id = myGroups[0].Actor.Id;
				GroupIcon.sprite = _groups.GetSpriteById(id);
				SetCurrentGroupData();
			}

			UserMoney.text = SUGARManager.Resource.UserGameResources.ContainsKey("Money")
				? "$" + SUGARManager.Resource.UserGameResources["Money"]
				: "$0";
		}
	}

	public struct GroupData
	{
		public int Id;
		public string Name;
		public string Description;
		public string Members;
		public long Money;
	}

	public void SetCurrentGroupData()
	{
		var inGroup = SUGARManager.UserGroup.Groups.Any(g => _groups.GroupIds().Contains(g.Actor.Id));
		if (inGroup)
		{
			var myGroup = SUGARManager.UserGroup.Groups.First(g => _groups.GroupIds().Contains(g.Actor.Id));
			MyGroupData = new GroupData();
			MyGroupData.Id = myGroup.Actor.Id;
			MyGroupData.Name = myGroup.Actor.Name;
			MyGroupData.Description = myGroup.Actor.Description;

			//SUGARManager.Resource.Get(Result);
		}
	}

	//private void Result(List<ResourceResponse> resourceResponses)
	//{
	//	var groupResources = resourceResponses.Where(r => r.ActorId == MyGroupData.Id);
	//	if (groupResources.Any())
	//	{
	//		MyGroupData.Money = groupResources.First(r => r.Key == "Money").Quantity;
	//	}
	//	else
	//	{
	//		MyGroupData.Money = 0;
	//	}
	//}
}
