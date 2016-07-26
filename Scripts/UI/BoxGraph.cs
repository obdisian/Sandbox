using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxGraph : MonoBehaviour {

	Image image;

	void Start () {
		image = GetComponent<Image> ();
		image.fillAmount = 0;

		for (int i = 1; i <= 15; i++) {
			if (gameObject.name == "BoxGraph" + i) {
				image.color = TitleSelectProgression.sgColor [i-1];
			}
		}
	}
	
	void Update () {
		for (int i = 1; i <= 15; i++) {
			if (gameObject.name == "BoxGraph" + i) {
				if (SubOptionPanel.openSubOptionPanel) {
					image.fillAmount = Mathf.Lerp (image.fillAmount, Mover.RatioMap (PlayCount.playCount [i-1], 0, BoxGraphBasePoint.maxPlayCount, 0.0f, 1.0f), 0.05f);
				} else {
					image.fillAmount = Mathf.Lerp (image.fillAmount, 0, 0.1f);
				}
			}
		}
	}
}
