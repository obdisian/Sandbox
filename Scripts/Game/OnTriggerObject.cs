using UnityEngine;
using System.Collections;

public class OnTriggerObject : MonoBehaviour {

	public Vector2 size;
	public bool isTrig;

	void Start () {
		isTrig = false;
		size = transform.localScale;
	}
}
