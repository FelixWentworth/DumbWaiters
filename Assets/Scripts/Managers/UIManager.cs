﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour {

	public enum UIState
	{
		None = 0,
		Menu,
		Tutorial,
		Lobby,
		Game,
		GameOver,
		DeviceControls
	}

	[Serializable]
	public class UIObject
	{
		public UIState State;
		public GameObject Parent;
	}

	[Serializable]
	public class UIStates
	{
		public List<UIObject> UI= new List<UIObject>
		{
			new UIObject{State = UIState.Menu},
			new UIObject{State = UIState.Tutorial},
			new UIObject{State = UIState.Lobby},
			new UIObject{State = UIState.Game},
			new UIObject{State = UIState.GameOver},
			new UIObject{State = UIState.DeviceControls},
		};
	}

	[SerializeField] private UIStates _gameUI;
	private int CurrentState = 0;

	private UIObject _activeUiObject;

	void Start()
	{
		if (NetworkManager.singleton != null)
		{
			if (MenuManager.NetworkAddress != "")
			{
				NetworkManager.singleton.networkAddress = MenuManager.NetworkAddress;
				NetworkManager.singleton.networkPort = MenuManager.NetworkPort;
				NetworkManager.singleton.StartClient();
			}
			else
			{
				//NetworkManager.singleton.networkAddress = MenuManager.NetworkAddress;
				NetworkManager.singleton.networkPort = MenuManager.NetworkPort;
				NetworkManager.singleton.StartServer();
			}
		}
	}

	void LateUpdate()
	{
		if (_activeUiObject != null)
		{
			if ((int) _activeUiObject.State != CurrentState)
			{
				GoToState((UIState) CurrentState);
			}
			if (!_activeUiObject.Parent.activeSelf)
			{
				_activeUiObject.Parent.SetActive(true);
			}
		}
	}

	public GameObject SetState(UIState state)
	{
		if (CurrentState != (int) state)
		{
			CurrentState = (int) state;
			return GoToState((UIState) CurrentState);
		}
		return null;
	}


	private GameObject GoToState(UIState state)
	{
		DisableStates();
		var newState = _gameUI.UI.First(ui => ui.State == state);
		if (newState.Parent != null)
		{
			newState.Parent.SetActive(true);
			_activeUiObject = newState;
			return newState.Parent;
		}
		return null;
	}

	private void DisableStates()
	{
		foreach (var uiObject in _gameUI.UI)
		{
			if (uiObject.Parent != null)
			{
				uiObject.Parent.SetActive(false);
			}
		}
	}
}
