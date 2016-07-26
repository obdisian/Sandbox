using UnityEngine;
using System.Collections;

public class MySingleton : MonoBehaviour {

	private static MySingleton instance = null;

	public static MySingleton Instance {
		get { return instance; }
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}
}
