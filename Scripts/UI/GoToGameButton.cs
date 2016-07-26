using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoToGameButton : MonoBehaviour {

	public Stage.StageMapList goStageMap;

	public void GoToGame () {
		//	行き先マップの選択
		Stage.stageMapList = goStageMap;
		SceneTransition.LoadLevel ("Game", SceneTransition.TransitionType.FadeScreen, Color.black, 0.5f);

		TitleSelectProgression.buttonReleased = true;
	}

	bool firstUpdate = true;

	void Update () {
		if (!firstUpdate) {
			return;
		}
		firstUpdate = false;
		if (Score.mapScore [(int)goStageMap-1] == 100) {
			GetComponent<Image> ().color = TitleSelectProgression.sgColor [(int)goStageMap-1];
		}
	}
}
