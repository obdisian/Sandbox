using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour {


	//	次にやっておきたいこと
	//	TransitionTypeをいろいろ追加する
	//	TransitionTypeとColorをstaticでとっておいて、シーン移動後も保持しておきたい
	//	忘れないように


	public enum TransitionType {
		FadeScreen,
	}
	public enum SceneState {
		StartTransition,
		EndTransition,
	}
	public static SceneState sceneState;

	static GameObject fadeScreen;

	void Awake () {
		//	フレームレートの設定
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		fadeScreen = (GameObject)Resources.Load ("Fade/FadeScreen");
	}

	void Start () {

		//	シーン開始時のフェードのロード。遷移するわけではないので第一引数は""を渡す
		if (sceneState == SceneState.EndTransition) {
			LoadLevel ("", TransitionType.FadeScreen, Color.black);
		}
	}
//	
//	void Update () {
//
//	}

	//	シーン遷移
	public static void LoadLevel (string sceneName, TransitionType tType, Color color, float speed = 1) {

		switch (tType) {
		case TransitionType.FadeScreen: CreateFadeScreen (sceneName, color, speed); break;
		}
	}

	//	以下、各シーン遷移画面の作成
	static void CreateFadeScreen (string sceneName, Color color, float t) {
		GameObject obj = (GameObject)Instantiate (fadeScreen, Vector2.zero, Quaternion.identity);
		obj.transform.SetParent (GameObject.Find ("FadeCanvas").transform, false);
		obj.GetComponent<FadeScreen> ().Init (sceneName, t, color);
	}
}
