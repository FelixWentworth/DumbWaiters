using System.Collections;
using System.Collections.Generic;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.UI;

public class SugarUI : MonoBehaviour
{

	/// <summary>
	/// Objects which are disabled until the player logs in
	/// </summary>
	public Text Username;

	void Start()
	{
		var isDevice = Application.platform != RuntimePlatform.WindowsEditor;
		//var isDevice = Application.platform == RuntimePlatform.Android ||
		//               Application.platform == RuntimePlatform.IPhonePlayer;

		if (isDevice && SUGARManager.CurrentUser == null)
		{
			SUGARManager.Account.DisplayPanel(OnSuccess);
		}
	}

	private void OnSuccess(bool success)
	{
		if (success)
		{
			Username.text = SUGARManager.CurrentUser.Name;
		}
	}
}
