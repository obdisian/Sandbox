using UnityEngine;
using System.Collections;

public static class Extention
{
	// デルタタイム
	public static float DeltaTime (this float value) {
		return value * Time.deltaTime * 60;
	}

	// ベクトルの長さ
	public static float Length (this Vector2 v) {
		return Mathf.Sqrt (v.x * v.x + v.y * v.y);
	}
	public static float Length (this Vector3 v) {
		return Mathf.Sqrt (v.x * v.x + v.y * v.y + v.z * v.z);
	}

	// リッチテキスト拡張系
	public static string Size (this string str, float size) {
		return "<size=" + size + ">" + str + "</size>";
	}
	public static string Color (this string str, Color color) {
		return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + str + "</color>";
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


    public static Vector3 zeroX(this Vector3 vector)
    {
        vector.x = 0f; return vector;
    }
    public static Vector3 zeroY(this Vector3 vector)
    {
        vector.y = 0f; return vector;
    }
    public static Vector3 zeroZ(this Vector3 vector)
    {
        vector.z = 0f; return vector;
    }


    // 反対色を作成する
    public static Color reverse(this Color color)
	{
		float max = Mathf.Max(color.r , Mathf.Max(color.g, color.b));
		float min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
		float sum = max + min;
		return new Color(sum - color.r, sum - color.g, sum - color.b);
	}
}
