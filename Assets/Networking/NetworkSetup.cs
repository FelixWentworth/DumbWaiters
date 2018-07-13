using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkSetup : MonoBehaviour
{
	private NetworkManager _networkManager;

	void Start()
	{
		_networkManager = GetComponent<NetworkManager>();
		var isClient = Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
		if (isClient)
		{
			SetupClient();
		}
		else
		{
			SetupServer();
		}
	}

	private void SetupClient()
	{
		var myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.Connect(_networkManager.networkAddress, _networkManager.networkPort);
	}

	private void SetupServer()
	{
		NetworkServer.Listen(_networkManager.networkPort);
		Debug.Log("Listening on Port: " + _networkManager.networkPort);
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		NetworkServer.Spawn(_networkManager.playerPrefab);
	}
}
