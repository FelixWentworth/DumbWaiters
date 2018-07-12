using System.Collections;
using System.Collections.Generic;
using PlayGen.SUGAR.Unity;
using UnityEngine;

public class SugarUI : MonoBehaviour
{

	/// <summary>
	/// Objects which are disabled until the player logs in
	/// </summary>
	public GameObject[] DisabledGameObjects;

	void Start()
	{
		var isDevice = Application.platform == RuntimePlatform.Android ||
		               Application.platform == RuntimePlatform.IPhonePlayer;

		if (isDevice)
		{
			foreach (var disabledGameObject in DisabledGameObjects)
			{
				disabledGameObject.SetActive(false);
			}
		

		
			SUGARManager.Account.DisplayPanel(OnSuccess);
		}
	}

	private void OnSuccess(bool success)
	{
		if (success)
		{
			foreach (var disabledGameObject in DisabledGameObjects)
			{
				disabledGameObject.SetActive(true);
			}
		}
	}
}
