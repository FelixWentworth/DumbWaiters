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

	public Text CommandText;

	void Start()
	{
		CommandText = GameObject.Find("StatusText").GetComponent<Text>();
	}

	public void SendMoveCommand(int x, int y)
	{
		if (isLocalPlayer)
		{
			CommandText.text = "Send Move";
			CmdMove(x, y);
		}
	}

	[Command]
	private void CmdMove(int x, int y)
	{
		CommandText.text = "Set Direction to: (" + x + ", " + y + ")";
	}

	public void SendUseCommand()
	{
		if (isLocalPlayer)
		{
			CommandText.text = "Send use";
			CmdUse();
		}
	}

	[Command]
	private void CmdUse()
	{
		CommandText.text = "Interact Pressed";
	}
}
