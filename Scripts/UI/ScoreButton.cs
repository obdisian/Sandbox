using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class ScoreButton : MonoBehaviour {

	//	以下、押した時用
	public void RePlay () {
		Stage.isSecondPlay = true;
		if (Stage.isGoal) {
			if (Stage.stageMapList == Stage.StageMapList.Level_5 && TitleSelectProgression.lock_Stage2 ||
				Stage.stageMapList == Stage.StageMapList.Level_10 && TitleSelectProgression.lock_Stage3 ||
				Stage.stageMapList == Stage.StageMapList.Level_15) {
				RePlayTrue ();
			} else {
				Stage.playCount = 1;
				Stage.stageMapList++;
				RePlayTrue ();
			}
		} else {
			Stage.isAddPlayCount = true;
			if (GameObject.Find ("Player").GetComponent<Player> ().isFlag) {
				if (Advertisement.IsReady ("rewardedVideo") && GameProgression.returnCount < 0) {
					RePlayPanel.isOpenReplayPanel = true;
				} else if (Advertisement.IsReady ("rewardedVideo")) {
					GameProgression.returnCount--;
					AdsButton.adsVideo = true;
				} else {
					RePlayTrue ();
				}
			} else {
				RePlayTrue ();
			}
		}
	}
	public void RePlayTrue () {
		if (AdsButton.adsVideoOK) {
			AdsButton.adsVideoOK = false;
		} else {
			Stage.isTheRePlay = true;
			Stage.gameScene = Stage.GameScene.Ready;
			SceneTransition.LoadLevel ("Game", SceneTransition.TransitionType.FadeScreen, Color.black);
		}
	}
	public void GoToSelect () {
		if (AdsButton.adsVideoOK) {
			AdsButton.adsVideoOK = false;
		}
		Stage.isTheRePlay = true;
		Stage.gameScene = Stage.GameScene.Ready;
		TitleSelectProgression.scenes = TitleSelectProgression.Scenes.Select;
		SceneTransition.LoadLevel ("TitleSelect", SceneTransition.TransitionType.FadeScreen, Color.black);
	}
	public void GoToGameCenter () {
		RankingUtility.ShowLeaderboardUI();
	}
}
