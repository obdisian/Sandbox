using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace animate
{
	public static class Easing
	{
		// イージングタイプ
		public enum Ease {
			In,
			Out,
			InOut,
		}

		// 補間タイプ
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

		public static float Curve (Type type, Ease ease, float t, float b, float c)
		{
			switch (type)
			{
			case Type.Linear: return Interpolation.Linear (t, b, c);
			case Type.Quadratic:
				switch (ease) {
				case Ease.In: return Interpolation.QuadraticIn (t, b, c);
				case Ease.Out: return Interpolation.QuadraticOut (t, b, c);
				case Ease.InOut: return Interpolation.QuadraticInOut (t, b, c);
				} break;
			case Type.Cubic:
				switch (ease) {
				case Ease.In: return Interpolation.CubicIn (t, b, c);
				case Ease.Out: return Interpolation.CubicOut (t, b, c);
				case Ease.InOut: return Interpolation.CubicInOut (t, b, c);
				} break;
			case Type.Quartic:
				switch (ease) {
				case Ease.In: return Interpolation.QuarticIn (t, b, c);
				case Ease.Out: return Interpolation.QuarticOut (t, b, c);
				case Ease.InOut: return Interpolation.QuarticInOut (t, b, c);
				} break;
			case Type.Quintic:
				switch (ease) {
				case Ease.In: return Interpolation.QuinticIn (t, b, c);
				case Ease.Out: return Interpolation.QuinticOut (t, b, c);
				case Ease.InOut: return Interpolation.QuinticInOut (t, b, c);
				} break;
			case Type.Sinusoidal:
				switch (ease) {
				case Ease.In: return Interpolation.SinusoidalIn (t, b, c);
				case Ease.Out: return Interpolation.SinusoidalOut (t, b, c);
				case Ease.InOut: return Interpolation.SinusoidalInOut (t, b, c);
				} break;
			case Type.Exponential:
				switch (ease) {
				case Ease.In: return Interpolation.ExponentialIn (t, b, c);
				case Ease.Out: return Interpolation.ExponentialOut (t, b, c);
				case Ease.InOut: return Interpolation.ExponentialInOut (t, b, c);
				} break;
			case Type.Circular:
				switch (ease) {
				case Ease.In: return Interpolation.CircularIn (t, b, c);
				case Ease.Out: return Interpolation.CircularOut (t, b, c);
				case Ease.InOut: return Interpolation.CircularInOut (t, b, c);
				} break;
			}
			Debug.LogError ("Error : Easing.Curve [ " + type + ", " + ease + " ]");
			return b;
		}
		public static Vector2 Curve (Type type, Ease ease, float t, Vector2 b, Vector2 c)
		{
			return new Vector2 (Curve (type, ease, t, b.x, c.x), Curve (type, ease, t, b.y, c.y));
		}
		public static Vector3 Curve (Type type, Ease ease, float t, Vector3 b, Vector3 c)
		{
			return new Vector3 (Curve (type, ease, t, b.x, c.x), Curve (type, ease, t, b.y, c.y), Curve (type, ease, t, b.z, c.z));
		}
		public static Color Curve (Type type, Ease ease, float t, Color b, Color c)
		{
			return new Color (Curve (type, ease, t, b.r, c.r), Curve (type, ease, t, b.g, c.g), Curve (type, ease, t, b.b, c.b), Curve (type, ease, t, b.a, c.a));
		}
	}

	public static class Interpolation {

		//----------------------------------------
		// Linear
		public static float Linear (float t, float b, float c) {
			c = c - b;
			return c * t / 1.0f + b;
		}

		//----------------------------------------
		// Quadratic
		public static float QuadraticIn (float t, float b, float c) {
			c = c - b; t /= 1.0f;
			return c * t * t + b;
		}
		public static float QuadraticOut (float t, float b, float c) {
			c = c - b; t /= 1.0f;
			return -c * t * (t - 2) + b;
		}
		public static float QuadraticInOut (float t, float b, float c) {
			c = c - b; t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t + b;
			t--;
			return -c / 2 * (t * (t - 2) - 1) + b;
		}

		//----------------------------------------
		// Cubic
		public static float CubicIn (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			return c * t * t * t + b;
		}
		public static float CubicOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			t--;
			return c * (t * t * t + 1) + b;
		}
		public static float CubicInOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t * t + b;
			t -= 2;
			return c / 2 * (t * t * t + 2) + b;
		}

		//----------------------------------------
		// Quartic
		public static float QuarticIn (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			return c * t * t * t * t + b;
		}
		public static float QuarticOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			t--;
			return -c * (t * t * t * t - 1) + b;
		}
		public static float QuarticInOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t * t * t + b;
			t -= 2;
			return -c / 2 * (t * t * t * t - 2) + b;
		}

		//----------------------------------------
		// Quintic
		public static float QuinticIn (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			return c * t * t * t * t * t + b;
		}
		public static float QuinticOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			t--;
			return c * (t * t * t * t * t + 1) + b;
		}
		public static float QuinticInOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * t * t * t * t * t + b;
			t -= 2;
			return c / 2 * (t * t * t * t * t + 2) + b;
		}

		//----------------------------------------
		// Sinusoidal
		public static float SinusoidalIn (float t, float b, float c) {
			c = c - b;
			return -c * Mathf.Cos (t / 1.0f * (Mathf.PI / 2)) + c + b;
		}
		public static float SinusoidalOut (float t, float b, float c) {
			c = c - b;
			return c * Mathf.Sin (t / 1.0f * (Mathf.PI / 2)) + b;
		}
		public static float SinusoidalInOut (float t, float b, float c) {
			c = c - b;
			return -c / 2 * (Mathf.Cos (Mathf.PI * t / 1.0f) - 1) + b;
		}

		//----------------------------------------
		// Exponential
		public static float ExponentialIn (float t, float b, float c) {
			c = c - b;
			return c * Mathf.Pow (2, 10 * (t / 1.0f - 1)) + b;
		}
		public static float ExponentialOut (float t, float b, float c) {
			c = c - b;
			return c * (-Mathf.Pow (2, -10 * t / 1.0f) + 1) + b;
		}
		public static float ExponentialInOut (float t, float b, float c) {
			c = c - b;
			t /= 1.0f / 2;
			if (t < 1) return c / 2 * Mathf.Pow (2, 10 * (t - 1)) + b;
			t--;
			return c / 2 * (-Mathf.Pow (2, -10 * t) + 2) + b;
		}

		//----------------------------------------
		// Circular
		public static float CircularIn (float t, float b, float c) {
			c = c - b;
			t /= 1.0f;
			return -c * (Mathf.Sqrt (1 - t * t) - 1) + b;
		}
		public static float CircularOut (float t, float b, float c) {
			t /= 1.0f;
			t--;
			return c * Mathf.Sqrt (1 - t * t) + b;
		}
		public static float CircularInOut (float t, float b, float c) {
			t /= 1.0f / 2;
			if (t < 1) return -c / 2 * (Mathf.Sqrt (1 - t * t) - 1) + b;
			t -= 2;
			return c / 2 * (Mathf.Sqrt (1 - t * t) + 1) + b;
		}
	}
}
