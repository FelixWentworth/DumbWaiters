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
		//var isDevice = Application.platform != RuntimePlatform.WindowsEditor;
		var isDevice = Application.platform == RuntimePlatform.Android ||
		               Application.platform == RuntimePlatform.IPhonePlayer;

		if (true || isDevice && SUGARManager.CurrentUser == null)
		{
			SUGARManager.Account.DisplayPanel(OnSuccess);
		}
	}

	private void OnSuccess(bool success)
	{
		if (success)
		{
			Username.text = SUGARManager.CurrentUser.Name;
			if (SUGARManager.CurrentGroup == null)
			{
				GameObject.Find("MenuManager").GetComponent<MenuManager>().ShowSelectGroupScreen();
				// the user has not joined a group, Select a group
			}
		}
	}
}
