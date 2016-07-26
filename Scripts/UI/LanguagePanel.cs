using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguagePanel : MonoBehaviour {

	public static bool isOpenLanguagePanel;

	GameObject childPanel;
	GameObject[] langObj = new GameObject[2];

	void Start () {
		childPanel = transform.GetChild (0).gameObject;
		langObj [0] = childPanel.transform.GetChild (0).gameObject;
		langObj [1] = childPanel.transform.GetChild (1).gameObject;

		langObj [Info_StringText.textLanguage].GetComponent<Image> ().color = Color.white;

		isOpenLanguagePanel = false;

	}
	void Update () {
		if (isOpenLanguagePanel) {
			transform.localScale = Vector2.one;
		} else {
			transform.localScale = Vector2.zero;
		}
	}

	public void CloseLanguagePanel () {
		isOpenLanguagePanel = false;
	}

	public void SelectLanguage (int i) {
		Info_StringText.textLanguage = i;
		PlayerPrefs.SetInt ("Language", i);
		SceneTransition.LoadLevel ("TitleSelect", SceneTransition.TransitionType.FadeScreen, Color.black);
	}
}
