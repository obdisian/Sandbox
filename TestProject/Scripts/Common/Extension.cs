using UnityEngine;
using System.Collections;

public static class Extention {

	// ベクトルの長さ
	public static float Length (this Vector2 v) {
		return Mathf.Sqrt (v.x * v.x + v.y * v.y);
	}
	public static float Length (this Vector3 v) {
		return Mathf.Sqrt (v.x * v.x + v.y * v.y + v.z * v.z);
	}


	// 比例変換
	public static float RatioMap (this float value, float start1, float end1, float start2, float end2) {
		return value / (start1 - end1) * (start2 - end2) + start2;
	}

	// 値の制限
	public static int Clamp (this int value, int min, int max) {
		return Mathf.Max (min, Mathf.Min (value, max));
	}
	public static float Clamp (this float value, float min, float max) {
		return Mathf.Max (min, Mathf.Min (value, max));
	}
}
