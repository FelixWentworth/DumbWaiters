using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLookAtCamera : MonoBehaviour {

	void Update () {
		transform.LookAt(Camera.main.transform.position);
		transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
	}
}
