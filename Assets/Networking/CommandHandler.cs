using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// This should be attached to our player object
/// </summary>
public class CommandHandler : NetworkBehaviour
{
	private Dictionary<int, Player> _connectedPlayers = new Dictionary<int, Player>();

	public GameObject PlayerGameObject;

	private int Id = 0;

	public void Start()
	{
		if (isLocalPlayer)
		{
			GameObject.Find("DeviceControlsManager").GetComponent<DeviceControls>().SetCommandHandler(this);
			CmdSpawn();
		}
	}

	[Command]
	private void CmdSpawn()
	{
		var p = Instantiate(PlayerGameObject, new Vector3(0, 1, 0), Quaternion.identity).GetComponent<Player>();
		p.ID = _connectedPlayers.Count + 1;

		NetworkServer.Spawn(p.gameObject);
		_connectedPlayers.Add(p.ID, p);

		RpcSetId(p.ID);
	}

	[ClientRpc]
	private void RpcSetId(int id)
	{
		if (Id == 0)
		{
			Id = id;
		}
	}

	public void SendMoveCommand(float x, float y)
	{
		if (isLocalPlayer)
		{
			CmdMove(Id, x, y);
		}
	}

	[Command]
	private void CmdMove(int id, float x, float y)
	{

		var player = _connectedPlayers[id];
		if (player != null)
		{
			player.Move(x, y);
		}
		else
		{
			Debug.Log(id + " does not exist in list");
		}

	}

	public void SendUseCommand()
	{
		if (isLocalPlayer)
		{
			CmdUse(Id);
		}
	}

	[Command]
	private void CmdUse(int id)
	{
		var player = _connectedPlayers[id];
		if (player != null)
		{
			player.Interact();
		}
		else
		{
			Debug.Log(id + " does not exist in list");
		}

	}
}
