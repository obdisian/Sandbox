using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class RankingUtility {

	public enum RankingID {
		TotalScore,
		PlayCount,
	}
	static string[] rankingString = {
		//	iOS
		"High_Score",
		"Play_Count",

		//	Android
		Running01.leaderboard_total_score,
		Running01.leaderboard_total_number_of_times_played,
	};
	//	Android専用
	#if UNITY_ANDROID
	public enum ProgressID {
	Skin_1 = 1, Skin_2, Skin_3, No_continue, Play_1000,
	}
	static string[] progressString = {
		"",
		Running01.achievement_new_skin_unlock_ver1,
		Running01.achievement_new_skin_unlock_ver2,
		Running01.achievement_new_skin_unlock_ver3,
		Running01.achievement_no_continue_clear,
		Running01.achievement_played_over_1000_times,
	};
	#endif


	//ユーザー認証
	public static void Auth() {
		// 認証のため ProcessAuthenticationをコールバックとして登録
		#if UNITY_ANDROID
		PlayGamesPlatform.Activate ();
		#endif

		Social.localUser.Authenticate (success => {
			if (success) {
				Debug.Log ("Authenticated, checking achievements");
				GetMyRanking (RankingID.TotalScore);
			} else {
				Debug.Log ("Failed to authenticate");
			}
		});
	}

	//	ランキング取得関数
	public static int GetMyRanking (RankingID rankId) {
		int rankNumber = 0;
		ILeaderboard leaderboard = Social.CreateLeaderboard ();
		leaderboard.id = GetLeaderboardID (rankId);
		leaderboard.LoadScores (result => {
			rankNumber = leaderboard.localUserScore.rank;
//			RankingPanel.rankingNumber = rankNumber;
			Mover.SaveData_Save (Mover.SaveKey.MyRanking, rankNumber);
		});
		return rankNumber;
	}

	// リーダーボードを表示する
	public static void ShowLeaderboardUI() {
		Social.ShowLeaderboardUI();
	}

	// リーダーボードにスコアを送信する
	public static void ReportScore (long score, RankingID rankingID) {

		string leaderboardID = GetLeaderboardID (rankingID);
	
		Debug.Log ("スコア " + score + " を次の Leaderboard に報告します。" + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			Debug.Log(success ? "スコア報告は成功しました" : "スコア報告は失敗しました");
		});
	}

	//	リーダーボードのIDゲット関数
	static string GetLeaderboardID (RankingID rankId, int addId = 0) {
		#if UNITY_ANDROID
			addId = 2;
		#endif
		return rankingString [(int)rankId + addId];
	}



	#if UNITY_ANDROID
	static bool StageClearCount (int integer) {
		int _1_5 = 0, _6_10 = 0, _11_15 = 0;
		for (int i = 0; i < 15; i++) {
			if (100 == Score.mapScore [i]) {
				if (i < 5) _1_5++; else if (i < 10) _6_10++; else if (i < 15) _11_15++;
			}
		}
		if (integer == 0 || integer == 1 && _1_5 == 5 || integer == 2 && _6_10 == 5 || integer == 3 && _11_15 == 5) {
			return true;
		} else {
			return false;
		}
	}

	public static void ReportProgress (ProgressID pID) {

		if ((int)pID > 0 && (int)pID <= 3 && StageClearCount ((int)pID)) {
		} else if (pID == ProgressID.No_continue) {
		} else if (pID == ProgressID.Play_1000) {
			int totalPlayCount = 0;
			foreach (int pc in PlayCount.playCount) {
				totalPlayCount += pc;
			}
			if (totalPlayCount >= 1000) {
			} else {
				return;
			}
		} else {
			return;
		}

		//	上のすべてをクリアした場合、プッシュする
		Social.ReportProgress(progressString [(int)pID], 100.0f, (bool success) => {
			if(success) {
			}
		});
	}
	#endif
}