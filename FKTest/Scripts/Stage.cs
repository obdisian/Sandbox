using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

	public GameObject human;

	void Awake () {
		Info.FPS (60);
	}

	void Start () {
//		for (int x = -5; x < 5; x++) {
//			for (int y = 0; y < 5; y++) {
//				GameObject obj = Instantiate (human, new Vector3 (x * 5, 0, y * 5), Quaternion.identity) as GameObject;
//				obj.transform.rotation = Quaternion.AngleAxis (180, Vector3.up);
//				obj.GetComponent<Human> ().state = Human.State.Gymnastics;
//			}
//		}

//		GameObject obj = Instantiate (human, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
//		obj.transform.rotation = Quaternion.AngleAxis (180, Vector3.up);
//		obj.GetComponent<Human> ().isPlayer = true;


		//	データの作成
		Data.Setup ();
//		MotionData testData = new MotionData ();
//		testData.IsLoop = 1;
//		testData.MaxFrame = 60;
//		testData.KeyFrame = new int[] { 0, 30 };
//		testData.SlerpT = new float[] { 0.2f, 0.2f };
//		testData.Pos = new Vector3[] { Vector3.zero, Vector3.zero };
//		testData.Angles = angleWalk;
//		Data.Save ("基本_歩き", testData);
//		testData.Angles = angleWait;
//		Data.Save ("基本_待機", testData);


		//	ログ
//		MotionData m = Data.Load ("野村");
//		Debug.Log ("IsLoop = " + m.IsLoop);
//		Debug.Log ("MaxFrame = " + m.MaxFrame);
//
//		for (int i = 0; i < m.KeyFrame.Length; i++) {
//			Debug.Log ("KeyFrame " + i + " = " + m.KeyFrame [i]);
//		}
//
//		for (int i = 0; i < m.SlerpT.Length; i++) {
//			Debug.Log ("SlerpT " + i + " = " + m.SlerpT [i]);
//		}
//
//		for (int i = 0; i < m.Pos.Length; i++) {
//			Debug.Log ("Pos " + i + " = " + m.Pos [i]);
//		}
//
//		for (int i = 0; i < m.Angles.Length; i++) {
//			for (int j = 0; j < m.Angles [i].Length; j++) {
//				Debug.Log ("Angles" + i + " " + j + " = " + m.Angles [i][j]);
//			}
//		}
	}
	
	void Update () {
	
	}
}
