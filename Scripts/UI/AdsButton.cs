using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdsButton : MonoBehaviour {

	Player player;

	public static bool adsVideo;
	public static bool adsVideoOK;

//	Image child_fill_video;
//	Image child_fill_replay;

	void Awake () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void Start () {
		adsVideo = false;
		adsVideoOK = false;

//		child_fill_video = gameObject.transform.GetChild(0).gameObject.GetComponent<Image> ();
//		child_fill_replay = gameObject.transform.GetChild(1).gameObject.GetComponent<Image> ();
	}
	
	void Update () {

		if (Stage.gameScene == Stage.GameScene.Score) {
			if (!Stage.isGoal && player.isFlag) {

				if (adsVideo) {
					adsVideo = false;
					adsVideoOK = true;
					player.isDead = false;
					player.isTimeOfDeath = false;
					player.lifePoint = 1;
					player.OnColliderObject_Setup ();
					RePlayPanel.isOpenReplayPanel = false;
				}
			}

//			child_fill_video.fillAmount =
//				1.0f-Mover.RatioMap (GameProgression.returnCount, 0, 5, 0.0f, 1.0f);
//			child_fill_replay.fillAmount =
//				Mover.RatioMap (GameProgression.returnCount, 0, 5, 0.0f, 1.0f);

		} else {
		}
	}
}
