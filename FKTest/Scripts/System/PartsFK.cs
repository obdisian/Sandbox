using UnityEngine;
using System.Collections;

public class PartsFK : FK {


	public bool IsCrash { get; set; }


	readonly Vector3 Gravity = new Vector3 (0, -0.0098f, 0);

	bool isPrevCrash;

	//	復帰フラグ
	bool isRemove;
	//	復帰時間
	float removeTimer;
	//	復帰前位置
	Vector3 crashPos;


	float crashSpeed = 0.15f;
	float groundHeight = -1.75f;


	//	速度ベクトル(砕ける時しか使わない)
	Vector3 velocity;


	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void Update () {
		UpdateParts ();
	}


	//================================================================================
	//	部位の更新
	//================================================================================
	public void UpdateParts () {

		if (IsCrash != isPrevCrash) {
			isPrevCrash = IsCrash;

			velocity = new Vector3 (Random.Range (-1.0f, 1), Random.Range (1, 2), Random.Range (-1.0f, 1)) * crashSpeed;

			//	直前まで壊れていた時
			if (!IsCrash) {
				isRemove = true;
				removeTimer = 0;
				crashPos = transform.position;
			}
		}

		if (IsCrash) {
			CrashMove ();
		}
		else if (isRemove) {
			removeTimer += 0.02f;
			CrashRemove ();
			if (removeTimer > 1) {
				isRemove = false;
			}
		}
		else {
			UpdateFK ();
		}
	}


	//================================================================================
	//	クラッシュ時
	//================================================================================
	void CrashMove () {
		velocity += Gravity;
		transform.position += velocity;

		//	地面の高さを一定にする
		if (transform.position.y <= groundHeight) {

			velocity = new Vector3 (velocity.x * Random.Range (-1.0f, 1), -velocity.y * 0.5f, velocity.z * Random.Range (-1.0f, 1));
			transform.position = new Vector3 (transform.position.x, groundHeight, transform.position.z);
		}
		transform.rotation = Quaternion.AngleAxis (10, Vector3.Cross (Vector3.up, velocity).normalized) * transform.rotation;
	}


	//================================================================================
	//	クラッシュ復帰時
	//================================================================================
	void CrashRemove () {
		
		BaseQuat = Quaternion.Slerp (BaseQuat, xyzAngle(Owner.transform.rotation, BaseAngle.x, BaseAngle.y, BaseAngle.z), SlerpT);

		transform.position =
			Interpolation.Cubic (Interpolation.Ease.Out, removeTimer, crashPos, Owner.transform.position + Owner.transform.rotation * BasePos);
		transform.rotation = BaseQuat * Owner.transform.rotation;
	}
}
