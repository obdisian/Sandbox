using UnityEngine;
using System.Collections;

public class ReplayLock : MonoBehaviour {

	Vector2 size;

	void Start () {
		size = transform.localScale;
		transform.localScale = Vector2.zero;
	}
	
	void Update () {
		if (!Stage.isGoal) {
			return;
		}

		if (Stage.stageMapList == Stage.StageMapList.Level_5 && TitleSelectProgression.lock_Stage2 ||
			Stage.stageMapList == Stage.StageMapList.Level_10 && TitleSelectProgression.lock_Stage3) {
			if (Stage.gameScene == Stage.GameScene.Score) {
				transform.localScale = size;
			}
		}
	}
}
