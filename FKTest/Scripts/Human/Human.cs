
using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

	public GameObject joint;


	Vector3[] jointPos = {
		new Vector3 (0, 0, 0),
		new Vector3 (0, 1, 0),

		new Vector3 (0.75f, 0.75f, 0),
		new Vector3 (-0.75f, 0.75f, 0),

		new Vector3 (0.45f, -0.75f, 0),
		new Vector3 (-0.45f, -0.75f, 0),
	};
	Vector3[] partsScale = {
		new Vector3 (1, 1.5f, 0.5f),
		new Vector3 (0.75f, 0.75f, 0.5f),

		new Vector3 (0.25f, 0.75f, 0.25f),
		new Vector3 (0.25f, 0.75f, 0.25f),
		new Vector3 (0.25f, 0.75f, 0.25f),
		new Vector3 (0.25f, 0.75f, 0.25f),
	};


	public enum State {
		Wait,
		Walk,

		Crash,
	}
	public State state, prevState;

	int timer;

	//	基盤オブジェクト
	HumanFK humanFK;

	public HumanFK[] humanJoints = new HumanFK [10];


	//	現在のモーションキー
	int motionKey = 0;

	//++++++++++++++++++++++++++++++++++++++++
	//	モーションリスト
	//	継承先で絶対にモーションリストを設定する
	//++++++++++++++++++++++++++++++++++++++++
	protected MotionData[] motionList = new MotionData[(int)State.Crash];


	//================================================================================
	//	関節作成
	//================================================================================
	GameObject createJoint (bool isRoot, GameObject owner, Vector3 pos, int createCount, Vector3 partsPos, Vector3 partsScale) {
		GameObject obj = Instantiate (joint, pos, Quaternion.identity) as GameObject;
		obj.GetComponent<HumanFK> ().Setup (owner, createCount, joint, partsPos, partsScale);
		if (isRoot) {
			humanFK = obj.GetComponent<HumanFK> ();
		} else {
			humanFK.children.Add (obj.GetComponent<HumanFK> ());
		}
		return obj;
	}


	//================================================================================
	//	初期化
	//================================================================================
	public void Start () {

		//	親作成
		GameObject root = createJoint (true, gameObject, jointPos[0], 0, Vector3.zero, partsScale[0]);
		//	子作成
		for (int i = 1; i < jointPos.Length; i++) {
			createJoint (false, root, jointPos [i], i == 1 ? 0 : 1, i == 1 ? Vector3.up : Vector3.down, partsScale[i]);
		}

		//	めんどいので直で配列に設定
		humanJoints [0] = humanFK;
		humanJoints [1] = humanFK.children [0];
		humanJoints [2] = humanFK.children [1];
		humanJoints [3] = humanFK.children [2];
		humanJoints [4] = humanFK.children [3];
		humanJoints [5] = humanFK.children [4];
		humanJoints [6] = humanFK.children [1].children [0];
		humanJoints [7] = humanFK.children [2].children [0];
		humanJoints [8] = humanFK.children [3].children [0];
		humanJoints [9] = humanFK.children [4].children [0];
	}


	//================================================================================
	//	更新
	//================================================================================
	public void Update () {

		timer++;

		ChangeState ();
			
		StateMove ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			state = State.Crash;
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			state = State.Wait;
		}

		//	一つずつバラバラに壊れる
		if (Input.GetKeyDown (KeyCode.Q)) {
			for (int i = humanJoints.Length - 1; i >= 0; i--) {
				if (!humanJoints [i].IsCrash) {
					humanJoints [i].IsCrash = true;
					break;
				}
			}
		}
	}


	//================================================================================
	//	状態遷移直後
	//================================================================================
	void ChangeState () {
		
		if (state == prevState) {
			return;
		}
		timer = 0;

		if (state == State.Crash) {
			for (int i = 0; i < humanJoints.Length; i++) {
				humanJoints [i].IsCrash = true;
			}
		} else if (prevState == State.Crash) {
			for (int i = 0; i < humanJoints.Length; i++) {
				humanJoints [i].IsCrash = false;
			}
		}
		prevState = state;
	}


	//================================================================================
	//	状態毎の処理
	//================================================================================
	void StateMove () {
		if (state == State.Wait) {
			MoveRot ();
		}
		else if (state == State.Walk) {
			MoveRot ();
		}
		else if (state == State.Crash) {
		}
	}


	//================================================================================
	//	回転更新
	//================================================================================
	void MoveRot () {

		int sNum = (int)state;

		//	非ループ処理
		if (!motionList [sNum].IsLoop) {
			if (timer >= motionList [sNum].MaxFrame - 1) {
				timer = motionList [sNum].MaxFrame - 1;
			}
		}

		//	現在のフレームと照らし合わせてモーション番号を作成
		for (int i = 0; i < motionList [sNum].KeyFrame.Length; i++) {
			if (timer % motionList [sNum].MaxFrame == motionList [sNum].KeyFrame [i]) {
				motionKey = i;
				break;
			}
		}

		//	モーションの設定
		humanJoints [0].BasePos = Vector3.Lerp (humanJoints [0].BasePos, motionList [sNum].Pos [motionKey], motionList [sNum].SlerpT [motionKey]);
		for (int i = 0; i < humanJoints.Length; i++) {
			humanJoints [i].SlerpT = motionList [sNum].SlerpT [motionKey];
			humanJoints [i].BaseAngle = motionList [sNum].Angles [motionKey][i];
		}
	}
}
