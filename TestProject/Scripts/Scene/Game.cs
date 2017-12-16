using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	void Start ()
	{
		AudioManager.Instance.LoadBgm ("1", "Audio/BGM/" + "SND_BGM_Stage1");
		AudioManager.Instance.LoadBgm ("2", "Audio/BGM/" + "SND_BGM_Stage2");
		AudioManager.Instance.LoadSe ("a", "Audio/SE/" + "SND_SE01");

		AudioManager.Instance.PlayBgm ("1");
	}

	void Update ()
	{
		//	テスト遷移
		if (Input.GetMouseButtonDown (0)) {
			Scene.Load(Scene.Name.Title);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			AudioManager.Instance.PlaySe ("a");
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			AudioManager.Instance.PlayBgm ("2");
		}
	}
}
