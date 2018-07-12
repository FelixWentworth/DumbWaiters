using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkingUI : NetworkBehaviour
{
	public GameObject Host_UI;
	public GameObject Client_UI;

	void Update()
	{
		Host_UI.SetActive(isServer);
		Client_UI.SetActive(!isServer);
	}
}
