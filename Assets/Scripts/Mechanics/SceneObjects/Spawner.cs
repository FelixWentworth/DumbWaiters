using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	[SerializeField] private GameObject _objectToSpawn;
	[Range(0, 60)] [SerializeField] private float _timeAlive;

	[SerializeField] private Vector3 _target;

	[Range(0, 180)] [SerializeField] private float _leftAngleLimit;
	[Range(0, 180)] [SerializeField] private float _rightAngleLimit;

	[Range(0, 100)][SerializeField] private float _power;
	[Range(0, 100)][SerializeField] private float _powerOffset;
	[Range(0, 300)][SerializeField] private float _spawnRate;

	private float _current;

	void Update()
	{
		_current += Time.deltaTime;

		if (_current > _spawnRate)
		{
			_current = 0.1f * Random.Range(-_spawnRate, _spawnRate); 
			SpawnObject();
		}
	}

	private void SpawnObject()
	{
		RotateSpawner();
		var obj = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
		var power = _power + Random.Range(-_powerOffset, _powerOffset);
		obj.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse);
		StartCoroutine(WaitToDestroy(obj, _timeAlive));
	}

	private void RotateSpawner()
	{
		var offset = Random.Range(-_leftAngleLimit, _rightAngleLimit);
		transform.LookAt(_target);
		transform.Rotate(0, offset, 0);
	}

	private IEnumerator WaitToDestroy(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);

		Destroy(obj);
	}

}
