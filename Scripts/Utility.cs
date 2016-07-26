using UnityEngine;
using System.Collections;

public static class Utility {

	//	transform.position用のSetter
	public static void SetPositionX (this Transform transform, float x) {
		var pos = transform.position;
		pos.x = x;
		transform.position = pos;
	}
	public static void SetPositionY (this Transform transform, float y) {
		var pos = transform.position;
		pos.y = y;
		transform.position = pos;
	}

	//	transform.localScale用のSetter
	public static void SetLocalScaleX (this Transform transform, float x) {
		var size = transform.localScale;
		size.x = x;
		transform.localScale = size;
	}
	public static void SetLocalScaleY (this Transform transform, float y) {
		var size = transform.localScale;
		size.y = y;
		transform.localScale = size;
	}

	//	RigidBody2D.velocity用のSetter
	public static void SetRigid2DVelocityX (this Rigidbody2D rigid2d, float x) {
		var vel = rigid2d.velocity;
		vel.x = x;
		rigid2d.velocity = vel;
	}
	public static void SetRigid2DVelocityY (this Rigidbody2D rigid2d, float y) {
		var vel = rigid2d.velocity;
		vel.y = y;
		rigid2d.velocity = vel;
	}


	//	TransformのLerp集
	public static void Lerp_Position (this Transform transform, Vector3 target, float t) {
		var pos = transform.position;
		pos = Vector3.Lerp (pos, target, t);
		transform.position = pos;
	}
	public static void Lerp_Position (this Transform transform, Vector2 target, float t) {
		var pos = transform.position;
		pos = Vector2.Lerp (pos, target, t);
		transform.position = pos + Vector3.forward * transform.position.z;
	}
	public static void Lerp_LocalScale (this Transform transform, Vector3 target, float t) {
		var size = transform.localScale;
		size = Vector3.Lerp (size, target, t);
		transform.localScale = size;
	}
	public static void Lerp_LocalScale (this Transform transform, Vector2 target, float t) {
		var size = transform.localScale;
		size = Vector2.Lerp (size, target, t);
		transform.localScale = size + Vector3.forward * transform.localScale.z;
	}
	public static void Lerp_Rotation (this Transform transform, Quaternion target, float t) {
		var rot = transform.rotation;
		rot = Quaternion.Lerp (rot, target, t);
		transform.rotation = rot;
	}
}
