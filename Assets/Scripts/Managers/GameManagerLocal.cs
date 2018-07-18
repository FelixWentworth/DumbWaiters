using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the creation of core game elements that set the game up,
/// Also handles transitions to UIs
/// </summary>
public class GameManagerLocal : GameManagerBase
{
	protected override void SetupGame()
	{
		base.SetupGame();
		// Create our players
		var p1 = Instantiate(PlayerGameObject, Vector3.up, Quaternion.identity).GetComponent<Player>();
		p1.Team = 1;
		p1.SetColours();

		var p2 = Instantiate(PlayerGameObject, Vector3.up, Quaternion.identity).GetComponent<Player>();
		p2.Team = 2;
		p2.SetColours();
	}
}
