using UnityEngine;
using System.Collections;

public class TitleSelectProgression : MonoBehaviour {

	public static Color[] sgColor = {
		new Color (0, 255, 0, 255)/255.0f,
		new Color (0, 255, 123, 255)/255.0f,
		new Color (0, 255, 187, 255)/255.0f,
		new Color (0, 255, 251, 255)/255.0f,
		new Color (0, 196, 255, 255)/255.0f,
		new Color (0, 132, 255, 255)/255.0f,
		new Color (0, 68, 255, 255)/255.0f,
		new Color (0, 4, 255, 255)/255.0f,
		new Color (60, 0, 255, 255)/255.0f,
		new Color (123, 0, 255, 255)/255.0f,
		new Color (187, 0, 255, 255)/255.0f,
		new Color (251, 0, 255, 255)/255.0f,
		new Color (255, 0, 196, 255)/255.0f,
		new Color (255, 0, 132, 255)/255.0f,
		new Color (255, 0, 68, 255)/255.0f,
	};

	public static bool bgmEnable = true;
	public static bool seEnable = true;
	public static bool skinEnable = false;
	public static bool helpEnable = false;
	public static bool creatorEnable = false;


	public static bool lock_Stage2 = true;
	public static bool lock_Stage3 = true;


	public static bool buttonReleased = false;


	public enum PlayerSkin {
		Normal,
		Map01,
		Map02,
		Map03,
	}
	public static PlayerSkin playerSkin;


	public enum Scenes {
		Title,
		Select,
		Option,
		Game,
	}
	public static Scenes scenes;
	public static TitleSelectPanel.Panel nowPanel;
	TitleSelectPanel.Panel prevPanel;

	public static bool isModeStart = false;
	int startCounter = 0;

	//	シーンパネルを動かす重要な奴
	int addPanel = 1;

	void Awake () {
		Mover.FrameRate_Control (60);

		buttonReleased = false;


		//	seの登録
		#if UNITY_EDITOR || UNITY_IOS
		Sound.LoadSe ("Jump", "SND_SE00");
		Sound.LoadSe ("Crush", "SND_SE01");
		Sound.LoadSe ("Frag", "SND_SE02");
		Sound.LoadSe ("Reverse", "SND_SE03");
		Sound.LoadSe ("Force", "SND_SE04");
		Sound.LoadSe ("Fast", "SND_SE05");
		Sound.LoadSe ("Slow", "SND_SE06");
		Sound.LoadSe ("Rejump", "SND_SE07");
		Sound.LoadSe ("ColorBlock", "SND_SE08");
		Sound.LoadSe ("Goal", "SND_SE09");
		#endif

		//	データのリセット
//		for (int i = 0; i < 15; i++) {
//			PlayerPrefs.SetInt ("MapScore_" + i, 100);
//			PlayerPrefs.SetInt ("FirstClearCount_" + i, 50 + i * 10);
//			PlayerPrefs.SetInt ("playCount_" + i, 100 + i * 10);
//		}

		#if UNITY_ANDROID
		RankingUtility.ReportProgress (RankingUtility.ProgressID.Skin_1);
		RankingUtility.ReportProgress (RankingUtility.ProgressID.Skin_2);
		RankingUtility.ReportProgress (RankingUtility.ProgressID.Skin_3);
		RankingUtility.ReportProgress (RankingUtility.ProgressID.Play_1000);
		#endif

		//	累計カウンタの初期化
		for (int i = 0; i < 15; i++) {
			Stage.isCumulative [i] = false;
		}
	}

	void Start () {
		Stage.playCount = 1;

		prevPanel = nowPanel;

		//	ステージアンロック条件更新
		int _1_5 = 0, _6_10 = 0;
		for (int i = 0; i < 10; i++) {
			if (100 == Score.mapScore [i]) {
				if (i < 5) _1_5++;
				else if (i < 10) _6_10++;
			}
		}
		if (_1_5 >= 3) lock_Stage2 = false;
		if (_6_10 >= 3) lock_Stage3 = false;
	}
	
	void Update () {

		//	パネルに合わせて曲を慣らしていく
		if (nowPanel == TitleSelectPanel.Panel.Title ||
		    nowPanel == TitleSelectPanel.Panel.Select_1) {
			BgmManager.Instance.Play ("SND_BGM_Stage1");
		} else if (nowPanel == TitleSelectPanel.Panel.Select_2) {
			BgmManager.Instance.Play ("SND_BGM_Stage2");
		} else if (nowPanel == TitleSelectPanel.Panel.Select_3) {
			BgmManager.Instance.Play ("SND_BGM_Stage3");
		}



		if (prevPanel != nowPanel) {
			if (prevPanel == TitleSelectPanel.Panel.Title) {
				isModeStart = true;
				nowPanel = TitleSelectPanel.Panel.Select_3;
			}
			prevPanel = nowPanel;
		}

		if (isModeStart) {
			startCounter++;
			int num = 60;
			if (startCounter == num || startCounter == num * 2) {
				nowPanel--;
			}
			if (startCounter > num * 2) {
				isModeStart = false;
			}
		} else {

			//	セレクト画面のタッチ処理系統
			if (Input.GetMouseButtonUp (0)) {
				//	タイトルからの一方通行処理
				if (nowPanel == TitleSelectPanel.Panel.Title) {
					nowPanel = TitleSelectPanel.Panel.Select_1;
				} else if (nowPanel != TitleSelectPanel.Panel.Title) {
					//	オプション、サブオプションパネル、スクリーンエフェクトパネルが開いてない時にスクロール
					if (!SubOptionPanel.openSubOptionPanel && !OptionButton.openWindow && !ScreenEffectSelect.isOpenScreenEffectPanel) {
						//	ゲーム画面行きのボタンが押されてなければ
						if (!buttonReleased) {
							//	スワイプした長さによってスライドするか決める
							if (Mathf.Abs (TouchController.swipeVec.y) > 10.0f) {
								nowPanel += TouchController.swipeVec.y > 0 ? addPanel : -addPanel;
							}
						}
					}
					//	ページの制限
					nowPanel = (TitleSelectPanel.Panel)Mover.Constrain ((int)nowPanel, (int)TitleSelectPanel.Panel.Select_1, (int)TitleSelectPanel.Panel.End - 1);
				}
			}
		}
	}
}
