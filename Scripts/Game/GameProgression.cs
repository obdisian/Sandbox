using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameProgression : MonoBehaviour {

	static GameObject ringEffect;
	static GameObject crushEffect;
	static GameObject[] rectEffect = new GameObject[4];

	public static bool isPause;
	public static bool isJumpTouch;

	public static int gameCounter = 30;

	//	復帰できる回数
	public static int returnCount = 5;

	public enum ColorFlag {
		Red, Blue, Green,
	}
	public static ColorFlag colorFlag;

	void Awake () {
		ringEffect = (GameObject)Resources.Load ("Ring");

		crushEffect = (GameObject)Resources.Load ("CrushEffect");
		for (int i = 0; i < 4; i++) {
			rectEffect [i] = (GameObject)Resources.Load ("RectParticle_" + (i+1));
		}

		//	ポーズの初期化
		//	AnimationControllerのUpdateにポーズを初期化している(現在は違う)
		//	ポーズ解放はStageのOnDestroyにて行っている
		isPause = true;

		LoadResources ();

		//	プレイヤーのジャンプするためのタッチフラグ
		//	消すのはtrueにするのはTapToPlayで、falseにするのはプレイヤー側
		//	ポーズ対象のプレイヤーはtrueにすることができないため
		isJumpTouch = false;

		gameCounter = 0;
		returnCount = 5;

		colorFlag = ColorFlag.Red;
	}

	void Update () {
		PauseSwitch ();

		gameCounter++;

		//	バックグラウンドの設定
		if (0 < (int)Stage.stageMapList) {
			TitleSelectProgression.nowPanel = TitleSelectPanel.Panel.Select_1;
		}
		if (5 < (int)Stage.stageMapList) {
			TitleSelectProgression.nowPanel = TitleSelectPanel.Panel.Select_2;
		}
		if (10 < (int)Stage.stageMapList) {
			TitleSelectProgression.nowPanel = TitleSelectPanel.Panel.Select_3;
		}

		//	パネルに合わせて曲を慣らしていく
		if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Title ||
			TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_1) {
			BgmManager.Instance.Play ("SND_BGM_Stage1");
		}
		else if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_2) {
			BgmManager.Instance.Play ("SND_BGM_Stage2");
		}
		else if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_3) {
			BgmManager.Instance.Play ("SND_BGM_Stage3");
		}

		if (Stage.isGoal) {
			MainCamera.mCamera.GetComponent<GoalEffect> ().enabled = true;
		}
	}

	//	ポーズの切り替え
	void PauseSwitch () {
		if (isPause) {
			Pauser.Pause ();
		} else {
			Pauser.Resume ();
		}
	}


	public static void CreateRingEffect (GameObject owner, Color color) {
		GameObject obj = (GameObject)Instantiate (ringEffect, owner.transform.position, Quaternion.identity);
		obj.GetComponent<RingEffect> ().Init (color);
	}

	public static void CreateCrushEffect (Vector2 pos) {
		Instantiate (crushEffect, pos, Quaternion.identity);
		for (int i = 0; i < 50; i++) {
			Instantiate (rectEffect [Random.Range (0, 4)], pos, Quaternion.identity);
		}
	}


	//	=================以下、ステージオブジェクト作成系=================


	//	順番に気をつける。両方追加すること
	public enum ResourceObject {
		Block, Reverse, Needle, Needle2, ReJump, Stop,
		Flag, BarBlock, FallBlock, SpeedUp, SpeedDown,
		ColorChangeFlag, ColorBlock, ColorBlock_2, ForceJump,

		End_Size,
	}
	string[] ResourceObject_Tag = {
		"Block", "Reverse", "Needle", "Needle2", "ReJump", "Stop",
		"Flag", "BarBlock", "FallBlock", "SpeedUp", "SpeedDown",
		"ColorChangeFlag", "ColorBlock", "ColorBlock_2", "ForceJump",
	};

	static GameObject[] mapObject = new GameObject[(int)ResourceObject.End_Size];

	//	リソースのロード
	void LoadResources () {
		for (int i = 0; i < (int)ResourceObject.End_Size; i++) {
			mapObject [i] = (GameObject)Resources.Load (ResourceObject_Tag [i]);

			//	ちゃんと取得できているか一応の確認用
//			Debug.Log ("enum " + ResourceObject_Tag [i] + " : string " + mapObject[i].name);
		}
	}


	public static GameObject CreateMapObj (ResourceObject rObj, Vector3 pos, int angle) {
		GameObject obj = mapObject [(int)rObj];
		return (GameObject)Instantiate (obj, pos, Quaternion.AngleAxis (angle, Vector3.forward));
	}

	public static GameObject CreateMapObj_Block (Vector3 pos, string blockName = "") {
		GameObject obj = (GameObject)Instantiate (mapObject [(int)ResourceObject.Block], pos, Quaternion.identity);
		if (blockName == "TopBlock" || blockName == "BottomBlock") {
			obj.tag = blockName;
		}
		return obj;
	}

	public static GameObject CreateMapObj_BarBlock (Vector3 pos, int scale, float rotSpeed) {
		GameObject obj = (GameObject)Instantiate (mapObject [(int)ResourceObject.BarBlock], pos, Quaternion.identity);
		obj.transform.SetLocalScaleY (scale*0.048828125f);	//	画像の差し替えに当たって
		obj.GetComponent<BarBlock> ().rotateSpeed = rotSpeed;
		return obj;
	}

	public static GameObject CreateMapObj_ColorChangeFlag (Vector3 pos, int color) {
		GameObject obj = (GameObject)Instantiate (mapObject [(int)ResourceObject.ColorChangeFlag], pos, Quaternion.identity);
		obj.GetComponent<ColorChangeFlag> ().colorFlag = (GameProgression.ColorFlag)color;
		return obj;
	}

	public static GameObject CreateMapObj_ColorBlock (Vector3 pos, int color, int XorY, int scale) {
		GameObject obj = null;
		//= (GameObject)Instantiate (mapObject [(int)ResourceObject.ColorBlock], pos, Quaternion.identity);
		if (XorY == 0) {
			obj = (GameObject)Instantiate (mapObject [(int)ResourceObject.ColorBlock], pos, Quaternion.AngleAxis (90, Vector3.forward));
			obj.transform.SetLocalScaleY (scale/0.65f+0.1f);
			obj.GetComponent<ColorBlock> ().colorFlag = (GameProgression.ColorFlag)color;
		} else if (XorY == 1) {
			obj = (GameObject)Instantiate (mapObject [(int)ResourceObject.ColorBlock_2], pos, Quaternion.identity);
			obj.transform.SetLocalScaleY (scale/0.65f+0.1f);
			obj.GetComponent<ColorBlock> ().colorFlag = (GameProgression.ColorFlag)color;
		}
		return obj;
	}
}
