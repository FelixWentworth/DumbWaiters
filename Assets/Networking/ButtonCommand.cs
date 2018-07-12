using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ButtonCommand : NetworkBehaviour {

	public enum CommandType
	{
		Left,
		Right,
		Up, 
		Down,
		Interact,
		Send
	}

	public CommandType ButtonCommandType;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(SendCommand);
	}

	private void SendCommand()
	{
		var commandHandler = GameObject.Find("Commands(Clone)").GetComponent<CommandHandler>();
		switch (ButtonCommandType)
		{
			case CommandType.Left:
				commandHandler.SendMoveCommand(-1, 0);
				break;
			case CommandType.Right:
				commandHandler.SendMoveCommand(1, 0);
				break;
			case CommandType.Up:
				commandHandler.SendMoveCommand(0, 1);
				break;
			case CommandType.Down:
				commandHandler.SendMoveCommand(0, -1);
				break;
			case CommandType.Interact:
				commandHandler.SendUseCommand();
				break;
			case CommandType.Send:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
