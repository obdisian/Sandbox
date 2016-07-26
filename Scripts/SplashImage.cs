using UnityEngine;
using System.Collections;

public class SplashImage : MonoBehaviour {

	int count = 0;

	void Start () {
		Info_StringText.textLanguage = PlayerPrefs.GetInt ("Language", -1) == -1 ?
			Application.systemLanguage == SystemLanguage.Japanese ? 0 : 1 : PlayerPrefs.GetInt ("Language", -1);

		//	フレームレートの設定
//		QualitySettings.vSyncCount = 0;
//		Application.targetFrameRate = 60;
//		Time.captureFramerate = 60;

		//	GameCenterへアクセス
		RankingUtility.Auth();
	}
	
	void Update () {
		count++;

		if (count < 200) return;

		if (count == 200) {
			SceneTransition.LoadLevel ("TitleSelect", SceneTransition.TransitionType.FadeScreen, Color.black);
		}
	}
}
