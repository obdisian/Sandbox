using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxGraphGauge : MonoBehaviour {

	public GameObject owner;

	Text text;
	Image ownerImage;

	int thisNumber = 0;

	int plusHeight = 20;

	void Start () {
		text = GetComponent<Text> ();
		ownerImage = owner.GetComponent<Image> ();

		text.text = "0";

		for (int i = 1; i <= 15; i++) {
			if (gameObject.name == "BoxGraphGauge" + i) {
				thisNumber = i;
				text.color = TitleSelectProgression.sgColor [i-1];
			}
		}

		#if UNITY_ANDROID
		plusHeight = 25;
		#endif
	}
	
	void Update () {
		transform.SetPositionY (plusHeight + Mover.RatioMap (ownerImage.fillAmount, 0.0f, 1.0f, BoxGraphBaseLine.text0_y, BoxGraphBaseLine.text100_y));
		if (SubOptionPanel.openFirstClearPanel) {
			text.color = Color.white;
			text.text = "" + SaveData.firstClearCount [thisNumber - 1];
		} else {
			text.color = TitleSelectProgression.sgColor [thisNumber-1];
			text.text = "" + PlayCount.playCount [thisNumber - 1];
		}

		if (text.text == "0") {
			text.text = "";
		}
	}
}
