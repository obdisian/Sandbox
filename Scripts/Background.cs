using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Background : MonoBehaviour {

	GameObject mainCamera;
	public Sprite[] image = new Sprite [3];

	void Start () {
		if (SceneManager.GetActiveScene ().name == "TitleSelect") {
			return;
		}

		mainCamera = GameObject.Find ("Main Camera");

		//	前面背景の読み込み
		if (gameObject.name == "BackGround") {
			gameObject.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().sprite = image[Mathf.Max (0, ((int)Stage.stageMapList-1) / 5)];
			gameObject.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().sprite = image[Mathf.Max (0, ((int)Stage.stageMapList-1) / 5)];
			gameObject.transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ().sprite = image[Mathf.Max (0, ((int)Stage.stageMapList-1) / 5)];
		}
	}

	void Update () {
		if (SceneManager.GetActiveScene ().name == "TitleSelect") {
			GetComponent<SpriteRenderer> ().sprite = image[Mathf.Max (0, (int)TitleSelectProgression.nowPanel-1)];
			return;
		}

		//	前面背景のスクロール範囲（18, -18）
		if (gameObject.name == "BackGround") {
			transform.SetPositionX (mainCamera.transform.position.x +
				Mover.RatioMap (mainCamera.transform.position.x, Stage.startPosX, Stage.endPosX, 18, -0));
			transform.SetPositionY (mainCamera.transform.position.y);
		}

		//	後面背景のスクロール範囲（19, 9）
		else if (gameObject.name == "BackGround_backLayer") {
			transform.SetPositionX (mainCamera.transform.position.x +
				Mover.RatioMap (mainCamera.transform.position.x, Stage.startPosX, Stage.endPosX, 18, 8));
			transform.SetPositionY (mainCamera.transform.position.y);
		}
	}
}
