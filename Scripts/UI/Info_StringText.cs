using UnityEngine;
using System.Collections;

public class Info_StringText : MonoBehaviour {

	//	0=日本語、1=英語
	public static int textLanguage;

	//	セレクト画面
	//	============================================

	//	プレイ回数
	public static string[] playCountText = {
		"各ステージのプレイ回数",
		"Times played for each stage"
	};
	public static string[] firstPlayCountText = {
		"初クリアするまでのプレイ回数",
		"Times played for 1st time clear",
	};

	//	ヘルプ画面
	public static string[] helpText_Jump = {
		"タップ、ホールドにてジャンプ",
		"TAP or HOLD to jump",
	};
	public static string[] helpText_Reverse = {
		"触れると天地が入れ替わる",
		"FlipSwitch:\n Flips the stage vertically",
	};
	public static string[] helpText_Force = {
		"触れると強制的に大ジャンプ",
		"JumpSwitch:\n Force you to make a large jump",
	};
	public static string[] helpText_Fast_Slow = {
		"触れると早くなったり遅くなったり",
		"Fast-foward & Slow:\n Let you speed up or slow down",
	};
	public static string[] helpText_Rejump = {
		"触れると空中にてもう一度ジャンプできる",
		"ReJumpSwitch:\n You can jump again in the air",
	};
	public static string[] helpText_Colorblock = {
		"触れた色のスイッチと同じ色のブロックが壁や地面になる",
		"ColorSwitch:\n Changes blocks of the same color into walls and floorboards",
	};

	//	ロック画面
	public static string[] keyPanelText_1 = {
		"1〜5の内、3つクリアで解放",
		"Unlocks when three stages of 1-5 are cleared",
	};
	public static string[] keyPanelText_2 = {
		"6〜10の内、3つクリアで解放",
		"Unlocks when three stages of 6-10 are cleared",
	};

	//	終了画面(Android)
	public static string[] quitNameText = {
		"終了メニュー",
		"Exit menu",
	};
	public static string[] quitDescriptionText = {
		"アプリケーションを終了しますか？",
		"Do you want to exit the application?",
	};
	public static string[] quitCancel = {
		"キャンセル",
		"Cancel",
	};
	public static string[] quitExit = {
		"終了",
		"Exit",
	};

	public static string[] rankingText = {
		"ランキング",
		"Ranking",
	};

	public static string[] skinDescriptionText_1 = {
		"1〜5の全てをクリアで解放",
		"Unlocks when all stages of 1-5 are cleared",
	};
	public static string[] skinDescriptionText_2 = {
		"6〜10の全てをクリアで解放",
		"Unlocks when all stages of 6-10 are cleared",
	};
	public static string[] skinDescriptionText_3 = {
		"11〜15の全てをクリアで解放",
		"Unlocks when all stages of 11-15 are cleared",
	};

	public static string[] allStageClearText = {
		"全てのステージをクリアしたら解放",
		"Unlocks when all stages are cleared",
	};

	public static string[,] effectText = {
		{ "通常", "Normal" },
		{ "反転", "Reversal" },
		{ "モザイク", "Mosaic" },
		{ "波", "Wave" },
	};


	//	ゲーム画面
	//	============================================

	public static string[] shareText = {
		"シェアする",
		"Share",
	};

	public static string[] arrivalFactorText = {
		"到達率",
		"Achievement",
	};
	//	st nd rd thの切り替え分
	public static string ArrivalPlayCountText (int number) {

		if (textLanguage == 0) {
			if (Stage.isGoal && !Stage.isSecondPlay) {
				return "一発クリア!!";
			}
			return number + "回目のプレイ";
		}
		if (Stage.isGoal && !Stage.isSecondPlay) {
			return "No continue clear!!";	//	一発クリア!!
		}

		string txt = " play";

		switch (number % 100) {
			case 11: case 12: case 13: return number + "th" + txt;
		}
		switch (number % 10) {
			case 1: return number + "st" + txt;
			case 2: return number + "nd" + txt;
			case 3: return number + "rd" + txt;
			default: return number + "th" + txt;
		}
	}
}
