using System;
using System.Collections;
using System.Collections.Generic;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[Serializable]
	public struct SUGARGroup
	{
		public string Name;
		public int Id;
		public Sprite Icon;
	}

	public SUGARGroup Group1;
	public SUGARGroup Group2;
	public SUGARGroup Group3;

	public GameObject NetworkInformation;
	public InputField NetworkAddressInput;
	public InputField NetworkPortInput;

	public GameObject HostInformation;
	public InputField HostPortInput;

	public GameObject SelectGroupScreen;

	public static string NetworkAddress;
	public static int NetworkPort;

	void Start()
	{
		NetworkInformation.SetActive(false);
		HostInformation.SetActive(false);
		SelectGroupScreen.SetActive(false);
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
		// TODO implement
	}

	public void ShowSelectGroupScreen()
	{
		SelectGroupScreen.SetActive(true);
	}

	public void HideSelectGroupScreen()
	{
		SelectGroupScreen.SetActive(false);
	}

	public void Btn_QuitGaem()
	{
		Application.Quit();
	}

	public void Btn_JoinGroup1()
	{
		SUGARManager.UserGroup.AddGroup(Group1.Id, false);
		HideSelectGroupScreen();
	}

	public void Btn_JoinGroup2()
	{
		SUGARManager.UserGroup.AddGroup(Group2.Id, false);
		HideSelectGroupScreen();
	}

	public void Btn_JoinGroup3()
	{
		SUGARManager.UserGroup.AddGroup(Group3.Id, false);
		HideSelectGroupScreen();
	}
}
