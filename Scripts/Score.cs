using UnityEngine;
using System.Collections;
using System.IO;

public class Score : MonoBehaviour {

	//	累計スコアは1000づつ獲得
	//	Stage.csとTitleSelectProgression.csに累計計算記入

	//	アクティブなステージ数
	public static int[] mapScore = new int[15];		//	%のスコア
	public static int[] rankScore = new int[15];	//	ノーコンボーナススコア
	public static int[] cumuScore = new int[15];	//	累計ボーナススコア

	public static void SaveMapScore (int n, int sc, int rsc = 0) {
		mapScore [n] = mapScore [n] < sc ? sc : mapScore [n];
		rankScore [n] = rankScore [n] < rsc ? rsc : rankScore [n];
		ScoreSave ();

		//	ステージアンロック条件更新
		int _1_5 = 0, _6_10 = 0;
		for (int i = 0; i < 10; i++) {
			if (100 == Score.mapScore [i]) {
				if (i < 5) _1_5++;
				else if (i < 10) _6_10++;
			}
		}
		if (_1_5 >= 3) TitleSelectProgression.lock_Stage2 = false;
		if (_6_10 >= 3) TitleSelectProgression.lock_Stage3 = false;

		//	iOSランキングに送信（後で一括送信に書き換えよう）
		RankingUtility.ReportScore (TotalScore (), RankingUtility.RankingID.TotalScore);
	}
	static int TotalScore () {
		int total = 0;
//		foreach (int score in mapScore) {
//			total += score;
//		}
		for (int i = 0; i < 15; i++) {
			total += mapScore [i] + rankScore [i] + cumuScore [i];
		}
		return total;
	}

	void Awake () {
		for (int i = 0; i < 15; i++) {
			mapScore [i] = Mathf.Min (100, PlayerPrefs.GetInt ("MapScore_" + i, 0));
			rankScore [i] = Mathf.Min (100 * i, PlayerPrefs.GetInt ("RankScore_" + i, 0));
			cumuScore [i] = Mathf.Min (100 * i, PlayerPrefs.GetInt ("CumuScore_" + i, 0));
		}
	}

	static void ScoreSave () {
		for (int i = 0; i < 15; i++) {
			PlayerPrefs.SetInt ("MapScore_" + i, Mathf.Min (100, mapScore [i]));
			PlayerPrefs.SetInt ("RankScore_" + i, Mathf.Min (100 * (i+1), rankScore [i]));
			PlayerPrefs.SetInt ("CumuScore_" + i, Mathf.Min (1000 * (i+1), cumuScore [i]));
		}
	}
}
