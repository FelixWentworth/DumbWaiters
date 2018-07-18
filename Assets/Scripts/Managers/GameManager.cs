using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the creation of core game elements that set the game up,
/// Also handles transitions to UIs
/// </summary>
public class GameManager : GameManagerBase
{
	private GameManagerNetworkCommands _behaviour;

	protected override void Update()
	{
		base.Update();
		if (_behaviour != null)
		{
			ServerCheck = _behaviour.isServer;
			ServerScene.SetActive(ServerCheck);
		}
		else
		{
			_behaviour = GetComponentInChildren<GameManagerNetworkCommands>();
		}
	}

	public override void GoToMenu()
	{
		if (_behaviour.isServer)
		{
			_behaviour.RpcGoToMenu();
		}
		base.GoToMenu();
	}
}
