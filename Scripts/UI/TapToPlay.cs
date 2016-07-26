using UnityEngine;
using System.Collections;

public class TapToPlay : MonoBehaviour {

	Vector2 outPos;
	Vector2 inPos;

	void Start () {
		outPos = new Vector2 (Screen.width * 1.3f, Screen.height * 0.1f);
		inPos = new Vector2 (Screen.width * 0.8f, Screen.height * 0.1f);
	}

	void Update () {
		if (!GameProgression.isPause) {
			if (Input.GetMouseButtonDown (0)) {
				GameProgression.isJumpTouch = true;
			}
		}

		if (Stage.isTheRePlay) {
			return;
		}

		if (Stage.gameScene == Stage.GameScene.Score) {
			transform.position = outPos;
			return;
		}

		//	gameSceneがReadyの時、タッチでGameに遷移
		if (GameProgression.isPause) {
			if (Input.GetMouseButtonDown (0)) {
				if (Stage.gameScene == Stage.GameScene.Ready) {
					Stage.gameScene = Stage.GameScene.Game;
				}
				GameProgression.isPause = false;
				//	強制解除
//				Pauser.Resume ();
			}
			transform.Lerp_Position (inPos, 0.075f);
		} else {
			transform.Lerp_Position (outPos, 0.075f);
		}
	}
}
