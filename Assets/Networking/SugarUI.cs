using System;
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
		var configObj = GameObject.Find("GroupConfig");
		if (configObj != null)
		{
			_groups = GameObject.Find("GroupConfig").GetComponent<Groups>();
		}

		if (SUGARManager.CurrentUser == null)
		{
			SUGARManager.Account.DisplayLogInPanel(OnSuccess);
		}
	}

	public GroupData UpdateGroupMoney()
	{
		if (SUGARManager.CurrentUser != null)
		{
			SetCurrentGroupData();
		}
		var money = SUGARManager.Client.Resource.Get(1, MyGroupData.Id, new[] {"Money"})
			.FirstOrDefault(r => r.Key == "Money");
		MyGroupData.Money = money != null ? Convert.ToInt64(money.Quantity) : 0;
		return MyGroupData;
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
			}

			if (UserMoney != null)
			{
				UserMoney.text = "$"+ SUGARManager.Resource.GetFromCache("Money");
			}
		}
	}

	public class GroupData
	{
		public int Id;
		public string Name;
		public string Description;
		public string Members;
		public long Money;
	}

	public void SetCurrentGroupData()
	{
		if (MyGroupData == null)
			MyGroupData = new GroupData();
		var inGroup = SUGARManager.UserGroup.Groups.Any(g => _groups.GroupIds().Contains(g.Actor.Id));
		if (inGroup)
		{
			var myGroup = SUGARManager.UserGroup.Groups.First(g => _groups.GroupIds().Contains(g.Actor.Id));
			
			MyGroupData.Id = myGroup.Actor.Id;
			MyGroupData.Name = myGroup.Actor.Name;
			MyGroupData.Description = myGroup.Actor.Description;
			MyGroupData.Members = SUGARManager.Client.Group.Get(myGroup.Actor.Id).MemberCount.ToString();
		}
	}
}
