using UnityEngine;
using System.Collections;

public class BarBlock : MonoBehaviour {

	public float rotateSpeed;

	void Update () {
		transform.rotation = Quaternion.AngleAxis (rotateSpeed, Vector3.forward) * transform.rotation;
	}
}
