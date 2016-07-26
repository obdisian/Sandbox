using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScorePanel : MonoBehaviour {

	int counter = 0;

	Color color;

	const float panelSpeed = 0.15f;

	void Start () {
		transform.position = Mover.UBPosition (Mover.UiBasePos.Top);
	}
	
	void Update () {

		if (Stage.isTheRePlay) {
			return;
		}

		if (Stage.gameScene == Stage.GameScene.Score) {
			counter++;

			if (Stage.isGoal && counter > 60 || !Stage.isGoal && counter > 40) {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);

				if (AdsButton.adsVideoOK) {
					AdsButton.adsVideoOK = false;
//					GameProgression.isPause = false;
					Stage.playCount++;
					Stage.gameScene = Stage.GameScene.Ready;
				}
			}
		} else {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Top), 0.1f);
			counter = 0;
		}
	}
}
