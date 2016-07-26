
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : Mover {

	//	読み込むオブジェクトの取得
	static GameObject mainCamera, player, goal;

	public enum GameScene {
		Ready, Game, Score
	}
	public enum StageMapList {
		Level_0,
		Level_1, Level_2, Level_3, Level_4, Level_5,
		Level_6, Level_7, Level_8, Level_9, Level_10,
		Level_11, Level_12, Level_13, Level_14, Level_15,
		Level_16, Level_17, Level_18, Level_19, Level_20,
		Level_21, Level_22, Level_23, Level_24, Level_25,
	}

	public static GameScene gameScene;
	public static StageMapList stageMapList;

	//	プレイした回数
	public static int playCount = 1;
	public static int runPercent = 0;
	public static float startPosX;
	public static float endPosX;

	public static bool isTheRePlay;
	public static bool isAddPlayCount;
	public static bool isGoal;

	public static bool isSecondPlay;
	public static bool[] isCumulative = new bool[15];

	//	ビルボード風にするオブジェクト
	public static List<GameObject> billboardObject = new List<GameObject> ();

	void Awake () {
		//	カメラはただの初期位置調整用
		mainCamera = GameObject.Find ("Main Camera");
		player = GameObject.Find ("Player");
		goal = GameObject.Find ("Goal");

		//	残ってるかもしれない過去の要素を排除
		billboardObject.Clear ();
	}

	public static void RunPercent () {
		runPercent = (int)RatioMap (player.transform.position.x, startPosX, endPosX, 0, 100);
		if (isGoal) {
			runPercent = 100;
			if (!isSecondPlay) {
				Score.SaveMapScore ((int)stageMapList-1, runPercent, (int)stageMapList * 100);
				#if UNITY_ANDROID
				RankingUtility.ReportProgress (RankingUtility.ProgressID.No_continue);
				#endif
			}
			//	1_Stage以外のStageで一発クリアして前も同じスコアなら
			if (stageMapList != StageMapList.Level_1 && playCount == 1 && isCumulative [(int)stageMapList - 2]) {
				isCumulative [(int)stageMapList - 1] = true;
				Score.cumuScore [(int)stageMapList - 1] = 1000*(int)stageMapList;
				for (int i = 0; i < 15; i++) {
					Debug.Log ("isCumulative ["+i+"] = "+ isCumulative [i]);
				}
			}
			//	1_Stageのみコンテなしクリアで
			else if (stageMapList == StageMapList.Level_1 && !isSecondPlay) {
				isCumulative [0] = true;
				Score.cumuScore [0] = 1000;
			}
		} else if (runPercent >= 100) {
			runPercent = 99;
		}
		if (stageMapList != StageMapList.Level_1 && !isGoal) {
			for (int i = 0; i < 15; i++) {
				isCumulative [i] = false;
			}
		}
		PlayCount.SavePlayCount ((int)stageMapList-1);
		if (Score.mapScore [(int)stageMapList-1] < 100 || PlayCount.playCount [(int)stageMapList-1] == 1) {
			SaveData.SaveFirstClearCount ((int)stageMapList-1);
		}
		Score.SaveMapScore ((int)stageMapList-1, runPercent);
	}

	void Start () {
		MapCreate (StageMap.map);
		mainCamera.transform.position = player.transform.position + Vector3.back * 10;

		//	スコア用の初期設定
		runPercent = 0;
		startPosX = player.transform.position.x;
		endPosX = goal.transform.position.x;

		isTheRePlay = false;
		isAddPlayCount = false;
		isGoal = false;

		isSecondPlay = false;
	}

	void OnDestroy () {
		if (isAddPlayCount) {
			isAddPlayCount = false;
			playCount++;
		}
		//	要素を排除
		billboardObject.Clear ();
		//	ポーズ解除
		GameProgression.isPause = false;
	}

	//	マップ制作
	void MapCreate (List<string> map) {
		for (int y = 0; y < map.Count; y++) {
			for (int x = 0; x < map [0].Length; x++) {
				Vector3 boxPos = new Vector3 (x * 1.0f, -y * 1.0f, 0);
				Vector3 boxPosLayer_Middle = new Vector3 (x * 1.0f, -y * 1.0f, 0.1f);

				switch (map [y][x]) {
				case 'P': player.transform.position = new Vector2 (boxPos.x, boxPos.y); break;
				case 'G': goal.transform.position = new Vector2 (boxPos.x, boxPos.y); break;

				case '#': GameProgression.CreateMapObj_Block (boxPos); break;
				case '[': GameProgression.CreateMapObj_Block (boxPos, "TopBlock"); break;
				case ']': GameProgression.CreateMapObj_Block (boxPos, "BottomBlock"); break;

				//	落ちていくブロック（ _ = 下, - = 上 ）
				case '_': GameProgression.CreateMapObj (GameProgression.ResourceObject.FallBlock, boxPos + Vector3.up * 0.38f, 0); break;
				case '-': GameProgression.CreateMapObj (GameProgression.ResourceObject.FallBlock, boxPos + Vector3.down * 0.38f, 180); break;

				case '^': GameProgression.CreateMapObj (GameProgression.ResourceObject.Needle2, boxPos, 0); break;
				case 'v': GameProgression.CreateMapObj (GameProgression.ResourceObject.Needle2, boxPos, 180); break;
				case '*': GameProgression.CreateMapObj (GameProgression.ResourceObject.Needle, boxPos, 0); break;
				
				case '@': GameProgression.CreateMapObj (GameProgression.ResourceObject.Reverse, boxPos, 0); break;

				case 'J': billboardObject.Add (GameProgression.CreateMapObj (GameProgression.ResourceObject.ReJump, boxPos, 0)); break;
				case 'K': billboardObject.Add (GameProgression.CreateMapObj (GameProgression.ResourceObject.ForceJump, boxPos, 0)); break;

				case 'F': GameProgression.CreateMapObj (GameProgression.ResourceObject.Flag, boxPosLayer_Middle, 0); break;
				case 'S': GameProgression.CreateMapObj (GameProgression.ResourceObject.Stop, boxPosLayer_Middle, 0); break;

				//	スピードアップ、ダウン
				case 'u': GameProgression.CreateMapObj (GameProgression.ResourceObject.SpeedUp, boxPos, 0); break;
				case 'd': GameProgression.CreateMapObj (GameProgression.ResourceObject.SpeedDown, boxPos, 0); break;
				
				case 'b': GameProgression.CreateMapObj_BarBlock (boxPos, int.Parse ("" + map [y][x+1]) * 2, 5); break;

				case 'c': GameProgression.CreateMapObj_ColorChangeFlag (boxPos, int.Parse ("" + map [y-1][x])); break;
				case 'C': GameProgression.CreateMapObj_ColorBlock (boxPos, int.Parse ("" + map [y-1][x]), int.Parse ("" + map [y+1][x]), int.Parse ("" + map [y][x+1])); break;
				}
			}
		}
	}
}
