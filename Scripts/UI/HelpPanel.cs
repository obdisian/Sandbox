using UnityEngine;
using System.Collections;

public class HelpPanel : MonoBehaviour {

	public enum HelpOpenFrag {
		None, Help_1, Help_2, Help_3, Help_4, Help_5, Help_6,
	}
	public static HelpOpenFrag hOpenFrag;
	public static bool isOnhOpenFrag;

	Vector2 size;

	void Start () {
		if (gameObject.name == "HelpBackPanelButton") {
			size = transform.localScale;
			transform.localScale = Vector2.zero;
		}
		else if (gameObject.name == "HelpPanel") {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Bottom);
//			size = transform.localScale;
//			transform.localScale = Vector2.zero;
		}
	}
	
	void Update () {
		if (gameObject.name == "HelpBackPanelButton") {
			if (TitleSelectProgression.helpEnable) {
				transform.localScale = size;
			} else {
				transform.localScale = Vector2.zero;
			}
		}
		else if (gameObject.name == "HelpPanel") {
			if (TitleSelectProgression.helpEnable) {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.15f);
//				transform.Lerp_LocalScale (size, 0.2f);
			} else {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Bottom), 0.15f);
//				transform.Lerp_LocalScale (Vector2.zero, 0.2f);
			}
		}
	}
}
