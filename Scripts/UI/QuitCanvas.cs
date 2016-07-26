using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitCanvas : MonoBehaviour {

	public void CancelButton () {
		isOpenQuitCanvas = false;
	}

	public void QuitButton () {
		Application.Quit();
	}
	bool isOpenQuitCanvas;
	#if UNITY_ANDROID

	GameObject panel;

	private static QuitCanvas instance = null;
	public static QuitCanvas Instance { get { return instance; } }
	void Awake () {
		if (instance != null && instance != this) { Destroy (this.gameObject); return; }
		else { instance = this; }
		DontDestroyOnLoad (this.gameObject);
	}


	void Start () {
		isOpenQuitCanvas = false;
		panel = gameObject.transform.GetChild (0).gameObject;

		//	テキストの代入
		GameObject menuPanel = panel.transform.GetChild (0).gameObject;
		menuPanel.transform.GetChild (0).gameObject.GetComponent<Text> ().text = Info_StringText.quitNameText [Info_StringText.textLanguage];
		menuPanel.transform.GetChild (1).gameObject.GetComponent<Text> ().text = Info_StringText.quitDescriptionText [Info_StringText.textLanguage];
		GameObject bbPanel = menuPanel.transform.GetChild (2).gameObject;
		bbPanel.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ().text = Info_StringText.quitCancel [Info_StringText.textLanguage];
		bbPanel.transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ().text = Info_StringText.quitExit [Info_StringText.textLanguage];
	}

	void Update () {

		if (isOpenQuitCanvas) {
			GameProgression.isPause = true;
			panel.transform.position = Mover.UBPosition (Mover.UiBasePos.Middle);
		} else {
			panel.transform.position = Mover.UBPosition (Mover.UiBasePos.Left);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			isOpenQuitCanvas = true;
		}
	}
	#endif
}
