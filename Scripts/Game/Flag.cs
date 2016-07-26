using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

	public bool isFlag;

	void Update () {
		if (isFlag) {
			transform.rotation = Quaternion.AngleAxis (10, Vector3.forward) * transform.rotation;
		}
	}
}
