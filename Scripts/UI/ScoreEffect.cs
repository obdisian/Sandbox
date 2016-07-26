using UnityEngine;
using System.Collections;

public class ScoreEffect : MonoBehaviour {

	Vector2 size;

	void Start () {
		size = transform.localScale;
		transform.localScale = Vector2.zero;
	}

	void Update () {
		transform.transform.localScale += (Vector3)size / 5;
	}
}
