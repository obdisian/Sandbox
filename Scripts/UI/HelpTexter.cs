using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpTexter : MonoBehaviour {

	string[] setumei = {
		Info_StringText.helpText_Jump [Info_StringText.textLanguage],
		Info_StringText.helpText_Reverse [Info_StringText.textLanguage],
		Info_StringText.helpText_Force [Info_StringText.textLanguage],
		Info_StringText.helpText_Fast_Slow [Info_StringText.textLanguage],
		Info_StringText.helpText_Rejump [Info_StringText.textLanguage],
		Info_StringText.helpText_Colorblock [Info_StringText.textLanguage],
	};

	Text text;
	//	iPadやタブレット端末などでの文字位置を修正する
	bool isTablet = false;

	void Start () {
		text = GetComponent<Text> ();
		transform.position = Mover.UBPosition (Mover.UiBasePos.Right);

		//	縦横の差分で位置修正するか決める
		if (Mathf.Abs (Screen.width - Screen.height) < (Screen.width + Screen.height) / 4) {
			isTablet = true;
		}
	}
	
	void Update () {
		text.text = setumei [Mathf.Max (0,  (int)HelpPanel.hOpenFrag-1)];

		if (HelpPanel.hOpenFrag == HelpPanel.HelpOpenFrag.None) {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
		} else {
			if (isTablet) {
				transform.position = Mover.UBPosition (Mover.UiBasePos.Middle) + Vector2.down * Screen.height * 0.2f;
			} else {
				transform.position = Mover.UBPosition (Mover.UiBasePos.Middle) + Vector2.down * Screen.height*0.25f;
			}
		}

		if (!TitleSelectProgression.helpEnable) {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
			HelpPanel.hOpenFrag = HelpPanel.HelpOpenFrag.None;
		}
	}
}
