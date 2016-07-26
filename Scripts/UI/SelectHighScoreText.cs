using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectHighScoreText : MonoBehaviour {

	//	補正用。命名適当すぎでも気にしない
	int ih = 0, oih = -1;

	Text vScoreText;

	void Start () {

		vScoreText = GetComponent<Text> ();
	}
	
	void Update () {
		ih = Mathf.Max (0, 5 * ((int)TitleSelectProgression.nowPanel-1));

		if (ih == oih) {
			return;
		} else {
			oih = ih;
			if (gameObject.name == "HighScoreText1") {
				vScoreText.text = Score.mapScore[0 + ih] + "%";
			}
			if (gameObject.name == "HighScoreText2") {
				vScoreText.text = Score.mapScore[1 + ih] + "%";
			}
			if (gameObject.name == "HighScoreText3") {
				vScoreText.text = Score.mapScore[2 + ih] + "%";
			}
			if (gameObject.name == "HighScoreText4") {
				vScoreText.text = Score.mapScore[3 + ih] + "%";
			}
			if (gameObject.name == "HighScoreText5") {
				vScoreText.text = Score.mapScore[4 + ih] + "%";
			}
		}
	}
}
