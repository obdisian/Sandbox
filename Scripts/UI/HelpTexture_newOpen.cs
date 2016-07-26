using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpTexture_newOpen : MonoBehaviour {

	Vector2 pos;
	Vector2 size;
	RectTransform rTransform;

	Color baseColor;
	Image image;

	void Start () {
		baseColor = new Color (1, 1, 1, 150.0f/255);
		image = GetComponent<Image> ();

		pos = transform.position;
		rTransform = GetComponent<RectTransform> ();
		size = rTransform.sizeDelta;
	}
	bool isSet;
	void Update () {
		if (!isSet) {
			isSet = true;
			pos = transform.position + (Vector3)Mover.UBPosition (Mover.UiBasePos.Left)/2 + (Vector3)Mover.UBPosition (Mover.UiBasePos.Top)/2;

			rTransform = GetComponent<RectTransform> ();
			size = rTransform.sizeDelta;
		}

		if (TitleSelectProgression.helpEnable && HelpPanel.isOnhOpenFrag) {
			if (gameObject.name == "HelpButton" + (int)HelpPanel.hOpenFrag) {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.2f);
				rTransform.sizeDelta = Vector2.Lerp (rTransform.sizeDelta, new Vector2 (675 - 22, 350 - 20), 0.2f);

				transform.SetSiblingIndex (10);

				image.color = Color.Lerp (image.color, Color.white, 0.1f);
			} else {
				transform.Lerp_Position (pos, 0.2f);
				rTransform.sizeDelta = Vector2.Lerp (rTransform.sizeDelta, size, 0.2f);

				image.color = Color.Lerp (image.color, baseColor, 0.1f);
			}
		} else {
			
			rTransform.sizeDelta = Vector2.Lerp (rTransform.sizeDelta, size, 0.2f);
		}
	}
}
