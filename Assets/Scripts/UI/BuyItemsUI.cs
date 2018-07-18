using UnityEngine;
using UnityEngine.UI;

public class BuyItemsUI : MonoBehaviour
{
	public bool ItemsSet { get; private set; }
	public HorizontalLayoutGroup parent;
	public GameObject ItemContainerPrefab;

	public void PopulateItems(ShopItems.Item[] items, int playerLevel, ShopItems itemsController)
	{
		for (var i = 0; i < items.Length; i++)
		{
			var index = i;
			var container = Instantiate(ItemContainerPrefab, parent.transform).GetComponent<ItemContainerUI>();
			var button = container.Set(items[i], playerLevel < items[i].LevelRequirement);
			button.onClick.AddListener(() =>
			{
				itemsController.GetItem(index, items[index].Price);
			});
		}
		ItemsSet = true;
	}
}
