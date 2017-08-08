using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	void Start ()
	{
		
	}

	void Update ()
	{
		//	テスト遷移
		if (Input.GetMouseButtonDown (0)) {
			Scene.Load(Scene.Name.Title);
		}
	}
}
