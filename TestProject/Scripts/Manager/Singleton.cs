using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;
	public static T Instance
	{
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType (typeof (T));
			}
			return instance;
		}
	}

	protected virtual void Awake ()
	{
		if (instance != null) {
			Destroy (gameObject);
			return;
		}
		instance = this as T;
		DontDestroyOnLoad (gameObject);
	}
}
