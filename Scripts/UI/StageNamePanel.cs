using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageNamePanel : MonoBehaviour {

	int timingTimer = 0;

	Vector2 basePos = Vector2.zero;
	Vector2 size = Vector2.zero;
	Vector2 nextScale = new Vector2 (Screen.width * 0.9f, Screen.height * 0.965f);

	Image panelImage;
	Color baseColor;

	void Start () {
		if (gameObject.tag == "UI_StageNamePanelText") {
			GetComponent<Text> ().text = "Stage " + (int)Stage.stageMapList;
		} else {
			transform.position = new Vector2 (Screen.width / 2, Screen.height * 0.7f);
			basePos = transform.position;
			size = transform.localScale;
			transform.localScale = Vector2.zero;

			panelImage = GetComponent<Image> ();
			baseColor = panelImage.color;
		}
	}
	
	void Update () {
		if (gameObject.tag == "UI_StageNamePanelText") return;

		if (Stage.gameScene == Stage.GameScene.Score) {
			timingTimer++;
			if (timingTimer < 60) {
				return;
			}
			transform.Lerp_Position (basePos + Vector2.up * basePos.y*0.2f, 0.1f);
			transform.Lerp_LocalScale (size, 0.075f);

			panelImage.color = Color.Lerp (panelImage.color, Color.clear, 0.1f);
		}
		else if (Stage.gameScene == Stage.GameScene.Ready) {
			transform.Lerp_Position (basePos + Vector2.up * basePos.y*0.2f, 0.1f);
			transform.Lerp_LocalScale (size, 0.075f);

			panelImage.color = Color.Lerp (panelImage.color, baseColor, 0.1f);
		}
		else {
			timingTimer = 0;

			if (GameProgression.gameCounter > 60 || !GameProgression.isPause) {
				transform.Lerp_Position (nextScale, 0.1f);
				transform.Lerp_LocalScale (size / 1.5f, 0.1f);
			}
			else if (GameProgression.gameCounter < 60) {
				transform.Lerp_LocalScale (size*1.25f, 0.075f);
			}
		}
	}
}
