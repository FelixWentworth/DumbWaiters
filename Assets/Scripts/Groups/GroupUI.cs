using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupUI : MonoBehaviour
{
	public int GroupNumber;

	public Button SelectButton;
	public MenuManager MenuManager;
	public Text GroupName;
	public Image Icon;
	public Image Background;

	private Groups _groups;

	void Start()
	{
		_groups = GameObject.Find("GroupConfig").GetComponent<Groups>();
		var id = _groups.GetGroupId(GroupNumber);
		GroupName.text = _groups.GetNameById(id);
		Icon.sprite = _groups.GetSpriteById(id);
		var color = _groups.GetColorById(id);
		Background.color = new Color(color.r, color.g, color.b, 0.2f);

		SelectButton.onClick.AddListener(() => MenuManager.Btn_JoinGroup(GroupNumber));
	}
}
