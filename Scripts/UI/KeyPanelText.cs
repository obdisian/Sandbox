using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyPanelText : MonoBehaviour {

	Text text;

	void Start () {
		text = GetComponent<Text> ();
	}
	
	void Update () {
		if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_2) {
			text.text = Info_StringText.keyPanelText_1 [Info_StringText.textLanguage];
		}
		else if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_3) {
			text.text = Info_StringText.keyPanelText_2 [Info_StringText.textLanguage];
		}
	}
}
