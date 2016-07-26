using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI_ScoreRePlay : MonoBehaviour {

	int adder = 0;
	int stageNumber = 0;

	bool isTextColorReverese;

	Image child_fill_image, n_image;
	Text child_text_text;

	void Start () {
		stageNumber = (int)Stage.stageMapList-1;

		child_fill_image = gameObject.transform.GetChild(0).gameObject.GetComponent<Image> ();
		child_text_text = gameObject.transform.GetChild(1).gameObject.GetComponent<Text> ();

		n_image = GetComponent<Image> ();
	}
	
	void Update () {

		//	ステージクリア時の移行用
		adder = Stage.isGoal ? 1 : 0;
		if ((Stage.StageMapList)stageNumber+1 == Stage.StageMapList.Level_15) {
			adder = 0;
		}

		//	難易度によっての色分け（後でRGB値を指定してもらう、、SelectButtonPercentColorから丸コピ）
		Color c = TitleSelectProgression.sgColor [stageNumber + adder];
		c.a = 0.4f;
		child_fill_image.color = c;

		if (Stage.isGoal) {
			if (adder == 0) {
				child_fill_image.fillAmount = 1.0f;
			} else {
				child_fill_image.fillAmount = Mover.RatioMap (Score.mapScore [stageNumber + adder], 0, 100, 0.0f, 1.0f);
			}
		} else {
			child_fill_image.fillAmount = Mathf.Lerp (child_fill_image.fillAmount, Mover.RatioMap (Stage.runPercent, 0, 100, 0.0f, 1.0f), 0.05f);
		}

		child_text_text.text = "" + (stageNumber + 1 + adder);


		//	動画コンテ時数字が点滅する
		if (!Stage.isGoal && GameProgression.returnCount < 0) {
			Color reverseColor = Color.white - TitleSelectProgression.sgColor [stageNumber];
			reverseColor.a = 1;
			if (isTextColorReverese) {
				child_text_text.color = Color.Lerp (child_text_text.color, TitleSelectProgression.sgColor [stageNumber], 0.06f);
				n_image.color = Color.Lerp (n_image.color, reverseColor, 0.06f);
			} else {
				child_text_text.color = Color.Lerp (child_text_text.color, reverseColor, 0.06f);
				n_image.color = Color.Lerp (n_image.color, TitleSelectProgression.sgColor [stageNumber], 0.06f);
			}
			if (Mathf.Abs (child_text_text.color.r - TitleSelectProgression.sgColor [stageNumber].r) < 0.1f &&
				Mathf.Abs (child_text_text.color.g - TitleSelectProgression.sgColor [stageNumber].g) < 0.1f &&
				Mathf.Abs (child_text_text.color.b - TitleSelectProgression.sgColor [stageNumber].b) < 0.1f ||
				Mathf.Abs (child_text_text.color.r - reverseColor.r) < 0.1f &&
				Mathf.Abs (child_text_text.color.g - reverseColor.g) < 0.1f &&
				Mathf.Abs (child_text_text.color.b - reverseColor.b) < 0.1f){
				isTextColorReverese = !isTextColorReverese;
			}
		} else {
			if (Score.mapScore [stageNumber + adder] == 100) {
				n_image.color = TitleSelectProgression.sgColor [stageNumber + adder];
			} else {
				n_image.color = Color.white;
			}
			child_text_text.color = Color.white;
		}
	}
}
