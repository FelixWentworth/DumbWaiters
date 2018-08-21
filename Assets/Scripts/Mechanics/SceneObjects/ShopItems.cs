using System;
using PlayGen.SUGAR.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
	[Serializable]
	public struct Item
	{
		public string Name;
		public string Effect;
		public int Price;
		public Sprite Icon;
		public int LevelRequirement;
		public GameObject ItemGameObject;
	}

	public Item[] AvailableItems;
	public BuyItemsUI BuyItemsUi;

	public DeviceControls DeviceControls;

	private int _index;
	
	void Start()
	{
		BuyItemsUi.gameObject.SetActive(false);
	}

	public void ToggleItems()
	{
		BuyItemsUi.gameObject.SetActive(!BuyItemsUi.gameObject.activeSelf);
		if (!BuyItemsUi.ItemsSet)
		{
			BuyItemsUi.PopulateItems(AvailableItems, 0, this);
		}
	}

	public void GetItem(int index, int cost)
	{
		// check the player has enough money
		if (SUGARManager.Resource.UserGameResources.ContainsKey("Money"))
		{
			var playerMoney = Convert.ToInt64(SUGARManager.Resource.UserGameResources["Money"]);
			if (playerMoney > cost)
			{
				_index = index;
				SUGARManager.Resource.Add("Money", -cost, Success);
			}
		}
		else
		{
			Debug.Log("No Money Found");
		}
	}

	private void Success(bool b)
	{
		if (b)
		{
			DeviceControls.SendCreateItem(_index);
			ToggleItems();
		}
	}
}
