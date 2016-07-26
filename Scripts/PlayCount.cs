using UnityEngine;
using System.Collections;
using System.IO;

public class PlayCount : MonoBehaviour {

	public static int[] playCount = new int[15];

	public static void SavePlayCount (int n) {
		playCount [n]++;

		//	セーブ
		for (int i = 0; i < 15; i++) {
			PlayerPrefs.SetInt ("playCount_" + i, playCount [i]);
		}

		//	トータルプレイ回数を送信
		int totalPlayCount = 0;
		foreach (int pc in playCount) {
			totalPlayCount += pc;
		}
		RankingUtility.ReportScore (totalPlayCount, RankingUtility.RankingID.PlayCount);
	}

	void Awake () {
		//	プレイ回数の読み込み
		for (int i = 0; i < 15; i++) {
			playCount [i] = Mathf.Max (0, Mathf.Min (PlayerPrefs.GetInt ("playCount_" + i, 0), 999999));
		}
	}
}
