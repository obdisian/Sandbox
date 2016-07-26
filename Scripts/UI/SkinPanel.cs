using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkinPanel : MonoBehaviour {

	Image sImage;
	Vector2 size;

	void Start () {
		sImage = GetComponent<Image> ();
		if (gameObject.name == "SkinBackPanelButton") {
			size = transform.localScale;
		}
		else if (gameObject.name == "SkinPanel") {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Left);
		}
	}

	void Update () {
		if (TitleSelectProgression.skinEnable) {
			if (gameObject.name == "SkinBackPanel") {
				sImage.color = Color.Lerp (sImage.color, Color.black * 0.5f, 0.1f);
			}
			else if (gameObject.name == "SkinBackPanelButton") {
				transform.localScale = size;
			}
			else if (gameObject.name == "SkinPanel") {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
			}
		}
		else {
			if (gameObject.name == "SkinBackPanel") {
				sImage.color = Color.Lerp (sImage.color, Color.clear, 0.1f);
			}
			else if (gameObject.name == "SkinBackPanelButton") {
				transform.localScale = Vector2.zero;
			}
			else if (gameObject.name == "SkinPanel") {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Left), 0.1f);
			}
		}
	}
}
