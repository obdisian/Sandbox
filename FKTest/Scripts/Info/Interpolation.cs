using UnityEngine;
using System.Collections;

public class Interpolation : MonoBehaviour {

	//	補間タイプ
	public enum Type {
		None,
		Linear,
		Quadratic,
		Cubic,
		Quartic,
		Quintic,
		Sinusoidal,
		Exponential,
		Circular,
	}

	//	イージングタイプ
	public enum Ease {
		In,
		Out,
		In_Out,
	}

	//================================================================================
	//	Linear
	//================================================================================
	public static Vector3 Linear (float t, Vector3 b, Vector3 c) {
		return new Vector3 (Linear (t, b.x, c.x), Linear (t, b.y, c.y), Linear (t, b.z, c.z));
	}
	public static float Linear (float t, float b, float c) {
		c = c - b;
		return c * t / 1.0f + b;
	}

	//================================================================================
	//	Quadratic
	//================================================================================
	public static Vector3 Quadratic (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Quadratic (ease, t, b.x, c.x), Quadratic (ease, t, b.y, c.y), Quadratic (ease, t, b.z, c.z));
	}
	public static float Quadratic (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			t /= 1.0f;
			return c * t * t + b;

		case Ease.Out:
			t /= 1.0f;
			return -c * t * (t - 2) + b;

		case Ease.In_Out:
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t + b;
			t--;
			return -c / 2 * (t * (t - 2) - 1) + b;

		default:
			return 0.0f;
		}
	}

	//================================================================================
	//	Cubic
	//================================================================================
	public static Vector3 Cubic (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Cubic (ease, t, b.x, c.x), Cubic (ease, t, b.y, c.y), Cubic (ease, t, b.z, c.z));
	}
	public static float Cubic (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			t /= 1.0f;
			return c * t * t * t + b;

		case Ease.Out:
			t /= 1.0f;
			t--;
			return c * (t * t * t + 1) + b;

		case Ease.In_Out:
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t * t + b;
			t -= 2;
			return c / 2 * (t * t * t + 2) + b;

		default:
			return 0.0f;
		}
	}

	//================================================================================
	//	Quartic
	//================================================================================
	public static Vector3 Quartic (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Quartic (ease, t, b.x, c.x), Quartic (ease, t, b.y, c.y), Quartic (ease, t, b.z, c.z));
	}
	public static float Quartic (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			t /= 1.0f;
			return c * t * t * t * t + b;

		case Ease.Out:
			t /= 1.0f;
			t--;
			return -c * (t * t * t * t - 1) + b;

		case Ease.In_Out:
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t * t * t + b;
			t -= 2;
			return -c / 2 * (t * t * t * t - 2) + b;

		default:
			return 0.0f;
		}
	}

	//================================================================================
	//	Quintic
	//================================================================================
	public static Vector3 Quintic (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Quintic (ease, t, b.x, c.x), Quintic (ease, t, b.y, c.y), Quintic (ease, t, b.z, c.z));
	}
	public static float Quintic (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			t /= 1.0f;
			return c * t * t * t * t * t + b;

		case Ease.Out:
			t /= 1.0f;
			t--;
			return c * (t * t * t * t * t + 1) + b;

		case Ease.In_Out:
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t * t * t * t + b;
			t -= 2;
			return c / 2 * (t * t * t * t * t + 2) + b;

		default:
			return 0.0f;
		}
	}

	//================================================================================
	//	Sinusoidal
	//================================================================================
	public static Vector3 Sinusoidal (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Sinusoidal (ease, t, b.x, c.x), Sinusoidal (ease, t, b.y, c.y), Sinusoidal (ease, t, b.z, c.z));
	}
	public static float Sinusoidal (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			return -c * Mathf.Cos (t / 1.0f * (Mathf.PI / 2)) + c + b;

		case Ease.Out:
			return c * Mathf.Sin (t / 1.0f * (Mathf.PI / 2)) + b;

		case Ease.In_Out:
			return -c / 2 * (Mathf.Cos (Mathf.PI * t / 1.0f) - 1) + b;

		default:
			return 0.0f;
		}
	}

	//================================================================================
	//	Exponential
	//================================================================================
	public static Vector3 Exponential (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Exponential (ease, t, b.x, c.x), Exponential (ease, t, b.y, c.y), Exponential (ease, t, b.z, c.z));
	}
	public static float Exponential (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			return c * Mathf.Pow (2, 10 * (t / 1.0f - 1)) + b;

		case Ease.Out:
			return c * (-Mathf.Pow (2, -10 * t / 1.0f) + 1) + b;

		case Ease.In_Out:
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * Mathf.Pow (2, 10 * (t - 1)) + b;
			t--;
			return c / 2 * (-Mathf.Pow (2, -10 * t) + 2) + b;

		default:
			return 0.0f;
		}
	}

	//================================================================================
	//	Circular
	//================================================================================
	public static Vector3 Circular (Ease ease, float t, Vector3 b, Vector3 c) {
		return new Vector3 (Circular (ease, t, b.x, c.x), Circular (ease, t, b.y, c.y), Circular (ease, t, b.z, c.z));
	}
	public static float Circular (Ease ease, float t, float b, float c) {
		c = c - b;

		switch (ease) {

		case Ease.In:
			t /= 1.0f;
			return -c * (Mathf.Sqrt (1 - t * t) - 1) + b;

		case Ease.Out:
			t /= 1.0f;
			t--;
			return c * Mathf.Sqrt (1 - t * t) + b;

		case Ease.In_Out:
			t /= 1.0f / 2;
			if (t < 1) return -c / 2 * (Mathf.Sqrt (1 - t * t) - 1) + b;
			t -= 2;
			return c / 2 * (Mathf.Sqrt (1 - t * t) + 1) + b;

		default:
			return 0.0f;
		}
	}
}