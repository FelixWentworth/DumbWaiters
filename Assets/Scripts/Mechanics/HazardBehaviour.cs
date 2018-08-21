using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{

	[SerializeField] private GameObject _hazardToSpawn;

	[SerializeField] private int _maxSpawns;

	private int _spawns;
	void OnCollisionEnter(Collision other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "PlayArea")
		{
			if (_spawns < _maxSpawns)
			{
				Instantiate(_hazardToSpawn, transform.position, Quaternion.identity).GetComponent<Modifier>().Place(-1);
				_spawns++;
			}
		}
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Player>().Hit();
		}
	}
}
