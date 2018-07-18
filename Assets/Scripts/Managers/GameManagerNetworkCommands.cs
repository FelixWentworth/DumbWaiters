using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManagerNetworkCommands : NetworkBehaviour {

	[ClientRpc]
	public void RpcGoToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
