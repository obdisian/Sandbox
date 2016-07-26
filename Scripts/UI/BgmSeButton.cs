using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BgmSeButton : MonoBehaviour {

	Image image;
	Color baseColor;

	void Start () {
		image = GetComponent<Image> ();
		baseColor = image.color;

		if (gameObject.name == "BGM") {
			if (!TitleSelectProgression.bgmEnable) {
				image.color = Color.gray;
			}
		}
		else if (gameObject.name == "SE") {
			if (!TitleSelectProgression.seEnable) {
				image.color = Color.gray;
			}
		}
	}
	
	void Update () {
	
		if (gameObject.name == "BGM") {
			if (!TitleSelectProgression.bgmEnable) {
				image.color = Color.gray;
			} else {
				image.color = baseColor;
			}
		}
		else if (gameObject.name == "SE") {
			if (!TitleSelectProgression.seEnable) {
				image.color = Color.gray;
			} else {
				image.color = baseColor;
			}
		}
	}
}
