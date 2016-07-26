using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayCountText : MonoBehaviour {

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
			if (gameObject.name == "TotalPlayCount1") {
				vScoreText.text = PlayCount.playCount [0 + ih] + "回";
			}
			if (gameObject.name == "TotalPlayCount2") {
				vScoreText.text = PlayCount.playCount [1 + ih] + "回";
			}
			if (gameObject.name == "TotalPlayCount3") {
				vScoreText.text = PlayCount.playCount [2 + ih] + "回";
			}
			if (gameObject.name == "TotalPlayCount4") {
				vScoreText.text = PlayCount.playCount [3 + ih] + "回";
			}
			if (gameObject.name == "TotalPlayCount5") {
				vScoreText.text = PlayCount.playCount [4 + ih] + "回";
			}
		}
	}
}
