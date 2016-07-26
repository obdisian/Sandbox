using UnityEngine;
using System.Collections;

public class ObjectIsEnabled : MonoBehaviour {

	void OnBecameInvisible () {
		enabled = false;
	}
	void OnBecameVisible () {
		enabled = true;
	}
}
