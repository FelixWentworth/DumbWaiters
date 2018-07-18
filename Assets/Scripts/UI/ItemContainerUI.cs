using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerUI : MonoBehaviour
{

	public Text Name;
	public Text Effect;
	public Image Icon;
	public Button BuyButton;
	public Text Cost;
	public GameObject LockedPanel;
	public Text LevelRequirement;

	public Button Set(ShopItems.Item item, bool locked)
	{
		Name.text = item.Name;
		Effect.text = item.Effect;
		Icon.sprite = item.Icon;
		Cost.text = "$" + item.Price;
		LevelRequirement.text = "Level " + item.LevelRequirement;
		LockedPanel.SetActive(locked);

		return BuyButton;
	}
}
