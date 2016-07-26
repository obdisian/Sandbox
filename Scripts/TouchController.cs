using UnityEngine;
using System.Collections;

public class TouchController : Mover {

	Vector3 standPos, mouseOrTouchVec;

	public static bool touchTrigger;		//押してるかどうか
	public static Vector3 swipeVec;			//スワイプの移動ベクトル
	public static Vector3 swipeVecNormal;	//上の単位ベクトル
	public static Vector3 touchMoveVec;		//前フレームとのベクトルの差分

	void Start () {
		touchTrigger = false;
		standPos = mouseOrTouchVec = swipeVec = swipeVecNormal = touchMoveVec = Vector3.zero;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			pos = prevPos = swipeVec = swipeVecNormal = touchMoveVec = Vector3.zero;

			mouseOrTouchVec = Input.mousePosition;
			touchTrigger = true;
			pos = prevPos = standPos = mouseOrTouchVec;
		} else if (Input.GetMouseButtonUp (0)) {
			touchTrigger = false;
		}
		if (touchTrigger) {
			if (pos != prevPos) {
				touchMoveVec = pos - prevPos;
				prevPos = pos;
			} else {
				touchMoveVec = Vector3.zero;
			}
			pos = Input.mousePosition;
			swipeVec = pos - standPos + Vector3.up * 0.001f;
			swipeVecNormal = Normalize (swipeVec);
		}
//		else {
//			pos = prevPos = swipeVec = swipeVecNormal = touchMoveVec = Vector3.zero;
//		}
	}
}
