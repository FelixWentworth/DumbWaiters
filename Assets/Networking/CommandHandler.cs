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
		
		NetworkServer.Spawn(p.gameObject);
		SetupPlayer(p);
	}

	[Server]
	private void SetupPlayer(Player p)
	{
		var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		gm.PlayerCount ++ ;
		if (p.ID == 0)
		{
			p.ID = gm.PlayerCount;
		}
		p.Team = p.ID % 2 == 1 ? 1 : 2;
		p.SetColours();
		gm.ConnectedPlayers.Add(p.ID, p);

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
		var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		var player = gm.ConnectedPlayers[id];

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
		var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		var player = gm.ConnectedPlayers[id];
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
