using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DeviceControls : MonoBehaviour
{
	public Joystick Joystick;
	public Button UseButton;
	private CommandHandler _commandHandler;

	void Start()
	{
		UseButton.onClick.AddListener(Use);
	}

	void Update()
	{
		var movement = Joystick.GetMovement();

		if (_commandHandler != null)
		{
			if (Mathf.Abs(movement.x) > 0.2 || Mathf.Abs(movement.y) > 0.2)
			{
				_commandHandler.SendMoveCommand(movement.x, movement.y);
			}
		}
	}

	private void Use()
	{
		if (_commandHandler != null)
		{
			_commandHandler.SendUseCommand();
		}
	}

	public void SetCommandHandler(CommandHandler handler)
	{
		_commandHandler = handler;
	}
}
