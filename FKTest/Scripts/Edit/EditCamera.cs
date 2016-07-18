using UnityEngine;
using System.Collections;

public class EditCamera : MonoBehaviour {

	Quaternion baseRot;


	float distance = 10;
	float height = 2;

	float turnSpeed = 0.2f;
	float turnAngle;

	Vector3 acceleration;


	//================================================================================
	//	初期化
	//================================================================================
	void Start () {
	
	}


	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void Update () {

		GameObject hitObj = InputController.MouseHitObject;
		if (hitObj) {
			if (hitObj.name == "X" || hitObj.name == "Y" || hitObj.name == "Z") {
				return;
			}
		}

		if (InputController.IsHoldDown || InputController.MoveVector != Vector3.zero) {
			acceleration = InputController.MoveVector;
		}
		else {
			acceleration *= 0.9f;
		}
		turnAngle += acceleration.x;

		transform.position = Quaternion.AngleAxis (turnAngle * turnSpeed, Vector3.up) * (Vector3.up * height + Vector3.back * distance);
		transform.rotation = Quaternion.LookRotation (-transform.position, Vector3.up);

		distance += acceleration.y * turnSpeed * 0.25f;
		distance = Mathf.Max (5, Mathf.Min (distance, 10));
		height += acceleration.y * turnSpeed * 0.25f;
		height = Mathf.Max (-1, Mathf.Min (height, 4));
	}
}
