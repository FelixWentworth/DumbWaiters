using System.Linq;
using PlayGen.SUGAR.Unity;
using UnityEngine.UI;
using PlayGen.Unity.Utilities.Text;
using UnityEngine;

public class AccountInterface : BaseAccountInterface
{
	/// <summary>
	/// Trigger DoBestFit method and add event listener for when resolution changes to trigger DoBestFit.
	/// </summary>
	private void OnEnable()
	{
		DoBestFit();
		BestFit.ResolutionChange += DoBestFit;
		_name.text = PlayerPrefs.GetString("SUGAR_Username");
	}

	/// <summary>
	/// Remove event listener on disable.
	/// </summary>
	private void OnDisable()
	{
		BestFit.ResolutionChange -= DoBestFit;
		PlayerPrefs.SetString("SUGAR_Username", _name.text);
	}

	/// <summary>
	/// Set the text of all the active buttons to be as big as possible and the same size.
	/// </summary>
	private void DoBestFit()
	{
		GetComponentsInChildren<Button>(true).Select(t => t.gameObject).Where(t => t.activeSelf).BestFit();
	}
}
