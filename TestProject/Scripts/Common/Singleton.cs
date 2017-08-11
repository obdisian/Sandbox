using UnityEngine;
using System;
using System.Collections;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;
	public static T Instance
	{
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType (typeof (T));

				if (instance == null) {
					instance = (new GameObject()).AddComponent<T>();
					instance.name = instance.GetType ().FullName;
				}
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
		Init ();
	}

	protected abstract void Init ();
}
