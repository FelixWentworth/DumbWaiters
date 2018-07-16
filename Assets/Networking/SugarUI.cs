using System.Linq;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.UI;

public class SugarUI : MonoBehaviour
{

	/// <summary>
	/// Objects which are disabled until the player logs in
	/// </summary>
	public Text Username;

	public Image GroupIcon;

	private Groups _groups;

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
			if (SUGARManager.UserGroup.Groups.Any() && _groups != null)
			{
				var id = SUGARManager.UserGroup.Groups[0].Actor.Id;
				GroupIcon.sprite = _groups.GetSpriteById(id);
			}
		}
	}
}
