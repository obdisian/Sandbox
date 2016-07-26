using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OptionButton : MonoBehaviour {

	public static bool openWindow;

	void Start () {
		if (gameObject.name != "OptionPanel") return;

		openWindow = false;
		transform.position = Mover.UBPosition (Mover.UiBasePos.Top);
	}
	
	void Update () {
		if (gameObject.name != "OptionPanel") return;

		if (SceneManager.GetActiveScene ().name == "TitleSelect" && TitleSelectProgression.scenes == TitleSelectProgression.Scenes.Option ||
			SceneManager.GetActiveScene ().name == "Game" && openWindow) {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
		} else {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Top), 0.1f);
		}
	}


	//	以下、オプションボタンのトリガー用
	public void OpenOption () {
		if (SceneManager.GetActiveScene ().name == "TitleSelect") {
			TitleSelectProgression.scenes = TitleSelectProgression.Scenes.Option;
		}
		openWindow = true;
	}
	public void CloseOption () {
		if (SceneManager.GetActiveScene ().name == "TitleSelect") {
			TitleSelectProgression.scenes = TitleSelectProgression.Scenes.Select;
		}
		openWindow = false;
	}
}
