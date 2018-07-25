using System;
using System.Collections;
using System.Collections.Generic;
using PlayGen.SUGAR.Common;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	private Groups _groups;

	public GameObject NetworkInformation;
	public InputField NetworkAddressInput;
	public InputField NetworkPortInput;

	public GameObject HostInformation;
	public InputField HostPortInput;

	public GameObject SelectGroupScreen;

	public static string NetworkAddress;
	public static int NetworkPort;

	public MyGroupUI MyGroup;
	public GameObject MyProfile;

	void Start()
	{
		_groups = GameObject.Find("GroupConfig").GetComponent<Groups>();
		NetworkInformation.SetActive(false);
		HostInformation.SetActive(false);
		SelectGroupScreen.SetActive(false);
		MyGroup.gameObject.SetActive(false);
		MyProfile.SetActive(false);
	}

	public void Btn_JoinLanGame()
	{
		NetworkInformation.SetActive(true);
	}

	public void Btn_HostGame()
	{
		HostInformation.SetActive(true);
	}

	public void Btn_Cancel()
	{
		NetworkInformation.SetActive(false);
		HostInformation.SetActive(false);
	}

	public void Btn_Connect()
	{
		NetworkAddress = NetworkAddressInput.text;
		NetworkPort = Convert.ToInt16(NetworkPortInput.text);

		SceneManager.LoadScene(1);
	}

	public void Btn_Host()
	{
		NetworkAddress = "";
		NetworkPort = Convert.ToInt16(HostPortInput.text);

		SceneManager.LoadScene(1);
	}

	public void Btn_PlayLocalGame()
	{
		SceneManager.LoadScene(2);
	}

	public void Btn_Settings()
	{
		// TODO add
	}

	public void ShowSelectGroupScreen()
	{
		SelectGroupScreen.SetActive(true);
	}

	public void HideSelectGroupScreen()
	{
		SelectGroupScreen.SetActive(false);
	}

	public void Btn_MyGroup()
	{
		
	}

	public void Btn_ShowMyProfile()
	{
		var myGroup = GameObject.Find("SUGARUI").GetComponent<SugarUI>().MyGroupData;
		MyGroup.Show(myGroup);
		MyGroup.gameObject.SetActive(true);
		MyProfile.SetActive(true);
	}

	public void Btn_CloseMyProfile()
	{
		MyGroup.gameObject.SetActive(false);
		MyProfile.SetActive(false);
	}

	public void Btn_ShowLeaderboard()
	{
		//SUGARManager.Leaderboard.Display("TotalReputation", LeaderboardFilterType.Top);
		SUGARManager.GameLeaderboard.DisplayGameList();
	}

	public void Btn_ShowAchievements()
	{
		SUGARManager.Evaluation.DisplayAchievementList();
	}

	public void Btn_JoinGroup(int num)
	{
		var group = _groups.GetGroupId(num);
		Debug.Log(SUGARManager.CurrentUser.Id);
		Debug.Log(group);
		SUGARManager.UserGroup.AddGroup(group, false);
		HideSelectGroupScreen();
	}

	public void Btn_QuitGame()
	{
		Application.Quit();
	}
}
