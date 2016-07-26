using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectButtonPercentColor : MonoBehaviour {

	Image image;
	public int ownerBaseNumber;

	void Start () {
		ownerBaseNumber--;

		image = GetComponent<Image> ();
		image.fillAmount = 0;

		//	難易度によっての色分け（後でRGB値を指定してもらう）
//		if (ownerBaseNumber < 3) {
//			image.color = new Color (0, 1, 0, 0.4f);
//		} else if (ownerBaseNumber < 6) {
//			image.color = new Color (0, 1, 1, 0.4f);
//		} else if (ownerBaseNumber < 9) {
//			image.color = new Color (0, 0, 1, 0.4f);
//		} else if (ownerBaseNumber < 12) {
//			image.color = new Color (1, 0, 1, 0.4f);
//		} else {
//			image.color = new Color (1, 0, 0, 0.4f);
//		}
		Color c = TitleSelectProgression.sgColor [ownerBaseNumber];
		c.a = 0.4f;
		image.color = c;

//		if (Score.mapScore [ownerBaseNumber] == 100) {
//			image.color = Color.yellow * new Color (1, 1, 1, 0.4f);
//		}
	}
	
	void Update () {

		if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_1 && ownerBaseNumber >= 0 && ownerBaseNumber < 5) {
			image.fillAmount = Mathf.Lerp (image.fillAmount, Mover.RatioMap (Score.mapScore [ownerBaseNumber], 0, 100, 0.0f, 1.0f), 0.05f);
		} else if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_2 && ownerBaseNumber >= 5 && ownerBaseNumber < 10) {
			image.fillAmount = Mathf.Lerp (image.fillAmount, Mover.RatioMap (Score.mapScore [ownerBaseNumber], 0, 100, 0.0f, 1.0f), 0.05f);
		} else if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_3 && ownerBaseNumber >= 10 && ownerBaseNumber < 15) {
			image.fillAmount = Mathf.Lerp (image.fillAmount, Mover.RatioMap (Score.mapScore [ownerBaseNumber], 0, 100, 0.0f, 1.0f), 0.05f);
		} else {
			image.fillAmount = Mathf.Lerp (image.fillAmount, 0, 0.1f);
		}
	}
}
