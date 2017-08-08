using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	void Start ()
	{
		Lib.SetFps (60);
	}
	
	void Update ()
	{
		//	テスト遷移
		if (Input.GetMouseButtonDown (0)) {
			Scene.Load(Scene.Name.Game);
		}
	}
}
