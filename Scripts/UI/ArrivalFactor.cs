using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrivalFactor : MonoBehaviour {

	Text text, rePlayCountText;

	void Start () {
		text = GetComponent<Text> ();

		gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ().text = Info_StringText.arrivalFactorText [Info_StringText.textLanguage];
		rePlayCountText = gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();
	}

	void Update () {
		text.text = Stage.runPercent + "%";
		if (Stage.gameScene == Stage.GameScene.Score) {
			rePlayCountText.text = Info_StringText.ArrivalPlayCountText (Stage.playCount);
		}
	}
}
