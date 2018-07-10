using System.Collections;
using UnityEngine;

/// <summary>
/// Attach this class to any object that can move in  the scene
/// </summary>
public class Movement : MonoBehaviour
{

	private float _x;
	private float _y;
	private float _z;

	public float MovementSpeed { get; set; }
	private float _deadzone = 0.05f;
	private float _reductionFactor = 1.25f;

	private Vector3 _destination = Vector3.one * 100;

	public void Move(float x = 0, float y = 0, float z = 0)
	{
		// Transition teh x, y, and z values to the new values
		_x = x;
		_y = y;
		_z = z;
	}

	public void SetDestination(Vector3 destination)
	{
		_destination = destination;
	}

	void Start()
	{
		StartCoroutine(MoveObject());
	}

	private IEnumerator MoveObject()
	{
		while (true)
		{
			if (_destination != Vector3.one * 100)
			{
				transform.LookAt(_destination);
				transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * MovementSpeed);

				if (Vector3.Distance(transform.position, _destination) < 1.25f)
				{
					transform.LookAt(transform.position + Vector3.forward);
					transform.position = _destination;
				}
			}
			else
			{
				var newPos = new Vector3(transform.position.x + _x, transform.position.y, transform.position.z + _z);
				transform.LookAt(newPos);
				transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * MovementSpeed);

				// reduce player speed over time
				ReduceSpeeds();
			}
			yield return null;
		}
	}

	private void ReduceSpeeds()
	{
		_x = _x > _deadzone || _x < -_deadzone ? _x / _reductionFactor : 0;
		_y = _y > _deadzone || _y < -_deadzone ? _y / _reductionFactor : 0;
		_z = _z > _deadzone || _z < -_deadzone ? _z / _reductionFactor : 0;
	}

}

