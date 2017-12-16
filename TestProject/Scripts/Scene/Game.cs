using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	void Start ()
	{
		AudioManager.Instance.Setup (AudioManager.Type.Bgm, "1", "Audio/BGM/" + "SND_BGM_Stage1");
		AudioManager.Instance.Setup (AudioManager.Type.Bgm, "2", "Audio/BGM/" + "SND_BGM_Stage2");
		AudioManager.Instance.Setup (AudioManager.Type.Se, "a", "Audio/SE/" + "SND_SE01");

		AudioManager.Instance.Play (AudioManager.Type.Bgm, "1");
	}

	void Update ()
	{
		//	テスト遷移
		if (Input.GetMouseButtonDown (0)) {
			Scene.Load(Scene.Name.Title);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			AudioManager.Instance.Play (AudioManager.Type.Se, "a");
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			AudioManager.Instance.Play (AudioManager.Type.Bgm, "2");
		}
	}
}
