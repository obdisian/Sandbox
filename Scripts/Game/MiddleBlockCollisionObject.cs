using UnityEngine;
using System.Collections;

public class MiddleBlockCollisionObject : MonoBehaviour {

	GameObject player;

	public static bool isPlayerY5CollisionEnter;

	void Awake () {
		player = GameObject.Find ("Player");
	}

	void Update () {
		if (player) {
			transform.position = player.transform.position;
		} else {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Block") {
		}
	}
}
