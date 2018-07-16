using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkingUI : NetworkBehaviour
{
	private UIManager _uiManager;

	void Start()
	{
		_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		if (isClient)
		{
			_uiManager.SetState(UIManager.UIState.DeviceControls);
		}
		if (isServer)
		{
			_uiManager.SetState(UIManager.UIState.Game);
		}
	}
	
}
