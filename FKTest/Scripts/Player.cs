using UnityEngine;
using System.Collections;

public class Player : Human {


	//================================================================================
	//	初期化
	//================================================================================
	void Start () {
		base.Start ();

		//	モーションリストに登録
		motionList [0] = Data.Load ("基本_待機");
		motionList [1] = Data.Load ("基本_歩き");
	}
	

	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void Update () {

		base.Update ();

		if (state != State.Crash) {
			Move ();
		}
	}


	//================================================================================
	//	位置更新
	//================================================================================
	void Move () {
		Vector3 vel = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")) * 0.1f;
		if (vel != Vector3.zero) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (vel, Vector3.up), 0.1f);
			state = State.Walk;
		} else {
			state = State.Wait;
		}
		transform.position += vel;
	}
}
